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

        private Stack<UndoRedoChange> Undos = new Stack<UndoRedoChange>();
        private Stack<UndoRedoChange> Redos = new Stack<UndoRedoChange>();
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
                    this.cellArray[i, j].PropertyChanged += this.CellPropertyChanged;
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
        
        public void CellPropertyChanged(object sender, EventArgs e)
        {
            Cell cell = sender as Cell;
            PropertyChangedEventArgs E = e as PropertyChangedEventArgs;
            
            if (E.PropertyName == "Value")
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(cell.RowIndex.ToString() + "," + cell.ColumnIndex.ToString() + "," + cell.Value));
                return;
            }

            if (E.PropertyName == "Color")
            {
                this.PropertyChanged("Color", new PropertyChangedEventArgs(cell.RowIndex.ToString() + "," + cell.ColumnIndex.ToString() + "," + cell.BGColor));
                return;
            }

            if (cell.Text.Length == 0 || cell.Text[0] != '=')
            {
                cell.Value = cell.Text;
                this.PropertyChanged(cell, new PropertyChangedEventArgs("Text"));

                if (cell.varNames.Count > 0)
                {
                    foreach (KeyValuePair<string, double> indexName in cell.varNames.ToList())
                    {
                        cell.UnSubscribeTreeToCell(this.GetCellFromString(indexName.Key));
                    }
                }

                this.PropertyChanged(cell, new PropertyChangedEventArgs("Text"));

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
        
        /// <summary>
        /// Push a new change to the undo stack everytime a cell property changes.
        /// </summary>
        /// <param name="change"> UndoRedo Change.</param>
        public void AddUndo(UndoRedoChange change)
        {
            this.Undos.Push(change);
            this.PropertyChanged(change, new PropertyChangedEventArgs("Undo"));
        }
        
        /// <summary>
        /// Perform an undo change in the spreadsheet.
        /// </summary>
        public void Undo()
        {
            this.Undos.Peek().UnExecute();
            this.Redos.Push(this.Undos.Pop());
            this.PropertyChanged(this.Redos.Peek(), new PropertyChangedEventArgs("Redo"));

            if (this.Undos.Count > 0)
            {
                this.PropertyChanged(this.Redos.Peek(), new PropertyChangedEventArgs("Undo"));
            }
            else
            {
                this.PropertyChanged(this.Redos.Peek(), new PropertyChangedEventArgs("!Undo"));
            }
        }

        /// <summary>
        /// Perform a redo change in the spreadsheet.
        /// </summary>
        public void Redo()
        {
            this.Redos.Peek().Execute();
            this.Undos.Push(this.Redos.Pop());
            this.PropertyChanged(this.Undos.Peek(), new PropertyChangedEventArgs("Undo"));

            if (this.Redos.Count > 0)
            {
                this.PropertyChanged(this.Undos.Peek(), new PropertyChangedEventArgs("Redo"));
            }
            else
            {
                this.PropertyChanged(this.Undos.Peek(), new PropertyChangedEventArgs("!Redo"));
            }
        }

    }
}


