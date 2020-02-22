/*
 * Name: Tala Karaki
 * ID : 11659015
 */
 
 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;
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

       
        /// <summary>
        /// ReadToEnd() Override method
        /// Calls ReadLine in a loop and concatenates all the lines together
        /// using stringbuilder object
        /// </summary>
        public override string ReadToEnd()
        {
            StringBuilder sb = new StringBuilder();
            while (count <= maxNum)
            {
                sb.AppendFormat("{0}: ", count);
                sb.AppendFormat(ReadLine());

                if (count < maxNum)
                {
                    sb.AppendLine();
                }
                
                ++count;
            }
            return sb.ToString();
        }

        
        /// <summary>
        /// ReadLine() Override method
        /// Passes current number to FibonacciSequence Function and returns the matching
        /// fibonacci number in a string
        /// </summary>
        public override string ReadLine()
        {
            return FibonacciTextReader.FibonacciSequence(count).ToString();
        }

        
        /// <summary>
        /// FibonacciSequence
        /// Returns the corresponding number in the fibonacci sequence 
        /// for an input using a loop 
        /// Algorithm taken from https://www.dotnetperls.com/fibonacci
        /// 
        /// </summary>
        public static BigInteger FibonacciSequence(int n)
        {
            BigInteger a = 0;
            BigInteger b = 1;
            BigInteger c = 0;


            if (n <= 1)
            {
                return 0;
            }


            else
            {
                for (int i = 3; i <= n; i++)
                {
                    c = a + b;
                    a = b;
                    b = c;
                }

                return b;
            }
        }

    }
}
