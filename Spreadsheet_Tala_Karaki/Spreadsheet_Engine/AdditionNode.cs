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

        public override double Evaluate()
        {
            try
            {
                return this.Left.Evaluate() + this.Right.Evaluate();
            }
            catch (Exception e)
            {
                throw new Exception("Error on Operator Children.");
            }

        }
    }
}
