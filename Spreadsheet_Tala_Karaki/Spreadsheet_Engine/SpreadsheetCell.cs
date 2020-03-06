/*
*    Name: Tala Karaki
*    Id: 11659015
*/


namespace Spreadsheet_Engine
{
    using System.ComponentModel;

    public abstract class SpreadsheetCell : INotifyPropertyChanged
    {
        protected string text = string.Empty;
        protected string value = string.Empty;
        private readonly int rowIndex = 0;
        private readonly int columnIndex = 0;

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        /// <summary>
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
    }

}

