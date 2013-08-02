using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using FlexDraw;

namespace FlexDrawTest
{
    class Background : IDrawable
    {
        public void Draw(IDrawAPI api)
        {
            api.FillRectangle(RectangleD.EverywhereYUp, Color.Black);
        }

        public bool Visible
        {
            get { return true; }
        }

        public PointD Origin
        {
            get { return new PointD(0, 0); }
        }

        public RectangleD Bounds
        {
            get { return new RectangleD(RectangleD.EverywhereYUp); }
        }

        public RectangleD LastBounds
        {
            get { return new RectangleD(RectangleD.EverywhereYUp); }
        }

        public event DrawableModifiedEvent Modified;
    }
}
