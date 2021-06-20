using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CropImage
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            int width = this.Size.Width;
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer, true);        
            GlobalClass.Initialize(this);
            brush = new SolidBrush(Color.AntiqueWhite);
                pages = new pages();   
        }
      
       public Brush brush;
       pages pages;



       
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A)
            {
                pages.AddImage();
                this.Invalidate();
            }
            else if (e.KeyCode == Keys.S)
            {
                pages.Save();
              
                

            }
            else if (e.KeyCode == Keys.N)
            {

               
                pages.addPage();
                pages.createtag();
                
                this.Invalidate();
               
            }
            else if(e.KeyCode==Keys.C)
            {
              //  pages.CentralizePage();
            }

            this.Invalidate();
        }
        protected override void OnPaint(PaintEventArgs e)
        {  
            pages.Paint(e.Graphics);
          
        }
        
        protected override void OnMouseDown(MouseEventArgs e)
        {
            
              pages.mouseDown(e.Location,e.Button);
            
             Cursor=   pages.GetCurrentCursor();
            
              this.Invalidate();
        }
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            pages.MouseWheel(e.Location,e.Delta) ;
            this.Invalidate();
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            pages.mouseMove(e.Location,e.Button);
            this.Invalidate();
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            Cursor = Cursors.Arrow;
            pages.mouseUp(e.Location);
            this.Invalidate();
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            pages.ParentSizeChange();
            GlobalClass.ResetInitialPageRect();
            this.Invalidate();
        }
        int x = 0;
        private void Form1_Load(object sender, EventArgs e)
        {
              pages.addPage();
              pages.createtag(x);
            this.Invalidate();
        }
    }
    }

