using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Spreadsheet_Tala_Karaki
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            this.dataGridView1.ColumnCount = 26;

             char c = 'A';
            for (int i = 0; i < 26; i++)
            {
                this.dataGridView1.Columns[i].Name = c.ToString();
                c++;
            }

            this.dataGridView1.RowCount = 50;
            for (int i = 1; i <= 50; i++)
            {
                this.dataGridView1.Rows[i - 1].HeaderCell.Value = i.ToString();
            }

           


        }
    }
}
