using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spreadsheet_Engine
{
    internal abstract class OperatorNode : BaseNode
    {
        public char Operator { get; set; }

        public BaseNode Left { get; set; }

        public BaseNode Right { get; set; }

        public abstract Int16 Precedence { get; set; }

        public OperatorNode(char c)
        {
            this.Operator = c;
            this.Left = this.Right = null;
        }

        public abstract override double Evaluate();

    }
}
