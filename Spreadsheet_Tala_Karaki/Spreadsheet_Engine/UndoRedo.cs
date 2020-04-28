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
        public abstract void Execute();
        public abstract void UnExecute();
    }

    public class TextChange : UndoRedoChange
    {
        public string ButtonMessage = " Text Change ";
        private string oldText;
        private string newText;
        private Cell cell;
        
        public TextChange(Cell cell, string oldText, string newText)
        {
            this.cell = cell;
            this.oldText = oldText;
            this.newText = newText;
        }
        
        public override void Execute()
        {
            this.cell.Text = this.newText;
        }
        
        public override void UnExecute()
        {
            this.cell.Text = this.oldText;
        }
    }

    public class ColorChange : UndoRedoChange
    {

        public string ButtonMessage = " Color Change ";
        private uint newColor;
        private List<uint> oldColor;
        private List<Cell> cells;

        public ColorChange(List<Cell> cells, List<uint> oldColor, uint newColor)
        {
            this.oldColor = oldColor;
            this.newColor = newColor;
            this.cells = cells;
        }

        public override void Execute()
        {
            foreach (Cell cell in this.cells)
            {
                cell.BGColor = this.newColor;
            }
        }

        public override void UnExecute()
        {
            for (int i = 0; i < this.cells.Count; i++)
            {
                this.cells[i].BGColor = this.oldColor[i];
            }
        }
    }
}
