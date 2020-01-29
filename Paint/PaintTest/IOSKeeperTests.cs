using System;
using System.Collections.Generic;
using System.Drawing;
using NUnit.Framework;
using Paint.Draw;
using Paint.iOS.Keeper;
using Paint.Keeper;

namespace PaintTest
{
    class IOSKeeperTests
    {
        FileKeeperIOS file;
        InternalKeeper internalKeeper;
        DrawKeeperFactory factory;
        RealmKeeper realm1;
      
        Paint.Keeper.SQLiteKepper sQLite;

        DrawModel testDrawModel;
        DrawModel actualDrawModel;

        [SetUp]
        public void Setup()
        {
            List<Paint.Draw.Point> points1 = new List<Paint.Draw.Point>() { new Paint.Draw.Point(10f, 10f), new Paint.Draw.Point(20f, 20f) };
            List<Paint.Draw.Point> points2 = new List<Paint.Draw.Point>() { new Paint.Draw.Point(50f, 50f), new Paint.Draw.Point(20f, 20f) };

            Path path = new Paint.Draw.Path(points1, new Paint.Draw.Color(1, 2, 3), 10f);
            Path path2 = new Paint.Draw.Path(points2, new Paint.Draw.Color(1, 2, 3), 10f);

            testDrawModel = new DrawModel(new List<Paint.Draw.Path>() { path, path2 }, new Paint.Draw.Color(1, 2, 3), 15f);


            string pathOne = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string dbPath = System.IO.Path.Combine(pathOne, "ormbase.db3");

            sQLite = new Paint.Keeper.SQLiteKepper(dbPath);
        }

       


        [Test]
        public void RealmKeeperIOSTest()
        {
            realm1 = new RealmKeeper();
            realm1.Save(testDrawModel);
            actualDrawModel = realm1.Load();

            Assert.AreEqual(testDrawModel.CurrentLineWidth, actualDrawModel.CurrentLineWidth);
            Assert.AreEqual(testDrawModel.CurrentColor, actualDrawModel.CurrentColor);
            Assert.AreEqual(testDrawModel.Paths.Count, actualDrawModel.Paths.Count);
        }


        [Test]
        public void FileKeeperIOSTest()
        {
            file = new FileKeeperIOS();
            file.Save(testDrawModel);

            actualDrawModel = file.Load();

            Assert.AreEqual(testDrawModel.CurrentLineWidth, actualDrawModel.CurrentLineWidth);
            Assert.AreEqual(testDrawModel.CurrentColor, actualDrawModel.CurrentColor);
            Assert.AreEqual(testDrawModel.Paths.Count, actualDrawModel.Paths.Count);
        }


        [Test]
        public void SQliteKeeperIOSTest()
        {

            sQLite.Save(testDrawModel);

            actualDrawModel = sQLite.Load();

            Assert.AreEqual(testDrawModel.CurrentLineWidth, actualDrawModel.CurrentLineWidth);
            Assert.AreEqual(testDrawModel.CurrentColor, actualDrawModel.CurrentColor);
            Assert.AreEqual(testDrawModel.Paths.Count, actualDrawModel.Paths.Count);
        }
       
       

  
    }
}