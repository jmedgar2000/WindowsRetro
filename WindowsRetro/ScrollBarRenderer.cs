using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;
using WindowsRetro;

namespace System.Windows.Forms
{
    public static class RetroScrollBarRenderer
    {
        private static Brush whiteBrush = new SolidBrush(Color.White);
        private static Brush brush4 = new SolidBrush(Color.FromArgb(192, 199, 200));
        private static Pen pen1 = new Pen(Color.FromArgb(135, 136, 143), 1);

        public static void DrawSystemButton(Graphics g, Rectangle bounds, SystemButtonType type,ButtonState buttonState)
        {
            //TODO: Aqui debe dibujarse el borde
            //TODO: Porque hay dos rectangulos blanco cuando debe haber solo una linea

            switch (type)
            {
                case SystemButtonType.Menu:
                    break;

                default:
                    switch (buttonState)
                    {
                        case ButtonState.Normal:
                            g.FillRectangle(whiteBrush, new Rectangle(1, 1, bounds.X - 3, 2));
                            g.FillRectangle(whiteBrush, new Rectangle(1, 1, 2, bounds.Y - 3)); //todo: Podria pintarse una linea
                            g.DrawLine(pen1, 2, bounds.Y - 3, bounds.X - 2, bounds.Y - 3);
                            g.DrawLine(pen1, 1, bounds.Y - 2, bounds.X - 2, bounds.Y - 2);
                            g.DrawLine(pen1, bounds.X - 3, 2, bounds.X - 3, bounds.Y - 2);
                            g.DrawLine(pen1, bounds.X - 2, 1, bounds.X - 2, bounds.Y - 2);
                            g.FillRectangle(brush4, new Rectangle(2, 2, bounds.X - 5, bounds.Y - 5));
                            break;

                        case ButtonState.Pushed:
                            g.DrawLine(pen1, 1, 1, bounds.X - 2, 1);
                            g.DrawLine(pen1, 1, 1, 1, bounds.Y - 2);
                            g.FillRectangle(brush4, new Rectangle(2, 2, bounds.X - 3, bounds.Y - 3));
                            break;
                    }

                    DrawSymbol(g, bounds, type);

                    break;

     
            }
        }

        public static void DrawSymbol(Graphics g, Rectangle bounds, SystemButtonType type)
        {
            //var point=new PointF(0F,0F);
            Bitmap bitmap=null;

            switch (type)
            {
                case SystemButtonType.Minimize:
                    bitmap= WindowsRetro.Properties.Resources.down_arrow;
                    break;

                case SystemButtonType.Maximize:
                    bitmap = WindowsRetro.Properties.Resources.up_arrow;
                    break;

                case SystemButtonType.Menu:
                    bitmap= WindowsRetro.Properties.Resources.menu_symbol;
                    break;

                case SystemButtonType.UpArrowScrollBar:
                    bitmap= WindowsRetro.Properties.Resources.up_arrow_1;
                    break;

                case SystemButtonType.DownArrowScrollBar:
                    bitmap = WindowsRetro.Properties.Resources.down_arrow_1;
                    break;

            }

            g.DrawImage(bitmap, (g.ClipBounds.Width - bitmap.Width) / 2, (g.ClipBounds.Height - bitmap.Height) / 2);

        }

        //public static void DrawSystemButton(Graphics g, Rectangle bounds, SystemButtonType buttonType,ButtonState buttonState)
        //{
        //    //Este metodo solo establece la imagen de sistema basado en el tipo

        //    switch (buttonType)
        //    {
        //        case SystemButtonType.Common:
        //            switch (buttonState)
        //            {
        //                case ButtonState.Normal:
        //                    g.FillRectangle(whiteBrush, new Rectangle(1, 1, bounds.X - 3, 2));
        //                    g.FillRectangle(whiteBrush, new Rectangle(1, 1, 2, bounds.Y - 3)); //todo: Podria pintarse una linea
        //                    g.DrawLine(pen1, 2, bounds.Y - 3, bounds.X - 2, bounds.Y - 3);
        //                    g.DrawLine(pen1, 1, bounds.Y - 2, bounds.X - 2, bounds.Y - 2);
        //                    g.DrawLine(pen1, bounds.X - 3, 2, bounds.X - 3, bounds.Y - 2);
        //                    g.DrawLine(pen1, bounds.X - 2, 1, bounds.X - 2, bounds.Y - 2);
        //                    g.FillRectangle(brush4, new Rectangle(2, 2, bounds.X - 5, bounds.Y - 5));

        //                    break;
        //            }
        //            break;
        //    }


        //    //drawbutton graphics, rectangle, state (normal, pushed...), ,type(arrow, close, delete...., arrow1) , 
        //    //drawbutton graphics, rectangle, type (minime, maximize, scrollbarup, menu ) , state(normal, pushed...), 
        //}

        private static void DrawSymbol(Graphics g, ScrollBarArrowButtonState state)
        {
            //Maximise
            //Minimize
            //Menu
            //Restaurar
            //Left Arrow ScrollBar
            //Right Arrow ScrollBar
            //Up Arrow ScrollBar
            //Down Arrow ScrollBar
            //Up Arrow numericUpDown
            //Down arroy numericUpDown
            //Combobox arrow
            //Litle Menu

            switch( state)
            {
                case ScrollBarArrowButtonState.UpNormal:
                    g.DrawImage(WindowsRetro.Properties.Resources.up_arrow_1, new PointF((g.ClipBounds.Width - 7) / 2, (g.ClipBounds.Height - 4) / 2));
                    break;

                case ScrollBarArrowButtonState.DownNormal:
                    g.DrawImage(WindowsRetro.Properties.Resources.up_arrow_1, new PointF((g.ClipBounds.Width - 7) / 2, (g.ClipBounds.Height - 4) / 2));
                    break;


            }

            if (state== ScrollBarArrowButtonState.UpNormal)
            {
                }
            if (state == ScrollBarArrowButtonState.UpNormal)
            {
                g.DrawImage(WindowsRetro.Properties.Resources.up_arrow_1, new PointF((g.ClipBounds.Width - 7) / 2, (g.ClipBounds.Height - 4) / 2));
            }
        }

    }
}
