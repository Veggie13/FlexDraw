using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace FlexDraw
{
    public class GCViewport : IDrawViewport
    {
        private Rectangle _window = new Rectangle();
        public Rectangle Window
        {
            get { return _window; }
            set { _window = value; }
        }

        private Graphics _gc;
        public Graphics GC
        {
            get { return _gc; }
            set { _gc = value; }
        }

        #region IDrawViewport
        private IDrawSurface _surface;
        public IDrawSurface Surface
        {
            get { return _surface; }
            set { _surface = value; }
        }

        private RectangleD _view = new RectangleD();
        public RectangleD View
        {
            get { return _view; }
            set { _view = value; }
        }

        private PointD _curOrigin = new PointD();
        public PointD CurrentOrigin
        {
            get { return _curOrigin; }
            set { _curOrigin.Copy(value); }
        }

        private bool _upY = false;
        public bool IsYUp
        {
            get { return _upY; }
            set { _upY = value; }
        }

        public bool Ready
        {
            get { return _gc != null; }
        }

        private List<IDrawable> _toDraw = new List<IDrawable>();
        public void BeginDraw()
        {
            _toDraw.Clear();
        }

        public void Draw(IDrawable item)
        {
            if (item.Visible && item.Bounds.Intersection(View) != null)
            {
                _toDraw.Add(item);
            }
        }

        public void EndDraw()
        {
            if (InDrawingThread && Ready)
            {
                foreach (IDrawable item in _toDraw)
                {
                    CurrentOrigin = item.Origin;
                    try
                    {
                        item.Draw(this);
                    }
                    catch (Exception e)
                    {
                        return;
                    }
                }
            }
        }

        #region IDrawAPI Implementation
        public void DrawPixel(PointD pt, Color c)
        {
            Point loc = Transform(pt.Offset(CurrentOrigin));
            SolidBrush b = new SolidBrush(c);
            _gc.FillRectangle(b, loc.X, loc.Y, 1, 1);
        }

        public void FillRectangle(RectangleD rect, Color c)
        {
            RectangleD r = rect.Offset(CurrentOrigin).Intersection(View);
            if (r == null)
                return;
            Rectangle o = Transform(r);
            SolidBrush b = new SolidBrush(c);
            _gc.FillRectangle(b, o);
        }

        public void FillEllipse(RectangleD rect, Color c)
        {
            Rectangle o = Transform(rect.Offset(CurrentOrigin));
            SolidBrush b = new SolidBrush(c);
            _gc.FillEllipse(b, o);
        }

        public void DrawLine(PointD pt1, PointD pt2, Color c)
        {
            Pen p = new Pen(c);
            _gc.DrawLine(p, Transform(pt1.Offset(CurrentOrigin)), Transform(pt2.Offset(CurrentOrigin)));
        }
        #endregion
        #endregion

        protected virtual bool InDrawingThread
        {
            get { return true; }
        }

        protected Point Transform(PointD pt)
        {
            double xPortion = (pt.X - View.Left) / View.Width;
            double yPortion = (View.Top - pt.Y) / View.Height;
            int xPixel = _window.Left + (int)Math.Round(xPortion * (double)_window.Width);
            int yPixel = _upY ?
                (_window.Top + (int)Math.Round(yPortion * (double)_window.Height)) :
                (_window.Bottom - (int)Math.Round(yPortion * (double)_window.Height));
            return new Point(xPixel, yPixel);
        }

        public PointD Transform(Point pt)
        {
            double xPortion = (double)(pt.X - _window.Left) / (double)_window.Width;
            double yPortion = (double)(pt.Y - _window.Top) / (double)_window.Height;
            double xCoord = View.Left + xPortion * View.Width;
            double yCoord = View.Top - (_upY ? yPortion : (1 - yPortion)) * View.Height;
            return new PointD(xCoord, yCoord);
        }

        protected Rectangle Transform(RectangleD r)
        {
            if (r == null)
                return new Rectangle();
            Point tl = Transform(_upY ? r.TopLeft : r.BottomLeft);
            Point br = Transform(_upY ? r.BottomRight : r.TopRight);
            return new Rectangle(tl.X, tl.Y, br.X - tl.X, br.Y - tl.Y);
        }
    }
}
