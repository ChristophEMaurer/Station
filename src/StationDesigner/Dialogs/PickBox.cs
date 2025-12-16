using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;

namespace StationDesigner
{
	/// <summary>
	/// This class implements sizing and moving functions for
	///	runtime editing of graphic controls
	/// </summary>
    public class PickBox
    {
        private bool _bSnapToGrid = true;
        private int _nGridSize = 8;

        private const int BOX_SIZE = 8;
        private Color BOX_COLOR = Color.White;
        private Control m_control;
        private Label[] lbl = new Label[8];
        private int startl;
        private int startt;
        private int startw;
        private int starth;
        private int startx;
        private int starty;
        private bool dragging;
        private Cursor[] arrArrow = new Cursor[] {Cursors.SizeNWSE, Cursors.SizeNS,
			    Cursors.SizeNESW, Cursors.SizeWE, Cursors.SizeNWSE, Cursors.SizeNS,
			    Cursors.SizeNESW, Cursors.SizeWE};
        private Cursor oldCursor;

        private const int MIN_SIZE = 20;

        private Hashtable _htWiredControls = new Hashtable();


        /// <summary>
        /// Constructor creates 8 sizing handles & wires mouse events
        /// to each that implement sizing functions
        /// </summary>
        public PickBox()
        {
            for (int i = 0; i < 8; i++)
            {
                lbl[i] = new Label();
                lbl[i].TabIndex = i;
                lbl[i].FlatStyle = 0;
                lbl[i].BorderStyle = BorderStyle.FixedSingle;
                lbl[i].BackColor = BOX_COLOR;
                lbl[i].Cursor = arrArrow[i];
                lbl[i].Text = "";
                lbl[i].BringToFront();
                lbl[i].MouseDown += new MouseEventHandler(this.lbl_MouseDown);
                lbl[i].MouseMove += new MouseEventHandler(this.lbl_MouseMove);
                lbl[i].MouseUp += new MouseEventHandler(this.lbl_MouseUp);
            }
        }

        public bool SnapToGrid
        {
            get { return _bSnapToGrid; }
            set { _bSnapToGrid = value; }
        }
        public int GridSize
        {
            get { return _nGridSize; }
            set { _nGridSize = value; }
        }

        /// <summary>
        /// Overloaded. Wires a Click event handler to the passed Control
        /// that attaches a pick box to the control when it is clicked.
        /// </summary>
        /// <param name="ctl">The control</param>
        /// <param name="bAllowResize">Indicates whether or not the control may be resized</param>
        public void WireControl(Control ctl, bool bAllowResize)
        {
            _htWiredControls[ctl] = bAllowResize;
            ctl.Click += new EventHandler(this.SelectControl);
        }

        /// <summary>
        /// Overloaded. Wires a Click event handler to the passed Control
        /// that attaches a pick box to the control when it is clicked.
        /// The control may be resized.
        /// </summary>
        /// <param name="ctl"></param>
        public void WireControl(Control ctl)
        {
            WireControl(ctl, true);
        }

        public void UnWireControl(Control ctl)
        {
            _htWiredControls.Remove(ctl);
            ctl.Click -= new EventHandler(this.SelectControl);
            Remove();
        }

        //
        // Attaches a pick box to the sender Control
        //
        private void SelectControl(object sender, EventArgs e)
        {
            if (m_control is Control)
            {
                m_control.Cursor = oldCursor;

                //Remove event any pre-existing event handlers appended by this class
                m_control.MouseDown -= new MouseEventHandler(this.ctl_MouseDown);
                m_control.MouseMove -= new MouseEventHandler(this.ctl_MouseMove);
                m_control.MouseUp -= new MouseEventHandler(this.ctl_MouseUp);

                m_control = null;
            }

            m_control = (Control)sender;
            //Add event handlers for moving the selected control around
            m_control.MouseDown += new MouseEventHandler(this.ctl_MouseDown);
            m_control.MouseMove += new MouseEventHandler(this.ctl_MouseMove);
            m_control.MouseUp += new MouseEventHandler(this.ctl_MouseUp);

            //Add sizing handles to Control's container (Form or PictureBox)
            for (int i = 0; i < 8; i++)
            {
                m_control.Parent.Controls.Add(lbl[i]);
                lbl[i].BringToFront();
            }

            //Position sizing handles around Control
            MoveHandles();

            //Display sizing handles
            ShowHandles();

            oldCursor = m_control.Cursor;
            m_control.Cursor = Cursors.SizeAll;
        }

        public void Remove()
        {
            HideHandles();
            if (m_control != null)
            {
                m_control.Cursor = oldCursor;
            }
        }

        private void ShowHandles()
        {
            if (m_control != null)
            {
                for (int i = 0; i < 8; i++)
                {
                    lbl[i].Visible = true;
                }
            }
        }

        private void HideHandles()
        {
            for (int i = 0; i < 8; i++)
            {
                lbl[i].Visible = false;
            }
        }

        private void MoveHandles()
        {
            int sX = m_control.Left - BOX_SIZE;
            int sY = m_control.Top - BOX_SIZE;
            int sW = m_control.Width + BOX_SIZE;
            int sH = m_control.Height + BOX_SIZE;
            int hB = BOX_SIZE / 2;
            int[] arrPosX = new int[] {sX+hB, sX + sW / 2, sX + sW-hB, sX + sW-hB,
			sX + sW-hB, sX + sW / 2, sX+hB, sX+hB};
            int[] arrPosY = new int[] {sY+hB, sY+hB, sY+hB, sY + sH / 2, sY + sH-hB,
			sY + sH-hB, sY + sH-hB, sY + sH / 2};
            for (int i = 0; i < 8; i++)
            {
                lbl[i].SetBounds(arrPosX[i], arrPosY[i], BOX_SIZE, BOX_SIZE);
            }
        }

