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
            Random rand = new Random();

            for (int i = 0; i < 10000; i++)
            {
                myList.Add(rand.Next(20000));
            }
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
            HashSet<int> hSet = new HashSet<int>(myList);

            return hSet.Count;
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

            int distinctNb = 0;   //Distinct Number Count, O(1) space
            bool isDistinct;

            for (int i = 0; i < myList.Count; i++)
            {
                isDistinct = true;       //reset flag to true

                for (int j = i + 1; j < myList.Count; j++) //Starts loop after the location of i for less runtime
                {
                    if (myList[i] == myList[j])
                    {
                        isDistinct = false;
                        break;  //break after first flag for less runtime
                    }

                }

                if (isDistinct == true)
                    distinctNb++;
            }


            return distinctNb;
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
            myList.Sort();

            // var is O(1) space
            int distinctNb = myList.Count;

            // loop requires O(n) time
            for (int i = 0; i < myList.Count - 1; i++)
            {
                if (myList[i] == myList[i + 1])
                    distinctNb--;
            }


            return distinctNb;
        }




        /* Program Results Print Function
         * This function initializes the list,
         * and runs it through each algorithm,
         * and returns a string containing the results.
         */
        public static string ProgramResults()
        {
            NumList.NumberList.Clear(); //clear the list for it to be initialized at 10000 elements again

            InitializeList(ref NumList.NumberList);
            var hset = HashSetAlg(NumList.NumberList);
            var O1comp = O1SpaceAlg(NumList.NumberList);
            var sorted = SortListAlg(NumList.NumberList);


            StringBuilder sb = new StringBuilder();

            sb.AppendFormat("HashSet Method: {0}\n", hset);
            sb.AppendLine();
            sb.Append("This method creates a hash set by copying all the elements of the List." +
                "Since the Hash Set structure does not allow duplicates," +
                "there are no extra steps taken to remove them," +
                "other than adding the list in the constructor for it to be copied at creation." +
                "In this way, the only function is creating the hash set, which is O(N):\n" +
                "Add function is O(1), and is being implemented for the whole List, i.e O(N).\n" +
                "The distinct count is simply finding the capacity of the set, O(1)," +
                "through the MSDN function HashSet.Count \n\n");
            sb.AppendLine();
            sb.AppendFormat("O(1) Storage Method: {0}\n\n", O1comp);
            sb.AppendLine();
            sb.AppendFormat("Sorted Method: {0}\n\n", sorted);


            return sb.ToString();
        }



    }
}
