using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Paint.Draw;

namespace PaintTest
{
    [TestFixture]
    class PathTest
    {
        [Test]
        public void TestParametersConstructor()
        {
            Point pointStart = new Point(100, 100);
            Point pointAppend = new Point(101, 101);
            List<Point> expectedPoints = new List<Point>
            {
                pointStart,
                pointAppend
            };
            Color expectedColor = new Color(255, 0, 0);
            float expectedLineWidth = 20f;

            Path path = new Path(expectedPoints, expectedColor, expectedLineWidth);

            List<Point> actualPoints = path.Points;
            Color actualColor = path.Color;
            float actualLineWidth = path.LineWidth;

            Assert.That(actualPoints, Is.EquivalentTo(expectedPoints));
            Assert.AreEqual(expectedColor, actualColor);
            Assert.AreEqual(expectedLineWidth, actualLineWidth);
        }
    }
}
