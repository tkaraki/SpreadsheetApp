using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spreadsheet_Engine
{
    internal class VariableNode : BaseNode
    {
        public VariableNode(string name)
        {
            this.Name = name;
        }

        public string Name { get; set; }
    }

}
