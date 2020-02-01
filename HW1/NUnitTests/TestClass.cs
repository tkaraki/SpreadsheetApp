// NUnit 3 tests
// See documentation : https://github.com/nunit/docs/wiki/NUnit-Documentation
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;

namespace NUnitTests
{
    [TestFixture]
    public class TestClass
    {

            [Test]
            public void TestBSTAdd()
            {
                HW1.BST myBST = new HW1.BST();
                myBST.Add(5);
                var answer = false;
                Assert.AreEqual(answer, Is.EqualTo(myBST.isEmpty()), "Add Test: passed");
            }


            [Test]
            public void TestBSTNodeCount()
            {
                HW1.BST myBST = new HW1.BST();
                myBST.Add(5);
                myBST.Add(6);
                myBST.Add(7);
                var answer = 3;
                Assert.AreEqual(answer, Is.EqualTo(myBST.NodeCount()), "Node Count Test: passed");
            }


            [Test]
            public void TestBSTLevels()
            {
                HW1.BST myBST = new HW1.BST();
                myBST.Add(5);
                myBST.Add(6);
                myBST.Add(7);
                var answer = 3;
                Assert.AreEqual(answer, Is.EqualTo(myBST.BSTLevels()), "Level Count Test: passed");
            }
        }
    }
