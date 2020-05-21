using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsRetro
{
    class RetroOption: Control
    {
        private Font font = new Font("MS Sans Serif", 8.25F, FontStyle.Bold);
        private Brush brush = new SolidBrush(Color.Black);
        private Pen pen = new Pen(Color.Black, 1);
        private bool isChecked=false;

        public bool Checked
        {
            get
            {
                return isChecked;
            }
            set
            {
                if (isChecked!=value)
                {
                    isChecked = value;
                    Invalidate();
                }

                if (value)
                {
                    BroadcastChange();
                }
            }
        }

        private void BroadcastChange()
        {
            var container = this.Parent;

            foreach(Control c in container.Controls)
            {
                if (this.Name !=c.Name )
                {
                    //todo: group index
                    if (c.GetType() == typeof(RetroOption))
                    {
                        ((RetroOption)c).Checked = false;
                    }
                }
            }
        }

        public RetroOption()
        {
            this.SetStyle(
                             ControlStyles.AllPaintingInWmPaint |
                             ControlStyles.DoubleBuffer |
                             ControlStyles.UserPaint |
                             ControlStyles.ResizeRedraw |
                             ControlStyles.SupportsTransparentBackColor,
                             true
                        );
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (isChecked)
                e.Graphics.DrawImage(Properties.Resources.option_checked, new Point(0, 0));
            else
                e.Graphics.DrawImage(Properties.Resources.option_unchecked, new Point(0, 0));

            e.Graphics.DrawString(Text, font, brush, new PointF(18, 0));
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);

            Checked = true;
        }
    }
}
