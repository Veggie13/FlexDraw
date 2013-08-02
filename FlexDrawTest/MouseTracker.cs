using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlexDraw;
using System.Drawing;

namespace FlexDrawTest
{
    class MouseTracker : IDrawable
    {
        public void Draw(IDrawAPI api)
        {
            PointD p1 = new PointD(-0.5, -0.5);
            PointD p2 = new PointD(0.5, 0.5);
            api.FillRectangle(new RectangleD(p1, p2), Color.Pink);
        }

        public bool Visible
        {
            get { return true; }
        }

        PointD _origin = new PointD(0, 0);
        public PointD Origin
        {
            get { return _origin; }
            set
            {
                _lastBounds.Copy(Bounds);
                _origin.Copy(value);
                if (Modified != null)
                    Modified(this);
            }
        }

        public RectangleD Bounds
        {
            get { return (new RectangleD(-0.5, 0.5, 0.5, -0.5)).Offset(_origin); }
        }

        RectangleD _lastBounds = new RectangleD();
        public RectangleD LastBounds
        {
            get { return _lastBounds; }
        }

        public event DrawableModifiedEvent Modified;
    }
}
