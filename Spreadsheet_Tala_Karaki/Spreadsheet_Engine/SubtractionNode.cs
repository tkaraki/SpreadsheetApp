using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spreadsheet_Engine
{
    internal class SubtractionNode : OperatorNode
    {
        public SubtractionNode() : base('-') { }

        public override Int16 Precedence { get; } = 0;

        public override double Evaluate(double left, double right)
        {
            try
            {
                return right - left;
            }
            catch (Exception e)
            {
                throw new Exception("Error with Subtraction Operator.");
            }
        }
    }

}
