/*
*    Name: Tala Karaki
*    Id: 11659015
*/


namespace Spreadsheet_Engine
{
    using System.ComponentModel;
    using System.Collections.Generic;

    public abstract class SpreadsheetCell : INotifyPropertyChanged
    {
        protected string text = string.Empty;
        protected string value = string.Empty;
        private readonly int rowIndex = 0;
        private readonly int columnIndex = 0;

        protected ExpressionTree tree;
        private Dictionary<int, string> cellLocation = new Dictionary<int, string>();
        public Dictionary<string, double> varNames = new Dictionary<string, double>();

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        /// <summary>
        /// Initializes a new instance of the <see cref="SpreadsheetCell"/> class.
        /// Constructor for Abstract SpreadsheetCell class.
        /// </summary>
        /// <param name="rIndex">cell row index</param>
        /// <param name="cIndex"> cell column index</param>
        /// <param name="text"> cell text contents</param>
        public SpreadsheetCell(int rIndex, int cIndex, string text)
        {
            this.rowIndex = rIndex;
            this.columnIndex = cIndex;
            this.text = text;
            this.value = text;

            // Set up dictionary with cell indexes to find cells faster.
            int k = 0;
            for (int i = 65; i < 91; i++)
            {
                this.cellLocation.Add(k, ((char)i).ToString());
                ++k;
            }
        }


        public int RowIndex
        {
            get
            {
                return this.rowIndex;
            }
        }

        public int ColumnIndex
        {
            get
            {
                return this.columnIndex;
            }
        }

        public string IndexName
        {
            get
            {
                return this.cellLocation[this.ColumnIndex].ToString() + (this.RowIndex + 1).ToString();
            }
        }

        public string Text
        {
            get
            {
                return this.text;
            }

            set
            {
                if (value != this.text)
                {
                    this.text = value;
                    this.PropertyChanged(this, new PropertyChangedEventArgs("Text"));
                }
            }
        }

        public string Value
        {
            get
            {
                return this.value;
            }

            protected internal set
            {
                if (value != this.value)
                {
                    this.value = value;
                    this.PropertyChanged(this, new PropertyChangedEventArgs("Value"));
                }
            }
        }
    }

    // Cell Class inherited from SpreadSheet to be instantiated in Spreadsheet
    public class Cell : SpreadsheetCell
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Cell"/> class.
        /// Constructor for inherited Cell class.
        /// </summary>
        /// <param name="rIndex">cell row index.</param>
        /// <param name="cIndex"> cell column index.</param>
        /// <param name="text"> cell text contents.</param>
        public Cell(int rindex, int cindex, string name)
                : base(rindex, cindex, name)
        {
        }

        public void SubscribeTreeToCell(Cell cell)
        {
            this.tree.SubscribeToCell(cell);
        }

        public void NewExpression(string exp)
        {
            this.tree = new ExpressionTree(exp);
            this.varNames = this.tree.Variables;
            this.tree.parent = this;
        }

        public string ComputeExpression()
        {
            return this.tree.Evaluate().ToString();
        }
    }
}