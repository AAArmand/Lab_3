using System;
using System.Collections.Generic;
using System.Linq;
using DrawablesUI;
using GraphicsEditor.Figures.Data.Interfaces;

namespace GraphicsEditor
{
    public class Picture : IContainer<IDrawable> {      
        private static readonly List<IDrawable> Drawables = new List<IDrawable>();
        private readonly object _lockObject = new object();

        public event Action Changed;

        public IReadOnlyList<T> GetAll<T>()
        {

            lock (_lockObject) {
                return Drawables?.OfType<T>().ToList();
            }

        }

        public void Remove(IDrawable shape) {
            lock (_lockObject) {
                Drawables.Remove(shape);
            }
        }

        public void RemoveAt(int index) {
            lock (_lockObject) {
                try {
                    Drawables.RemoveAt(index);
                    Changed?.Invoke();
                } catch (ArgumentOutOfRangeException e) {
                    Console.WriteLine(e.Message);
                }

            }
        }

        public void Add(IDrawable shape) {
            lock (_lockObject) {
                Drawables.Add(shape);
                Changed?.Invoke();
            }
        }

        public void Add(int index, IDrawable shape) {
            lock (_lockObject) {
                Drawables.Insert(index, shape);
                Changed?.Invoke();
            }
        }

        public void OnChanged() {
            lock (_lockObject) {
                Changed?.Invoke();
            }
        }

        public void Draw(IDrawer drawer) {
            lock (_lockObject) {
                foreach (var shape in Drawables) {
                    shape.Draw(drawer);
                }
            }
        }
    }

    
    
}