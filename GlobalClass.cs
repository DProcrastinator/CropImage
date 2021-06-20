using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CropImage
{
  public static  class GlobalClass
    {
        
        public static int xRect = 0;
        public static RectangleF InitialRect;
        public static RectangleF InitialPageRect;
        public static Form ParentForm;
        public static int pageCount = 0;
        public static int X = 0;
        public static Size FormSize;
        public static bool SavingImage { get; internal set; }

        public static void Initialize(Form parentForm)
        {
            FormSize = parentForm.Size;
            ParentForm = parentForm;
            InitialRect = new RectangleF(0, 0, 500, 300);
            ResetInitialPageRect();
        }


        internal static void ResetInitialPageRect()
        {
            InitialPageRect = new RectangleF(ParentForm.Width / 2 - 800 / 2, ParentForm.Height / 2 - 600 / 2, 800, 600);
        }
    }
}
