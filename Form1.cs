using DXFReaderNET;
using DXFReaderNET.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DXFImage
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.DefaultExt = "DXF";
            openFileDialog1.Filter = "DXF|*.dxf";
            openFileDialog1.FileName = "";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                dxfReaderNETControl1.ReadDXF(openFileDialog1.FileName);

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dxfReaderNETControl1.ZoomCenter();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DXFReaderNETControl _dxfControl;

            _dxfControl = new DXFReaderNETControl();

            _dxfControl.ReadDXF(dxfReaderNETControl1.FileName);
            
            _dxfControl.Width = 2000;
            _dxfControl.Height = 2000;

            _dxfControl.AntiAlias = false;
            _dxfControl.ShowAxes = false;
            _dxfControl.ShowGrid = false;
            _dxfControl.BackColor = System.Drawing.Color.White;
            _dxfControl.ForeColor = System.Drawing.Color.Black;


            _dxfControl.ZoomCenter();
            pictureBox1.Image = _dxfControl.Image;

            _dxfControl.Dispose();
        }

        private void button4_Click(object sender, EventArgs e)
        {
          
            DXFReaderNETControl _dxfControl;

            _dxfControl = new DXFReaderNETControl();

            _dxfControl.ReadDXF(dxfReaderNETControl1.FileName);

            _dxfControl.Width = 2000;
            _dxfControl.Height = 2000;

            _dxfControl.AntiAlias = false;
            _dxfControl.ShowAxes = false;
            _dxfControl.ShowGrid = false;
            _dxfControl.BackColor = System.Drawing.Color.White;
            _dxfControl.ForeColor = System.Drawing.Color.Black;
            foreach (EntityObject entity in _dxfControl.DXF.Entities)
            {
                entity.Color = AciColor.FromCadIndex(7);
            }

            foreach (DXFReaderNET.Blocks.Block _block in _dxfControl.DXF.Blocks.Items)
            {
                foreach (EntityObject ent in _block.Entities)
                {
                    ent.Color = AciColor.FromCadIndex(7);
                }
            }

            _dxfControl.ZoomCenter();
            pictureBox1.Image = _dxfControl.Image;

            _dxfControl.Dispose();

        }

        private void button6_Click(object sender, EventArgs e)
        {
            saveFileDialog1.DefaultExt = "jpg";
            saveFileDialog1.Filter = "JPEG (*.jpg)|*.jpg|PNG (*.png)|*.png|BMP (*.bmp)|*.bmp";
            saveFileDialog1.FileName = "";
            saveFileDialog1.FilterIndex = 1;
            saveFileDialog1.Title = "Export file";

           

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
               
                switch (saveFileDialog1.FilterIndex)
                {
                    case 1:
                        dxfReaderNETControl1.Image.Save(saveFileDialog1.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                        break;
                    case 2:
                        dxfReaderNETControl1.Image.Save(saveFileDialog1.FileName, System.Drawing.Imaging.ImageFormat.Png);
                        break;
                    case 3:
                        dxfReaderNETControl1.Image.Save(saveFileDialog1.FileName, System.Drawing.Imaging.ImageFormat.Bmp);
                        break;

                }

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DXFReaderNETControl _dxfControl;

            _dxfControl = new DXFReaderNETControl();

            _dxfControl.ReadDXF(dxfReaderNETControl1.FileName);

            _dxfControl.Width = 2000;
            _dxfControl.Height = 2000;

            _dxfControl.AntiAlias = false;
            _dxfControl.ShowAxes = false;
            _dxfControl.ShowGrid = false;
            _dxfControl.BackColor = System.Drawing.Color.White;
            _dxfControl.ForeColor = System.Drawing.Color.Black;
            foreach (EntityObject entity in _dxfControl.DXF.Entities)
            {
                Color color = entity.Color.ToColor();
                if (entity.Color == AciColor.ByLayer)
                {
                    color = _dxfControl.DXF.Layers[entity.Layer.Name].Color.ToColor();
                }
                int grayScale = (int)((color.R * .3) + (color.G * .59) + (color.B * .11));
                Color newColor = Color.FromArgb(grayScale, grayScale, grayScale);
                entity.Color = new AciColor(newColor);
            }

            foreach (DXFReaderNET.Blocks.Block _block in _dxfControl.DXF.Blocks.Items)
            {
                foreach (EntityObject ent in _block.Entities)
                {
                    Color color = ent.Color.ToColor();
                    if (ent.Color == AciColor.ByLayer)
                    {
                        color = _dxfControl.DXF.Layers[ent.Layer.Name].Color.ToColor();
                    }
                    int grayScale = (int)((color.R * .3) + (color.G * .59) + (color.B * .11));
                    Color newColor = Color.FromArgb(grayScale, grayScale, grayScale);
                    ent.Color = new AciColor(newColor);
                }
            }

            _dxfControl.ZoomCenter();
            pictureBox1.Image = _dxfControl.Image;

            _dxfControl.Dispose();
        }
        private Color _GetGrayValue(Color color)
        {
            int grayScale = (int)((color.R * .3) + (color.G * .59) + (color.B * .11));
            Color newColor = Color.FromArgb(grayScale, grayScale, grayScale);
            return newColor;
        }
    }
}
