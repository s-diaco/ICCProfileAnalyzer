using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ColorProfileInfo;

namespace ICCProfileAnalyzer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                label1.Text = "";
                label2.Text = "";
                System.IO.StreamReader sr = new
                   System.IO.StreamReader(openFileDialog1.FileName);
                ImageAnalizer IA = new ImageAnalizer();
                label1.Text = IA.GetChannelsCount(sr);
                label2.Text = IA.GetChannelIds(sr);
                sr.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (var bitmap = new Aurigma.GraphicsMill.Bitmap(@"Images\cmyk.tif"))
            {
                label1.Text = "";
                label1.Text = bitmap.Channels.Count.ToString() + " Channels";
                label2.Text = "";
                for (int i = 0; i < bitmap.Channels.Count; i++)
                {
                    label2.Text += bitmap.Channels[i].Id + ",";
                }
                var cyanBitmap = bitmap.Channels[Aurigma.GraphicsMill.Channel.Cyan];
                cyanBitmap.Save(@"Images\Output\Cmyk_test_Channel_C.tif");

                var alphaBitmap = bitmap.Channels[Aurigma.GraphicsMill.Channel.Yellow];
                alphaBitmap.Save(@"Images\Output\Cmyk_test_Channel_A.tif");
            }
        }
    }
}
