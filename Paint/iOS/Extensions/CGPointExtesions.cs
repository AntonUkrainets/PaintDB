using CoreGraphics;
using Paint.Draw;

namespace Paint.iOS.Extensions
{
    public static class CGPointExtesions
    {
        public static Point GetPoint(this CGPoint point)
        {
            var res = new Point((float)point.X, (float)point.Y);
            return res;
        }
        
        public static CGPoint GetPoint(this Point point)
        {
            var res = new CGPoint(point.X, point.Y);
            return res;
        }
    }
}