using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlexDraw
{
    public delegate void DrawableModifiedEvent(IDrawable sender);
    public interface IDrawable
    {
        void Draw(IDrawAPI api);

        bool Visible { get; }
        PointD Origin { get; }
        RectangleD Bounds { get; }
        RectangleD LastBounds { get; }

        event DrawableModifiedEvent Modified;
    }
}
