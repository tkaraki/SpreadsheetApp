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
        public void TestExpressions()
        {
            // Testing addition expressions
            double answer1 = 15;
            Spreadsheet_Engine.ExpressionTree expression1= new Spreadsheet_Engine.ExpressionTree("4+(5+6)");
            Assert.AreEqual(answer1, expression1.Evaluate());

            // Testing negative expressions
            double answer3 = 101;
            Spreadsheet_Engine.ExpressionTree expression3 = new Spreadsheet_Engine.ExpressionTree("100-(1-2)");
            Assert.AreEqual(answer3, expression3.Evaluate());

            // Testing mixed expressions
            double answer2 = 1;
            Spreadsheet_Engine.ExpressionTree expression2 = new Spreadsheet_Engine.ExpressionTree("10/(5+5)");
            Assert.AreEqual(answer2, expression2.Evaluate());

            // Testing multiple expressions
            double answer4 = 20;
            Spreadsheet_Engine.ExpressionTree expression4 = new Spreadsheet_Engine.ExpressionTree("(4/2)*(6+4)");
            Assert.AreEqual(answer4, expression4.Evaluate());

        }


    }
 }
