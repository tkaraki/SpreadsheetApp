using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Spreadsheet_Engine
{
    public class ExpressionTree
    {
        public string Expression { get; set; }
        private BaseNode root;
        public Cell parent;
        public Dictionary<string, double> Variables { get; set; }
        private Stack<BaseNode> postFixExpressionStack = new Stack<BaseNode>();
        private Stack<BaseNode> opStack = new Stack<BaseNode>();

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionTree"/> class.
        /// Constructor for expression tree.
        /// </summary>
        /// <param name="expression">input expression.</param>
        public ExpressionTree(string expression)
        {
            this.Expression = expression;
            this.Variables = new Dictionary<string, double>();
            this.Compile(expression);
            this.root = this.postFixExpressionStack.Pop();
            this.root = this.BuildTree(this.root);
        }

        /// <summary>
        /// Sets variables in Variables dictionary.
        /// </summary>
        /// <param name="variableName">variable name.</param>
        /// <param name="variableValue">variable value.</param>
        public void SetVariable(string variableName, double variableValue)
        {
            this.Variables[variableName] = variableValue;
        }

        /// <summary>
        /// Evaluates the expression to a double value.
        /// </summary>
        /// <returns>Evaluation value of tree.</returns>
        public double Evaluate()
        {
            // Evaluate Expression from root operator
            if (this.root != null)
            {
                return this.Evaluate(this.root);
            }
            else
            {
                throw new Exception("Error evaluating empty expression.");
            }
        }

        /// <summary>
        /// Private method to evaluate the tree.
        /// </summary>
        /// <returns>Evaluation value of tree.</returns>
        private double Evaluate(BaseNode treeNode)
        {
            if (treeNode != null)
            {
                // If node is operator, evaluate left and right nodes
                if (treeNode is OperatorNode)
                {
                    OperatorNode op = (OperatorNode)treeNode;
                    return op.Evaluate(this.Evaluate(op.Left), this.Evaluate(op.Right));
                }

                // If node is variable, return value from dictionary
                if (treeNode is VariableNode)
                {
                    VariableNode val = (VariableNode)treeNode;
                    return this.Variables[val.Name];
                }

                // If node is constant, return value
                if (treeNode is ConstantNode)
                {
                    ConstantNode cons = (ConstantNode)treeNode;
                    return cons.Value;
                }
            }

            return 0;
        }

        /// <summary>
        /// Builds the expression tree to be compiled.
        /// </summary>
        /// <returns> treeNode if not operator node.</returns>
        private BaseNode BuildTree(BaseNode treeNode)
        {
            if (treeNode is OperatorNode)
            {
                OperatorNode temp = (OperatorNode)treeNode;
                temp.Left = this.BuildTree(this.postFixExpressionStack.Pop());
                temp.Right = this.BuildTree(this.postFixExpressionStack.Pop());
            }

            return treeNode;
        }

        /// <summary>
        /// Determines if the operator is valid from expression tree factory.
        /// </summary>
        /// <returns>True if valid.</returns>
        /// <returns>False if not valid.</returns>
        private static bool IsOperator(char symbol)
        {
            return (ExpressionTreeFactory.CreateOperatorNode(symbol) != null);
        }

        private void Compile(string expression)
        {
            string segment = string.Empty;
            double num = 0.0;
            int operatorIndex = 0;

            if (expression.Length != 0)
            {
                for (int i = 0; i < expression.Length; i++)
                {
                    operatorIndex = i;

                    // Find the first occurence of an operator
                    while (operatorIndex < expression.Length && !ExpressionTreeFactory.IsValidOperator(expression[operatorIndex]))
                    {
                        operatorIndex++;
                    }

                    // If operator is equivalent to i, then store it in a string segment
                    if (operatorIndex == i)
                    {
                        segment = expression[i].ToString();
                    }

                    // Else store the expression in a segment
                    else
                    {
                        segment = expression.Substring(i, operatorIndex - i);
                        if (operatorIndex - i > 1)
                        {
                            i += operatorIndex - i;
                            i--;
                        }
                    }

                    // Find all other operators.
                    if (ExpressionTreeFactory.IsValidOperator(segment[0]))
                    {
                        // Create operatorNode and distribute expression to other nodes
                        OperatorNode newNode = ExpressionTreeFactory.CreateOperatorNode(segment[0]);

                        if (newNode.Operator == '(')
                        {
                            this.opStack.Push(newNode);
                        }

                        else if (newNode.Operator == ')')
                        {
                            while ((char)this.opStack.Peek().GetType().GetProperty("Operator").GetValue(this.opStack.Peek()) != '(')
                            {
                                this.postFixExpressionStack.Push(this.opStack.Pop());
                            }

                            this.opStack.Pop();
                        }

                        else if (this.opStack.Count == 0 || (char)this.opStack.Peek().GetType().GetProperty("Operator").GetValue(this.opStack.Peek()) == '(')
                        {
                            this.opStack.Push(newNode);
                        }

                        else if (newNode.Precedence > (ushort)this.opStack.Peek().GetType().GetProperty("Precedence").GetValue(this.opStack.Peek()) ||
                            (newNode.Precedence == (ushort)this.opStack.Peek().GetType().GetProperty("Precedence").GetValue(this.opStack.Peek())))
                        {
                            this.opStack.Push(newNode);
                        }

                        else if (newNode.Precedence < (ushort)this.opStack.Peek().GetType().GetProperty("Precedence").GetValue(this.opStack.Peek()) ||
                            (newNode.Precedence == (ushort)this.opStack.Peek().GetType().GetProperty("Precedence").GetValue(this.opStack.Peek())))
                        {
                            while (this.opStack.Count > 0 && ((OperatorNode)this.opStack.Peek()).Operator != '(' && ((OperatorNode)this.opStack.Peek()).Precedence >= newNode.Precedence)
                            {
                                this.postFixExpressionStack.Push(this.opStack.Pop());
                            }

                            this.opStack.Push(newNode);
                        }

                    }

                    // Else if the expression is just a number, create a constant node
                    else if (double.TryParse(segment, out num))
                    {
                        ConstantNode newNode = new ConstantNode(num);
                        newNode.Value = num;

                        this.postFixExpressionStack.Push(newNode);
                    }

                    // Else the expression is just a variable, create a variable node
                    else
                    {
                        VariableNode newNode = new VariableNode(segment);
                        this.Variables[segment] = 0.0;

                        this.postFixExpressionStack.Push(newNode);
                    }
                }
            }

            // Clear all operators from stack
            while (this.opStack.Count > 0)
            {
                this.postFixExpressionStack.Push(this.opStack.Pop());
            }
        }

        /// <summary>
        /// Subscribes the expression tree to the property changed event of a cell.
        /// Sets the variables in the expression tree to the corresponding cells in the spreadsheet.
        /// </summary>
        public void SubscribeToCell(Cell cell)
        {
            cell.PropertyChanged += this.CellChanged;

            if (this.Variables.ContainsKey(cell.IndexName))
            {
                // Set the variable in the dict to the value of the cell.
                if (double.TryParse(cell.Value, out double num))
                {
                    this.Variables[cell.IndexName] = num;
                }
                else
                {
                    this.Variables[cell.IndexName] = 0.0;
                }
            }
        }

        /// <summary>
        /// Updates all other cells that depend on a cell that has changed.
        /// </summary>
        private void CellChanged(object sender, EventArgs e)
        {
            if (sender.GetType() == typeof(Cell))
            {
                Cell cell = sender as Cell;

                if (this.Variables.ContainsKey(cell.IndexName))
                {
                    // Set the variable in the dict to the value of the cell.
                    if (double.TryParse(cell.Value, out double num))
                    {
                        this.Variables[cell.IndexName] = num;
                    }
                    else
                    {
                        this.Variables[cell.IndexName] = 0.0;
                    }

                    // Update cell expression tree
                    this.parent.Value = this.Evaluate().ToString();
                }
            }
        }
    }
}


