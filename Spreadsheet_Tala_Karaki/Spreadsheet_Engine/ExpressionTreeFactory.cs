using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spreadsheet_Engine
{
    internal class ExpressionTreeFactory
    {
        public static OperatorNode CreateOperatorNode(char op)
        {
            switch (op)
            {
                case '-':
                    return new SubtractionNode();
                case '+':
                    return new AdditionNode();
                case '*':
                    return new MultiplicationNode();
                case '/':
                    return new DivisionNode();
                case '(':
                    return new LeftParenthesisNode();
                case ')':
                    return new RightParenthesisNode();
            }

            return null;
        }

        public static bool IsValidOperator(char op)
        {
            switch (op)
            {
                case '-':
                    return true;
                case '+':
                    return true;
                case '*':
                    return true;
                case '/':
                    return true;
                case '(':
                    return true;
                case ')':
                    return true;
            }

            return false;
        }
    }
}
