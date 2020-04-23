using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spreadsheet_Engine
{
    internal class LeftParenthesisNode: OperatorNode
    {
        public LeftParenthesisNode() : base('(') { }

        public override Int16 Precedence { get; } = 2;

        public override double Evaluate(double left, double right)
        {
            try
            {
                return right;
            }
            catch (Exception e)
            {
                throw new Exception("Error with paranthesis.");
            }
        }
    }
}
