using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spreadsheet_Engine
{
    internal class AdditionNode : OperatorNode
    {
        public AdditionNode() : base('+') { }

        public override Int16 Precedence { get; set; } = 2;

        public override double Evaluate()
        {
            try
            {
                return this.Left.Evaluate() + this.Right.Evaluate();
            }
            catch (Exception e)
            {
                throw new Exception("Error with Operator Children.");
            }

        }
    }
}
