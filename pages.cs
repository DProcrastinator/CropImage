using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CropImage
{
   public  class pages
    {
       
        List<Page> listofPage;
        Brush brush;
        List<Rectangle> tagRectangles;
        Rectangle tagrectangle { get; set; }
        Font font;
   
        public Rectangle rectangle { get; set; }
        public int selectedTag = -1;
        public pages()
        {
            listofPage = new List<Page>();
            
            tagRectangles = new List<Rectangle>();
            font = new Font("hello world",10.2f);
            brush = new SolidBrush(Color.Black);
        }



        public void addPage()
        {
            listofPage.Add(new Page());
            selectedTag = listofPage.Count - 1;
        }

        internal void AddImage()
        {
            //page.AddImage();
            if (selectedTag != -1)
                listofPage[selectedTag].AddImage();
        }

        internal void Save()
        {
            //page.Save();
            if (selectedTag != -1)
                listofPage[selectedTag].Save();
        }

        internal void Paint(Graphics graphics)
        {

            drawtag(graphics);
            if (selectedTag != -1)
            {
                listofPage[selectedTag].Paint(graphics);
            }
        }

        internal void CentralizePage()
        {
            for(int i=0;i<listofPage.Count;i++)
            listofPage[i].CentralizePage();
        }

       
        void drawtag(Graphics g)
        {
            
            for (int i = 0; i < listofPage.Count; i++)
            {
                g.DrawRectangle(new Pen(Color.Black), tagRectangles[i]);
                if (selectedTag == i)
                    g.FillRectangle(new SolidBrush(Color.White), tagRectangles[i]);
                g.DrawString("page" + (i + 1), font, brush, tagRectangles[i]);

            }
        }


       public  void moveTagForwad()
        {
            if (tagRectangles[tagRectangles.Count-1].X > GlobalClass.FormSize.Width)
            {
                for (int i = 0; i < tagRectangles.Count; i++)
                {
                    tagRectangles[i] = new Rectangle(tagRectangles[i].X + 5, tagRectangles[i].Y,
                        tagRectangles[i].Width, tagRectangles[i].Height); 
                    

                }
            }
           

        }
        public void moveTagBackward()
        {
            //if (tagRectangles[tagRectangles.Count - 1].X <= GlobalClass.FormSize.Width)
            //{
                for (int i = 0; i < tagRectangles.Count; i++)
                {
                    tagRectangles[i] = new Rectangle(tagRectangles[i].X - 5, tagRectangles[i].Y,
                        tagRectangles[i].Width, tagRectangles[i].Height); ;

                }

          //  }
          

        }
        internal void mouseDown(Point location, MouseButtons button)
        {
            //  page[selectedTag].mouseDown(location,button);
            if (selectedTag !=-1)
            {
                listofPage[selectedTag].mouseDown(location, button);
               
            }

            tagDown(location);

        }
        Rectangle mouseRect;
       
        private void tagDown(Point location)
        {
            mouseRect = new Rectangle(location,new Size(1,1));
            for (int i =0; i< tagRectangles.Count; i++)
            {
                if ( mouseRect.IntersectsWith(tagRectangles[i]))
                {
                    selectedTag = i;
                    
                }
            }
        }
        

        internal Cursor GetCurrentCursor()
        {
            
                return listofPage[selectedTag].GetCurrentCursor(); 
        }

        internal void MouseWheel(Point location, int delta)
        {
            //page.MouseWheel(location,delta);
            mouseRect = new Rectangle(location, new Size(1, 1));
            for (int i=0;  i< tagRectangles.Count; i++)
            {
                if (tagRectangles[i].IntersectsWith(mouseRect))
                {
                    if (delta > 0 )
                    {
                        moveTagForwad();
                    }
                    else
                    {
                        moveTagBackward();
                    }
                }
            }
            
            if (selectedTag != -1)
            {
                listofPage[selectedTag].MouseWheel(location, delta);
            }
            
        }
        internal void mouseMove(Point location, MouseButtons button)
        {
            // page.mouseMove(location,button);
            if(selectedTag != -1)
            listofPage[selectedTag].mouseMove(location,button);
        }

        internal void mouseUp(Point location)
        {
            // page.mouseUp(location);
            if (selectedTag != -1)
                listofPage[selectedTag].mouseUp(location);
        }

        internal void ParentSizeChange()
        {
            //  page.ParentSizeChange();
            if (selectedTag != -1)
                listofPage[selectedTag].ParentSizeChange();
        }

        internal void createtag()
        {
            tagrectangle = new Rectangle(tagRectangles[tagRectangles.Count - 1].X + 100, 3, 100, 25);
            tagRectangles.Add(tagrectangle);
           

        }
      public void createtag(int x)
        {
            tagrectangle = new Rectangle(x, 3, 100, 25);
            tagRectangles.Add(tagrectangle);
        }
       
    }

}
