using System;
using Paint.Keeper;

namespace Paint.iOS.Keeper
{
    public class DrawKeeperFactory : IDrawKeeperFactory
    {
        public IDrawKeeper Create(EDrawKeeperType type)
        {
            switch (type)
            {
                case EDrawKeeperType.Internal:
                    return new InternalKeeper();
                case EDrawKeeperType.File:
                    return new FileKeeperIOS();
                case EDrawKeeperType.SQLite:
                    {
                        var path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                        var dbPath = System.IO.Path.Combine(path, "ormbase.db3");

                        return new SQLiteKepper(dbPath);
                    }
                case EDrawKeeperType.Realm:
                    return new RealmKeeper();
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}