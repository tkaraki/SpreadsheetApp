// NUnit 3 tests
// See documentation : https://github.com/nunit/docs/wiki/NUnit-Documentation
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;


namespace NUnit.ListMethods
{
    [TestFixture]
    public class DistinctAlgorithmTests
    {
        //Test that the list initiliazer creates 10,000 items
        [Test]
        public void TestListCapacity()
        {
            List<int> NumberList = new List<int>();
            WindowsFormsApp.DistinctAlgorithms.InitializeList(ref NumberList);
            var answer = 10000;
            Assert.That(answer, Is.EqualTo(NumberList.Count));
        }



        //Test that the list intitializer contains no elements greater than 20,000
        [Test]
        public void TestListElements()
        {
            List<int> NumberList = new List<int>();
            WindowsFormsApp.DistinctAlgorithms.InitializeList(ref NumberList);
            bool isGreater = false;

            foreach (int element in NumberList)
            {
                if (element > 20000)
                    isGreater = true;
            }

            var answer = false;
            Assert.That(answer, Is.EqualTo(isGreater));
        }



        //Test the equivalency of the algorithms 
        [Test]
        public void TestAlgorithms()
        {
            List<int> NumberList = new List<int>();
            WindowsFormsApp.DistinctAlgorithms.InitializeList(ref NumberList);
            int method1 = WindowsFormsApp.DistinctAlgorithms.HashSetAlg(NumberList);
            int method2 = WindowsFormsApp.DistinctAlgorithms.O1SpaceAlg(NumberList);
            int method3 = WindowsFormsApp.DistinctAlgorithms.SortListAlg(NumberList);
            bool isEqual = false;

            if (method1 == method2 && method2 == method3)
                isEqual = true;

            var answer = true;
            Assert.That(answer, Is.EqualTo(isEqual));
        }


        //Test that the funcition is returning a string
        [Test]
        public void TestResultsFunc()
        {
            string result = WindowsFormsApp.DistinctAlgorithms.ProgramResults();

            bool isEmpty = true;

            if (result != "")
                isEmpty = false;

            var answer = false;

            Assert.That(answer, Is.EqualTo(isEmpty));
        }
    }
}
