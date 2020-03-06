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
        public void TestMethod()
        {
            // Testing multiple expressions
            string answer1 = "ABHello6";
            Spreadsheet_Engine.ExpressionTree expression1 = new Spreadsheet_Engine.ExpressionTree("A+B+C1+Hello+6");
            Assert.AreEqual(answer1, expression1.Evaluate());

            // Testing multiple expressions
            string answer2 = "15";
            Spreadsheet_Engine.ExpressionTree expression2 = new Spreadsheet_Engine.ExpressionTree("4+5+6");
            Assert.AreEqual(answer2, expression2.Evaluate());

            // Testing multiple expressions
            string answer3 = "97";
            Spreadsheet_Engine.ExpressionTree expression3 = new Spreadsheet_Engine.ExpressionTree("100-1-2");
            Assert.AreEqual(answer3, expression3.Evaluate());

        }
    }
}
