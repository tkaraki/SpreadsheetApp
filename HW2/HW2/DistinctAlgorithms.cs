using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp
{
    public class DistinctAlgorithms
    {


        /* Global Variable: List of Numbers
         */
        static class NumList
        {
            public static List<int> NumberList = new List<int>();
        }



        /* Initilize List 
         * This function takes a list by reference, 
         * and pads it with numbers from 0 to 20,000,
         * until capacity reaches 10,000 elements
         */
        public static void InitializeList(ref List<int> myList)
        {
            
        }



        /* Hash Set Algorithm 
        * This function takes a list,
        * generates a hash set from the built-in library,
        * and fills it with elements from the list.
        * Add and Search function for HashSet is O(1),
        * and it is being done for all the elements in the list, 
        * So O(N) time in all.
        * Since the HashSet functionality cannot take duplicate elements,
        * when it returns the element count, 
        * it returns the count of distinct numbers.
        */
        public static int HashSetAlg(List<int> myList)
        {
           
        }



        /* O(1) Space Complexity Algorithm
        * This function takes a list,
        * and loops through it twice,
        * flagging duplicates with a boolean,
        * and counting every distinct number,
        * both requiring O(1) space,
        * and returns the sum.
        */
        public static int O1SpaceAlg(List<int> myList)
        {
            
        }




        /* Sorted List Algorithm
         * This function takes a list,
         * and sorts it using built-in library,
         * and loops through the list once counting every duplicate, O(n) time,
         * and subtracts it from the Count of elements in the list,
         * and returns the number of distinct items. 
         */
        public static int SortListAlg(List<int> myList)
        {
        }




        /* Program Results Print Function
         * This function initializes the list,
         * and runs it through each algorithm,
         * and returns a string containing the results.
         */
        public static string ProgramResults()
        {
            
        }



    }
}
