using System;
using Paint.Draw;
using Paint.Keeper;
using Android.Content;
using Android.Preferences;
using Newtonsoft.Json;

namespace Paint.Droid.Keeper
{
    public class InternalKeeper : IDrawKeeper
    {
        private const string UserDefaultsKey = "DrawModel";
        Context mContext = Android.App.Application.Context;
        string strModel;
        public DrawModel Load()
        {
            var prefs = PreferenceManager.GetDefaultSharedPreferences(mContext);
            var mString = prefs.GetString(UserDefaultsKey, "");
            var drawModel = JsonConvert.DeserializeObject<DrawModel>(mString);
            return drawModel;
        }

        public void Save(DrawModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException();
            }

            strModel = JsonConvert.SerializeObject(model);

            //Core          
            ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(mContext);
            ISharedPreferencesEditor editor = prefs.Edit();
            editor.PutString(UserDefaultsKey, strModel);
            editor.Apply();
        }
    }
}
