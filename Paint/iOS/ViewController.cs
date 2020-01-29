using System;
using Paint.Draw;
using Paint.iOS.Extensions;
using Paint.iOS.Keeper;
using Paint.Keeper;
using UIKit;

namespace Paint.iOS
{
    public partial class ViewController : UIViewController, IPaintViewDelegate
    {
        private DrawModel _drawModel;
        private IDrawKeeper _drawKeeper;
        public ViewController(IntPtr handle) : base(handle) { }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            _drawModel = new DrawModel();
            _paintView.Delegate = this;
            
            _btnClear.TouchUpInside += BtnClearTouchUpInside;
            _btnBack.TouchUpInside += BtnBackTouchUpInside;
            _btnThin.TouchUpInside += BtnThinTouchUpInside;
            _btnMedium.TouchUpInside += BtnMediumTouchUpInside;
            _btnThick.TouchUpInside += BtnThickTouchUpInside;

            _btnRed.TouchUpInside += BtnRedTouchUpInside;
            _btnYellow.TouchUpInside += BtnYellowTouchUpInside;
            _btnGreen.TouchUpInside += BtnGreenTouchUpInside;
            
            _btnSave.TouchUpInside += BtnSaveOnTouchUpInside;
            _btnLoad.TouchUpInside += BtnLoadOnTouchUpInside;
        }

        private void BtnLoadOnTouchUpInside(object sender, EventArgs e)
        {
            ShowLoadFromFileAlertDialog(sender);
        }

        #region Load From File Alert Dialog

        private void ShowLoadFromFileAlertDialog(object sender)
        {
            var alertDialog = CreateLoadFromFileAlertDialog(sender);

            PresentViewController(alertDialog, animated: true, completionHandler: null);
        }

        private UIAlertController CreateLoadFromFileAlertDialog(object sender)
        {
            var alertDialog = UIAlertController.Create("Load", "Select load paint", UIAlertControllerStyle.Alert);
            if (alertDialog.PopoverPresentationController != null)
                alertDialog.PopoverPresentationController.BarButtonItem = sender as UIBarButtonItem;

            alertDialog.AddAction(UIAlertAction.Create("Cancel", UIAlertActionStyle.Cancel, null));

            var loadFromFileAction = CreateLoadImageAction("Load from File", EDrawKeeperType.File);
            var loadFromRealmAction = CreateLoadImageAction("Load from Realm", EDrawKeeperType.Realm);
            var loadFromSQLiteAction = CreateLoadImageAction("Load from SQLite", EDrawKeeperType.SQLite);
            var loadFromInternalAction = CreateLoadImageAction("Load from NSUserDefault", EDrawKeeperType.Internal);

            alertDialog.AddAction(loadFromFileAction);
            alertDialog.AddAction(loadFromRealmAction);
            alertDialog.AddAction(loadFromSQLiteAction);
            alertDialog.AddAction(loadFromInternalAction);

            return alertDialog;
        }

        #endregion

        private UIAlertAction CreateLoadImageAction(string message, EDrawKeeperType loadType)
        {
            var loadAction = UIAlertAction.Create(message, UIAlertActionStyle.Default, action => {
                _drawKeeper = new DrawKeeperFactory().Create(loadType);
                LoadFile();
            });

            return loadAction;
        }

        private void BtnSaveOnTouchUpInside(object sender, EventArgs e)
        {
            ShowSaveFileAlertDialog(sender);
        }

        #region Save File Alert Dialog

        private void ShowSaveFileAlertDialog(object sender)
        {
            var alertDialog = CreateSaveFileAlertDialog(sender);

            PresentViewController(alertDialog, animated: true, completionHandler: null);
        }

        private UIAlertController CreateSaveFileAlertDialog(object sender)
        {
            var alertDialog = UIAlertController.Create("Save", "Select save paint", UIAlertControllerStyle.Alert);
            if (alertDialog.PopoverPresentationController != null)
                alertDialog.PopoverPresentationController.BarButtonItem = sender as UIBarButtonItem;

            alertDialog.AddAction(UIAlertAction.Create("Cancel", UIAlertActionStyle.Cancel, null));

            var saveToFileAction = CreateSaveToFileAction("Save to File", EDrawKeeperType.File);
            var saveToRealmAction = CreateSaveToFileAction("Save to Realm", EDrawKeeperType.Realm);
            var saveToSQLiteAction = CreateSaveToFileAction("Save to SQLite", EDrawKeeperType.SQLite);
            var saveToInternalAction = CreateSaveToFileAction("Save to Internal", EDrawKeeperType.Internal);

            alertDialog.AddAction(saveToFileAction);
            alertDialog.AddAction(saveToRealmAction);
            alertDialog.AddAction(saveToSQLiteAction);
            alertDialog.AddAction(saveToInternalAction);

            return alertDialog;
        }

        private UIAlertAction CreateSaveToFileAction(string message, EDrawKeeperType loadType)
        {
            var loadAction = UIAlertAction.Create(message, UIAlertActionStyle.Default, action =>
            {
                _drawKeeper = new DrawKeeperFactory().Create(loadType);
                _drawKeeper.Save(_drawModel);
            });

            return loadAction;
        }

        #endregion

        private void LoadFile()
        {
            try
            {
                _drawModel = _drawKeeper.Load();
                _paintView.UpdateView(_drawModel.Paths);
            }
            catch
            {
                _drawModel = new DrawModel();
                _paintView.UpdateView(_drawModel.Paths);
            }
        }

        private void BtnBackTouchUpInside(object sender, EventArgs e)
        {
            _drawModel.Back();
            _paintView.UpdateView(_drawModel.Paths);
        }

        private void BtnGreenTouchUpInside(object sender, EventArgs e)
        {
            _drawModel.CurrentColor = UIColor.Green.GetColor();
        }

        private void BtnYellowTouchUpInside(object sender, EventArgs e)
        {
            _drawModel.CurrentColor = UIColor.Yellow.GetColor();
        }

        private void BtnRedTouchUpInside(object sender, EventArgs e)
        {
            _drawModel.CurrentColor = UIColor.Red.GetColor();
        }

        private void BtnThickTouchUpInside(object sender, EventArgs e)
        {
            _drawModel.CurrentLineWidth = 20;
        }

        private void BtnMediumTouchUpInside(object sender, EventArgs e)
        {
            _drawModel.CurrentLineWidth = 10;
        }

        private void BtnThinTouchUpInside(object sender, EventArgs e)
        {
            _drawModel.CurrentLineWidth = 5;
        }

        private void BtnClearTouchUpInside(object sender, EventArgs e)
        {
            _drawModel.Clear();
            _paintView.UpdateView(_drawModel.Paths);
        }

        public void PathStartedAt(Point point)
        {
            _drawModel.StartPath(point);
        }

        public void PathAppendedAt(Point point)
        {
           _drawModel.AppendPath(point);
           _paintView.UpdateView(_drawModel.Paths);
        }
    }
}