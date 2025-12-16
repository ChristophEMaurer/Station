using System;
using System.Collections;
using System.Windows.Forms;

using StationDesigner.UserControls;
using Station;

namespace StationDesigner
{
    /// <summary>
    /// Loescht alle Diagramme, Zimmer und Betten und fuegt alle neu ein.
    /// Kein Update.
    /// </summary>
    public class DiagrammVisitorDatabaseWriterScratch : IDiagramVisitor
    {
        DatabaseLayer _databaseLayer;
        Diagramm _diagramm;
        int _nID_Diagramme;
        int _nID_Zimmer;

        public DiagrammVisitorDatabaseWriterScratch(DatabaseLayer d)
        {
            _databaseLayer = d;
        }

        public void DeleteDiagram(int nID_Diagramme)
        {
            string strSQL = @"
                delete from 
                    Betten
                where 
                    ID_Zimmer in (select ID_Zimmer from Zimmer where ID_Diagramme=@ID_Diagramme)";

            ArrayList ar = new ArrayList();
            ar.Add(_databaseLayer.SqlParameter("@ID_Diagramme", nID_Diagramme));
            _databaseLayer.ExecuteNonQuery(strSQL, ar);

            strSQL = @"
                delete from 
                    Zimmer
                where 
                    ID_Diagramme=@ID_Diagramme";
            ar = new ArrayList();
            ar.Add(_databaseLayer.SqlParameter("@ID_Diagramme", nID_Diagramme));
            _databaseLayer.ExecuteNonQuery(strSQL, ar);

            strSQL = @"
                delete from 
                    Texte
                where 
                    ID_Diagramme=@ID_Diagramme";
            ar = new ArrayList();
            ar.Add(_databaseLayer.SqlParameter("@ID_Diagramme", nID_Diagramme));
            _databaseLayer.ExecuteNonQuery(strSQL, ar);

            strSQL = @"
                delete from 
                    Diagramme 
                where 
                    ID_Diagramme=@ID_Diagramme";
            ar = new ArrayList();
            ar.Add(_databaseLayer.SqlParameter("@ID_Diagramme", nID_Diagramme));
            _databaseLayer.ExecuteNonQuery(strSQL, ar);

        }

