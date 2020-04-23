using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spreadsheet_Engine
{
    internal class DivisionNode : OperatorNode
    {
        public DivisionNode() : base('/') { }

        public override Int16 Precedence { get; } = 1;

        public override double Evaluate(double left, double right)
        {
            try
            {
                return right / left;
            }
            catch (Exception e)
            {
                throw new Exception("Error with Division Operator.");
            }
        }
    }

}
