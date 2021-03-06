﻿/*
*    Name: Tala Karaki
*    Id: 11659015
*/


namespace Spreadsheet_Engine
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;
    using System.Linq;
    using System.Xml;

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
                    foreach (KeyValuePair<string, double> index in cell.varNames.ToList())
                    {
                        cell.UnSubscribeTreeToCell(this.GetCellFromString(index.Key));
                    }
                }

                this.PropertyChanged(cell, new PropertyChangedEventArgs("Text"));

            }

            else
            {
                if (cell.varNames.Count > 0)
                {
                    foreach (KeyValuePair<string, double> index in cell.varNames.ToList())
                    {
                        cell.UnSubscribeTreeToCell(this.GetCellFromString(index.Key));
                    }
                }

                cell.NewExpression(cell.Text.Substring(1));

                if (this.IsValidReference(cell))
                {
                    foreach (KeyValuePair<string, double> index in cell.varNames.ToList())
                    {
                    cell.SubscribeTreeToCell(this.GetCellFromString(index.Key));
                    }

                    cell.Value = cell.ComputeExpression();
                    this.PropertyChanged(this, new PropertyChangedEventArgs(cell.RowIndex.ToString() + "," + cell.ColumnIndex.ToString() + "," + cell.Value));

                }
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
        /// Returns True if input is a valid reference.
        /// Returns False if there is an invalid reference.
        /// </summary>
        /// <param name="cell"> Spreadsheet Cell.</param>
        private bool IsValidReference(Cell cell)
        {
            // check each cell stored in the cell's variable dictionary.
            foreach (KeyValuePair<string, double> indexName in cell.varNames.ToList())
            {
                // if this cell's dictionary contains a cell with the same index then it is a self reference
                if (indexName.Key == cell.IndexName)
                {
                    cell.Value = "!(self reference)";
                    return false;
                }

                // if this cell's indexes dont pass the isCell function then it is not a reference
                else if (!this.IsCell(indexName.Key))
                {
                    cell.Value = "!(bad reference)";
                    return false;
                }

                // if this cell's dictionary contains a cell with a dictionary that references it, then it is a circular reference.
                else if (this.IsCircularReference(cell.IndexName, indexName.Key))
                {
                    cell.Value = "!(circular reference)";
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Returns True if input is a cell.
        /// Returns False if there is not a cell.
        /// </summary>
        /// <param name="index"> Cell index.</param>
        private bool IsCell(string index)
        {
            int rowIndex, columnIndex;
            int.TryParse(index.Substring(1), out rowIndex);
            columnIndex = Convert.ToInt32(index[0]) - 65;

            if (rowIndex > this.rowCount || rowIndex < 0 ||
                columnIndex > this.columnCount || columnIndex < 0)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Returns True if cell has no circular reference.
        /// Returns False if there is a circular reference.
        /// </summary>
        /// <param name="parentCell"> Cell being checked.</param>
        /// <param name="dictCell"> Cell in parent cell's variable dict .</param>
        private bool IsCircularReference(string parentCell, string dictCell)
        {
            Cell cell = this.GetCellFromString(dictCell);

            // check each cell stored in the cell's variable dictionary.
            foreach (KeyValuePair<string, double> indexName in cell.varNames.ToList())
            {
                // if cell is subscribed to its parent then it is a circular reference.
                if (indexName.Key == parentCell)
                {
                    return true;
                }

                // if cell is referencing a cell with a bad reference then it is a circular reference.
                if (this.GetCellFromString(indexName.Key).Value == "!(self reference)")
                {
                    return true;
                }

                return this.IsCircularReference(indexName.Key, parentCell);
            }

            return false;
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

        /// <summary>
        /// Save the spreasheet to file.
        /// </summary>
        public void Save(Stream stream)
        {
            var writerSettings = new XmlWriterSettings()
            {
                Indent = true,
                IndentChars = "    "
            };

            XmlWriter writer = XmlWriter.Create(stream, writerSettings);
            writer.WriteStartDocument();
            writer.WriteStartElement("spreadsheet");

            for (int i = 0; i < this.RowCount; i++)
            {
                for (int j = 0; j < this.ColumnCount; j++)
                {
                    Cell cell = this.GetCell(i, j);
                    if (cell.Text != string.Empty || cell.BGColor != 0xFFFFFFFF)
                    {
                        writer.WriteStartElement("cell");
                        writer.WriteStartElement("name");
                        writer.WriteString(cell.IndexName);
                        writer.WriteEndElement();
                        writer.WriteStartElement("bgcolor");
                        writer.WriteString(cell.BGColor.ToString());
                        writer.WriteEndElement();
                        writer.WriteStartElement("text");
                        writer.WriteString(cell.Text);
                        writer.WriteEndElement();
                        writer.WriteEndElement();
                    }
                }
            }

            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Close();
        }
        
        /// <summary>
        /// Load the spreasheet from file.
        /// </summary>
        public void Load(Stream stream)
        {
            var readerSettings = new XmlReaderSettings()
            {
                IgnoreWhitespace = true,
            };

            XmlReader reader = XmlReader.Create(stream, readerSettings);

            string temp;
            Cell cell;

            reader.ReadStartElement("spreadsheet");

            while (reader.Name == "cell")
            {
                reader.ReadStartElement("cell");
                reader.ReadStartElement("name");
                temp = reader.ReadContentAsString();
                cell = this.GetCellFromString(temp);
                reader.ReadEndElement();
                reader.ReadStartElement("bgcolor");
                temp = reader.ReadContentAsString();
                uint.TryParse(temp, out uint result);
                cell.BGColor = result;
                reader.ReadEndElement();
                reader.ReadStartElement("text");
                temp = reader.ReadContentAsString();
                cell.Text = temp;
                reader.ReadEndElement();
                reader.ReadEndElement();
            }

            reader.ReadEndElement();
        }

    }

}



