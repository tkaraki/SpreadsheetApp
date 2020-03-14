﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spreadsheet_Engine
{
    public class ExpressionTree
    {
        public string Expression { get; set; }
        public Dictionary<string, double> Variables { get; set; }
        private OperatorNode ExpressionTreeRoot { get; set; }
        public string PostFixExpression { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionTree"/> class.
        /// Constructor for expression tree.
        /// </summary>
        /// <param name="expression">input expression.</param>
        public ExpressionTree(string expression)
        {
            this.Expression = expression;
            this.Variables = new Dictionary<string, double>();
            this.ExpressionTreeRoot = null;
            this.BuildTree();
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
            double result = 0;

            // Build an expression tree with root as the operator
            this.BuildTree();

            // Evaluate Expression from root operator
            if (this.ExpressionTreeRoot != null)
            {
                try
                {
                    OperatorNode root = this.ExpressionTreeRoot;
                    return result = root.Evaluate();
                }
                catch (Exception e)
                {
                    throw new Exception("Invalid operator at root");
                }
            }
            else
            {
                throw new Exception("Error evaluating empty expression.");
            }
        }

        /// <summary>
        /// Builds the expression tree to be compiled.
        /// </summary>
        private void BuildTree()
        {
            BaseNode node;
            Stack<BaseNode> stack = new Stack<BaseNode>();

            // Compile a postfix expression from the input
            this.Compile();

            // Build tree from input
            foreach (var component in PostFixExpression.Split(' '))
            {
                // if var is a constant value create constant node
                if (component != "")
                {
                    if (Double.TryParse(component, out _))
                    {
                        node = new ConstantNode(Double.Parse(component));
                    }

                    // if var is a variable name add to dictionary and create variable node
                    else if (Variables.Keys.Contains(component))
                    {
                        node = new VariableNode(component, Variables[component]);
                    }

                    // if var is an operator create operator node
                    else if (ExpressionTreeFactory.operators.ContainsKey(component[0]))
                    {
                        node = ExpressionTreeFactory.CreateOperatorNode(component[0]);
                        ((OperatorNode)node).Right = stack.Pop();
                        ((OperatorNode)node).Left = stack.Pop();
                    }
                    else
                    {
                        node = null;
                    }

                    stack.Push(node);
                }
            }

            // Make expression tree root an operator node
            try
            {
                this.ExpressionTreeRoot = stack.Pop() as OperatorNode;
            }
            catch (Exception e)
            {
                throw new Exception("Expression Tree could not be created. Error finding operator on stack.");
            }
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

        /// <summary>
        /// Compiles a postfix expression using shunting yard algorithm from https://www.geeksforgeeks.org/expression-evaluation/.
        /// </summary>
        private void Compile()
        {
            var stack = new Stack<string>();
            var output = new List<string>();
            char token;

            for (int index = 0; index < this.Expression.Length; index++)
            {
                token = this.Expression[index];

                // token is a number: push it onto the value stack.
                if (char.IsDigit(token))
                {
                    string digitBuilder = "";
                    while (char.IsDigit(token) && index < this.Expression.Length)
                    {
                        digitBuilder += token;
                        index++;
                        if (index < this.Expression.Length)
                        {
                            token = this.Expression[index];
                        }
                    }

                    output.Add(digitBuilder + " ");
                    index--;
                }

                else if (token == '(')
                {
                    stack.Push(token.ToString());
                }

                else if (token == ')')
                {
                    string buff = "";
                    while (stack.Count > 0 && ( buff = stack.Pop()) != "(")
                    {
                        output.Add(buff);
                    }

                    if (buff != "(")
                    {
                        throw new Exception("Error finding matching paranthesis.");

                    }
                }

                // token is an operator, push it onto the stack.
                else if (IsOperator(token))
                {
                    OperatorNode tempOpNode = ExpressionTreeFactory.CreateOperatorNode(token);

                    while (stack.Count > 0 && IsOperator(stack.Peek()[0]))
                    {
                        OperatorNode tempNode = ExpressionTreeFactory.CreateOperatorNode(stack.Peek()[0]);
                        if (tempNode.Precedence >= tempNode.Precedence)
                        {
                            output.Add(stack.Pop());
                        }
                        else
                        {
                            break;
                        }
                    }

                    stack.Push(token.ToString());
                }

                // token is a variable name, add it to the variable dictionary
                else
                {
                    string varName = "";
                    while (!ExpressionTreeFactory.operators.ContainsKey(token) && index < this.Expression.Length)
                    {
                        varName += token;
                        index++;
                        if (index < this.Expression.Length)
                        {
                            token = this.Expression[index];
                        }
                    }

                    output.Add(varName + " ");
                    if (!this.Variables.ContainsKey(varName))
                    {
                        this.Variables.Add(varName, 0);
                    }

                    index--;
                }
            }

            // push remaining elements to output stack.
            while (stack.Count > 0)
            {
                var top = stack.Pop();
                output.Add(top);
            }

            // set PostFixExpression to output stack
            this.PostFixExpression = string.Join(" ", output);
        }
    }
}
