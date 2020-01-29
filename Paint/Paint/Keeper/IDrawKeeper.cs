using Paint.Draw;

namespace Paint.Keeper
{
    public interface IDrawKeeper
    {
        void Save(DrawModel model);
        DrawModel Load();
    }
}