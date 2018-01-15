using System;
using System.Collections.Generic;
using System.Linq;
using DrawablesUI;
using GraphicsEditor.Figures.Data;
using GraphicsEditor.Figures.Data.Interfaces;

namespace GraphicsEditor.Figures {
    class CompoundFigure : FigureDescribtion,IFigure, IContainer<IFigure> 
        {
        private readonly IContainer<IDrawable> _picture;
        private readonly List<IFigure> _figures;
    
        private readonly object _lockObject = new object();

        public CompoundFigure(IContainer<IDrawable> picture, List<IFigure> figures) {
            _picture = picture;
            _figures = figures;
            Description = "Составная фигура";
            Changed += picture.OnChanged;
        }

        public event Action Changed;

        public IReadOnlyList<TYpe> GetAll<TYpe>(){

            lock (_lockObject) {
                return _figures?.OfType<TYpe>().ToList();
            }

        }
        public void Remove(IFigure figure)
        {
            lock (_lockObject)
            {
                _figures.Remove(figure);
                Changed.Invoke();
            }
        }
        public void RemoveAt(int index)
        {
            lock (_lockObject)
            {               
                _figures.RemoveAt(index);
                Changed.Invoke();
            }
        }

        public void Add(IFigure figure)
        {
            lock (_lockObject)
            {
                _figures.Add(figure);
                
            }
        }
        public void Add(int index, IFigure figure) {
            lock (_lockObject) {
                _figures.Insert(index, figure);
                _picture.Remove(figure);
            }
        }
        public void Draw(IDrawer drawer) {
            lock (_lockObject)
            {
                drawer.SelectPen(Format.Color, Format.Width);
                foreach (IFigure figure in _figures)
                {
                    figure.Draw(drawer);
                }
            }
        }
        public void OnChanged() {
            Changed.Invoke();
        }

        public void Transform(Transformation trans)
        {

        }

        public override string GenerateDescription(uint[] indexes) {
            lock (_lockObject)
            {
                List<string> description = new List<string> {base.GenerateDescription(indexes)};
                Array.Resize(ref indexes, indexes.Length + 1);
                uint lastElement = (uint) (indexes.Length - 1);
                indexes[lastElement] = 0;


                foreach (IFigure figure in _figures)
                {
                    description.Add(figure.GenerateDescription(indexes));
                    indexes[lastElement]++;

                }

                return String.Join("\n", description);
            }
        }

        public override string GenerateDescription(uint index) {       
            return GenerateDescription(new[]{ index });          
        }

    }
}
