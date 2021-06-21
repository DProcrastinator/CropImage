using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;


namespace MDCollection
{
  public static  class ImageZoom
    {
     /// <summary>
     /// returns the zoomed rectangle to zoom an image
     /// </summary>
     /// <param name="imageRectangle">display rectangle of given image</param>
     /// <param name="mousepoint">current position of mouse</param>
     /// <param name="zoomFactor">positive value to zoom in and negetive value to zoom out </param>
     /// <returns></returns>
        public static RectangleF Zoom(RectangleF imageRectangle, Point mousepoint, float zoomFactor)
        {
            //orginal size and location of image
            SizeF OrginalSize = imageRectangle.Size;
            PointF Orginalpoint = imageRectangle.Location;
            //Mouse cursor location -located in width% and height% of totaloriginal image
            float mouse_widthPercent = System.Math.Abs(Orginalpoint.X - mousepoint.X) / OrginalSize.Width * 100;
            float mouse_heightPercent = System.Math.Abs(Orginalpoint.Y - mousepoint.Y) / OrginalSize.Height * 100;


            //Zoomed Image by scalefactor
            SizeF newSize = new SizeF(
                 OrginalSize.Width + OrginalSize.Width * zoomFactor,
                 OrginalSize.Height + OrginalSize.Height * zoomFactor
                 );

            if (newSize.Width > 50000 || newSize.Height > 50000 || newSize.Width < 10 || newSize.Height < 10)
                return imageRectangle;

            //How much width increases and height increases
            float width_incrasesBy = newSize.Width - OrginalSize.Width;
            float height_incresesBy = newSize.Height - OrginalSize.Height;

            //Adjusting Image location after zooming the image
            PointF newPoint = new PointF(

                  Orginalpoint.X - width_incrasesBy * mouse_widthPercent / 100,

                  Orginalpoint.Y - height_incresesBy * mouse_heightPercent / 100

                  );
           
            return new RectangleF(newPoint, newSize);

        }

        public static void Test()
        {

        }

    }
}
