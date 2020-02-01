using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW1
{
    class Program
    {
        static void Main(string[] args)
        {
            int Exited = 0;

            while (Exited != 1)
            {


                Program p = new Program();
                p.PrintMenu();
                Exited = p.ReadMenu();
            }


            Console.WriteLine("\n\n\nPress any key to close");
            Console.ReadKey();
        }

        void PrintMenu() { }

        int ReadMenu() { }
    }
}
