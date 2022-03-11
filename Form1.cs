using Emgu.CV;
using Emgu.CV.OCR;
using Emgu.CV.Structure;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace RecogTxt
{
    public partial class Form1 : Form
    {
        private string filePath = string.Empty;
        private string lang = string.Empty;
        public Form1()
        {
            InitializeComponent();
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult res = openFileDialog1.ShowDialog();
            if (res == DialogResult.OK)
            {
                filePath = openFileDialog1.FileName;
                pictureBox1.Image = Image.FromFile(filePath);
            }
            else
            {
                MessageBox.Show("Картинка не выбрана", "Необходимо выбрать картинку", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(filePath) || String.IsNullOrWhiteSpace(filePath))
                {
                    throw new Exception("Картинка не выбрана");
                }
                else if (toolStripComboBox1.SelectedItem == null)
                {
                    throw new Exception("Язык не выбран");
                }
                else
                {
                    Tesseract tesseract = new Tesseract(@"C:\Users\SP\Documents\testdata", lang, OcrEngineMode.TesseractLstmCombined);
                    tesseract.SetImage(new Image<Bgr, byte>(filePath));
                    tesseract.Recognize();
                    richTextBox1.Text = tesseract.GetUTF8Text();
                    tesseract.Dispose();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (toolStripComboBox1.SelectedIndex == 0)
                lang = "rus";
            if (toolStripComboBox1.SelectedIndex == 1)
                lang = "eng";
        }


    }
}
