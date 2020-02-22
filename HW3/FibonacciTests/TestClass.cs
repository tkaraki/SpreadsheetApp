// NUnit 3 tests
// See documentation : https://github.com/nunit/docs/wiki/NUnit-Documentation
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;

namespace FibonacciTests
{
    [TestFixture]
    public class TestClass
    {

        [Test]
        public void TestFibonacciSequence()
        {

            string answer = "1: 0\r\n2: 1";
            HW3.FibonacciTextReader fibonacci = new HW3.FibonacciTextReader(2);
            Assert.AreEqual(answer, fibonacci.ReadToEnd());

        }

        [Test]
        public void TestFibonacciSequenceZero()
        {
            string answer2 = "1: 0";
            HW3.FibonacciTextReader fibonacci = new HW3.FibonacciTextReader(1);
            Assert.AreEqual(answer2, fibonacci.ReadToEnd());
        }

        [Test]
        public void TestFibonacciSequenceNegative()
        {
            string answer3 = string.Empty;
            HW3.FibonacciTextReader fibonacci = new HW3.FibonacciTextReader(-1);
            Assert.AreEqual(answer3, fibonacci.ReadToEnd());
        }

    }
}
