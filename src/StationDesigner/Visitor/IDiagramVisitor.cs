using System;
using System.Collections.Generic;
using System.Text;

using StationDesigner.UserControls;

namespace StationDesigner
{
    public interface IDiagramVisitor
    {
        void Visit(Diagramm diagramm);
        void Visit(Zimmer zimmer);
        void Visit(Bett bett);
        void Visit(Textfeld text);
    }
}
