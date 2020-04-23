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
            this.DoCompile(expression);
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

        // private method to evaluate the tree.
        private double Evaluate(BaseNode theNode)
        {
            // Evaluate left and right nodes if theNode is an operator.
            if (theNode != null && theNode is OperatorNode)
            {
                OperatorNode temp = (OperatorNode)theNode;

                return temp.Evaluate(this.Evaluate(temp.Left), this.Evaluate(temp.Right));
            }

            // Return the value of theNode after looking for it in the dictionary if it is a variable.
            if (theNode != null && theNode is VariableNode)
            {
                VariableNode temp = (VariableNode)theNode;
                return this.Variables[temp.Name];
            }

            // Return the value of theNode if it is a constant.
            if (theNode != null && theNode is ConstantNode)
            {
                ConstantNode temp = (ConstantNode)theNode;
                return temp.Value;
            }

            return 0;
        }

        /// <summary>
        /// Builds the expression tree to be compiled.
        /// </summary>
        private BaseNode BuildTree(BaseNode cur)
        {
            if (cur is OperatorNode)
            {
                OperatorNode temp = (OperatorNode)cur;
                temp.Left = this.BuildTree(this.postFixExpressionStack.Pop());
                temp.Right = this.BuildTree(this.postFixExpressionStack.Pop());
            }
            else
            {
            }

            return cur;
        }

        /// <summary>
        /// Determines if the operator is valid.
        /// </summary>
        /// <returns>True if valid.</returns>
        /// <returns>False if not valid.</returns>
        private static bool IsOperator(char symbol)
        {
            return (ExpressionTreeFactory.CreateOperatorNode(symbol) != null);
        }

        private void DoCompile(string expression)
        {
            this.Compile(expression);

            // 7. At the end of the expression, pop and print all operators on the stack. (No parentheses should remain.)
            while (this.opStack.Count > 0)
            {
                this.postFixExpressionStack.Push(this.opStack.Pop());
            }
        }

        /// <summary>
        /// Compiles a postfix expression using shunting yard algorithm from https://www.geeksforgeeks.org/expression-evaluation/.
        /// </summary>
        private void Compile(string expression)
        {
            double number;
            int operatorIndex = 0;
            string sub = string.Empty;

            // Checks if expression holds anything
            if (expression.Length != 0)
            {
                for (int i = 0; i < expression.Length; i++)
                {
                    operatorIndex = i;

                    // Looks for type of first operator inside the expression
                    while (operatorIndex < expression.Length && !ExpressionTreeFactory.IsValidOperator(expression[operatorIndex]))
                    {
                        operatorIndex++;
                    }

                    // if the operartorIndex is the same as the i than expression[i] is an operator and the else statement wouldn't normally process this correctly.
                    if (operatorIndex == i)
                    {
                        sub = expression[i].ToString();
                    }

                    // finds the correct substring otherwise
                    else
                    {
                        sub = expression.Substring(i, operatorIndex - i);
                        if (operatorIndex - i > 1)
                        {
                            i += operatorIndex - i;
                            i--;
                        }
                    }

                    // Checks for any other operator. operatorIndex will be greater than
                    // expression.Length if no operator is found
                    if (ExpressionTreeFactory.IsValidOperator(sub[0]))
                    {
                        // Creates operator node and divides expression accordingly
                        OperatorNode newNode = ExpressionTreeFactory.CreateOperatorNode(sub[0]);

                        // newNode.Left = this.Compile(expression.Substring(0, operatorIndex));
                        // newNode.Right = this.Compile(expression.Substring(operatorIndex + 1));

                        // 2. If the incoming symbol is a left parentheses, push it on the stack.
                        if (newNode.Operator == '(')
                        {
                            this.opStack.Push(newNode);
                        }

                        // 3. If the incoming symbol is a right parenthesis: discard the right parenthesis,
                        //    pop and print the stack symbols until you see a left parenthesis.
                        //    Pop the left parenthesis and discard it.
                        else if (newNode.Operator == ')')
                        {
                            while ((char)this.opStack.Peek().GetType().GetProperty("Operator").GetValue(this.opStack.Peek()) != '(')
                            {
                                this.postFixExpressionStack.Push(this.opStack.Pop());
                            }

                            this.opStack.Pop();
                        }

                        // 4. If the incoming symbol is an operator and the stack is empty or contains a left parentheses on top,
                        //    push the incoming operator onto the stack.
                        else if (this.opStack.Count == 0 || (char)this.opStack.Peek().GetType().GetProperty("Operator").GetValue(this.opStack.Peek()) == '(')
                        {
                            this.opStack.Push(newNode);
                        }

                        // 5. If the incoming symbol is an operator and has either higher precedence than the operator on the top of the stack,
                        //    or has the same precedence as the operator on the top of the stack and is right associative--push it on the stack.
                        else if (newNode.Precedence > (ushort)this.opStack.Peek().GetType().GetProperty("Precedence").GetValue(this.opStack.Peek()) ||
                            (newNode.Precedence == (ushort)this.opStack.Peek().GetType().GetProperty("Precedence").GetValue(this.opStack.Peek())))
                        {
                            this.opStack.Push(newNode);
                        }

                        // 6. If the incoming symbol is an operator and has either lower precedence than the operator on the top of the stack,
                        //    or has the same precedence as the operator on the top of the stack and is left associative--continue to pop the
                        //    stack until this is not true. Then, push the incoming operator.
                        else if (newNode.Precedence < (ushort)this.opStack.Peek().GetType().GetProperty("Precedence").GetValue(this.opStack.Peek()) ||
                            (newNode.Precedence == (ushort)this.opStack.Peek().GetType().GetProperty("Precedence").GetValue(this.opStack.Peek())))
                        {
                            // OperatorNode temp = (OperatorNode)this.opStack.Peek();
                            while (this.opStack.Count > 0 && ((OperatorNode)this.opStack.Peek()).Operator != '(' && ((OperatorNode)this.opStack.Peek()).Precedence >= newNode.Precedence)
                            {
                                this.postFixExpressionStack.Push(this.opStack.Pop());
                            }

                            this.opStack.Push(newNode);
                        }

                        // return newNode;
                    }

                    // 1. If the incoming symbols is an operand, output it..

                    // Checks if the expression contains only a number
                    else if (double.TryParse(sub, out number))
                    {
                        // Creates new constant node to return
                        ConstantNode newNode = new ConstantNode(number);
                        newNode.Value = number;

                        this.postFixExpressionStack.Push(newNode);

                        // return newNode;
                    }

                    // Checks if the expression contains only a variable
                    else
                    {
                        // Creates new variable node to return
                        VariableNode newNode = new VariableNode(sub);
                        this.Variables[sub] = 0.0;

                        this.postFixExpressionStack.Push(newNode);

                        // return newNode;
                    }

                    // this.Compile(expression.Substring(0, operatorIndex));
                    // this.Compile(expression.Substring(operatorIndex + 1));
                }
            }

            // Returns null if the expression is empty
            // return null;
        }

        // Subscribes the expression tree to the property changed event of a cell. Also sets the variables in the expression tree to the corresponding
        // cells in the spreadsheet.
        public void SubscribeToCell(Cell cell)
        {
            cell.PropertyChanged += this.CellChanged;

            if (this.Variables.ContainsKey(cell.IndexName))
            {
                // sets the variable in the dict to the value of the cell if it is a double or 0 if it is not.
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

        // This event fires when a cell that has been subscribed to has been changed.
        private void CellChanged(object sender, EventArgs e)
        {
            if (sender.GetType() == typeof(Cell))
            {
                Cell cell = sender as Cell;

                if (this.Variables.ContainsKey(cell.IndexName))
                {
                    // sets the variable in the dict to the value of the cell if it is a double or 0 if it is not.
                    if (double.TryParse(cell.Value, out double num))
                    {
                        this.Variables[cell.IndexName] = num;
                    }
                    else
                    {
                        this.Variables[cell.IndexName] = 0.0;
                    }

                    // The expression has changed so the value of the cell that this tree belongs to must also change.
                    this.parent.Value = this.Evaluate().ToString();
                }
            }
        }
    }
}


