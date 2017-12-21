using System;
using System.Collections.Generic;
using DrawablesUI;

namespace GraphicsEditor
{
    public class Picture : IDrawable
    {
        private readonly List<IDrawable> _shapes = new List<IDrawable>();
        private readonly object _lockObject = new object();
        
        public IEnumerable<IDrawable> Shapes { get
        {
            lock (_lockObject)
            {
                return _shapes;
            }
        } }

        public event Action Changed;

        public void Remove(IDrawable shape)
        {
            lock (_lockObject)
            {              
                _shapes.Remove(shape);
            }
        }

        public void RemoveAt(int index)
        {
            lock (_lockObject)
            {
                _shapes.RemoveAt(index);
                Changed?.Invoke();
            }
        }

        public void Add(IDrawable shape)
        {
            lock (_lockObject)
            {
                _shapes.Add(shape);
                Changed?.Invoke();
            }
        }

        public void Add(int index, IDrawable shape)
        {
            lock (_lockObject)
            {
                _shapes.Insert(index, shape);
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
                foreach (var shape in _shapes)
                {
                    shape.Draw(drawer);
                }
            }
        }
    }
}