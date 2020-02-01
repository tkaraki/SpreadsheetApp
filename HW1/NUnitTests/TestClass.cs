// NUnit 3 tests
// See documentation : https://github.com/nunit/docs/wiki/NUnit-Documentation
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using System.IO;
using System;

namespace NUnitTests
{
    [TestFixture]
    public class TestClass
    {

            [Test]
            public void TestBSTAddCorrectly()
            {
                HW1.BST myBST = new HW1.BST();
                myBST.Add(5);
                var answer = false;
                Assert.That(answer, Is.EqualTo(myBST.isEmpty()), "Add Test: passed");
            }

        //No Need for this test because boundary checking is in main
            //[Test]
            //public void TestBSTAddBoundaries()
            //{
            //    HW1.BST myBST = new HW1.BST();
            //    myBST.Add(-5); //check <0
            //    myBST.Add(200);  //check >100
            //    var answer = true;
            //    Assert.That(answer, Is.EqualTo(myBST.isEmpty()), "Add Test: passed");
            //}


            [Test]
            public void TestBSTNodeCountCorrectly()
            {
                HW1.BST myBST = new HW1.BST();
                myBST.Add(6);
                myBST.Add(7);
                var answer = 2;
                Assert.That(answer, Is.EqualTo(myBST.NodeCount()), "Node Count Test: passed");
            }


            [Test]
            public void TestBSTLevelsCorrectly()
            {
                HW1.BST myBST = new HW1.BST();
                myBST.Add(5);
                myBST.Add(6);
                myBST.Add(7);
                var answer = 3;
                Assert.That(answer, Is.EqualTo(myBST.BSTLevels()), "Level Count Test: passed");
            }


            [Test]
            public void TestMinLevelCorrectly()
            {
                HW1.BST myBST = new HW1.BST();
                myBST.Add(5);
                myBST.Add(6);
                myBST.Add(7);
                myBST.Add(9);
                var answer = 3;
                Assert.That(answer, Is.EqualTo(myBST.MinLevels()), "Min Level Test: passed");
            }


            [Test]
            public void TestInOrderTraversalCorrectly()
            {
                HW1.BST myBST = new HW1.BST();
                myBST.Add(5);
                myBST.Add(6);
                myBST.Add(7);
                string x = "\t5 6 7 ";
                

            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                myBST.Print();
                
                Assert.AreEqual(x, sw.ToString());
            }
        }
    }
    }
