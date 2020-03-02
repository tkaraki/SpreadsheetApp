using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spreadsheet_Engine
{
    using System;
    using System.ComponentModel;

    public class SpreadsheetEngine
    {
        protected Cell[,] cellArray;
        private readonly int rowCount = 0;
        private readonly int columnCount = 0;

        public event PropertyChangedEventHandler CellPropertyChanged;

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
            if (e.PropertyName == "Text")
            {
                if (((Cell)sender).Text.StartsWith("="))
                {
                    char columnLetter = ((Cell)sender).Text[1];
                    int columnIndex = char.ToUpper(columnLetter) - 65;
                    int rowIndex = int.Parse(((Cell)sender).Text.Substring(2)) - 1;
                    ((Cell)sender).Value = this.GetCell(rowIndex, columnIndex).Value;
                }
                else
                {
                    ((Cell)sender).Value = ((Cell)sender).Text;
                }
            }

            this.CellPropertyChanged?.Invoke(sender, new PropertyChangedEventArgs("Value"));
        }
    }
}


