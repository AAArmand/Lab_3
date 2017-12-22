using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DrawablesUI;
using GraphicsEditor.Figures.Data.Interfaces;

namespace GraphicsEditor
{
    class Picture : IDrawable
    {
        private readonly List<IDrawable> _drawables = new List<IDrawable>();
        private readonly object _lockObject = new object();

        public IEnumerable<IShape> Shapes
        {
            get
            {
                lock (_lockObject)
                {
                    IEnumerable<IShape> shapes = _drawables?.Where(shape =>
                    {
                        Type[] type = shape.GetType().GetInterfaces();
                        if (type.Contains(typeof(IShape)))
                        {
                            return true;
                        }
                        return false;
                    }).Cast<IShape>();

                    return shapes;
                }
            }
        }

        public IEnumerable<IFigure> Figures {
            get
            {
                lock (_lockObject)
                {
                    IEnumerable<IFigure> figures = _drawables?.Where(shape =>
                    {
                        Type[] type = shape.GetType().GetInterfaces();
                        if (type.Contains(typeof(IFigure)))
                        {
                            return true;
                        }
                        return false;
                    }).Cast<IFigure>();

                    return figures;
                }
            }
        }

        public IEnumerable<int> ShapesIndexes {
            get {
                lock (_lockObject) {                        
                    return Enumerable.Range(0, _drawables.Count);
                }
            }
        }

        public event Action Changed;

        public void Remove(IDrawable shape)
        {
            lock (_lockObject)
            {
                _drawables.Remove(shape);
            }
        }

        public void RemoveAt(int index)
        {
            lock (_lockObject)
            {
                _drawables.RemoveAt(index);
                Changed?.Invoke();
            }
        }

        public void Add(IDrawable shape)
        {
            lock (_lockObject)
            {
                _drawables.Add(shape);
                Changed?.Invoke();
            }
        }

        public void Add(int index, IDrawable shape)
        {
            lock (_lockObject)
            {
                _drawables.Insert(index, shape);
                Changed?.Invoke();
            }
        }

        public void OnChanged() {
            lock (_lockObject)
            {
                Changed?.Invoke();
            }
        }

        public void Draw(IDrawer drawer)
        {
            lock (_lockObject)
            {
                foreach (var shape in _drawables)
                {
                    shape.Draw(drawer);
                }
            }
        }
    }
}