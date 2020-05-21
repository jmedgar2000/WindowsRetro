using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace WindowsRetro
{
    public partial class Form1 : W31Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            // Draws a flat button on button1.
            //ControlPaint.DrawButton(
            //    System.Drawing.Graphics.FromHwnd(button1.Handle), 0, 0,button1.Width, button1.Height,ButtonState.Normal);

            //ControlPaint.DrawBorder3D(button1.CreateGraphics(), 0, 0, button1.Width, button1.Height, Border3DStyle.SunkenOuter);
            //ControlPaint.DrawFocusRectangle(button1.CreateGraphics(), new Rectangle(0, 0, button1.Width-5, button1.Height-5));
            //ControlPaint.(button1.CreateGraphics(), new Rectangle(0, 0, button1.Width - 5, button1.Height - 5));
        }

        private void retroButton1_Click(object sender, EventArgs e)
        {
            var g = VisualStyleInformation.IsSupportedByOS;
            g = VisualStyleInformation.IsEnabledByUser;
            var ff=Application.VisualStyleState;
        }
    }
}
