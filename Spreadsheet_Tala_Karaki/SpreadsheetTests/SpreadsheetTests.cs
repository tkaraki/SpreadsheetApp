using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpreadsheetTests
{
    
    public class SpreadsheetTests
    {
        private Spreadsheet_Engine.SpreadsheetEngine spreadsheet;
        private Spreadsheet_Engine.Cell A1;
        private Spreadsheet_Engine.Cell B1;
        private Spreadsheet_Engine.Cell C1;
        private Spreadsheet_Engine.Cell D1;

        [SetUp]
        public void Setup()
        {
            spreadsheet = new Spreadsheet_Engine.SpreadsheetEngine(5, 5);
            A1 = (Spreadsheet_Engine.Cell)spreadsheet.GetCell(0, 0);
            B1 = (Spreadsheet_Engine.Cell)spreadsheet.GetCell(0, 1);
            C1 = (Spreadsheet_Engine.Cell)spreadsheet.GetCell(0, 2);
            D1 = (Spreadsheet_Engine.Cell)spreadsheet.GetCell(0, 3);
        }

        [Test]
        public void TestCellDependency()
        {
            //test constant values
            A1.Text = "10";
            Assert.AreEqual(A1.Text, A1.Value);

            //test expression tree
            D1.Text = "=10+6";
            Assert.AreEqual(D1.Value, "16");

            //test cell dependency
            C1.Text = "=D1";
            Assert.AreEqual(D1.Value, C1.Value);

            //test cell dependency with expression tree
            C1.Text = "=D1+4";
            Assert.AreEqual("20", C1.Value);

            // test more cell dependencies
            B1.Text = "=C1+D1";
            Assert.AreEqual("36", B1.Value);


            // test cell dependency update
            D1.Text = "10";
            Assert.AreEqual("14", C1.Value);
            Assert.AreEqual("24", B1.Value);
        }

        [Test]
        public void TestUndoRedo()
        {
            A1.Text = "10";
            Assert.AreEqual(A1.Text, A1.Value);
            
            B1.Text = "=10+6";
            Assert.AreEqual(B1.Value, "16");
            
            C1.Text = "=B1";
            Assert.AreEqual(B1.Value, C1.Value);

            spreadsheet.Undo();
            Assert.AreEqual("0", C1.Value);
            Assert.AreEqual(B1.Value, "16");

            spreadsheet.Undo();
            Assert.AreEqual(B1.Value, "0");


        }

    }
}
