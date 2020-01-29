using System;
using System.IO;
using Newtonsoft.Json;
using Paint.Draw;
using Paint.Keeper;

namespace Paint.iOS.Keeper
{
    public class FileKeeperIOS : IDrawKeeper
    {
        private const string UserDefaultsKey = "SaveModal.json";
        public DrawModel Load()
        {
            var strModel = File.ReadAllText(GetFileName());
            var drawModel = JsonConvert.DeserializeObject<DrawModel>(strModel);

            return drawModel;
        }

        public void Save(DrawModel model)
        {
            if (model == null)
                throw new ArgumentNullException();

            var strModel = JsonConvert.SerializeObject(model);
            var fileName = GetFileName();

            File.WriteAllText(fileName, strModel);
        }

        private string GetFileName()
        {
            var documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            return System.IO.Path.Combine(documents, UserDefaultsKey);
        }
    }
}