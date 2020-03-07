using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spreadsheet_Engine
{
    internal class ConstantNode : BaseNode
    {
        public double Value { get; set; }

        public ConstantNode(double value)
        {
            this.Value = value;
        }

        public override double Evaluate()
        {
            return this.Value;
        }
    }
}
