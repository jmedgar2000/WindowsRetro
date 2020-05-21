using PinkieControls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static WindowsRetro.RetroButton;

namespace WindowsRetro
{
    public class W31Form : Form
    {
        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int WM_SYSCOMMAND = 0x112;
        private const int HT_CAPTION = 0x2;
        private Panel pnlTitle;
        private RetroButton retroButton1;
        private RetroButton btnMinimize;
        private RetroButton btnMaximize;
        private const int SC_SIZE = 0xF000;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        //private Panel panel;
        private States state = States.Normal;
        private Pen pen = new Pen(Color.Black, 1);
        private Pen borderColor2 = new Pen(Color.FromArgb(192, 192, 192), 2);
        private Font titleFont = new Font("MS Sans Serif", 10, FontStyle.Bold);
        private Rectangle resizeLowerRightZone;
        private Rectangle resizeLowerZone;
        private Rectangle resizeRightZone;
        private bool isMouseDown;

        public W31Form()
        {

            // set control styles:
            this.SetStyle(
                 ControlStyles.AllPaintingInWmPaint |
                 ControlStyles.DoubleBuffer |
                 ControlStyles.UserPaint |
                 ControlStyles.ResizeRedraw |
                 ControlStyles.SupportsTransparentBackColor,
                 true
            );

            InitializeComponent();
        }

        private void upButton_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }

        private void downButton_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void InitializeComponent()
        {
            this.pnlTitle = new System.Windows.Forms.Panel();
            this.retroButton1 = new WindowsRetro.RetroButton();
            this.btnMaximize = new WindowsRetro.RetroButton();
            this.btnMinimize = new WindowsRetro.RetroButton();
            this.pnlTitle.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTitle
            // 
            this.pnlTitle.BackColor = System.Drawing.Color.Navy;
            this.pnlTitle.Controls.Add(this.btnMinimize);
            this.pnlTitle.Controls.Add(this.retroButton1);
            this.pnlTitle.Controls.Add(this.btnMaximize);
            this.pnlTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTitle.Location = new System.Drawing.Point(4, 4);
            this.pnlTitle.Name = "pnlTitle";
            this.pnlTitle.Size = new System.Drawing.Size(392, 19);
            this.pnlTitle.TabIndex = 0;
            this.pnlTitle.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlTitle_Paint);
            this.pnlTitle.DoubleClick += new System.EventHandler(this.pnlTitle_DoubleClick);
            this.pnlTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnlTitle_MouseDown);
            this.pnlTitle.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnlTitle_MouseMove);
            this.pnlTitle.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pnlTitle_MouseUp);
            // 
            // retroButton1
            // 
            this.retroButton1.Location = new System.Drawing.Point(-1, -1);
            this.retroButton1.Name = "retroButton1";
            this.retroButton1.Size = new System.Drawing.Size(20, 20);
            this.retroButton1.Symbol = WindowsRetro.SystemSymbol.SystemOptions;
            this.retroButton1.TabIndex = 1;
            this.retroButton1.Text = "retroButton1";
            this.retroButton1.Type = WindowsRetro.ButtonType.System;
            // 
            // btnMaximize
            // 
            this.btnMaximize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMaximize.Location = new System.Drawing.Point(373, -1);
            this.btnMaximize.Name = "btnMaximize";
            this.btnMaximize.Size = new System.Drawing.Size(20, 20);
            this.btnMaximize.Symbol = WindowsRetro.SystemSymbol.Up;
            this.btnMaximize.TabIndex = 2;
            this.btnMaximize.Text = "retroButton2";
            this.btnMaximize.Type = WindowsRetro.ButtonType.System;
            this.btnMaximize.Click += new System.EventHandler(this.btnMaximize_Click);
            // 
            // btnMinimize
            // 
            this.btnMinimize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMinimize.Location = new System.Drawing.Point(354, -1);
            this.btnMinimize.Name = "btnMinimize";
            this.btnMinimize.Size = new System.Drawing.Size(20, 20);
            this.btnMinimize.Symbol = WindowsRetro.SystemSymbol.Down;
            this.btnMinimize.TabIndex = 3;
            this.btnMinimize.Text = "retroButton3";
            this.btnMinimize.Type = WindowsRetro.ButtonType.System;
            this.btnMinimize.Click += new System.EventHandler(this.btnMinimize_Click);
            // 
            // W31Form
            // 
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(400, 264);
            this.Controls.Add(this.pnlTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "W31Form";
            this.Padding = new System.Windows.Forms.Padding(4);
            this.pnlTitle.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Invalidate();
        }

        protected override void OnResizeBegin(EventArgs e)
        {

            this.Invalidate();
            base.OnResizeBegin(e);
        }
        protected override void OnMouseMove(System.Windows.Forms.MouseEventArgs e)
        {
            #region Sizing
            ResizeWindow(e);
            #endregion

            base.OnMouseMove(e);
        }

        private Cursor GetZoneCursor(Point p)
        {
            if (PointInRect(p, resizeLowerRightZone))
            {
                return Cursors.SizeNWSE;
            }

            if (PointInRect(p, resizeLowerZone))
            {
                return Cursors.SizeNS;
            }

            if (PointInRect(p, resizeRightZone))
            {
                return Cursors.SizeWE;
            }

            return null;

        }

        private int GetSizeCommand(Point p)
        {
            if (PointInRect(p, resizeLowerRightZone))
            {
                return 8;
            }

            if (PointInRect(p, resizeLowerZone))
            {
                return 6;
            }

            if (PointInRect(p, resizeRightZone))
            {
                return 2;
            }

            return 0;

        }

        private void ResizeWindow(MouseEventArgs e)
        {
            bool bResizing = true;

            var cursor = GetZoneCursor(e.Location);


            if (cursor != null)
            {
                Cursor = cursor;

                if (isMouseDown && bResizing)
                {

                    if (e.Button == MouseButtons.Left)
                    {
                        if (this.Width < this.MinimumSize.Width) bResizing = false;
                        if (this.Height < this.MinimumSize.Height) bResizing = false;

                        ReleaseCapture();
                        SendMessage(this.Handle, WM_SYSCOMMAND, (SC_SIZE + GetSizeCommand(e.Location)), 0);

                    }
                }
            }
            else
            {
                Cursor = Cursors.Default;
            }
        }

        private void Panel_MouseDown(object sender, MouseEventArgs e)
        {
            HitTestMoveTitleBar(e);

            base.OnMouseMove(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            e.Graphics.DrawRectangle(pen, new Rectangle(0, 0, Width - 1, Height - 1));
            e.Graphics.DrawRectangle(pen, new Rectangle(3, 3, Width - 7, Height - 7));
            e.Graphics.DrawRectangle(borderColor2, new Rectangle(2, 2, Width - 4, Height - 4));

            //Draw horizontal border micro lines
            e.Graphics.DrawLine(pen, 0, 22, 3, 22);
            e.Graphics.DrawLine(pen, Width - 3, 22, Width, 22);
            e.Graphics.DrawLine(pen, 0, Height - 23, 3, Height - 23);
            e.Graphics.DrawLine(pen, Width - 3, Height - 23, Width, Height - 23);

            //Draw vertical border micro lines
            e.Graphics.DrawLine(pen, 22, 0, 22, 3);
            e.Graphics.DrawLine(pen, Width - 23, 0, Width - 23, 3);
            e.Graphics.DrawLine(pen, 22, Height - 3, 22, Height);
            e.Graphics.DrawLine(pen, Width - 23, Height - 3, Width - 23, Height);

        }

        protected override void OnMouseUp(System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                state = States.Normal;
                isMouseDown = false;
            }
        }

        protected override void OnMouseDown(System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                state = States.Normal;
                isMouseDown = true;
            }
        }

        private void HitTestMoveTitleBar(MouseEventArgs e)
        {
            if (isMouseDown)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);

            }
        }

        private bool PointInRect(Point p, Rectangle rc)
        {
            if ((p.X > rc.Left && p.X < rc.Right && p.Y > rc.Top && p.Y < rc.Bottom))
                return true;
            else
                return false;
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);

            if (pnlTitle != null)
            {
                //this.pnlTitle.Size = new System.Drawing.Size(ClientSize.Width - 8, 19);
                this.pnlTitle.Invalidate();
            }

            var dimension = 10;
            resizeLowerRightZone = new Rectangle(Width - dimension, Height - dimension, dimension, dimension);
            resizeLowerZone = new Rectangle(dimension, Height - dimension, Width - dimension, dimension);
            resizeRightZone = new Rectangle(Width - dimension, 23, dimension, Height - 23 - dimension);

            Invalidate();
        }

        private void pnlTitle_DoubleClick(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
                this.WindowState = FormWindowState.Maximized;
            }
            else
            {
                if (this.WindowState == FormWindowState.Maximized) this.WindowState = FormWindowState.Normal;

            }

        }

        private void pnlTitle_Paint(object sender, PaintEventArgs e)
        {
            var size = TextUtil.GetTextSize(this.CreateGraphics(), this.Text, titleFont, pnlTitle.Size);
            var tPoint = new Point((pnlTitle.Width - size.Width - 2) / 2, (pnlTitle.Height - this.Font.Height) / 2);

            e.Graphics.DrawLine(pen, 0, pnlTitle.Height - 1, pnlTitle.Width, pnlTitle.Height - 1);
            e.Graphics.DrawString(this.Text, titleFont, new SolidBrush(Color.White), tPoint.X, tPoint.Y - 1);
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnMaximize_Click(object sender, EventArgs e)
        {
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
            this.WindowState = FormWindowState.Maximized;
        }

        private void pnlTitle_MouseDown(object sender, MouseEventArgs e)
        {
            

            //base.OnMouseMove(e);
        }

        private void pnlTitle_MouseMove(object sender, MouseEventArgs e)
        {
            isMouseDown = true;
            HitTestMoveTitleBar(e);
        }

        private void pnlTitle_MouseUp(object sender, MouseEventArgs e)
        {
            isMouseDown = false;
        }
    }
 }