        //
        // Store control position and size when mouse button pushed over
        // any sizing handle
        //
        private void lbl_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            startl = m_control.Left;
            startt = m_control.Top;
            startw = m_control.Width;
            starth = m_control.Height;
            HideHandles();
        }

        //
        // Size the picked control in accordance with sizing handle being dragged
        //	0   1   2
        //  7       3
        //  6   5   4
        //
        private void lbl_MouseMove(object sender, MouseEventArgs ev)
        {
            int l = m_control.Left;
            int w = m_control.Width;
            int t = m_control.Top;
            int h = m_control.Height;

            int x = ev.X;
            int y = ev.Y;

            if (_bSnapToGrid)
            {
                x = (x / _nGridSize) * _nGridSize;
                y = (y / _nGridSize) * _nGridSize;
            }

            if (dragging && (bool)_htWiredControls[m_control])
            {
                switch (((Label)sender).TabIndex)
                {
                    case 0: // Dragging top-left sizing box
                        l = startl + x < startl + startw - MIN_SIZE ? startl + x : startl + startw - MIN_SIZE;
                        t = startt + y < startt + starth - MIN_SIZE ? startt + y : startt + starth - MIN_SIZE;
                        w = startl + startw - m_control.Left;
                        h = startt + starth - m_control.Top;
                        break;
                    case 1: // Dragging top-center sizing box
                        t = startt + y < startt + starth - MIN_SIZE ? startt + y : startt + starth - MIN_SIZE;
                        h = startt + starth - m_control.Top;
                        break;
                    case 2: // Dragging top-right sizing box
                        w = startw + x > MIN_SIZE ? startw + x : MIN_SIZE;
                        t = startt + y < startt + starth - MIN_SIZE ? startt + y : startt + starth - MIN_SIZE;
                        h = startt + starth - m_control.Top;
                        break;
                    case 3: // Dragging right-middle sizing box
                        w = startw + x > MIN_SIZE ? startw + x : MIN_SIZE;
                        break;
                    case 4: // Dragging right-bottom sizing box
                        w = startw + x > MIN_SIZE ? startw + x : MIN_SIZE;
                        h = starth + y > MIN_SIZE ? starth + y : MIN_SIZE;
                        break;
                    case 5: // Dragging center-bottom sizing box
                        h = starth + y > MIN_SIZE ? starth + y : MIN_SIZE;
                        break;
                    case 6: // Dragging left-bottom sizing box
                        l = startl + x < startl + startw - MIN_SIZE ? startl + x : startl + startw - MIN_SIZE;
                        w = startl + startw - m_control.Left;
                        h = starth + y > MIN_SIZE ? starth + y : MIN_SIZE;
                        break;
                    case 7: // Dragging left-middle sizing box
                        l = startl + x < startl + startw - MIN_SIZE ? startl + x : startl + startw - MIN_SIZE;
                        w = startl + startw - m_control.Left;
                        break;
                }
                l = (l < 0) ? 0 : l;
                t = (t < 0) ? 0 : t;
                m_control.SetBounds(l, t, w, h);
            }
        }

        //
        // Display sizing handles around picked control once sizing has completed
        //
        private void lbl_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
            MoveHandles();
            ShowHandles();
            m_control.Invalidate();
        }

        /////////////////////////////////////////////////////////////////
        // MOUSE EVENTS THAT MOVE THE PICKED CONTROL AROUND THE FORM
        /////////////////////////////////////////////////////////////////

        //
        // Get mouse pointer starting position on mouse down and hide sizing handles
        //
        private void ctl_MouseDown(object sender, MouseEventArgs ev)
        {
            dragging = true;
            int x = ev.X;
            int y = ev.Y;
            if (_bSnapToGrid)
            {
                x = (x / _nGridSize) * _nGridSize;
                y = (y / _nGridSize) * _nGridSize;
            }
            startx = x;
            starty = y;
            HideHandles();
        }

        //
        // Reposition the dragged control
        //
        private void ctl_MouseMove(object sender, MouseEventArgs ev)
        {
            if (dragging)
            {
                int x = ev.X;
                int y = ev.Y;

                if (_bSnapToGrid)
                {
                    x = (x / _nGridSize) * _nGridSize;
                    y = (y / _nGridSize) * _nGridSize;
                }

                int l = m_control.Left + x - startx;
                int t = m_control.Top + y - starty;
                int w = m_control.Width;
                int h = m_control.Height;
                l = (l < 0) ? 0 : ((l + w > m_control.Parent.ClientRectangle.Width) ?
                  m_control.Parent.ClientRectangle.Width - w : l);
                t = (t < 0) ? 0 : ((t + h > m_control.Parent.ClientRectangle.Height) ?
                m_control.Parent.ClientRectangle.Height - h : t);
                m_control.Left = l;
                m_control.Top = t;
            }
        }

        //
        // Display sizing handles around picked control once dragging has completed
        //
        private void ctl_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
            MoveHandles();
            ShowHandles();
            m_control.Invalidate();
        }
    }
}

