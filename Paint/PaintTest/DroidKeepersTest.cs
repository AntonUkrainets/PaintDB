using Newtonsoft.Json;
using NUnit.Framework;
using Paint.Draw;
using Paint.Droid.Keeper;
using Paint.Keeper;
using System;
using System.Collections.Generic;

namespace PaintTest
{
    class DroidKeepersTest
    {
        FileKeeperDroid file;
        InternalKeeper internalKeeper;
        RealmKeeper realm;
        Paint.Keeper.SQLiteKepper sqlite;

        DrawModel testDrawModel;
        DrawModel actualDrawModel;

        

        [SetUp]
        public void Setup()
        {
            List<Paint.Draw.Point> points1 = new List<Paint.Draw.Point>() { new Paint.Draw.Point(10f, 10f), new Paint.Draw.Point(20f, 20f) };
            List<Paint.Draw.Point> points2 = new List<Paint.Draw.Point>() { new Paint.Draw.Point(50f, 50f), new Paint.Draw.Point(20f, 20f) };

            Path path = new Path(points1, new Paint.Draw.Color(1, 2, 3), 10f);
            Path path2 = new Path(points2, new Paint.Draw.Color(1, 2, 3), 10f);

            testDrawModel = new DrawModel(new List<Paint.Draw.Path>() { path, path2 }, new Paint.Draw.Color(1, 2, 3), 15f);
        }
        [Test]
        public void FileKeeperDroidTest()
        {
            file = new FileKeeperDroid();
            file.Save(testDrawModel);

            actualDrawModel = file.Load();

            Assert.AreEqual(testDrawModel.CurrentLineWidth, actualDrawModel.CurrentLineWidth);
            Assert.AreEqual(testDrawModel.CurrentColor, actualDrawModel.CurrentColor);
            Assert.AreEqual(testDrawModel.Paths.Count, actualDrawModel.Paths.Count);
        }



        [Test]
        public void RealmKeeperDroidTest()
        {
            realm = new RealmKeeper();
            realm.Save(testDrawModel);

            actualDrawModel = realm.Load();

            Assert.AreEqual(testDrawModel.CurrentLineWidth, actualDrawModel.CurrentLineWidth);
            Assert.AreEqual(testDrawModel.CurrentColor, actualDrawModel.CurrentColor);
            Assert.AreEqual(testDrawModel.Paths.Count, actualDrawModel.Paths.Count);
        }
        [Test]
        public void SQLiteKeeperDroidTest()
        {
            string pathOne = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string dbPath = System.IO.Path.Combine(pathOne, "ormbase.db3");

            sqlite = new Paint.Keeper.SQLiteKepper(dbPath);
            sqlite.Save(testDrawModel);

            actualDrawModel = sqlite.Load();

            Assert.AreEqual(testDrawModel.CurrentLineWidth, actualDrawModel.CurrentLineWidth);
            Assert.AreEqual(testDrawModel.CurrentColor, actualDrawModel.CurrentColor);
            Assert.AreEqual(testDrawModel.Paths.Count, actualDrawModel.Paths.Count);
        }


 

    }
}
