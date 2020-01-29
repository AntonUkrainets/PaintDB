using Paint.Draw;


namespace Paint.Droid
{
    interface IPaintViewDelegate
    {
        void PathStartedAt(Point point);
        void PathAppendedAt(Point point);
    }
}