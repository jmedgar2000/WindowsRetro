using PinkieControls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsRetro
{
    class RetroButton : Control, IButtonControl
    {
        public enum States
        {
            Normal,
            MouseOver,
            Pushed
        }

        private ButtonType type= ButtonType.Normal;
        private SystemSymbol symbol = SystemSymbol.Up;
        private States state = States.Normal;
        private Rectangle[] rects0;
        private Rectangle[] rects1;
        private Brush blackBrush = new SolidBrush(Color.Black);
        private Brush whiteBrush = new SolidBrush(Color.White);
        private Brush brush3 = new SolidBrush(Color.FromArgb(135,136,143));
        private Brush brush4 = new SolidBrush(Color.FromArgb(192, 199, 200));
        private Pen blackPen = new Pen(Color.Black,1);
        private Pen pen1 = new Pen(Color.FromArgb(135, 136, 143), 1);
        private Pen pen2 = new Pen(Color.FromArgb(135, 136, 143), 1);
        
        private Font font = new Font("MS Sans Serif", 8.25F,FontStyle.Bold);

        public ButtonType Type
        {
            get
            {
                return type;
            }
            set
            {
                if (value != type)
                {
                    type = value;
                    Invalidate();
                }
            }
        }

        public SystemSymbol Symbol
        {
            get
            {
                return symbol;
            }
            set
            {
                if (value != symbol)
                {
                    symbol = value;
                    Invalidate();
                }
            }
        }

        DialogResult IButtonControl.DialogResult { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public RetroButton()
        {
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.SetStyle(ControlStyles.StandardDoubleClick, false);
            this.SetStyle(ControlStyles.Selectable, true);
            this.Size = new Size(120, 30);
            this.ResizeRedraw = true;

            //float[] dashValues = { 0, 1 ,0 ,1};
            //pen2.DashPattern = dashValues;
            pen2.DashStyle = DashStyle.Dot;

            //int X = this.Width;
            //int Y = this.Height;

            //rects0 = new Rectangle[2];
            //rects0[0] = new Rectangle(2, 4, 2, Y - 8);
            //rects0[1] = new Rectangle(X - 4, 4, 2, Y - 8);

            //rects1 = new Rectangle[8];
            //rects1[0] = new Rectangle(2, 1, 2, 2);
            //rects1[1] = new Rectangle(1, 2, 2, 2);
            //rects1[2] = new Rectangle(X - 4, 1, 2, 2);
            //rects1[3] = new Rectangle(X - 3, 2, 2, 2);
            //rects1[4] = new Rectangle(2, Y - 3, 2, 2);
            //rects1[5] = new Rectangle(1, Y - 4, 2, 2);
            //rects1[6] = new Rectangle(X - 4, Y - 3, 2, 2);
            //rects1[7] = new Rectangle(X - 3, Y - 4, 2, 2);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            //// Call the OnPaint method of the base class.  
            //base.OnPaint(e);
            //// Call methods of the System.Drawing.Graphics object.  
            //e.Graphics.DrawString(Text, Font, new SolidBrush(ForeColor), ClientRectangle);

            base.OnPaint(e);

            int X = this.Width;
            int Y = this.Height;

            //CreatePensBrushes();
            e.Graphics.SmoothingMode = SmoothingMode.HighSpeed;
            e.Graphics.CompositingQuality = CompositingQuality.HighSpeed;

            if (!this.Enabled)
            {
                //e.Graphics.FillRectangle(_brush02, 2, 2, X - 4, Y - 4);
                //e.Graphics.DrawLine(_pen01, 3, 1, X - 4, 1);
                //e.Graphics.DrawLine(_pen01, 3, Y - 2, X - 4, Y - 2);
                //e.Graphics.DrawLine(_pen01, 1, 3, 1, Y - 4);
                //e.Graphics.DrawLine(_pen01, X - 2, 3, X - 2, Y - 4);

                //e.Graphics.DrawLine(_pen02, 1, 2, 2, 1);
                //e.Graphics.DrawLine(_pen02, 1, Y - 3, 2, Y - 2);
                //e.Graphics.DrawLine(_pen02, X - 2, 2, X - 3, 1);
                //e.Graphics.DrawLine(_pen02, X - 2, Y - 3, X - 3, Y - 2);
                //e.Graphics.FillRectangles(_brush01, rects1);
            }
            else
            {


                e.Graphics.FillRectangle(blackBrush, new Rectangle(0, 0, X, Y));
                Size size = TextUtil.GetTextSize(e.Graphics, Text.Replace("&", ""), font, new Size(X, Y));
                var tPoint = new Point((X - size.Width - 2) / 2, (Y - this.Font.Height) / 2);

                switch (state)
                {
                    case States.Normal:
                        DrawNormalButton(e.Graphics, type, tPoint);
                        
                        //if (isDefault)
                        //{
                        //    e.Graphics.FillRectangles(bluesilverBrush02, rects0);
                        //    e.Graphics.DrawLine(pen23, 3, 4, 3, Y - 4);
                        //    e.Graphics.DrawLine(pen23, X - 4, 4, X - 4, Y - 4);

                        //    e.Graphics.DrawLine(bluesilverPen06, 2, 2, X - 3, 2);
                        //    e.Graphics.DrawLine(bluesilverPen07, 2, 3, X - 3, 3);
                        //    e.Graphics.DrawLine(bluesilverPen08, 2, Y - 4, X - 3, Y - 4);
                        //    e.Graphics.DrawLine(bluesilverPen09, 2, Y - 3, X - 3, Y - 3);
                        //}
                        break;

                    case States.Pushed:
                        DrawPushedButton(e.Graphics, type, tPoint, size);

                        break;
                }

                //TODO: Atencion como el borde exterior es negro al aplicar un cuadro negro como focused pues puede que no e vea efecto
                //if (this.Focused) ControlPaint.DrawFocusRectangle(e.Graphics,
                //    new Rectangle(3, 3, X - 6, Y - 6), Color.Black, this.BackColor);
            }   

   
            //DisposePensBrushes();
        }

        private void DrawNormalButton(Graphics g, ButtonType type, Point textPoint)
        {
            int X = this.Width;
            int Y = this.Height;

            if (type== ButtonType.Normal)
            {
                g.FillRectangle(whiteBrush, new Rectangle(1, 1, X - 3, 2));
                g.FillRectangle(whiteBrush, new Rectangle(1, 1, 2, Y - 3));
                g.DrawLine(pen1, 2, Y - 3, X - 2, Y - 3);
                g.DrawLine(pen1, 1, Y - 2, X - 2, Y - 2);
                g.DrawLine(pen1, X - 3, 2, X - 3, Y - 2);
                g.DrawLine(pen1, X - 2, 1, X - 2, Y - 2);
                g.FillRectangle(brush4, new Rectangle(3, 3, X - 6, Y - 6));

                g.DrawString(this.Text, font, blackBrush, textPoint.X, textPoint.Y);

                Point[] points = {
                                 new Point(1, 0),
                                 new Point(X-1, 0),
                                 new Point(X-1, 1),
                                 new Point(X, 1),
                                 new Point(X, Y-1),
                                 new Point(X-1, Y-1),
                                 new Point(X-1, Y),
                                 new Point(1, Y),
                                 new Point(1, Y-1),
                                 new Point(0, Y-1),
                                 new Point(0, 1),
                                 new Point(1, 1)};

                GraphicsPath path = new GraphicsPath();
                path.AddLines(points);

                this.Region = new Region(path);
            }

            if (type == ButtonType.System)
            {
                if (symbol== SystemSymbol.Down || symbol == SystemSymbol.Up )
                {

                    g.FillRectangle(whiteBrush, new Rectangle(1, 1, X - 3, 2));
                    g.FillRectangle(whiteBrush, new Rectangle(1, 1, 2, Y - 3)); //todo: Podria pintarse una linea
                    g.DrawLine(pen1, 2, Y - 3, X - 2, Y - 3);
                    g.DrawLine(pen1, 1, Y - 2, X - 2, Y - 2);
                    g.DrawLine(pen1, X - 3, 2, X - 3, Y - 2);
                    g.DrawLine(pen1, X - 2, 1, X - 2, Y - 2);
                    g.FillRectangle(brush4, new Rectangle(2, 2, X - 5, Y - 5));

                    if (symbol == SystemSymbol.Up)
                    {
                        g.DrawImage(WindowsRetro.Properties.Resources.up_arrow, new Point((Width - 7) / 2, (Height - 4) / 2));
                    }

                    if (symbol == SystemSymbol.Down)
                    {
                        g.DrawImage(WindowsRetro.Properties.Resources.down_arrow, new Point((Width - 7) / 2, (Height - 4) / 2));
                       
                    }
                }
                
                if (symbol == SystemSymbol.SystemOptions)
                {
                    g.FillRectangle(brush4, new Rectangle(1, 1, X - 2, Y - 2));
                    g.DrawImage(WindowsRetro.Properties.Resources.menu_symbol, new Point(((Width - 14) / 2), (Height - 4) / 2));
                }
            }
        }

        private void DrawPushedButton(Graphics g, ButtonType type, Point textPoint, Size textSize)
        {
            int X = this.Width;
            int Y = this.Height;

            if (type == ButtonType.Normal)
            {
                g.DrawLine(pen1, 2, 2, 2, Y - 3);
                g.DrawLine(pen1, 2, 2, X - 3, 2);
                g.FillRectangle(brush4, new Rectangle(3, 3, X - 5, Y - 5));

                g.DrawString(this.Text, font, blackBrush, textPoint.X + 1, textPoint.Y + 1);
                g.DrawRectangle(pen2, new Rectangle(textPoint.X + 1, textPoint.Y, textSize.Width + 4, textSize.Height + 1));

                Point[] points = {
                                 new Point(1, 0),
                                 new Point(X-1, 0),
                                 new Point(X-1, 1),
                                 new Point(X, 1),
                                 new Point(X, Y-1),
                                 new Point(X-1, Y-1),
                                 new Point(X-1, Y),
                                 new Point(1, Y),
                                 new Point(1, Y-1),
                                 new Point(0, Y-1),
                                 new Point(0, 1),
                                 new Point(1, 1)};

                GraphicsPath path = new GraphicsPath();
                path.AddLines(points);

                this.Region = new Region(path);
            }

            if (type == ButtonType.System)
            {
                if (symbol == SystemSymbol.Down || symbol == SystemSymbol.Up)
                {

                    g.DrawLine(pen1, 1, 1, X - 2, 1);
                    g.DrawLine(pen1, 1, 1, 1, Y - 2);
                    g.FillRectangle(brush4, new Rectangle(2, 2, X - 3, Y - 3));

                    var point = new Point(((Width - 7) / 2) + 1, ((Height - 4) / 2) + 1);
                    Bitmap bitmap = null;

                    if (symbol == SystemSymbol.Up)
                    {
                        bitmap = WindowsRetro.Properties.Resources.up_arrow;
                    }

                    if (symbol == SystemSymbol.Down)
                    {
                        bitmap = WindowsRetro.Properties.Resources.down_arrow;
                    }

                    g.DrawImage(bitmap, point);
                }

                if (symbol == SystemSymbol.SystemOptions)
                {
                    g.FillRectangle(brush4, new Rectangle(1, 1, X - 2, Y - 2));
                    g.DrawImage(WindowsRetro.Properties.Resources.menu_symbol, new Point(((Width - 14) / 2), (Height - 4) / 2));
                }
            }
        }

        //protected override void OnMouseDown(System.Windows.Forms.MouseEventArgs e)
        //{
        //    if ((e.Button & MouseButtons.Left) != MouseButtons.Left) return;

        //    state = States.Pushed;
        //    this.Invalidate();
        //    //this.Invalidate(bounds);
        //    base.OnMouseDown(e);
        //}

        protected void DisposePensBrushes()
        {
            blackBrush.Dispose();
            whiteBrush.Dispose();
            brush3.Dispose();
            brush4.Dispose();
        }

        void IButtonControl.NotifyDefault(bool value)
        {
            //throw new NotImplementedException();
        }

        void IButtonControl.PerformClick()
        {
            //throw new NotImplementedException();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) != MouseButtons.Left) return;

            state = States.Pushed;
            this.Invalidate();
            //this.Invalidate(bounds);
            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) != MouseButtons.Left) return;

            state = States.Normal;
            this.Invalidate();
            //this.Invalidate(bounds);
            base.OnMouseDown(e);
        }
    }
}
