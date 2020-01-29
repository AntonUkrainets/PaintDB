using Newtonsoft.Json;
using Paint.Draw;
using Paint.Keeper;
using SQLite;
using System;

namespace Paint.Keeper
{
    [Table("DrawModel")]
    public class Stock
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }

        public string Data { get; set; }
    }
    public class SQLiteKepper : IDrawKeeper
    {
        private string _path;
        DrawModel drawModel;
        SQLiteConnection db;

        public SQLiteKepper(string path)
        {
            _path = path;
        }
        public DrawModel Load()
        {
            sqlConnet(_path);
            var table = db.Table<Stock>();

            foreach (var mod in table)
            {
                drawModel = JsonConvert.DeserializeObject<DrawModel>(mod.Data);
            };

            db.Close();
            return drawModel;
        }

        public void Save(DrawModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException();
            }
            var strModel = JsonConvert.SerializeObject(model);

            sqlConnet(_path);
            db.CreateTable<Stock>();

            if (db.Table<Stock>().Count() > 0)
            {
                db.DropTable<Stock>();
                db.CreateTable<Stock>();
            }

            if (db.Table<Stock>().Count() == 0)
            {
                var newStock = new Stock();
                newStock.Data = strModel;
                db.Insert(newStock);
            }
            db.Close();
        }
        private void sqlConnet(string dbPath)
        {
            db = new SQLiteConnection(dbPath);
        }
    }
}

