using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace MergingGraphics
{
    public partial class Form1 : Form
    {

        int PValueFirstImage = 0;
        int PValueSecondImage = 0;
        int UThreads = 1;
        Image image1;
        Image image2;
        Image output;
        ImageConvert Converter = new ImageConvert();

        OpenFileDialog openFileDialogX1 = new OpenFileDialog();
        OpenFileDialog openFileDialogX2 = new OpenFileDialog();

        public Form1()
        {
            InitializeComponent();
            UThreads = Environment.ProcessorCount;
            trackBar1.Value = UThreads;
            label7.Text = "" + UThreads;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            UThreads = trackBar1.Value;

            label7.Text = "" + UThreads;
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
           PValueFirstImage = trackBar2.Value;
           PValueSecondImage = 100-trackBar2.Value;

            label4.Text = ""+PValueFirstImage+"%";
            label6.Text = ""+PValueSecondImage+"%";


        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialogX1.ShowDialog();
            image1 = Image.FromFile(openFileDialogX1.FileName);
            pictureBox1.Image = image1;
            button2.Enabled = true;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            openFileDialogX2.ShowDialog();
            image2 = Image.FromFile(openFileDialogX2.FileName);
            pictureBox2.Image = image2;
            button3.Enabled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            pictureBox3.Image = null;
            if(output != null)
            output.Dispose();
            
            Bitmap image3 = new Bitmap(image2.Width , image2.Height) ;
            byte[] im1 = new byte[image2.Width * image2.Height*3];
            byte[] im2 = new byte[image2.Width * image2.Height*3];
            byte[] modified = new byte[image2.Width * image2.Height*3];


            im1 = Converter.imageToByteArray(image1, image2.Width, image2.Height);
            im2 = Converter.imageToByteArray(image2, image2.Width, image2.Height);
            if (radioButton1.Checked)
            {
                modified = Converter.cppmodify(im1, im2, image2.Width, image2.Height, PValueFirstImage, UThreads);
                image3 = Converter.byteArrayToImage(modified, image2.Width, image2.Height);
            }
            else if (radioButton2.Checked)
            {
                modified = Converter.asmmodify(im1, im2, image2.Width, image2.Height, PValueFirstImage, UThreads);
                image3 = Converter.byteArrayToImage(modified, image2.Width, image2.Height);
            }

            
            output = Image.FromFile(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\obrazki\\wyjscie.png");
            pictureBox3.Image = output;
        }
    }
}

