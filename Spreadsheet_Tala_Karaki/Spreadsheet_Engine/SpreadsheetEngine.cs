/*
*    Name: Tala Karaki
*    Id: 11659015
*/


namespace Spreadsheet_Engine
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;

    public class SpreadsheetEngine
    {
        protected Cell[,] cellArray;
        private Dictionary<string, int> cellLocation = new Dictionary<string, int>();
        private readonly int rowCount = 0;
        private readonly int columnCount = 0;

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        /// <summary>
        /// Initializes a new instance of the <see cref="Spreadsheet"/> class.
        /// Constructor creates a 2D array of Cells with rowCount and columnCount size.
        /// </summary>
        /// <param name="rowC"> Row count.</param>
        /// <param name="colC"> Column count.</param>
        public SpreadsheetEngine(int rowC, int colC)
        {
            this.rowCount = rowC;
            this.columnCount = colC;
            this.cellArray = new Cell[rowC, colC];

            for (int i = 0; i < rowC; i++)
            {
                for (int j = 0; j < colC; j++)
                {
                    this.cellArray[i, j] = new Cell(i, j, string.Empty);
                    this.cellArray[i, j].PropertyChanged += this.OnPropertyChanged;
                }
            }

            // Set up dictionary with cell indexes to find cells faster.
            int k = 0;
            for (int i = 65; i < 91; i++)
            {
                this.cellLocation.Add(((char)i).ToString(), k);
                ++k;
            }
        }

        /// <summary>
        /// Returns the number of rows in cellArray.
        /// </summary>
        public int RowCount
        {
            get
            {
                return this.rowCount;
            }
        }

        /// <summary>
        /// Returns the number of columns in cellArray.
        /// </summary>
        public int ColumnCount
        {
            get
            {
                return this.columnCount;
            }
        }
        
        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Cell cell = sender as Cell;
            if (cell.Text[0] != '=')
            {
                cell.Value = cell.Text;
                this.PropertyChanged(cell, new PropertyChangedEventArgs("Text"));

            }

           else if (e.PropertyName == "Value")
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(cell.RowIndex.ToString() + "," + cell.ColumnIndex.ToString() + "," + cell.Value));
                return;
            }

            else
            {
                cell.NewExpression(cell.Text.Substring(1));
                foreach (KeyValuePair<string, double> index in cell.varNames.ToList())
                {
                    cell.SubscribeTreeToCell(this.GetCellFromString(index.Key));
                }

                cell.Value = cell.ComputeExpression();
                this.PropertyChanged(this, new PropertyChangedEventArgs(cell.RowIndex.ToString() + "," + cell.ColumnIndex.ToString() + "," + cell.Value));
            }
        }

        /// <summary>
        /// Returns the cell from a string containing indexes.
        /// </summary>
        /// <param name="indexes"> string indexes.</param>
        public Cell GetCellFromString(string indexes)
        {
            int col = this.cellLocation[indexes[0].ToString()];
            int row = Convert.ToInt32(indexes.Substring(1)) - 1;
            return this.GetCell(row, col);
        }

        /// <summary>
        /// Returns the Cell at rowIndex and columnIndex in cellArray.
        /// Returns Null if indexes are out of bounds.
        /// </summary>
        /// <param name="rowIndex"> Row count.</param>
        /// <param name="columnIndex"> Column count.</param>
        public Cell GetCell(int rowIndex, int columnIndex)
        {
            if (rowIndex > this.rowCount || rowIndex < 0 ||
                columnIndex > this.columnCount || columnIndex < 0)
            {
                return null;
            }
            else
            {
                return this.cellArray[rowIndex, columnIndex];
            }
        }

    }
}


