/*
 * Name: Tala Karaki
 * ID : 11659015
 */
 
 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW3
{
    public class FibonacciTextReader: System.IO.TextReader
    {
        private int maxNum = 0; //Max Numbers to Generate, 50 or 100
        private int count = 1; // count for current number
        

        /// <summary>
        /// Constructor
        /// Takes max numbers to generate sequence
        /// </summary>
        public FibonacciTextReader(int n) => maxNum = n;
    }
}
