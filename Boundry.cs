using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CropImage
{
   public class Boundry
    {
        public RectangleF rectanle { get; set; }
        public bool IsCropBoundry { get; set; }

        RectangleF TopLeft { get { return new RectangleF(rectanle.X - 2.5f, rectanle.Y - 2.5f, 5, 5);} }
        RectangleF TopRight { get { return new RectangleF(rectanle.X +rectanle.Width - 2.5f, rectanle.Y  -  2.5f, 5, 5); } }
        RectangleF BottomRight { get { return new RectangleF(rectanle.X + rectanle.Width - 2.5f, rectanle.Y + rectanle.Height - 2.5f, 5, 5); } }
        RectangleF BottomLeft { get { return new RectangleF(rectanle.X - 2.5f, rectanle.Y +rectanle.Height - 2.5f, 5, 5); } }
        Pen BoudnryPen;

        public enum Corners
        {
            TopLeft,
            TopRight,
            BottomRight,
            BottomLeft,
            Body,
            None
        }

      public  Corners SelectedPart;
        public Boundry(RectangleF rect,bool iscropboundry)
        {
            rectanle = rect;
            IsCropBoundry = iscropboundry;
            BoudnryPen = new Pen(new HatchBrush(HatchStyle.Percent50, Color.White, Color.Black), 1f);
            SelectedPart = Corners.None;
        }
        public void Move(float dx, float dy)
        {
            if (IsCropBoundry)
            {
                switch (SelectedPart)
                {
                    case Corners.Body:
                        rectanle = new RectangleF(rectanle.X + dx, rectanle.Y + dy, rectanle.Width, rectanle.Height);
                        break;

                    case Corners.TopLeft:
                        rectanle = new RectangleF(rectanle.X + dx, rectanle.Y + dy, rectanle.Width - dx, rectanle.Height - dy);
                        break;
                    case Corners.TopRight:
                        rectanle = new RectangleF(rectanle.X , rectanle.Y + dy, rectanle.Width + dx, rectanle.Height - dy);
                        break;
                    case Corners.BottomLeft:
                        rectanle = new RectangleF(rectanle.X+dx, rectanle.Y , rectanle.Width - dx, rectanle.Height+  dy);
                        break;
                    case Corners.BottomRight:
                        rectanle = new RectangleF(rectanle.X, rectanle.Y, rectanle.Width + dx, rectanle.Height + dy);
                        break;
                }
            }
            else
                rectanle = new RectangleF(rectanle.X + dx, rectanle.Y + dy, rectanle.Width, rectanle.Height);
        }
        public RectangleF zoomedRectangle;
        internal void Zoom(Point mouseLocation, float zoomFactor)
        {
            
            zoomedRectangle=  MDCollection.ImageZoom.Zoom(rectanle, mouseLocation, zoomFactor);


            rectanle = new RectangleF(zoomedRectangle.X,zoomedRectangle.Y ,zoomedRectangle.Width ,zoomedRectangle.Height);
            

      }

        internal Cursor GetCurrentCursor()
        {
            switch (SelectedPart)
            {


                case Corners.TopLeft:
                    return Cursors.SizeNWSE;

                case Corners.BottomRight:
                    return Cursors.SizeNWSE;


                case Corners.TopRight:
                    return Cursors.SizeNESW;

                case Corners.BottomLeft:
                    return Cursors.SizeNESW;
                case Corners.Body:
                    return Cursors.SizeAll;
            }

            return Cursors.Arrow;
        }

        public void ResizeBy(float dw,float dh)
        {
            rectanle = new RectangleF(rectanle.X , rectanle.Y , rectanle.Width+dw, rectanle.Height+dh);
        }

        internal bool IsMouseInterset(Point location)
        {
            RectangleF mouseRect = new RectangleF(location, new Size(1, 1));

            if (IsCropBoundry)
            {
                if (mouseRect.IntersectsWith(TopLeft))
                {
                    SelectedPart = Corners.TopLeft;
                    return true;
                }
                else if (mouseRect.IntersectsWith(TopRight))
                {
                    SelectedPart = Corners.TopRight;
                    return true;
                }
                else if (mouseRect.IntersectsWith(BottomLeft))
                {
                    SelectedPart = Corners.BottomLeft;
                    return true;
                }
                else if (mouseRect.IntersectsWith(BottomRight))
                {
                    SelectedPart = Corners.BottomRight;
                    return true;
                }
                else
                {
                    if (mouseRect.IntersectsWith(rectanle))
                    {
                        SelectedPart = Corners.Body;
                        return true;
                    }
                    else
                    {
                        SelectedPart = Corners.None;
                        return false;
                    }
                }
            }
            else
            {
                if (mouseRect.IntersectsWith(rectanle))
                {
                    SelectedPart = Corners.Body;
                    return true;
                }
                else
                {
                    SelectedPart = Corners.None;
                    return false;
                }
            }
        }

        internal void Centerlize(RectangleF parentrect)
        {
            rectanle = new RectangleF(parentrect.Width / 2 - rectanle.Width / 2, parentrect.Height / 2 - rectanle.Height / 2, rectanle.Width, rectanle.Height);
        }

        public void Paint(Graphics g)
        {
            g.DrawRectangle(BoudnryPen, rectanle.X, rectanle.Y, rectanle.Width, rectanle.Height);

            if(IsCropBoundry)
            {
                g.DrawRectangle(Pens.Black, TopLeft.X, TopLeft.Y, TopLeft.Width, TopLeft.Height);
                g.DrawRectangle(Pens.Black, TopRight.X, TopRight.Y, TopRight.Width, TopRight.Height);
                g.DrawRectangle(Pens.Black, BottomLeft.X, BottomLeft.Y, BottomLeft.Width, BottomLeft.Height);
                g.DrawRectangle(Pens.Black, BottomRight.X, BottomRight.Y, BottomRight.Width, BottomRight.Height);
            }
        }
    }
}
