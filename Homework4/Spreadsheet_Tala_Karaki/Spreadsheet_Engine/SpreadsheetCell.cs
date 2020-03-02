using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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


        }
    }
