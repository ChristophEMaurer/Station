using System;
using System.Collections.Generic;
using System.Text;

namespace Station.AppFramework
{
    public sealed class ProgressEventArgs : EventArgs
    {
        private bool _cancel = false;
        private string _data;

        public bool Cancel
        {
            get { return _cancel; }
            set { _cancel = value; }
        }
        public string Data
        {
            get { return _data; }
            set { _data = value; }
        }
    }

    public delegate void ProgressCallback(ProgressEventArgs e);
}
