namespace Paint.Keeper
{
    public interface IDrawKeeperFactory
    {
        IDrawKeeper Create(EDrawKeeperType type);
    }
}