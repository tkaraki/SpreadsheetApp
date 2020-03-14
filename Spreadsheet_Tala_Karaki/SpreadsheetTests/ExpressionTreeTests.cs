// NUnit 3 tests
// See documentation : https://github.com/nunit/docs/wiki/NUnit-Documentation
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;

namespace SpreadsheetTests
{
    [TestFixture]
    public class ExpressionTreeTests
    {
        [Test]
        public void TestSimpleExpressions()
        {
            // Testing multiple expressions
            double answer1 = 66;
            Spreadsheet_Engine.ExpressionTree expression1 = new Spreadsheet_Engine.ExpressionTree("A+B+6");
            expression1.SetVariable("A", 30);
            expression1.SetVariable("B", 30);
            Assert.AreEqual(answer1, expression1.Evaluate());

            // Testing multiple expressions
            double answer2 = 15;
            Spreadsheet_Engine.ExpressionTree expression2 = new Spreadsheet_Engine.ExpressionTree("4+5+6");
            Assert.AreEqual(answer2, expression2.Evaluate());

            // Testing multiple expressions
            double answer3 = 97;
            Spreadsheet_Engine.ExpressionTree expression3 = new Spreadsheet_Engine.ExpressionTree("100-1-2");
            Assert.AreEqual(answer3, expression3.Evaluate());

        }

        [Test]
        public void TestMultipleOperators()
        {
            double answer1 = 210;
            Spreadsheet_Engine.ExpressionTree expression1 = new Spreadsheet_Engine.ExpressionTree("A*6+B");
            expression1.SetVariable("A", 30);
            expression1.SetVariable("B", 30);
            Assert.AreEqual(answer1, expression1.Evaluate());

            // Testing multiple expressions
            double answer2 = 7;
            Spreadsheet_Engine.ExpressionTree expression2 = new Spreadsheet_Engine.ExpressionTree("10/5+5");
            Assert.AreEqual(answer2, expression2.Evaluate());

            // Testing multiple expressions
            double answer3 = 16;
            Spreadsheet_Engine.ExpressionTree expression3 = new Spreadsheet_Engine.ExpressionTree("4/2*6+4");
            Assert.AreEqual(answer3, expression3.Evaluate());
        }

        [Test]
        public void TestParanthesisOperations()
        {
            double answer1 = 210;
            Spreadsheet_Engine.ExpressionTree expression1 = new Spreadsheet_Engine.ExpressionTree("A+(B*6)");
            expression1.SetVariable("A", 30);
            expression1.SetVariable("B", 30);
            Assert.AreEqual(answer1, expression1.Evaluate());

            // Testing multiple expressions
            double answer2 = 1;
            Spreadsheet_Engine.ExpressionTree expression2 = new Spreadsheet_Engine.ExpressionTree("(10/(5+5))");
            Assert.AreEqual(answer2, expression2.Evaluate());

            // Testing multiple expressions
            double answer3 = 12;
            Spreadsheet_Engine.ExpressionTree expression3 = new Spreadsheet_Engine.ExpressionTree("20-((4/2)*6)+4");
            Assert.AreEqual(answer3, expression3.Evaluate());
        }

        [Test]
        public void TestNegative()
        {
            double answer1 = -150;
            Spreadsheet_Engine.ExpressionTree expression1 = new Spreadsheet_Engine.ExpressionTree("A-(B*6)");
            expression1.SetVariable("A", 30);
            expression1.SetVariable("B", 30);
            Assert.AreEqual(answer1, expression1.Evaluate());

            // Testing 2 negative values 
            double answer2 = 2;
            Spreadsheet_Engine.ExpressionTree expression2 = new Spreadsheet_Engine.ExpressionTree("(0-10)/(0-5)");
            Assert.AreEqual(answer2, expression2.Evaluate());

            // Testing multiple expressions
            double answer3 = -16;
            Spreadsheet_Engine.ExpressionTree expression3 = new Spreadsheet_Engine.ExpressionTree("0-((4/2)*6)-4");
            Assert.AreEqual(answer3, expression3.Evaluate());
        }

  
    }
 }
