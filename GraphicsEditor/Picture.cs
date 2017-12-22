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

        public IReadOnlyList<IFigure> Figures {
            get
            {
                lock (_lockObject)
                {
                    IReadOnlyList<IFigure> figures = _drawables?.Where(shape =>
                    {
                        Type[] type = shape.GetType().GetInterfaces();
                        if (type.Contains(typeof(IFigure)))
                        {
                            return true;
                        }
                        return false;
                    }).Cast<IFigure>().ToList();

                    return figures;
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
                try
                {
                    _drawables.RemoveAt(index);
                    Changed?.Invoke();
                }
                catch (ArgumentOutOfRangeException e)
                {                   
                    Console.WriteLine(e.Message);
                }
                
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