using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spreadsheet_Engine
{
    internal class MultiplicationNode : OperatorNode
    {
        public MultiplicationNode() : base('*') { }

        public override Int16 Precedence { get; } = 1;

        public override double Evaluate(double left, double right)
        {
            try
            {
                return left * right;
            }
            catch (Exception e)
            {
                throw new Exception("Error with Multiplication Operator.");
            }
        }
    }

}
