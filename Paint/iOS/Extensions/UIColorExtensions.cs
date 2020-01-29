using Paint.Draw;
using UIKit;

namespace Paint.iOS.Extensions
{
    public static class UIColorExtensions
    {
        public static Color GetColor(this UIColor color)
        {
            return new Color(
                (byte)(color.CGColor.Components[0] * 255),
                (byte)(color.CGColor.Components[1] * 255),
                (byte)(color.CGColor.Components[2] * 255)
                );
        }
        
        public static UIColor GetColor(this Color color)
        {
            return UIColor.FromRGB(color.Red, color.Green, color.Blue);
        }
    }
}