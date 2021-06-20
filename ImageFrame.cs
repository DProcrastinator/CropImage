using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CropImage
{
 public   class ImageFrame
    {
        public Boundry displayBoundry { get; set; }
        public Boundry sourceBoundry { get;  set; }
     
        public Boundry ClipBroundy { get; set; }
        public Image image { get; set; }


        public enum SelectedRectangle
        {
            DisplayRectangle,
            SourceRectangle,
        }

        SelectedRectangle lastSelectedRect=SelectedRectangle.DisplayRectangle;
        //public ImageFrame(Image img)
        //{
        //    image = img;
        //    SetRectangles();
        //}
        public ImageFrame(String imagelocation)
        {
            image = Image.FromFile(imagelocation);
            SetRectangles();
        }

        private void SetRectangles()
        {
            sourceBoundry = new Boundry(new RectangleF(0, 0, image.Width, image.Height),false);
            displayBoundry = new Boundry(new RectangleF(GlobalClass.InitialRect.Location,
                GetResizedRectangle(sourceBoundry.rectanle, GlobalClass.InitialRect)),false);
            ClipBroundy = new Boundry(displayBoundry.rectanle, true);
        }

        private SizeF GetResizedRectangle(RectangleF srcrectanle, RectangleF initialRect)
        {            
                float sw = srcrectanle.Width;
                float sh = srcrectanle.Height;
                float dw = initialRect.Width;
                float dh = initialRect.Height;
                int finalHeight, finalWidth;
                float Sourceratio = sw / sh;

                if (Sourceratio >= 1)
                {
                    finalWidth = (int)dw;
                    float ratio = sw / dw;
                    finalHeight = (int)(sh / ratio);
                }
                else
                {
                    finalHeight = (int)dh;
                    float ratio = sh / dh;
                    finalWidth = (int)(sw / ratio);
                }
                return new SizeF(finalWidth, finalHeight);
            }

        internal void Zoom(Point p, float zoomFactor)
        {
            if (lastSelectedRect == SelectedRectangle.DisplayRectangle)
            {
                displayBoundry.Zoom(p, zoomFactor);
                ClipBroundy.Zoom(p, zoomFactor);
            } 
            //else if (lastSelectedRect == SelectedRectangle.SourceRectangle)
            //{
            //    sourceBoundry.Zoom(p, zoomFactor);
            //}
        }

        public void Paint(Graphics g)
        {
          
            g.SetClip(ClipBroundy.rectanle);
            g.DrawImage(image, displayBoundry.rectanle, sourceBoundry.rectanle, GraphicsUnit.Pixel);           
            g.ResetClip();
            if(!GlobalClass.SavingImage)
            ClipBroundy.Paint(g);
        }
        internal bool MouseDown(Point location,MouseButtons button)
        {
            if(button==MouseButtons.Left)
            {
                lastSelectedRect = SelectedRectangle.DisplayRectangle;
            }
            else if (button == MouseButtons.Right)
            {
                lastSelectedRect = SelectedRectangle.SourceRectangle;
            }
           return ClipBroundy.IsMouseInterset(location);        
       
        }
        internal void Move(int dx, int dy,MouseButtons button)
        {

            if (button == MouseButtons.Left)
            {
                if (ClipBroundy.SelectedPart == Boundry.Corners.Body)
                {
                    displayBoundry.Move(dx, dy);
                    ClipBroundy.Move(dx, dy);
                }
                else if (ClipBroundy.SelectedPart != Boundry.Corners.None)
                    ClipBroundy.Move(dx, dy);
            }
            else if(button==MouseButtons.Right)
            {
                sourceBoundry.Move(-dx, -dy);
            }
          
           
        }
    }
}