        public void Visit(Diagramm diagramm)
        {
            try
            {
                _diagramm = diagramm;

                _databaseLayer.OpenForImport();

                DeleteDiagram(diagramm.ID_Diagramme);

                string strSQL = @"
                    insert into Diagramme (ID_Diagramme, [Name], Beschreibung, InfoLocationX, InfoLocationY)
                    values (@ID_Diagramme, @Name, @Beschreibung, @InfoLocationX, @InfoLocationY)";

                ArrayList aSQLParameters = new ArrayList();
                aSQLParameters.Add(_databaseLayer.SqlParameter("@ID_Diagramme", diagramm.ID_Diagramme));
                aSQLParameters.Add(_databaseLayer.SqlParameter("@Name", diagramm.DiagrammName));
                aSQLParameters.Add(_databaseLayer.SqlParameter("@Beschreibung", diagramm.DiagrammBeschreibung));
                aSQLParameters.Add(_databaseLayer.SqlParameter("@InfoLocationX", diagramm.InfoLocationX));
                aSQLParameters.Add(_databaseLayer.SqlParameter("@InfoLocationY", diagramm.InfoLocationY));
                _nID_Diagramme = _databaseLayer.InsertRecord(strSQL, aSQLParameters, "Diagramme");

                foreach (Zimmer zimmer in diagramm.Zimmer)
                {
                    zimmer.Accept(this);
                }
                foreach (Textfeld text in diagramm.Texte)
                {
                    text.Accept(this);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Schreiben des Diagrammes fehlgeschlagen:\n\n" + ex.Message);
            }
            finally
            {
                _databaseLayer.CloseForImport();

                MessageBox.Show("Schreiben des Diagrammes abgeschlossen.");
            }
        }

        public void Visit(Zimmer zimmer)
        {
            string strSQL = @"
                insert into Zimmer(
                    ID_Diagramme, 
                    Station, 
                    ZimmerNummer, 
                    LocationX, 
                    LocationY,
                    Height,
                    Width,
                    NummerLocationX,
                    NummerLocationY,
                    InfoLocationX,
                    InfoLocationY,
                    IsolationLocationX,
                    IsolationLocationY
                    )
                values (
                    @ID_Diagramme, 
                    @Station, 
                    @ZimmerNummer, 
                    @LocationX, 
                    @LocationY,
                    @Height,
                    @Width,
                    @NummerLocationX,
                    @NummerLocationY,
                    @InfoLocationX,
                    @InfoLocationY,
                    @IsolationLocationX,
                    @IsolationLocationY
                    )
                ";

            ArrayList aSQLParameters = new ArrayList();
            aSQLParameters.Add(_databaseLayer.SqlParameter("@ID_Diagramme", _diagramm.ID_Diagramme));
            aSQLParameters.Add(_databaseLayer.SqlParameter("@Station", zimmer.Station));
            aSQLParameters.Add(_databaseLayer.SqlParameter("@ZimmerNummer", zimmer.ZimmerNummer));
            aSQLParameters.Add(_databaseLayer.SqlParameter("@LocationX", zimmer.LocationX));
            aSQLParameters.Add(_databaseLayer.SqlParameter("@LocationY", zimmer.LocationY));
            aSQLParameters.Add(_databaseLayer.SqlParameter("@Height", zimmer.Height));
            aSQLParameters.Add(_databaseLayer.SqlParameter("@Width", zimmer.Width));
            aSQLParameters.Add(_databaseLayer.SqlParameter("@NummerLocationX", zimmer.NummerLocationX));
            aSQLParameters.Add(_databaseLayer.SqlParameter("@NummerLocationY", zimmer.NummerLocationY));
            aSQLParameters.Add(_databaseLayer.SqlParameter("@InfoLocationX", zimmer.InfoLocationX));
            aSQLParameters.Add(_databaseLayer.SqlParameter("@InfoLocationY", zimmer.InfoLocationY));
            aSQLParameters.Add(_databaseLayer.SqlParameter("@IsolationLocationX", zimmer.IsolationLocationX));
            aSQLParameters.Add(_databaseLayer.SqlParameter("@IsolationLocationY", zimmer.IsolationLocationY));
            _nID_Zimmer = _databaseLayer.InsertRecord(strSQL, aSQLParameters, "Zimmer");

            foreach (Bett bett in zimmer.Betten)
            {
                bett.Accept(this);
            }
        }

        public void Visit(Bett bett)
        {
            string strSQL = @"insert into Betten (ID_Zimmer, BettenNummer, LocationX, LocationY)
                    values (@ID_Zimmer, @BettenNummer, @LocationX, @LocationY)";

            ArrayList aSQLParameters = new ArrayList();
            aSQLParameters.Add(_databaseLayer.SqlParameter("@ID_Zimmer", _nID_Zimmer));
            aSQLParameters.Add(_databaseLayer.SqlParameter("@BettenNummer", bett.BettenNummer));
            aSQLParameters.Add(_databaseLayer.SqlParameter("@LocationX", bett.LocationX));
            aSQLParameters.Add(_databaseLayer.SqlParameter("@LocationY", bett.LocationY));
            int nID_Betten = _databaseLayer.InsertRecord(strSQL, aSQLParameters, "Betten");
        }
        public void Visit(Textfeld text)
        {
            string strSQL = @"
                insert into Texte(
                    ID_Diagramme, 
                    [Text], 
                    LocationX, 
                    LocationY,
                    Height,
                    Width
                    )
                values (
                    @ID_Diagramme, 
                    @Text, 
                    @LocationX, 
                    @LocationY,
                    @Height,
                    @Width
                    )
                ";

            ArrayList aSQLParameters = new ArrayList();

            aSQLParameters.Add(_databaseLayer.SqlParameter("@ID_Diagramme", _diagramm.ID_Diagramme));
            aSQLParameters.Add(_databaseLayer.SqlParameter("@Text", text.Text));
            aSQLParameters.Add(_databaseLayer.SqlParameter("@LocationX", text.LocationX));
            aSQLParameters.Add(_databaseLayer.SqlParameter("@LocationY", text.LocationY));
            aSQLParameters.Add(_databaseLayer.SqlParameter("@Height", text.Height));
            aSQLParameters.Add(_databaseLayer.SqlParameter("@Width", text.Width));

            _databaseLayer.InsertRecord(strSQL, aSQLParameters, "Texte");
        }
    }
}

