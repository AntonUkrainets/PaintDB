using System;
using System.Collections.Generic;
using System.Text;
using Paint.Draw;
using NUnit.Framework;

namespace PaintTest
{
    class PointStructTest
    {        
        [Test]
        public void Test_X_Property_Equ_20()
        {
            Point point = new Point(20, 0);
            float actual = point.X;
            float expected = 20;
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void Test_Y_Property_Equ_20()
        {
            Point point = new Point(0, 20);
            float actual = point.Y;
            float expected = 20;
            Assert.AreEqual(expected, actual);
        }
        
    }
}
