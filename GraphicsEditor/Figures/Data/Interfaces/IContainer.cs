using System;
using System.Collections.Generic;
using DrawablesUI;

namespace GraphicsEditor.Figures.Data.Interfaces
{
    interface IContainer<in TYpe> : IDrawable      
        {
        event Action Changed;
        IReadOnlyList<T> GetAll<T>();
        void Remove(TYpe shape);

        void RemoveAt(int index);

        void Add(TYpe drawable);

        void Add(int index, TYpe shape);
        void OnChanged();
    }

}