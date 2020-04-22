/*
*    Name: Tala Karaki
*    Id: 11659015
*/


namespace Spreadsheet_Engine
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    public class SpreadsheetEngine
    {
        protected Cell[,] cellArray;
        private Dictionary<Cell, HashSet<Cell>> dependencyDict;
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
            this.dependencyDict = new Dictionary<Cell, HashSet<Cell>>();
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
                this.ClearDependency((Cell)sender);
                this.Evaluate((Cell)sender);
                this.CellPropertyChanged?.Invoke(sender, new PropertyChangedEventArgs("Value"));
            }

            if (e.PropertyName == "Value")
            {
                this.UpdateDependency((Cell)sender);
                this.CellPropertyChanged?.Invoke(sender, new PropertyChangedEventArgs("Value"));
            }

        }

        /// <summary>
        ///Evaluates the value of a cell.
        /// </summary>
        /// <param name="cell"> cell.</param>
        private void Evaluate(Cell cell)
        {
            if (cell.Text.Length == 0)
            {
                cell.Value = string.Empty;
            }
            else if (cell.Text.StartsWith("="))
            {
                if (cell.Text.Length > 1 && isFormula(cell.Text)) //cell is a formula and will  expression tree
                {
                    CalculateTree(cell);
                }

                else  // cell is only a reference to another cell
                {
                    char columnLetter = cell.Text[1];
                    int columnIndex = char.ToUpper(columnLetter) - 65;
                    int rowIndex = int.Parse(cell.Text.Substring(2)) - 1;
                    cell.Value = this.GetCell(rowIndex, columnIndex).Value;
                }

            }
            else
            {
                cell.Value = cell.Text;
            }
        }

        private void CalculateTree(Cell cell)
        {
            try
            {
                ExpressionTree expTree = new ExpressionTree(cell.Text.Substring(1));
                foreach (string variableName in expTree.Variables.Keys)
                {
                    char columnLetter = variableName[1];
                    int columnIndex = char.ToUpper(columnLetter) - 65;
                    int rowIndex = int.Parse(variableName.Substring(2)) - 1;

                    Cell dependentCell = GetCell(rowIndex, columnIndex);

                    if (!dependencyDict.ContainsKey(cell))
                    {
                        dependencyDict.Add(cell, new HashSet<Cell>());
                    }

                    dependencyDict[cell].Add(dependentCell);

                    bool success = Double.TryParse(dependentCell.Value, out double result);
                    if (success)
                    {
                        expTree.SetVariable(variableName, result);
                    }
                    else
                    {
                        expTree.SetVariable(variableName, 0.0);
                    }

                }

                cell.Value = expTree.Evaluate().ToString();
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        ///Returns true if there is a dependency between 2 cells.
        /// </summary>
        /// <param name="baseCell"> Base cell.</param>
        /// <param name="depCell"> Dependent cell.</param>
        private bool checkDependency(Cell baseCell, Cell depCell)
        {
            if (!dependencyDict.ContainsKey(depCell)) return true;

            bool result = true;
            foreach (Cell ac in dependencyDict[depCell])
            {
                if (ReferenceEquals(ac, baseCell)) return false;
                result = result && checkDependency(baseCell, ac);
            }
            return result;
        }

        /// <summary>
        /// Updates all other cells with a dependency on this cell.
        /// </summary>
        /// <param name="cell"> cell.</param>
        private void UpdateDependency(Cell cell)
        {
            if (checkDependency(cell, cell))
            {
                foreach (Cell key in dependencyDict.Keys)
                {
                    if (dependencyDict[key].Contains(cell))
                    {
                        Evaluate(key);
                    }
                }
            }
        }

        /// <summary>
        /// Clear cell dependencies from dependency dict.
        /// </summary>
        /// <param name="cell"> cell.</param>
        private void ClearDependency(Cell cell)
        {
            if (dependencyDict.ContainsKey(cell))
            {
                dependencyDict[cell].Clear();
            }

            dependencyDict.Remove(cell);
        }

        /// <summary>
        /// Returns true if string is a formula containg math symbols.
        /// Returns false if not formula.
        /// </summary>
        /// <param name="text"> cell text.</param>
        private bool isFormula(string text)
        {
            if (text.Contains("+") || text.Contains("-") || text.Contains("/") || text.Contains("*"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}


