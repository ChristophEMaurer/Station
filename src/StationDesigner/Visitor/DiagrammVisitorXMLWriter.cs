using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Windows.Forms;
using System.IO;

using StationDesigner.UserControls;

namespace StationDesigner
{
    public class DiagrammVisitorXMLWriter : IDiagramVisitor
    {
        XmlTextWriter _xmlWriter;
        string _strFilename;

        public DiagrammVisitorXMLWriter()
        {
        }

        private void VisitMain(Diagramm diagramm)
        {
            _xmlWriter = new XmlTextWriter(_strFilename, Encoding.ASCII);

            _xmlWriter.Indentation = 4;
            _xmlWriter.Formatting = Formatting.Indented;


            _xmlWriter.WriteStartDocument(true);
            _xmlWriter.WriteStartElement("diagram");

            // ID_Diagramme
            _xmlWriter.WriteStartAttribute("ID_Diagramme");
            _xmlWriter.WriteString(diagramm.ID_Diagramme.ToString());
            _xmlWriter.WriteEndAttribute();

            // InfoLocationX
            _xmlWriter.WriteStartAttribute("InfoLocationX");
            _xmlWriter.WriteString(diagramm.InfoLocationX.ToString());
            _xmlWriter.WriteEndAttribute();

            // InfoLocationY
            _xmlWriter.WriteStartAttribute("InfoLocationY");
            _xmlWriter.WriteString(diagramm.InfoLocationY.ToString());
            _xmlWriter.WriteEndAttribute();

            foreach (Zimmer zimmer in diagramm.Zimmer)
            {
                zimmer.Accept(this);
            }

            foreach (Textfeld text in diagramm.Texte)
            {
                text.Accept(this);
            }
            
            _xmlWriter.WriteEndElement();

            _xmlWriter.Close();
        }

        public void Visit(Diagramm diagramm)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            if (DialogResult.OK == dlg.ShowDialog())
            {
                _strFilename = dlg.FileName;
                VisitMain(diagramm);
            }
        }

        public void Visit(Zimmer zimmer)
        {
            _xmlWriter.WriteStartElement("Zimmer");

            // Station
            _xmlWriter.WriteStartAttribute("Station");
            _xmlWriter.WriteString(zimmer.Station.ToString());
            _xmlWriter.WriteEndAttribute();

            // ZimmerNummer
            _xmlWriter.WriteStartAttribute("ZimmerNummer");
            _xmlWriter.WriteString(zimmer.ZimmerNummer.ToString());
            _xmlWriter.WriteEndAttribute();

            // LocationX
            _xmlWriter.WriteStartAttribute("LocationX");
            _xmlWriter.WriteString(zimmer.LocationX.ToString());
            _xmlWriter.WriteEndAttribute();

            // LocationY
            _xmlWriter.WriteStartAttribute("LocationY");
            _xmlWriter.WriteString(zimmer.LocationY.ToString());
            _xmlWriter.WriteEndAttribute();

            // InfoLocationX
            _xmlWriter.WriteStartAttribute("InfoLocationX");
            _xmlWriter.WriteString(zimmer.InfoLocationX.ToString());
            _xmlWriter.WriteEndAttribute();

            // InfoLocationY
            _xmlWriter.WriteStartAttribute("InfoLocationY");
            _xmlWriter.WriteString(zimmer.InfoLocationY.ToString());
            _xmlWriter.WriteEndAttribute();

            // IsolationLocationX
            _xmlWriter.WriteStartAttribute("IsolationLocationX");
            _xmlWriter.WriteString(zimmer.IsolationLocationX.ToString());
            _xmlWriter.WriteEndAttribute();

            // IsolationLocationY
            _xmlWriter.WriteStartAttribute("IsolationLocationY");
            _xmlWriter.WriteString(zimmer.IsolationLocationY.ToString());
            _xmlWriter.WriteEndAttribute();

            // NummerLocationX
            _xmlWriter.WriteStartAttribute("NummerLocationX");
            _xmlWriter.WriteString(zimmer.NummerLocationX.ToString());
            _xmlWriter.WriteEndAttribute();

            // NummerLocationY
            _xmlWriter.WriteStartAttribute("NummerLocationY");
            _xmlWriter.WriteString(zimmer.NummerLocationY.ToString());
            _xmlWriter.WriteEndAttribute();

            // Betten
            foreach (Bett bett in zimmer.Betten)
            {
                bett.Accept(this);
            }

            _xmlWriter.WriteEndElement();
        }

        public void Visit(Bett bett)
        {
            _xmlWriter.WriteStartElement("bett");

            // BettenNummer
            _xmlWriter.WriteStartAttribute("BettenNummer");
            _xmlWriter.WriteString(bett.BettenNummer.ToString());
            _xmlWriter.WriteEndAttribute();

            // LocationX
            _xmlWriter.WriteStartAttribute("LocationX");
            _xmlWriter.WriteString(bett.LocationX.ToString());
            _xmlWriter.WriteEndAttribute();

            // LocationY
            _xmlWriter.WriteStartAttribute("LocationY");
            _xmlWriter.WriteString(bett.LocationY.ToString());
            _xmlWriter.WriteEndAttribute();

            _xmlWriter.WriteEndElement();
        }
        public void Visit(Textfeld text)
        {
            _xmlWriter.WriteStartElement("Textfeld");

            // Text
            _xmlWriter.WriteStartAttribute("Text");
            _xmlWriter.WriteString(text.Text);
            _xmlWriter.WriteEndAttribute();

            _xmlWriter.WriteEndElement();
        }
    }
}

