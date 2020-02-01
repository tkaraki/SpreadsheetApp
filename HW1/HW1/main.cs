using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW1
{
    class main
    {
        HW1.BST myBST = new HW1.BST();

        static void Main(string[] args)
        {
            int Exited = 0;

            while (Exited != 1)
            {


                main p = new main();
                p.PrintMenu();
                Exited = p.ReadMenu();
            }


            Console.WriteLine("\n\n\nPress any key to close");
            Console.ReadKey();
        }



        /* Prints a menu to run or exit the program */
        public void PrintMenu()
        {
            Console.Write("\n\n\n\n====================");
            Console.Write("\nWelcome to the BST Calculator");
            Console.Write("\n\n\t 1. Run the program");
            Console.Write("\n\n\t 0. Exit");
            Console.Write("\n\n====================");
            Console.Write("\nEnter Your Choice (0-1):");
        }



        /* Runs user input from menu options */
        public int ReadMenu()
        {

            int exit = 0;
            int caseSwitch = Convert.ToInt32(Console.ReadLine());


            switch (caseSwitch)
            {
                case 1:
                    Console.Write("\n\n ...Running Program ...\n\n");
                    Console.WriteLine("\nEnter your numbers on a single line, ranging from 0 to 100, separated by spaces:");
                    ParseInput();
                    Console.WriteLine("\n\n\n ... Creating BST... ");
                    Console.WriteLine("\nBST Items:");
                    DisplayBST();
                    Console.WriteLine("\nBST Stats:");
                    DisplayStats();
                    break;

                case 0:
                    Console.WriteLine("\nMenu Exited.");
                    exit = 1;
                    break;

                default:
                    Console.WriteLine("Invalid Input");
                    break;
            }

            return exit;
        }



        /* Reads a collection of numbers from user and parses data into BST */
        public void ParseInput()
        {
            // obtain user input
            string input = "";
            input = Console.ReadLine();

            //start parsing by inserting into a list
            int num;
            List<int> NumList = new List<int>();


            if (input != "")
            {
                char[] separator = { ' ' };
                String[] strlist = input.Split(separator);

                foreach (String s in strlist)
                {
                    if (s != "")
                    {
                        num = Convert.ToInt32(s);
                        if (num > 0 && num < 100 && !NumList.Contains(num))
                        {
                            NumList.Add(num);
                        }
                    }
                }

            }

            //Print accepted numbers
            Console.Write("\nThe following numbers were saved:");
            foreach (Int32 element in NumList)
            {
                Console.Write(element + " ");
            }


            //insert into BST
            CreateBST(NumList);

        }



        /* Inserts parsed numbers from list into the BST */
        public void CreateBST(List<int> myList)
        {
            foreach (Int32 element in myList)
            {
                myBST.Add(element);
            }
        }



        /* Displays contents of BST from smallest to largest */
        public void DisplayBST()
        {
            myBST.Print();
        }



        /* Displays the stats of the BST: Node count, Level Count, Theoritical Minimum */
        public void DisplayStats()
        {
            Console.WriteLine("\t# of Items:" + myBST.NodeCount());
            Console.WriteLine("\t# of Levels:" + myBST.BSTLevels());
            Console.WriteLine("\tTheoretical Minimum # of Levels:" + myBST.MinLevels());
        }
    }
}
