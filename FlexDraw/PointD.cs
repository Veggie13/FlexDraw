using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlexDraw
{
    public struct PointD
    {
        public PointD(double x, double y) { X = x; Y = y; }
        
        public void Copy(PointD other)
        {
            X = other.X;
            Y = other.Y;
        }

        public PointD Offset(PointD pt)
        {
            return new PointD(X + pt.X, Y + pt.Y);
        }

        public void Subtract(PointD pt, out double dx, out double dy)
        {
            dx = X - pt.X;
            dy = Y - pt.Y;
        }

        public double X;
        public double Y;
    }

}
