using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace FlexDraw
{
    public interface IDrawViewport : IDrawAPI
    {
        IDrawSurface Surface { get; set; }
        RectangleD View { get; set; }

        PointD CurrentOrigin { get; set; }
        bool IsYUp { get; set; }
        bool Ready { get; }

        void BeginDraw();
        void Draw(IDrawable item);
        void EndDraw();
    }
}
