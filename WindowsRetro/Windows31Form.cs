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
    public class Windows31Form: Form
    {
        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int WM_SYSCOMMAND = 0x112;
        private const int HT_CAPTION = 0x2;
        private Button button1;
        private const int SC_SIZE = 0xF000;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd,int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        private Panel panel;
        private States state = States.Normal;
        private Pen pen = new Pen(Color.Black, 1);
        private Pen borderColor2 = new Pen(Color.FromArgb(192, 192, 192), 2);
        private Font titleFont = new Font("MS Sans Serif", 10, FontStyle.Bold);
        private Rectangle resizeLowerRightZone;
        private Rectangle resizeLowerZone;
        private Rectangle resizeRightZone;
        private bool isMouseDown;

        public Windows31Form()
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

            panel = new Panel();
            this.panel.Location = new System.Drawing.Point(4, 4);
            this.panel.Size = new System.Drawing.Size(ClientSize.Width - 8, 19);
            this.panel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
   | System.Windows.Forms.AnchorStyles.Right)));
            this.panel.BackColor = System.Drawing.Color.Navy;
            this.panel.DoubleClick += Panel_DoubleClick;
            this.panel.MouseDoubleClick += Panel_MouseDoubleClick;
            this.Controls.Add(panel);

            panel.Paint += Panel_Paint;
            panel.MouseDown += Panel_MouseDown;

            RetroButton retroMenu = new RetroButton();
            retroMenu.Size = new Size(20, 20);
            retroMenu.Type = ButtonType.System;
            retroMenu.Symbol = SystemSymbol.SystemOptions;
            //retro1.Location = new Point(0, 200);
            retroMenu.Location = new Point(-1, -1);
            //retro1.Dock = DockStyle.Right;

            RetroButton upButton = new RetroButton();
            upButton.Size = new Size(20, 20);
            upButton.Text = "t";
            upButton.Type = ButtonType.System;
            upButton.Symbol = SystemSymbol.Up;
            upButton.Click += upButton_Click;
            //retro1.Location = new Point(0, 200);
            upButton.Location = new Point(panel.Width - upButton.Width +1, -1);
            upButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right))));
            //retro1.Dock = DockStyle.Right;

            RetroButton downButton = new RetroButton();
            downButton.Size = new Size(20, 20);
            downButton.Text = "i";
            //retro2.Dock = DockStyle.Right;
            downButton.Type = ButtonType.System;
            downButton.Symbol = SystemSymbol.Down;
            downButton.Click += downButton_Click;
            downButton.Location = new Point(panel.Width - upButton.Width - downButton.Width + 2, -1);
            downButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right))));

            panel.Controls.Add(retroMenu);
            panel.Controls.Add(upButton);
            panel.Controls.Add(downButton);
        }

        private void Panel_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //if (this.WindowState == FormWindowState.Normal) this.WindowState = FormWindowState.Maximized;
            //if (this.WindowState == FormWindowState.Maximized) this.WindowState = FormWindowState.Normal;
        }

        private void Panel_DoubleClick(object sender, EventArgs e)
        {
            if (this.WindowState== FormWindowState.Normal) this.WindowState = FormWindowState.Maximized;
            if (this.WindowState == FormWindowState.Maximized) this.WindowState = FormWindowState.Normal;
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
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(165, 97);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // Windows31Form
            // 
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(400, 264);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Windows31Form";
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
        protected override void OnMouseMove( System.Windows.Forms.MouseEventArgs e)
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

            var cursor= GetZoneCursor(e.Location);

                      
            if (cursor!=null)
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

        private void Panel_Paint(object sender, PaintEventArgs e)
        {
            var size = TextUtil.GetTextSize(this.CreateGraphics(), this.Text, titleFont, panel.Size);
            var tPoint = new Point((panel.Width - size.Width - 2) / 2, (panel.Height - this.Font.Height) / 2);

            e.Graphics.DrawLine(pen, 0, panel.Height - 1, panel.Width, panel.Height - 1);
            e.Graphics.DrawString(this.Text, titleFont, new SolidBrush(Color.White), tPoint.X, tPoint.Y - 1);
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
            e.Graphics.DrawLine(pen, 22, Height - 3, 22, Height );
            e.Graphics.DrawLine(pen, Width - 23, Height - 3, Width-23, Height );

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
            //if (state== States.Pushed )
            //{

            //    if (e.Button == MouseButtons.Left)
            //    {
            //if (PointInRect(e.Location,panel.Bounds))
            //{
            ReleaseCapture();
            SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);

            //}
            //    }
            //}
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

            if (panel!=null)
            {
                this.panel.Size = new System.Drawing.Size(ClientSize.Width - 8, 19);
                this.panel.Invalidate();
            }

            var dimension = 10;
            resizeLowerRightZone = new Rectangle(Width - dimension, Height - dimension, dimension, dimension);
            resizeLowerZone = new Rectangle(dimension, Height - dimension, Width - dimension, dimension);
            resizeRightZone = new Rectangle(Width - dimension, 23, dimension, Height-23 - dimension);

            Invalidate();
        }

    }
}
