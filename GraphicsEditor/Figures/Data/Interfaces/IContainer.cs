using System;
using System.Collections.Generic;
using DrawablesUI;

namespace GraphicsEditor.Figures.Data.Interfaces
{
    interface IContainer<in TDrawable> : IDrawable      
        {
        event Action Changed;
        IReadOnlyList<T> GetAll<T>();
        void Remove(TDrawable shape);

        void RemoveAt(int index);

        void Add(TDrawable drawable);

        void Add(int index, TDrawable shape);
        void OnChanged();
    }

}