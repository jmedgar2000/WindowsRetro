using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace WindowsRetro
{
    public class RetroVerticalScrollBar: Control
    {
        private Rectangle clickedBarRectangle;
        private Rectangle thumbRectangle;
        private Rectangle upArrowRectangle;
        private Rectangle downArrowRectangle;
        private bool upArrowClicked = false;
        private bool downArrowClicked = false;
        private bool upBarClicked = false;
        private bool DownBarClicked = false;
        private bool thumbClicked = false;
        private ScrollBarState thumbState = ScrollBarState.Normal;
        private ScrollBarArrowButtonState upButtonState =ScrollBarArrowButtonState.UpNormal;
        private ScrollBarArrowButtonState downButtonState =ScrollBarArrowButtonState.DownNormal;

        // This control does not allow these widths to be altered.
        private int thumbHeight = 15;
        private int arrowHeight = 17;


        private int thumbDownLimitDown = 0;

        private int thumbDownLimitUp= 0;

        private int thumbUpLimit = 0;

        private int thumbPosition = 0;

        private int trackPosition = 0;

        // This timer draws the moving thumb while the scroll arrows or 
        // track are pressed.
        private Timer progressTimer = new Timer();

        public RetroVerticalScrollBar(): base()
        {
            //this.Location = new Point(10, 10);
            //this.Width = 20;
            //this.Height = 200;
            this.Size = new Size(20, 200);
            this.DoubleBuffered = true;

            SetUpScrollBar();
            progressTimer.Interval = 20;
            //progressTimer.Tick += new EventHandler(progressTimer_Tick);
        }

        // Calculate the sizes of the scroll bar elements.
        private void SetUpScrollBar()
        {
            //clickedBarRectangle = ClientRectangle;
            thumbRectangle = new Rectangle(0, Height/2, Width, thumbHeight);
            upArrowRectangle = new Rectangle(0, 0, Width, arrowHeight);
            downArrowRectangle = new Rectangle(0, Height- arrowHeight, Width, arrowHeight);

            //// Set the default starting thumb position.
            //thumbPosition = thumbWidth / 2;

            //// Set the right limit of the thumb's right border.
            //thumbRightLimitRight = ClientRectangle.Right - arrowWidth;

            //// Set the right limit of the thumb's left border.
            //thumbRightLimitLeft = thumbRightLimitRight - thumbWidth;

            //// Set the left limit of the thumb's left border.
            //thumbLeftLimit = ClientRectangle.X + arrowWidth;
        }

        // Draw the scroll bar in its normal state.
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // Visual styles are not enabled.
            if (!ScrollBarRenderer.IsSupported)
            {
                //this.Parent.Text = "CustomScrollBar Disabled";
                return;
            }

            // Draw the scroll bar track.
            ScrollBarRenderer.DrawRightHorizontalTrack(e.Graphics, ClientRectangle, ScrollBarState.Normal);

            // Draw the thumb and thumb grip in the current state.
            ScrollBarRenderer.DrawVerticalThumb(e.Graphics,thumbRectangle, thumbState);
            ScrollBarRenderer.DrawVerticalThumbGrip(e.Graphics, thumbRectangle, thumbState);
            

            // Draw the scroll arrows in the current state.
            ScrollBarRenderer.DrawArrowButton(e.Graphics, upArrowRectangle, upButtonState);
            ScrollBarRenderer.DrawArrowButton(e.Graphics, downArrowRectangle, downButtonState);

            // Draw a highlighted rectangle in the left side of the scroll 
            // bar track if the user has clicked between the left arrow 
            // and thumb.
            //if (leftBarClicked)
            //{
            //    clickedBarRectangle.X = thumbLeftLimit;
            //    clickedBarRectangle.Width = thumbRectangle.X - thumbLeftLimit;

            //    ScrollBarRenderer.DrawLeftHorizontalTrack(e.Graphics, clickedBarRectangle, ScrollBarState.Pressed);
            //}

            //// Draw a highlighted rectangle in the right side of the scroll 
            //// bar track if the user has clicked between the right arrow 
            //// and thumb.
            //else if (rightBarClicked)
            //{
            //    clickedBarRectangle.X = thumbRectangle.X + thumbRectangle.Width;
            //    clickedBarRectangle.Width = thumbRightLimitRight - clickedBarRectangle.X;

            //    ScrollBarRenderer.DrawRightHorizontalTrack(e.Graphics, clickedBarRectangle, ScrollBarState.Pressed);
            //}
        }
    }
}
