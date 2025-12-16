using System;
using System.Collections;
using System.Windows.Forms;
using System.Data;

using StationDesigner.UserControls;
//using Station;

namespace StationDesigner
{
    /// <summary>
    /// Macht ausschlieﬂlich UPDATE auf alle Diagramme, Zimmer und Betten.
    /// Kein Delete.
    /// </summary>
    public class DiagrammVisitorDatabaseWriter : IDiagramVisitor
    {
        DatabaseLayer _databaseLayer;
        Diagramm _diagramm;

        public DiagrammVisitorDatabaseWriter(DatabaseLayer d)
        {
            _databaseLayer = d;
        }

        public void Visit(Diagramm diagramm)
        {
            try
            {
                _diagramm = diagramm;

                _databaseLayer.OpenForImport();

                DataRow row = _databaseLayer.CreateDataRowDiagramm();

                row["ID_Diagramme"] = diagramm.ID_Diagramme;
                row["Name"] = diagramm.DiagrammName;
                row["Beschreibung"] = diagramm.DiagrammBeschreibung;
                row["InfoLocationX"] = diagramm.InfoLocationX;
                row["InfoLocationY"] = diagramm.InfoLocationY;

                _databaseLayer.DesignerUpdateDiagramm(row);

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
                MessageBox.Show("Das Schreiben des Diagrammes ist fehlgeschlagen:\n\n" + ex.Message);
            }
            finally
            {
                _databaseLayer.CloseForImport();

                MessageBox.Show("Das Diagramm wurde erfolgreich in der Datenbank aktualisiert.");
            }
        }

        public void Visit(Zimmer zimmer)
        {
            DataRow row = _databaseLayer.CreateDataRowZimmer();

            row["ID_Zimmer"] = zimmer.ID_Zimmer;
            row["ID_Diagramme"] = _diagramm.ID_Diagramme;
            row["Station"] = zimmer.Station;
            row["ZimmerNummer"] = zimmer.ZimmerNummer;
            row["LocationX"] = zimmer.LocationX;
            row["LocationY"] = zimmer.LocationY;
            row["Height"] = zimmer.Height;
            row["Width"] = zimmer.Width;
            row["NummerLocationX"] = zimmer.NummerLocationX;
            row["NummerLocationY"] = zimmer.NummerLocationY;
            row["InfoLocationX"] = zimmer.InfoLocationX;
            row["InfoLocationY"] = zimmer.InfoLocationY;
            row["IsolationLocationX"] = zimmer.IsolationLocationX;
            row["IsolationLocationY"] = zimmer.IsolationLocationY;

            _databaseLayer.DesignerUpdateZimmer(row);
            
            foreach (Bett bett in zimmer.Betten)
            {
                bett.Accept(this);
            }
        }

        public void Visit(Bett bett)
        {
            DataRow row = _databaseLayer.CreateDataRowBett();

            row["ID_Betten"] = bett.ID_Betten;
            row["ID_Zimmer"] = bett.ID_Zimmer;
            row["BettenNummer"] = bett.BettenNummer;
            row["LocationX"] = bett.LocationX;
            row["LocationY"] = bett.LocationY;

            _databaseLayer.DesignerUpdateBett(row);
        }
        public void Visit(Textfeld text)
        {
            DataRow row = _databaseLayer.CreateDataRowTextfeld();

            row["ID_Texte"] = text.ID_Texte;
            row["ID_Diagramme"] = _diagramm.ID_Diagramme;
            row["Text"] = text.TheText;
            row["LocationX"] = text.LocationX;
            row["LocationY"] = text.LocationY;
            row["Height"] = text.Height;
            row["Width"] = text.Width;

            _databaseLayer.DesignerUpdateTextfeld(row);
        }
    }
}

