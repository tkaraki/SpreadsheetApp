using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionTreeDemo
{
    class Program
    {
        static void Main(string[] args)
        {

            Spreadsheet_Engine.ExpressionTree exptree = new Spreadsheet_Engine.ExpressionTree("");
            string input = "0";
            
            while (input != "4")
            {
                Console.WriteLine("Enter an option from menu below:");
                Console.WriteLine("1. Enter a new expression");
                Console.WriteLine("2. Set a variable value");
                Console.WriteLine("3. Evaluate tree");
                Console.WriteLine("4. Quit");
                Console.WriteLine("Enter input: ");
                input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        string expression;
                        Console.WriteLine("Enter an expression.");
                        expression = Console.ReadLine();
                        exptree = new Spreadsheet_Engine.ExpressionTree(expression);
                        break;

                    case "2":
                        Console.WriteLine("Enter variable name: ");
                        string var = Console.ReadLine();
                        Console.WriteLine("Enter variable value: ");
                        string val = Console.ReadLine();
                        double value = Convert.ToDouble(val);
                        exptree.SetVariable(var, value);
                        break;

                    case "3":
                        double eval = exptree.Evaluate();
                        Console.WriteLine("Tree evaluates to " + eval);
                        break;
                }
            }

        }

    }
}
