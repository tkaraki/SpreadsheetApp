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

namespace HW3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }



        /// <summary>
        /// LoadText Function that uses System.IO.TextReader to take a TextReader
        /// object and puts it in the text box of interface.
        /// </summary>
        private void LoadText(TextReader sr)
        {
            textBox1.Clear();

            try
            {
                textBox1.AppendText(sr.ReadToEnd());

            }
            catch (Exception e)
            {
                MessageBox.Show("The process failed: {0}", e.ToString()); // Print Error Message 
            }
        }


        /// <summary>
        /// Load From File Function
        /// Passes a StreamReader to the LoadText Function to load text from a file
        /// </summary>
        private void loadFromFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "txt files (*.txt)|*.txt";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                using (StreamReader sr = new StreamReader(ofd.FileName))
                {
                    LoadText(sr);
                }
            }

        }


        /// <summary>
        /// Save to File Function
        /// Uses a StreamWriter to save text in a .txt file
        /// </summary>
        private void saveToFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.InitialDirectory = @"C:\";
            sfd.RestoreDirectory = true;
            sfd.FileName = "* .txt";
            sfd.DefaultExt = "txt";
            sfd.Filter = "txt files (*.txt)|*.txt";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                Stream fileStream = sfd.OpenFile();
                StreamWriter sw = new StreamWriter(fileStream);

                sw.Write(textBox1.Text);

                sw.Close();
                fileStream.Close();

                textBox1.Clear();

            }

        }

        private void loadFibonacciToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void first50ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void first100ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
