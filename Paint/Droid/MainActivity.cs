using Android.App;
using Android.Widget;
using Android.OS;
using Paint.Draw;
using Android.Support.V7.App;
using Android.Views;
using Paint.Keeper;
using Paint.Droid.Keeper;

namespace Paint.Droid
{
    [Activity(Label = "Paint", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : AppCompatActivity, IPaintViewDelegate
    {
        private IDrawKeeper _drawKeeper;
        private DrawLine _drawingLine;
        private DrawModel _drawModel;
        private Color colorPaint;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Main);

            var menu = FindViewById<@Android.Support.V7.Widget.Toolbar>(Resource.Id.menuForSave);
            SetSupportActionBar(menu);
            LinearLayout linear = FindViewById<LinearLayout>(Resource.Id.viewDraw);
            _drawingLine = new DrawLine(this);
            linear.AddView(_drawingLine);
            _drawingLine.Delegate = this;
            _drawModel = new DrawModel();

            InitButtons();
        }
      
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.ToolbarMenu, menu);

            return base.OnCreateOptionsMenu(menu);
        }
      
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            SaveDataToFile(item.ItemId);

            return true;
        }

        #region Save Data To File

        private bool SaveDataToFile(int itemId)
        {
            switch (itemId)
            {
                case Resource.Id.SaveToFile:
                    SaveFile(EDrawKeeperType.File);
                    return true;
                case Resource.Id.SaveToSharedPreferance:
                    SaveFile(EDrawKeeperType.Internal);
                    return true;
                case Resource.Id.SaveToRealm:
                    SaveFile(EDrawKeeperType.Realm);
                    return true;
                case Resource.Id.SaveToSQLite:
                    SaveFile(EDrawKeeperType.SQLite);
                    return true;
            }

            return false;
        }

        private void SaveFile(EDrawKeeperType saveType)
        {
            _drawKeeper = new DrawKeeperFactory().Create(saveType);
            _drawKeeper.Save(_drawModel);
        }

        #endregion

        #region Load Data From File

        private bool LoadDataFromFile(int itemId)
        {
            switch (itemId)
            {
                case Resource.Id.ClickLoadToFile:
                    LoadDataFromFile(EDrawKeeperType.File);
                    return true;
                case Resource.Id.ClickLoadToSharedPreferance:
                    LoadDataFromFile(EDrawKeeperType.Internal);
                    return true;
                case Resource.Id.ClickLoadToRealm:
                    LoadDataFromFile(EDrawKeeperType.Realm);
                    return true;
                case Resource.Id.ClickLoadToSQLite:
                    LoadDataFromFile(EDrawKeeperType.SQLite);
                    return true;
            }

            return false;
        }

        private void LoadDataFromFile(EDrawKeeperType loadType)
        {
            _drawKeeper = new DrawKeeperFactory().Create(loadType);
            LoadFile();
        }

        private void LoadFile()
        {
            try
            {
                _drawModel = _drawKeeper.Load();
                _drawingLine.UpdateView(_drawModel.Paths);
            }
            catch (System.Exception ex)
            {
                _drawModel = new DrawModel();
                _drawingLine.UpdateView(_drawModel.Paths);
            }
        }

        #endregion

        private void ClearButton_Clicked(object sender, System.EventArgs e)
        {
            _drawModel.Clear();
            _drawingLine.UpdateView(_drawModel.Paths);
        }

        private void UndoButton_Clicked(object sender, System.EventArgs e)
        {
            _drawModel.Back();
            _drawingLine.UpdateView(_drawModel.Paths);
        }

        private void GreenColorButton_Clicked(object sender, System.EventArgs e)
        {
            colorPaint = new Color(10, 252, 10);
            _drawModel.CurrentColor = colorPaint;
        }

        private void YellowColorButton_Clicked(object sender, System.EventArgs e)
        {
            colorPaint = new Color(255, 255, 0);
            _drawModel.CurrentColor = colorPaint;
        }

        private void RedColorButton_Clicked(object sender, System.EventArgs e)
        {
            colorPaint = new Color(255, 0, 0);
            _drawModel.CurrentColor = colorPaint;
        }

        private void ThickButton_Clicked(object sender, System.EventArgs e)
        {
             _drawModel.CurrentLineWidth = 60f;
        }

        private void MediumButton_Clicked(object sender, System.EventArgs e)
        {
            _drawModel.CurrentLineWidth = 40f;
        }

        private void ThinButton_Clicked(object sender, System.EventArgs e)
        {
            _drawModel.CurrentLineWidth = 20f;
        }

        public void PathStartedAt(Point point)
        {
            _drawModel.StartPath(point);
        }

        public void PathAppendedAt(Point point)
        {
            _drawModel.AppendPath(point);
            _drawingLine.UpdateView(_drawModel.Paths);
        }

        private void InitButtons()
        {
            FindViewById<Button>(Resource.Id.ThinButton).Click += ThinButton_Clicked;
            FindViewById<Button>(Resource.Id.MediumButton).Click += MediumButton_Clicked;
            FindViewById<Button>(Resource.Id.ThickButton).Click += ThickButton_Clicked;

            FindViewById<Button>(Resource.Id.RedColorButton).Click += RedColorButton_Clicked;
            FindViewById<Button>(Resource.Id.YellowColorButton).Click += YellowColorButton_Clicked;
            FindViewById<Button>(Resource.Id.GreenColorButton).Click += GreenColorButton_Clicked;

            FindViewById<ImageButton>(Resource.Id.UndoButton).Click += UndoButton_Clicked;
            FindViewById<ImageButton>(Resource.Id.ClearButton).Click += ClearButton_Clicked;
        }
    }
}