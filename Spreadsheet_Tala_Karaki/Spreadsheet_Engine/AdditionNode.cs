﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spreadsheet_Engine
{
    internal class AdditionNode : OperatorNode
    {
        public AdditionNode() : base('+') { }

        public override Int16 Precedence { get; } = 0;

        public override double Evaluate(double left, double right)
        {
            try
            {
                return left + right;
            }
            catch (Exception e)
            {
                throw new Exception("Error with Addition Operator.");
            }

        }
    }
}
