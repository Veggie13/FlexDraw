using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace FlexDraw
{
    public interface IDrawSurface
    {
        ICollection<IDrawable> Items { get; }
        ICollection<IDrawViewport> Viewports { get; }
    }

    public class DrawSurface : IDrawSurface
    {
        public DrawSurface()
        {
            _items.CollectionChanged += new NotifyCollectionChangedEventHandler(_items_CollectionChanged);
        }

        void _items_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
                foreach (IDrawable d in e.NewItems)
                    d.Modified += new DrawableModifiedEvent(DrawableModified);
            else if (e.Action == NotifyCollectionChangedAction.Remove)
                foreach (IDrawable d in e.OldItems)
                    d.Modified -= DrawableModified;
        }

        void DrawableModified(IDrawable sender)
        {
            foreach (IDrawViewport view in Viewports)
                if (sender.Bounds.Intersection(view.View) != null ||
                    sender.LastBounds.Intersection(view.View) != null)
                    Draw(view);
        }

        #region IDrawSurface
        private ObservableCollection<IDrawable> _items = new ObservableCollection<IDrawable>();
        public ICollection<IDrawable> Items
        {
            get { return _items; }
        }

        private List<IDrawViewport> _views = new List<IDrawViewport>();
        public ICollection<IDrawViewport> Viewports
        {
            get { return _views; }
        }

        public void Draw()
        {
            foreach (IDrawViewport view in Viewports)
            {
                Draw(view);
            }
        }
        #endregion

        private void Draw(IDrawViewport view)
        {
            view.BeginDraw();
            foreach (IDrawable item in Items)
                view.Draw(item);
            view.EndDraw();
        }
    }
}
