using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

using StationDesigner.UserControls;

namespace StationDesigner
{
    public class Diagramm : IDiagramVisitable
    {
        int _nID_Diagramme;
        string _strName;
        string _strBeschreibung;

        List<Textfeld> _texte = new List<Textfeld>();
        List<Zimmer> _zimmer = new List<Zimmer>();
        List<Label> _zimmerNummern = new List<Label>();
        GroupBox _infoBox;

        public Diagramm(int nID_Diagramme, string strName, string strBeschreibung)
        {
            _nID_Diagramme = nID_Diagramme;
            _strName = strName;
            _strBeschreibung = strBeschreibung;
        }

        public GroupBox InfoBox
        {
            get { return _infoBox; }
            set { _infoBox = value; }
        }

        public List<Textfeld> Texte
        {
            get { return _texte; }
        }

        public List<Zimmer> Zimmer
        {
            get { return _zimmer; }
        }

        public List<Label> ZimmerNummern
        {
            get { return _zimmerNummern; }
        }
        public int ID_Diagramme
        {
            get { return _nID_Diagramme; }
        }
        public int InfoLocationX
        {
            get { return _infoBox.Location.X; }
        }

        public int InfoLocationY
        {
            get { return _infoBox.Location.Y; }
        }

        public void Accept(IDiagramVisitor visitor)
        {
            visitor.Visit(this);
        }
        public string DiagrammName
        {
            get { return _strName; }
            set { _strName = value; }
        }
        public string DiagrammBeschreibung
        {
            get { return _strBeschreibung; }
            set { _strBeschreibung = value; }
        }
    }
}
