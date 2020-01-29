using Paint.Draw;

namespace Paint.iOS
{
    public interface IPaintViewDelegate
    {
        void PathStartedAt(Point point);
        void PathAppendedAt(Point point);
    }
}