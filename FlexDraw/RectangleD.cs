using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlexDraw
{
    public class RectangleD
    {
        public static readonly RectangleD EverywhereYUp =
            new RectangleD(double.MinValue, double.MaxValue, double.MaxValue, double.MinValue);
        public static readonly RectangleD EverywhereYDown =
            new RectangleD(double.MinValue, double.MaxValue, double.MinValue, double.MaxValue);

        public RectangleD() { }
        
        public RectangleD(double x1, double x2, double y1, double y2)
        {
            _left = Math.Min(x1, x2);
            _right = Math.Max(x1, x2);
            _top = Math.Max(y1, y2);
            _bottom = Math.Min(y1, y2);
        }

        public RectangleD(PointD p1, PointD p2)
        {
            _left = Math.Min(p1.X, p2.X);
            _right = Math.Max(p1.X, p2.X);
            _top = Math.Max(p1.Y, p2.Y);
            _bottom = Math.Min(p1.Y, p2.Y);
        }

        public RectangleD(RectangleD other)
        {
            _left = other._left;
            _right = other._right;
            _top = other._top;
            _bottom = other._bottom;
        }

        public void Copy(RectangleD other)
        {
            _left = other._left;
            _right = other._right;
            _top = other._top;
            _bottom = other._bottom;
        }

        public RectangleD Offset(PointD o)
        {
            return new RectangleD(_left + o.X, _right + o.X, _top + o.Y, _bottom + o.Y);
        }

        private void FlipXOrientation()
        {
            double temp = _left;
            _left = _right;
            _right = temp;
        }

        private void FlipYOrientation()
        {
            double temp = _top;
            _top = _bottom;
            _bottom = temp;
        }

        public RectangleD Intersection(RectangleD other)
        {
            double left = Math.Max(Left, other.Left);
            double right = Math.Min(Right, other.Right);
            double bottom = Math.Max(Bottom, other.Bottom);
            double top = Math.Min(Top, other.Top);

            if (right < left || top < bottom)
                return null;
            return new RectangleD(left, right, top, bottom);
        }

        public PointD TopLeft
        {
            get { return new PointD(_left, _top); }
        }

        public PointD TopRight
        {
            get { return new PointD(_right, _top); }
        }

        public PointD BottomLeft
        {
            get { return new PointD(_left, _bottom); }
        }

        public PointD BottomRight
        {
            get { return new PointD(_right, _bottom); }
        }

        public double Width
        {
            get { return _right - _left; }
        }

        public double Height
        {
            get { return _top - _bottom; }
        }

        private double _left;
        public double Left
        {
            get { return _left; }
            set
            {
                _left = value;
                if (Width < 0)
                    FlipXOrientation();
            }
        }

        private double _right;
        public double Right
        {
            get { return _right; }
            set
            {
                _right = value;
                if (Width < 0)
                    FlipXOrientation();
            }
        }

        private double _top;
        public double Top
        {
            get { return _top; }
            set
            {
                _top = value;
                if (Height < 0)
                    FlipYOrientation();
            }
        }

        private double _bottom;
        public double Bottom
        {
            get { return _bottom; }
            set
            {
                _bottom = value;
                if (Height < 0)
                    FlipYOrientation();
            }
        }

    }
}
