using System;
using System.Linq;
using Newtonsoft.Json;
using Paint.Draw;
using Paint.Model;
using Realms;

namespace Paint.Keeper
{
    public class RealmKeeper : IDrawKeeper
    {
        public DrawModel Load()
        {
            var realm = Realm.GetInstance();
            var tmpDraws = realm.All<DrawRealm>().ToList();
            if(tmpDraws.Count() > 0)
            {
                var draws = tmpDraws.Last();
                if(draws != null)
                {
                    var drawModel = JsonConvert.DeserializeObject<DrawModel>(draws.DrowModelJsong);
                    return drawModel;
                }

                return null;
            }
            return null;
        }

        public void Save(DrawModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException();
            }
            var realm = Realm.GetInstance();
            var strModel = JsonConvert.SerializeObject(model);
            DrawRealm draw = new DrawRealm();
            draw.DrowModelJsong = strModel;
            realm.Write(() =>
            {
                realm.Add(draw);
            });
        }
    }
}
