using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Spirograph_v1.Controls.RPButton
{
    public partial class RPButton : Button
    {
        bool _hoverState = false;
        bool _downState  = false;

        public RPButton()
        {
            this.BackColor                = Color.Black;
            this.HoverBackgroundColor     = Color.Black;
            this.ClickedBackgroundColor   = Color.White;
            this.BackgroundGradientOffset = 0;
        }


        public Point TextPosition { get; set; }

        public Color ClickedBackgroundColor { get; set; }

        public Color HoverBackgroundColor { get; set; }

        public int BackgroundGradientOffset { get; set; }



        protected override void OnPaint(PaintEventArgs pevent)
        {
            Graphics g = pevent.Graphics;

            Rectangle area = new(0, 0, this.Width, this.Height);

            g.DrawRectangle(new Pen(new SolidBrush(this.BackColor)), area);

            if (this._hoverState)
            {
                g.FillRectangle(new SolidBrush(ClickedBackgroundColor), area);
                g.DrawString(this.Text, new Font(this.Font, this.Font.Style), new SolidBrush(this.BackColor), this.TextPosition.X, this.TextPosition.Y);
            }
            else if (this._downState)
            {
                g.FillRectangle(new SolidBrush(Color.Black), area);
                g.DrawString(this.Text, new Font(this.Font, this.Font.Style), new SolidBrush(this.BackColor), this.TextPosition.X, this.TextPosition.Y);
            }
            else
            {
                int red   = (this.BackColor.R > this.BackgroundGradientOffset) ? this.BackColor.R - this.BackgroundGradientOffset : 0;
                int green = (this.BackColor.G > this.BackgroundGradientOffset) ? this.BackColor.G - this.BackgroundGradientOffset : 0;
                int blue  = (this.BackColor.B > this.BackgroundGradientOffset) ? this.BackColor.B - this.BackgroundGradientOffset : 0;

                Color gradientColor = Color.FromArgb(red, green, blue);
                LinearGradientBrush lgBrush = new(area, this.BackColor, gradientColor, 1);
                //gradientColor = Color.FromArgb(red, green, blue);

                g.FillRectangle(lgBrush, area);
                g.DrawString(this.Text, new Font(this.Font, this.Font.Style), new SolidBrush(this.ForeColor), this.TextPosition.X, this.TextPosition.Y);
            }

        }   // OnPaint()


        protected override void OnMouseEnter(EventArgs e)
        {
            _hoverState = true;
            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            _hoverState = false;
            base.OnMouseLeave(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            _downState  = true;
            _hoverState = false;
            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            _downState  = false;
            _hoverState = false;
            base.OnMouseUp(e);
        }


    }   // class RPButton

}   // namespace Spirograph_v1

