using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace PaintTest
{
    [TestFixture]
    class TestColorStruct
    {
        [Test]
        public void TestParametersConstructor()
        {
            byte expectedRed = 10;
            byte expectedGreen = 20;
            byte expectedBlue = 30;

            Paint.Draw.Color color = new Paint.Draw.Color(10, 20, 30);

            byte actualRed = color.Red;
            byte actualGreen = color.Green;
            byte actualBlue = color.Blue;

            Assert.AreEqual(expectedRed, actualRed);
            Assert.AreEqual(expectedGreen, actualGreen);
            Assert.AreEqual(expectedBlue, actualBlue);
        }
    }
}
