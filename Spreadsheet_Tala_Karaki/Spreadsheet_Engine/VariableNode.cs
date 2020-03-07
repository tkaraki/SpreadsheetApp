using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spreadsheet_Engine
{
    internal class VariableNode : BaseNode
    {
        public VariableNode(string name, double value = 0)
        {
            this.Name = name;
            this.Value = value;
        }

        public string Name { get; set; }

        public double Value { get; set; }

        public override double Evaluate()
        {
            return this.Value;
        }
    }

}
