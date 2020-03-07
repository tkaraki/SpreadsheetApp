using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spreadsheet_Engine
{
    internal class ExpressionTreeFactory
    {
        public static Dictionary<char, Type> operators = new Dictionary<char, Type>
        {
            { '*', typeof(MultiplicationNode) },
            { '/', typeof(DivisionNode) },
            { '+', typeof(AdditionNode) },
            { '-', typeof(SubtractionNode) },
        };

        public static OperatorNode CreateOperatorNode(char c)
        {
            if (operators.ContainsKey(c))
            {
                object operatorNodeChild = System.Activator.CreateInstance(operators[c]);
                if (operatorNodeChild is OperatorNode)
                {
                    return (OperatorNode)operatorNodeChild;
                }

                throw new Exception("Invalid Operator");
            }

            return null;
        }
    }

}
