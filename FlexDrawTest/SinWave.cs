using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using FlexDraw;

namespace FlexDrawTest
{
    class SinWave : IDrawable
    {
        public void Draw(IDrawAPI api)
        {
            double step = 4 * Math.PI / 1000;
            for (double x = -2 * Math.PI; x < 2 * Math.PI; x += step)
            {
                double y = Math.Sin(x);
                api.DrawPixel(new PointD(x, y), Color.Red);
            }
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
            get { return new RectangleD(-2 * Math.PI, 2 * Math.PI, 1, -1); }
        }

        public RectangleD LastBounds
        {
            get { return new RectangleD(-2 * Math.PI, 2 * Math.PI, 1, -1); }
        }

        public event DrawableModifiedEvent Modified;
    }
}
