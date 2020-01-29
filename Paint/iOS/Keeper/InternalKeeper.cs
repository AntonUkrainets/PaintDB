using System;
using Foundation;
using Newtonsoft.Json;
using Paint.Draw;
using Paint.Keeper;

namespace Paint.iOS.Keeper
{
    public class InternalKeeper : IDrawKeeper
    {
        private const string UserDefaultsKey = "DrawModel";
        public void Save(DrawModel model)
        {
            if (model == null)
                throw new ArgumentNullException();

            var strModel = JsonConvert.SerializeObject(model);
            
            //Core
            NSUserDefaults.StandardUserDefaults.SetString(strModel, UserDefaultsKey);
            NSUserDefaults.StandardUserDefaults.Synchronize();
        }

        public DrawModel Load()
        {
            var strModel = NSUserDefaults.StandardUserDefaults.StringForKey(UserDefaultsKey);
            var drawModel = JsonConvert.DeserializeObject<DrawModel>(strModel);

            return drawModel;
        }
    }
}