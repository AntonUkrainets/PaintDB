using System;
using Paint.Keeper;

namespace Paint.Droid.Keeper
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
                    return new FileKeeperDroid();
                case EDrawKeeperType.SQLite:
                    {
                        var path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                        var dbPath = System.IO.Path.Combine(path, "ormdemo.db3");
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
