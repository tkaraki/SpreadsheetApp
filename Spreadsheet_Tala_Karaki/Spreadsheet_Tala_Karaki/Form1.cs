/*
*    Name: Tala Karaki
*    Id: 11659015
*/

namespace Spreadsheet_Tala_Karaki
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
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
            this.InitializeGrid();
            this.spreadSheet1.PropertyChanged += this.CellPropertyChanged;
            this.dataGridView1.CellBeginEdit += new DataGridViewCellCancelEventHandler(this.CellBeginEdit);
            this.dataGridView1.CellEndEdit += new DataGridViewCellEventHandler(this.CellEndEdit);
            this.undoToolStripMenuItem.Enabled = false;
            this.redoToolStripMenuItem.Enabled = false;
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

            this.dataGridView1.RowHeadersWidth = 60;
        }

        /// <summary>
        /// Event handler for updating a cell’s value in the DataGridView and SpreadsheetEngine.
        /// </summary>
        private void CellPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (sender.GetType() == typeof(SpreadsheetEngine))
            {
                string temp = e.PropertyName;
                string[] values = temp.Split(','); 
                this.dataGridView1.Rows[Convert.ToInt32(values[0])].Cells[Convert.ToInt32(values[1])].Value = values[2];
            }
            else if (sender == "Color")
            {
                string temp = e.PropertyName;
                string[] values = temp.Split(',');
                if (uint.TryParse(values[2], out uint result))
                {
                    this.dataGridView1.Rows[Convert.ToInt32(values[0])].Cells[Convert.ToInt32(values[1])].Style.BackColor = Color.FromArgb(unchecked((int)result));
                }
            }
            else if (sender is UndoRedoChange)
            {
                string temp = e.PropertyName;

                if (temp == "!Undo")
                {
                    this.undoToolStripMenuItem.Enabled = false;
                }
                else if (temp == "Undo")
                {
                    this.undoToolStripMenuItem.Enabled = true;
                }
                else if (temp == "!Redo")
                {
                    this.redoToolStripMenuItem.Enabled = false;
                }
                else if (temp == "Redo")
                {
                    this.redoToolStripMenuItem.Enabled = true;
                }
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

        private void CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = this.spreadSheet1.GetCell(e.RowIndex, e.ColumnIndex).Text;
        }

        private void CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            string val = this.spreadSheet1.GetCell(e.RowIndex, e.ColumnIndex).Value;

            if (val.Length > 0)
            {
                this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = val;
            }
        }

        private void CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != -1)
            {
                if (this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null)
                {
                    Cell cell = this.spreadSheet1.GetCell(e.RowIndex, e.ColumnIndex);
                    TextChange command = new TextChange(cell, cell.Text, String.Empty);
                    this.spreadSheet1.AddUndo(command);
                    this.spreadSheet1.GetCell(e.RowIndex, e.ColumnIndex).Text = String.Empty;
                }

                else if (this.spreadSheet1.GetCell(e.RowIndex, e.ColumnIndex).Value != this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString())
                {
                    Cell cell = this.spreadSheet1.GetCell(e.RowIndex, e.ColumnIndex);
                    TextChange command = new TextChange(cell, cell.Text, this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                    this.spreadSheet1.AddUndo(command);
                    this.spreadSheet1.GetCell(e.RowIndex, e.ColumnIndex).Text = this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                }
            }
        }

        private void changeBackgroundColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<Cell> cells = new List<Cell>();
            List<uint> oldColors = new List<uint>();
            ColorDialog myDialog = new ColorDialog();
            
            myDialog.AllowFullOpen = true;
            myDialog.ShowHelp = true;
            
            if (myDialog.ShowDialog() == DialogResult.OK)
            {
                foreach (DataGridViewTextBoxCell cell in this.dataGridView1.SelectedCells)
                {
                    cells.Add(this.spreadSheet1.GetCell(cell.RowIndex, cell.ColumnIndex));
                    oldColors.Add(this.spreadSheet1.GetCell(cell.RowIndex, cell.ColumnIndex).BGColor);
                    this.spreadSheet1.GetCell(cell.RowIndex, cell.ColumnIndex).BGColor = this.ColorToUInt(myDialog.Color);
                }

                ColorChange command = new ColorChange(cells, oldColors, this.ColorToUInt(myDialog.Color));
                this.spreadSheet1.AddUndo(command);
            }
        }

        private uint ColorToUInt(Color color)
        {
            return (uint)((color.A << 24) | (color.R << 16) |
                          (color.G << 8) | (color.B << 0));
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.spreadSheet1.Undo();
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.spreadSheet1.Redo();
        }
    }
}
