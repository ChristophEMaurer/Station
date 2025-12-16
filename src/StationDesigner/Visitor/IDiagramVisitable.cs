using System;
using System.Collections.Generic;
using System.Text;

namespace StationDesigner
{
    public interface IDiagramVisitable
    {
        void Accept(IDiagramVisitor visitor);
    }
}
