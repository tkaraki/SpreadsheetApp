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

        public override Int16 Precedence { get; set; } = 3;

        public override double Evaluate()
        {
            try
            {
                return this.Left.Evaluate() * this.Right.Evaluate();
            }
            catch (Exception e)
            {
                throw new Exception("Error with Operator Children.");
            }
        }
    }

}
