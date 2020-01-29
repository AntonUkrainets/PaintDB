using System;
using System.IO;
using Newtonsoft.Json;
using Paint.Draw;
using Paint.Keeper;

namespace Paint.Droid.Keeper
{
    public class FileKeeperDroid : IDrawKeeper
    {
        private const string UserDefaultsKey = "SaveModal.json";
        public DrawModel Load()
        {
            var backingFile = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), UserDefaultsKey);

            if (backingFile == null || !File.Exists(backingFile))
            {
                return null;
            }
            string strModel;
            using (var reader = new StreamReader(backingFile, true))
            {
                strModel = reader.ReadLine();
            }

            var drawModel = JsonConvert.DeserializeObject<DrawModel>(strModel);
            return drawModel;
        }



        public void Save(DrawModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException();
            }

            var strModel = JsonConvert.SerializeObject(model);
            var backingFile = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), UserDefaultsKey);
            using (var writer = File.CreateText(backingFile))
            {
               writer.WriteLine(strModel);
            }
        }


    }
}
