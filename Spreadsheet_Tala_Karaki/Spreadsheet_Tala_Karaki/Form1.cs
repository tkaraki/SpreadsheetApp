/*
*    Name: Tala Karaki
*    Id: 11659015
*/

namespace Spreadsheet_Tala_Karaki
{
    using System;
    using System.ComponentModel;
    using System.Windows.Forms;
    using Spreadsheet_Engine;

    public partial class Form1 : Form
    {
        private SpreadsheetEngine spreadSheet1;

        /// <summary>
        /// Initializes a new instance of the <see cref="SpreadsheetForm"/> class.
        /// </summary>
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles SpreadsheetForm's Load.
        /// </summary>
        private void Form1_Load(object sender, EventArgs e)
        {
            this.spreadSheet1 = new SpreadsheetEngine(50, 26);
            this.spreadSheet1.CellPropertyChanged += this.CellPropertyChanged;
            this.InitializeGrid();
        }

        /// <summary>
        /// Creates and names the rows and columns of the dataGridView.
        /// </summary>
        public void InitializeGrid()
        {
            this.dataGridView1.RowCount = this.spreadSheet1.RowCount;
            this.dataGridView1.ColumnCount = this.spreadSheet1.ColumnCount;

            for (int i = 1; i <= spreadSheet1.RowCount; i++)
            {
                this.dataGridView1.Rows[i - 1].HeaderCell.Value = i.ToString();
            }

            char c = 'A';
            for (int i = 0; i < spreadSheet1.ColumnCount; i++)
            {
                this.dataGridView1.Columns[i].Name = c.ToString();
                c++;
            }
        }

        /// <summary>
        /// Event handler for updating a cell’s value in the DataGridView and SpreadsheetEngine.
        /// </summary>
        private void CellPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Cell cell = (Cell)sender;
            if (e.PropertyName == "Value")
            {
                this.dataGridView1.Rows[cell.RowIndex].Cells[cell.ColumnIndex].Value = cell.Value;
            }
        }


        /// <summary>
        /// Handles what happens when the Demo button is clicked.
        /// </summary>
        private void DemoButton_Click(object sender, EventArgs e)
        {
            this.SpreadsheetDemo();
        }

       
        /// <summary>
        /// Runs a Demo of the spreadsheet form.
        /// </summary>
        private void SpreadsheetDemo()
        {
            // set the text in about 50 random cells
            Random rnd = new Random();
            int rIndex = 0;
            int cIndex = 0;
            for (int i = 0; i < 50; i++)
            {
                rIndex = rnd.Next(0, this.spreadSheet1.RowCount);
                cIndex = rnd.Next(0, this.spreadSheet1.ColumnCount);
                this.spreadSheet1.GetCell(rIndex, cIndex).Text = "Hello World!";
            }

            // set the text in every cell in column B to “This is cell B#”
            for (int i = 0; i < this.spreadSheet1.RowCount; i++)
            {
                this.spreadSheet1.GetCell(i, 1).Text = "This is Cell B" + (i + 1).ToString();
            }

            // set the text in every cell in column A to “=B#”
            for (int i = 0; i < this.spreadSheet1.RowCount; i++)
            {
                spreadSheet1.GetCell(i, 0).Text = "=B" + (i + 1).ToString();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
