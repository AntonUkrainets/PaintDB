using System;
using System.Collections.Generic;
using Android.Content;
using Android.Graphics;
using Android.Runtime;
using Android.Views;
using Android.Util;

namespace Paint.Droid
{
    class DrawLine : View
    {
        Android.Graphics.Paint paint;
        private List<Draw.Path> _paths = new List<Draw.Path>();
        public IPaintViewDelegate Delegate { get; set; }

        #region Constructor

        public DrawLine(Context context) : base(context)
        {
            paint = new Android.Graphics.Paint();
        }
        public DrawLine(Context context, IAttributeSet attrs) : base(context, attrs)
        {
        }

        public DrawLine(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
        }

        public DrawLine(Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes) : base(context, attrs, defStyleAttr, defStyleRes)
        {
        }

        protected DrawLine(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        protected override void DispatchDraw(Canvas canvas)
        {
            base.DispatchDraw(canvas);
        }
        #endregion

        public override void Draw(Canvas canvas)
        {
            paint = new Android.Graphics.Paint();
            paint.SetStyle(Android.Graphics.Paint.Style.Stroke);
            paint.StrokeCap = Android.Graphics.Paint.Cap.Round;
            paint.StrokeJoin = Android.Graphics.Paint.Join.Round;

            if (_paths == null)
            {
                return;
            }
            foreach (var pathFromModel in _paths)
            {
                if (pathFromModel?.Points == null || pathFromModel.Points.Count < 2)
                {
                    continue;
                }
                paint.SetARGB(255, pathFromModel.Color.Red, pathFromModel.Color.Green, pathFromModel.Color.Blue);
                paint.StrokeWidth = pathFromModel.LineWidth;
                var path = new Android.Graphics.Path();
                path.MoveTo(pathFromModel.Points[0].X, pathFromModel.Points[0].Y);
                for (int i = 1; i < pathFromModel.Points.Count; i++)
                {
                    path.LineTo(pathFromModel.Points[i].X, pathFromModel.Points[i].Y);
                }

                canvas.DrawPath(path, paint);
            }
        }
        public override bool OnTouchEvent(MotionEvent e)
        {
            float touchX = e.GetX();
            float touchY = e.GetY();

            switch (e.ActionMasked)
            {
                case MotionEventActions.Down:
                    var point = new Paint.Draw.Point(touchX, touchY);
                    Delegate?.PathStartedAt(point);
                    Delegate?.PathAppendedAt(new Paint.Draw.Point(point.X, point.Y));
                    break;
                case MotionEventActions.Move:
                    var pointMove = new Paint.Draw.Point(e.GetX(), e.GetY());
                    Delegate?.PathAppendedAt(pointMove);
                    break;
                default:
                    return false;
            }
            Invalidate();
            return true;
        }
        public void UpdateView(List<Draw.Path> paths)
        {
            _paths = paths;
            Invalidate();
        }
    }
}