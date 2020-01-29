using CoreGraphics;
using System;
using System.Collections.Generic;
using Paint.Draw;
using Paint.iOS.Extensions;
using UIKit;

namespace Paint.iOS
{
    public partial class PaintView : UIView
    {
        private List<Path> _paths = new List<Path>();
        public IPaintViewDelegate Delegate { get; set; }

        public PaintView(IntPtr handle) : base(handle)
        {
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();
            var tapGestureRecognizer = new UITapGestureRecognizer(Tap);
            var panGestureRecogrnizer = new UIPanGestureRecognizer(Pan);

            AddGestureRecognizer(tapGestureRecognizer);
            AddGestureRecognizer(panGestureRecogrnizer);
        }

        public void UpdateView(List<Path> paths)
        {
            _paths = paths;
            SetNeedsDisplay();
        }

       private void Tap(UITapGestureRecognizer tapGestureRecognizer)
        {
            var point = tapGestureRecognizer.LocationInView(this).GetPoint();
            Delegate?.PathStartedAt(point);
            Delegate?.PathAppendedAt(new Point(point.X, point.Y));
        }

        private void Pan(UIPanGestureRecognizer panGestureRecognizer)
        {
            var point = panGestureRecognizer.LocationInView(this).GetPoint();
            if (panGestureRecognizer.State == UIGestureRecognizerState.Began)
                Delegate?.PathStartedAt(point);
            else
                Delegate?.PathAppendedAt(point);
        }
        
        public override void Draw(CGRect rect)
        {
            if (_paths == null)
                return;

            foreach (var pathFromModel in _paths)
            {
                if (pathFromModel?.Points == null || pathFromModel.Points.Count < 2)
                    continue;

                pathFromModel.Color.GetColor().SetStroke();

                var path = new UIBezierPath
                {
                    LineCapStyle = CGLineCap.Round,
                    LineJoinStyle = CGLineJoin.Round,
                    LineWidth = pathFromModel.LineWidth
                };

                path.MoveTo(pathFromModel.Points[0].GetPoint());
                for (var i = 0; i < pathFromModel.Points.Count; i++)
                {
                    var point = pathFromModel.Points[i].GetPoint();
                    path.AddLineTo(point);
                }

                path.Stroke();
            }
        }
    }
}