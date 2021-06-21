using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDCollection
{
 public static  class ResizeImage
    {
        /// <summary>
        /// Return ResizedRectangle 
        /// </summary>
        /// <param name="srcrectanle">source rectangle of image</param>
        /// <param name="initSize">initial size of the page to draw image</param>
        /// <returns></returns>
        public static SizeF getResizedRectangle(RectangleF srcrectanle, SizeF initSize)
        {
            float sw = srcrectanle.Width;
            float sh = srcrectanle.Height;
            float dw = initSize.Width;
            float dh = initSize.Height;
            float finalHeight, finalWidth;
            float Sourceratio = sw / sh;

            if (Sourceratio >= 1)
            {
                finalWidth = (int)dw;
                float ratio = sw / dw;
                finalHeight = (sh / ratio);
            }
            else
            {
                finalHeight = (int)dh;
                float ratio = sh / dh;
                finalWidth = (sw / ratio);
            }
            return new SizeF(finalHeight, finalHeight);


        }
    }
}
