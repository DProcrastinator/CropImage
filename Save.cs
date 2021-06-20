using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CropImage
{
    public partial class SaveImg : Form
    {
        public SaveImg()
        {
            InitializeComponent();
        }

        public bool SaveImage = false;
        public enum Format
        {
            Jpg,
            Png
        }

        public Format ImageFormat;
        private void button1_Click(object sender, EventArgs e)
        {
            SaveImage = true;
            this.Dispose();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox1.SelectedItem.ToString()=="Jpg")
            {
                ImageFormat = Format.Jpg;
            }
            if (comboBox1.SelectedItem.ToString() == "Png")
            {
                ImageFormat = Format.Png;

            }
        }
    }
}
