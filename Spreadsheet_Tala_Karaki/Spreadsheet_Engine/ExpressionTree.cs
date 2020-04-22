using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Spreadsheet_Engine
{
    //internal abstract class OpNodeFactory
    //{
    //    public abstract OperatorNode FactoryMethod(string op);
    //}

    ///// <summary>
    ///// Concrete Factory for OperationNodes
    ///// Given an input character (the operator), this factory's "FactoryMethod"
    ///// method will return the appropriate node.
    ///// </summary>
    //internal class ConcreteOpNodeFactory : OpNodeFactory
    //{
    //    public override OperatorNode FactoryMethod(string op)
    //    {
    //        switch (op)
    //        {
    //            case "+": return new AdditionNode();
    //            case "-": return new SubtractionNode();
    //            case "*": return new MultiplicationNode();
    //            case "/": return new DivisionNode();
    //            default: return null;
    //        }
    //    }
    //}

    //internal abstract class TreeNodeFactory
    //{
    //    public abstract BaseNode FactoryMethod(string expression);
    //}

    ///// <summary>
    ///// Concrete Factory for TreeNodes.
    ///// Given an input expression, if it's not an operator, then it is assumed
    ///// to be a valuenode or cellreferencenode. If conversion to int fails, it automatically
    ///// becomes a cellreferencenode.
    ///// </summary>
    //internal class ConcreteTreeNodeFactory : TreeNodeFactory
    //{
    //    public override BaseNode FactoryMethod(string expression)
    //    {
    //        OpNodeFactory opNodeFactory = new ConcreteOpNodeFactory();
    //        OperatorNode @operator = opNodeFactory.FactoryMethod(expression);
    //        if (@operator != null)
    //        {
    //            return @operator;
    //        }
    //        else
    //        {
    //            bool int_success = Int32.TryParse(expression, out int int_result),
    //                double_success = Double.TryParse(expression, out double double_result);
    //            if (int_success)
    //            {
    //                return new ConstantNode(int_result);
    //            }
    //            else if (double_success)
    //            {
    //                return new ConstantNode(double_result);
    //            }
    //            else
    //            {
    //                return new DependencyNode(expression);
    //            }
    //        }
    //    }
    //}

    public class ExpressionTree
    {
        //    /*
        //     * Fields
        //     */
        //    private BaseNode _root = null;
        //    private Dictionary<string, HashSet<DependencyNode>> _variable_dict;   // HashSet allows for indentical cell references in the same expression

        //    /// <summary>
        //    /// Construct the expression tree and initialize dictionary
        //    /// </summary>
        //    /// <param name="expression"></param>
        //    public ExpressionTree(string expression)
        //    {
        //        expression = expression.Replace(" ", String.Empty);    // Remove spaces from expression
        //        this._variable_dict = new Dictionary<string, HashSet<DependencyNode>>();
        //        try
        //        {
        //            _root = ConstructTree(InfixToPostfix(expression));
        //        }
        //        catch
        //        {
        //            throw;              // propagate it up to UI layer
        //        }
        //    }

        //    /// <summary>
        //    /// Construct the expression tree from a postfix input string. 
        //    /// Pseudocode from: https://www.geeksforgeeks.org/expression-tree/
        //    /// Determine node type by utilizing the factory method. In this way, 
        //    /// we don't have to directly determine the type of BAseNode
        //    /// in this method.
        //    /// </summary>
        //    /// <param name="expression"></param>
        //    /// <returns></returns>
        //    private BaseNode ConstructTree(string expression)
        //    {
        //        // Step 1: Tokenize once again.
        //        var list = Tokenize(expression, false);    // false ==> wildcards are NOT present

        //        // Step 2: Walk through tokens and build tree using a stack
        //        Stack<BaseNode> stack = new Stack<BaseNode>();
        //        TreeNodeFactory treeNodeFactory = new ConcreteTreeNodeFactory();
        //        foreach (string tok in list)
        //        {
        //            BaseNode tree = treeNodeFactory.FactoryMethod(tok);
        //            switch (tree)      // Switch on the TreeNode's type.
        //            {
        //                case OperatorNode opnode:
        //                    switch (opnode)          // switch statement because eventually we'll have unary operators and possibly others (function types)
        //                    {
        //                        case OperatorNode bopnode:
        //                            OperatorNode binaryOperator = bopnode as OperatorNode;     // cast as binary op
        //                            BaseNode right = stack.Pop(), left = stack.Pop();                   // since stack is LIFO, right child is top
        //                            binaryOperator.Left = left; binaryOperator.Right = right;           // set children
        //                            stack.Push(binaryOperator);                                         // push back onto stack
        //                            break;
        //                        default:
        //                            break;
        //                    }
        //                    break;
        //                case DependencyNode crnode:
        //                    DependencyNode DependencyNode = tree as DependencyNode;
        //                    if (!_variable_dict.ContainsKey(DependencyNode.Name))      // variable not currently in dictionary
        //                    {
        //                        _variable_dict.Add(DependencyNode.Name, new HashSet<DependencyNode>());
        //                    }
        //                    _variable_dict[DependencyNode.Name].Add(DependencyNode);    // hashset allows for multiple nodes with identical keys (ex: A5 + A5 + 6)
        //                    stack.Push(tree);     // simply push
        //                    break;
        //                case ConstantNode vnode:
        //                    stack.Push(tree);     // simply push
        //                    break;
        //                default:
        //                    break;
        //            }
        //        }
        //        return stack.Pop();
        //    }

        //    /// <summary>
        //    /// Tokenize the expression into a list of strings. Matches decimal/integers, cell labels, operators, and wildcard.
        //    /// We match wildcard when wild_cards_present == true so that invalid input can be detected during the 
        //    /// infixToPostfix stage.
        //    /// </summary>
        //    /// <param name="expression"></param>
        //    /// <returns></returns>
        //    private List<string> Tokenize(string expression, bool wild_cards_present)
        //    {
        //        // REGEX Notes:
        //        // [] matches just single char in that group
        //        // () matches full expression
        //        // * means matches previous thing 0 or more times; + means one or more
        //        // ? means 0 or more
        //        // use '\' for special characters to escape it

        //        // This pattern will match decimal numbers (first part), cell label (second part), the operators: +-*/() (third part), or wildcard: . (fourth part)
        //        string @pattern = @"[\d]+\.?[\d]*|[A-Za-z]+[0-9]+|[-/\+\*\(\)]";
        //        if (wild_cards_present) pattern += "|.+";         // add in this optional wildcard if bool passed in is true
        //        Regex r = new Regex(@pattern);
        //        MatchCollection matchList = Regex.Matches(expression, @pattern);
        //        return matchList.Cast<Match>().Select(match => match.Value).ToList();
        //    }

        //    /// <summary>
        //    /// Shunting Yard Algorithm
        //    /// Developed from pseudocode here: https://brilliant.org/wiki/shunting-yard-algorithm/
        //    /// </summary>
        //    /// <param name="expression"></param>
        //    /// <returns></returns>
        //    private string InfixToPostfix(string expression)
        //    {
        //        HashSet<char> operators = new HashSet<char>(new char[] { '+', '-', '*', '/' });
        //        Dictionary<char, int> precedence = new Dictionary<char, int>
        //        {
        //            ['('] = 0,
        //            ['+'] = 1,
        //            ['-'] = 1,
        //            ['*'] = 2,
        //            ['/'] = 2,
        //            [')'] = 10            // high precedence since it's closing off a priority subsection 
        //        };

        //        var list = Tokenize(expression, true);       // true ==> wildcards ARE present
        //        Queue<string> output_list = new Queue<string>(list.Capacity);
        //        Stack<char> opStack = new Stack<char>();
        //        foreach (string tok in list)
        //        {
        //            if (int.TryParse(tok, out int int_result) || double.TryParse(tok, out double dec_result)
        //                || Regex.Match(tok, @"[A-Za-z]+[0-9]+").Success)        // if token is an integer or double or a cell label (i.e. some letters followed by some numbers)
        //            {
        //                output_list.Enqueue(tok);    // push to output queue
        //            }
        //            else    // assume it's an operator type
        //            {
        //                if (operators.Contains(tok[0]))          // i.e. tok is an operator
        //                {
        //                    while (opStack.Count != 0 && precedence[opStack.Peek()] > precedence[tok[0]])    // while operator on opstack has higher precedence than current op
        //                    {
        //                        output_list.Enqueue(opStack.Pop().ToString());          // pop operator and enqueue to output
        //                    }
        //                    opStack.Push(tok[0]);       // push current op onto stack
        //                }
        //                else if (tok.StartsWith("("))      // mark left parenthesis by pushing onto opstack
        //                {
        //                    opStack.Push(tok[0]);
        //                }
        //                else if (tok.StartsWith(")"))
        //                {
        //                    try
        //                    {
        //                        while (opStack.Peek() != '(')          // If we run into a right parenthesis, keep popping opstack til we get to left parenthesis
        //                        {
        //                            output_list.Enqueue(opStack.Pop().ToString());       // this will throw an exception if opStack is empty
        //                        }
        //                    }
        //                    catch (InvalidOperationException)
        //                    {
        //                        throw new Exception("Mismatched Parenthesis in expression");
        //                    }
        //                    opStack.Pop();    // pop left parenthesis from stack
        //                }
        //                else           // unrecognized token
        //                {
        //                    throw new ArgumentException(string.Format("{0} is not a valid formula value.", tok));
        //                }
        //            }
        //        }

        //        // If there are still operators on the opstack, pop them to the result queue.
        //        while (opStack.Count > 0)
        //        {
        //            if (opStack.Peek() != '(' || opStack.Peek() != ')')
        //                output_list.Enqueue(opStack.Pop().ToString());
        //            else
        //                throw new Exception("Mismatched Parenthesis in expression");
        //        }
        //        return string.Join(" ", output_list.ToArray());
        //    }

        //    /// <summary>
        //    /// Add variable to internal dictionary to keep track of cell references and their values
        //    /// </summary>
        //    /// <param name="varName"></param>
        //    /// <param name="varValue"></param>
        //    public void SetVar(string varName, double varValue)
        //    {
        //        if (_variable_dict.ContainsKey(varName))
        //        {
        //            foreach (DependencyNode node in _variable_dict[varName])    // for each identical cell reference, set its value
        //            {
        //                node.Value = varValue;
        //            }
        //        }
        //        else
        //        {
        //            throw new KeyNotFoundException(String.Format("{0} is not a variable in the expression", varName));
        //        }
        //    }

        //    /// <summary>
        //    /// Return a list of variables in the expression tree.
        //    /// </summary>
        //    /// <returns></returns>
        //    public List<string> VariablesInExpression
        //    {
        //        get
        //        {
        //            return new List<string>(_variable_dict.Keys);
        //        }
        //    }

        //    /// <summary>
        //    /// Recursively evaluate the expression tree from the root.
        //    /// Throws a null reference exception if value's (i.e. a variable's) value is unknown
        //    /// </summary>
        //    /// <returns></returns>
        //    public double Eval()
        //    {
        //        try
        //        {
        //            return _root.Evaluate();
        //        }
        //        catch (NullReferenceException)    // thrown if a variable (cell reference node) has no value
        //        {
        //            throw;
        //        }
        //    }
        //}
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
                    while (stack.Count > 0 && (buff = stack.Pop()) != "(")
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


