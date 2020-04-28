using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spreadsheet_Engine
{
    public abstract class UndoRedoChange
    {
        public string ButtonMessage;
    }

    public class TextChange : UndoRedoChange
    {
        private string oldText;
        private string newText;
        private Cell cell;
        
        public TextChange(Cell cell, string oldText, string newText)
        {
            this.cell = cell;
            this.oldText = oldText;
            this.newText = newText;
        }
        
        public void Do()
        {
            this.cell.Text = this.newText;
        }
        
        public void Undo()
        {
            this.cell.Text = this.oldText;
        }
    }

    public class ColorChange : UndoRedoChange
    {
        private uint newColor;
        private List<uint> oldColor;
        private List<Cell> cells;

        public ColorChange(List<Cell> cells, List<uint> oldColor, uint newColor)
        {
            this.oldColor = oldColor;
            this.newColor = newColor;
            this.cells = cells;
        }

        public void Do()
        {
            foreach (Cell cell in this.cells)
            {
                cell.BGColor = this.newColor;
            }
        }

        public void Undo()
        {
            for (int i = 0; i < this.cells.Count; i++)
            {
                this.cells[i].BGColor = this.oldColor[i];
            }
        }
    }
}
