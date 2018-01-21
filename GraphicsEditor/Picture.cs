using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DrawablesUI;
using GraphicsEditor.Figures.Data.Interfaces;

namespace GraphicsEditor
{
<<<<<<< Updated upstream
    class Picture : IDrawable
    {
        private readonly List<IDrawable> _drawables = new List<IDrawable>();
=======
    public class Picture : IContainer<IDrawable> {      
        private static readonly List<IDrawable> Drawables = new List<IDrawable>();
>>>>>>> Stashed changes
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

<<<<<<< Updated upstream
                    return shapes;
                }
=======
            lock (_lockObject) {
                return Drawables?.OfType<T>().ToList();
>>>>>>> Stashed changes
            }
        }

<<<<<<< Updated upstream
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
=======
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
>>>>>>> Stashed changes
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

<<<<<<< Updated upstream
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
=======
        public void Add(IDrawable shape) {
            lock (_lockObject) {
                Drawables.Add(shape);
>>>>>>> Stashed changes
                Changed?.Invoke();
            }
        }

<<<<<<< Updated upstream
        public void Add(int index, IDrawable shape)
        {
            lock (_lockObject)
            {
                _drawables.Insert(index, shape);
=======
        public void Add(int index, IDrawable shape) {
            lock (_lockObject) {
                Drawables.Insert(index, shape);
>>>>>>> Stashed changes
                Changed?.Invoke();
            }
        }

        public void OnChanged() {
            lock (_lockObject)
            {
                Changed?.Invoke();
            }
        }

<<<<<<< Updated upstream
        public void Draw(IDrawer drawer)
        {
            lock (_lockObject)
            {
                foreach (var shape in _drawables)
                {
=======
        public void Draw(IDrawer drawer) {
            lock (_lockObject) {
                foreach (var shape in Drawables) {
>>>>>>> Stashed changes
                    shape.Draw(drawer);
                }
            }
        }
    }
}