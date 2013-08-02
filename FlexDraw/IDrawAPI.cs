using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace FlexDraw
{
    public interface IDrawAPI
    {
        void DrawPixel(PointD pt, Color c);
        void FillRectangle(RectangleD rect, Color c);
        void FillEllipse(RectangleD rect, Color c);
        void DrawLine(PointD pt1, PointD pt2, Color c);
    }
}
