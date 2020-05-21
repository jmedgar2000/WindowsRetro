using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsRetro
{
    class RetroCheck: Control
    {
        private Pen pen = new Pen(Color.Black, 1);
        private Brush brush = new SolidBrush(Color.Black);
        private bool isChecked = false;
        private Font font = new Font("MS Sans Serif", 8.25F, FontStyle.Bold);

        public bool Checked
        {
            get
            {
                return isChecked;
            }
            set
            {
                if (isChecked != value)
                {
                    isChecked = value;
                    Invalidate();
                }
            }
        }

        public RetroCheck()
        {

        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            e.Graphics.DrawRectangle(pen, new Rectangle(0, 0, 13, 13));
            e.Graphics.DrawString(Text, font, brush, new PointF(18, 0));

            if (isChecked)
            {
                e.Graphics.DrawLine(pen, 0, 0, 13, 13);
                e.Graphics.DrawLine(pen, 13, 0, 0, 13);
            }
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);

            isChecked = !isChecked;
            Invalidate();
        }
    }
}
