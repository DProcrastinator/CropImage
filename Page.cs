using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CropImage
{
    class Page
    {


        public   Boundry pageBoundry { get; set; }
        public List<ImageFrame> imageFrames;
        public int SelectedIndex = -1;
        public bool PageSelected = false;
       

        public Brush brush;

        public Page()
        {
            pageBoundry = new Boundry(GlobalClass.InitialPageRect,true);
            imageFrames = new List<ImageFrame>();
           
            brush = new SolidBrush(Color.Black);
        }
        public void AddImage()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = true;
            dialog.Filter = "All Images|*.png;*.jpg;*.jpeg";
            if(dialog.ShowDialog()==DialogResult.OK)
            {
                string[] filenames = dialog.FileNames;
                foreach (string s in filenames)
                {
                    AddImage(s);
                }
            }
        }

        internal void Save()
        {
            SaveImg img = new SaveImg();
            img.ShowDialog();
            if (img.SaveImage)
            {
                Bitmap bit = new Bitmap((int)pageBoundry.rectanle.Width, (int)pageBoundry.rectanle.Height);
                Graphics g = Graphics.FromImage(bit);
                g.TranslateTransform(-pageBoundry.rectanle.X, -pageBoundry.rectanle.Y);
                GlobalClass.SavingImage = true;
                Paint(g);
                GlobalClass.SavingImage = false;
                if (!Directory.Exists(@"D:\SavedImages"))
                    Directory.CreateDirectory(@"D:\SavedImages");

                if(img.ImageFormat == SaveImg.Format.Png)
                bit.Save(@"D:\SavedImages\" + "Image" + DateTime.Now.Ticks + ".png",System.Drawing.Imaging.ImageFormat.Png);

                if (img.ImageFormat == SaveImg.Format.Jpg)
                    bit.Save(@"D:\SavedImages\" + "Image" + DateTime.Now.Ticks + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
            }
        }

        internal void CentralizePage()
        {
            pageBoundry.Centerlize(GlobalClass.ParentForm.ClientRectangle);
        }

        //internal void PaintSelected(Graphics g, int selectedTag)
        //{
        //    imageFrames[selectedTag].Paint(g);
        //}

        public void AddImage(string s)
        {
            imageFrames.Add(new ImageFrame(s));
        }

        Point Initial;
        bool drag = false;
        internal void mouseDown(Point location,MouseButtons button)
        {
            SelectedIndex = -1;
            PageSelected = false;

            for (int i=0;i<imageFrames.Count;i++)
            {
                if (imageFrames[i].MouseDown(location,button))
                    SelectedIndex = i;
            }

            if(SelectedIndex==-1)
            {
                if(pageBoundry.IsMouseInterset(location))
                {
                    PageSelected = true;
                }
            }
            Initial = location;
            drag = true;
        }

        internal void MouseWheel(Point p, int delta)
        {
            if (PageSelected)
            {
               
            }
            else if (SelectedIndex != -1)
            {
                if (delta > 0)
                {
                    imageFrames[SelectedIndex].Zoom(p, 0.4f);
                }
                else
                {
                    imageFrames[SelectedIndex].Zoom(p,-0.4f);
                }
              
            }
        }

        internal Cursor GetCurrentCursor()
        {
            if (PageSelected)
            {
                return pageBoundry.GetCurrentCursor();
            }
            else if (SelectedIndex != -1)
            {
                return imageFrames[SelectedIndex].ClipBroundy.GetCurrentCursor();
            }
            return Cursors.Arrow;
        }

        internal void ParentSizeChange()
        {
            pageBoundry.Centerlize(GlobalClass.ParentForm.ClientRectangle);
            GlobalClass.InitialRect = new RectangleF(pageBoundry.rectanle.X, pageBoundry.rectanle.Y, GlobalClass.InitialRect.Width, GlobalClass.InitialRect.Height);
        }

        internal void mouseMove(Point location,MouseButtons button)
        {
            if (drag)
            {
                if (PageSelected)
                {
                    pageBoundry.Move(location.X - Initial.X, location.Y - Initial.Y);
                 }
                else if(SelectedIndex!=-1)
                {
                    imageFrames[SelectedIndex].Move(location.X - Initial.X, location.Y - Initial.Y, button);
                }

                Initial = location;
            }
           
        }

        internal void mouseUp(Point location)
        {
            drag = false;
        }
        Font font = new Font("Times New Roman", 12.0f);
        public string tagName;
        public Point point;


        public void Paint(Graphics g)
        {
            //  g.SetClip(pageBoundry.rectanle,  System.Drawing.Drawing2D.CombineMode.Intersect);
            //  g.FillRectangle(Brushes.White, pageBoundry.rectanle);
            //   g.FillEllipse(Brushes.Red, pageBoundry.rectanle.X+pageBoundry.rectanle.Width/2-3f,pageBoundry.rectanle.Y+pageBoundry.rectanle.Height/2-3f,6,6);

            for (int i=0;i<imageFrames.Count;i++)
            {
                imageFrames[i].Paint(g);
            }
         //   g.ResetClip();

            if(!GlobalClass.SavingImage)
            pageBoundry.Paint(g);
        }
    }
}
