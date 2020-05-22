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
        private bool downBarClicked = false;
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
            progressTimer.Tick += new EventHandler(progressTimer_Tick);
        }

        // Calculate the sizes of the scroll bar elements.
        private void SetUpScrollBar()
        {
            //clickedBarRectangle = ClientRectangle;
            thumbRectangle = new Rectangle(0, Height/2, Width, thumbHeight);
            upArrowRectangle = new Rectangle(0, 0, Width, arrowHeight);
            downArrowRectangle = new Rectangle(0, Height- arrowHeight, Width, arrowHeight);

            //// Set the default starting thumb position.
            thumbPosition = thumbHeight / 2;

            //// Set the right limit of the thumb's right border.
            thumbDownLimitDown = Height - arrowHeight;

            //// Set the right limit of the thumb's left border.
            thumbDownLimitUp = thumbDownLimitDown - thumbHeight;

            //// Set the left limit of the thumb's left border.
            thumbUpLimit = ClientRectangle.Y + arrowHeight;
        }

        // Draw the scroll bar in its normal state.
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // Draw the scroll bar track.
            ScrollBarRenderer.DrawRightHorizontalTrack(e.Graphics, ClientRectangle, ScrollBarState.Normal);

            // Draw the thumb and thumb grip in the current state.
            RetroScrollBarRenderer.DrawSystemButton(e.Graphics, thumbRectangle, SystemButtonType.Base, ButtonState.Normal);

            // Draw the scroll arrows in the current state.
            RetroScrollBarRenderer.DrawSystemButton(e.Graphics, upArrowRectangle, SystemButtonType.UpArrowScrollBar, ButtonState.Normal);
            RetroScrollBarRenderer.DrawSystemButton(e.Graphics, downArrowRectangle, SystemButtonType.DownArrowScrollBar, ButtonState.Normal);


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


        // Handle a mouse click in the scroll bar.
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            // When the thumb is clicked, update the distance from the left
            // edge of the thumb to the cursor tip.
            if (thumbRectangle.Contains(e.Location))
            {
                thumbClicked = true;
                thumbPosition = e.Location.Y - thumbRectangle.Y;
                thumbState = ScrollBarState.Pressed;
            }

            // When the left arrow is clicked, start the timer to scroll 
            // while the arrow is held down.
            else if (upArrowRectangle.Contains(e.Location))
            {
                upArrowClicked = true;
                upButtonState = ScrollBarArrowButtonState.UpPressed;
                progressTimer.Start();
            }

            // When the right arrow is clicked, start the timer to scroll 
            // while the arrow is held down.
            else if (downArrowRectangle.Contains(e.Location))
            {
                downArrowClicked = true;
                downButtonState = ScrollBarArrowButtonState.DownPressed;
                progressTimer.Start();
            }

            // When the scroll bar is clicked, start the timer to move the
            // thumb while the mouse is held down.
            else
            {
                trackPosition = e.Location.Y;

                if (e.Location.Y < this.thumbRectangle.Y)
                {
                    upBarClicked = true;
                }
                else
                {
                    downBarClicked = true;
                }
                progressTimer.Start();
            }

            Invalidate();
        }


        // Draw the track.
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            // Update the thumb position, if the new location is within 
            // the bounds.
            if (thumbClicked)
            {
                thumbClicked = false;
                thumbState = ScrollBarState.Normal;

                if (e.Location.Y > (thumbUpLimit + thumbPosition) &&
                    e.Location.Y < (thumbDownLimitUp + thumbPosition))
                {
                    thumbRectangle.Y = e.Location.Y - thumbPosition;
                    thumbClicked = false;
                }
            }

            // If one of the four thumb movement areas was clicked, 
            // stop the timer.
            else if (upArrowClicked)
            {
                upArrowClicked = false;
                upButtonState = ScrollBarArrowButtonState.UpNormal;
                progressTimer.Stop();
            }

            else if (downArrowClicked)
            {
                downArrowClicked = false;
                downButtonState = ScrollBarArrowButtonState.DownNormal;
                progressTimer.Stop();
            }

            else if (upBarClicked)
            {
                upBarClicked = false;
                progressTimer.Stop();
            }

            else if (downBarClicked)
            {
                downBarClicked = false;
                progressTimer.Stop();
            }

            Invalidate();
        }

        // Track mouse movements if the user clicks on the scroll bar thumb.
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            // Update the thumb position, if the new location is 
            // within the bounds.
            if (thumbClicked)
            {
                // The thumb is all the way to the left.
                if (e.Location.Y <= (thumbUpLimit + thumbPosition))
                {
                    thumbRectangle.Y = thumbUpLimit;
                }

                // The thumb is all the way to the right.
                else if (e.Location.Y >= (thumbDownLimitUp + thumbPosition))
                {
                    thumbRectangle.Y = thumbDownLimitUp;
                }

                // The thumb is between the ends of the track.
                else
                {
                    thumbRectangle.Y = e.Location.Y - thumbPosition;
                }

                Invalidate();
            }
        }

        // Recalculate the sizes of the scroll bar elements.
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            SetUpScrollBar();
        }

        // Handle the timer tick by updating the thumb position.
        private void progressTimer_Tick(object sender, EventArgs myEventArgs)
        {
            // If an arrow is clicked, move the thumb in small increments.
            if (downArrowClicked && thumbRectangle.Y < thumbDownLimitUp)
            {
                thumbRectangle.Y++;
            }
            else if (upArrowClicked && thumbRectangle.Y > thumbUpLimit)
            {
                thumbRectangle.Y--;
            }

            // If the track bar to right of the thumb is clicked, move the 
            // thumb to the right in larger increments until the right edge 
            // of the thumb hits the cursor.
            else if (downBarClicked && thumbRectangle.Y < thumbDownLimitUp &&
                thumbRectangle.Y + thumbRectangle.Height < trackPosition)
            {
                thumbRectangle.Y += 3;
            }

            // If the track bar to left of the thumb is clicked, move the 
            // thumb to the left in larger increments until the left edge 
            // of the thumb hits the cursor.
            else if (upBarClicked && thumbRectangle.Y > thumbUpLimit &&
                thumbRectangle.Y > trackPosition)
            {
                thumbRectangle.Y -= 3;
            }

            Invalidate();
        }
    }
}
