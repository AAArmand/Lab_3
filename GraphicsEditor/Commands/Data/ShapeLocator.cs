using System;
using System.Collections.Generic;
using System.Linq;
using DrawablesUI;
using GraphicsEditor.Figures.Data.Interfaces;

namespace GraphicsEditor.Commands.Data
{
    
    class ShapeLocator<TDrawable> where TDrawable : IDrawable
    {
        
        public TDrawable Shape { get; }

       
        public IContainer<TDrawable> GrandParent { get; set; }

        
        public IContainer<TDrawable> Parent { get; set; }

        private static readonly object LockObject = new object();

       
        public static ShapeLocator<TDrawable> ParseOrFail(uint[] indexes, IContainer<IDrawable> picture)
        {
            lock (LockObject)
            {
                try
                {
                    IContainer<TDrawable> parent = (IContainer<TDrawable>) picture;
                    IContainer<TDrawable> grandParent = null;
                    for (uint count = 0; count < indexes.Length; count++)
                    {
                        IReadOnlyList<TDrawable> shapes = parent.GetAll<TDrawable>();
                        uint index = indexes[count];

                        if (count < indexes.Length - 1)
                        {
                            if (!shapes[(int) index].GetType().GetInterfaces().Contains(typeof(IContainer<TDrawable>)))
                            {
                                uint[] currentIndex = new uint[count + 1];
                                Array.Copy(indexes, 0, currentIndex, 0, count + 1);
                                throw new ArgumentException("Не существует сгруппированной фигуры с индексом " +
                                                            IndexHelper.IndexesToString(currentIndex) +
                                                            "(введенный индекс: " +
                                                            IndexHelper.IndexesToString(indexes) + ")");
                            }
                            grandParent = parent;
                            parent = (IContainer<TDrawable>) shapes[(int) index];
                        }
                        else
                        {
                            if (index >= shapes.Count)
                            {
                                throw new ArgumentException("Не существует фигуры с индексом " +
                                                            IndexHelper.IndexesToString(indexes));
                            }
                            return new ShapeLocator<TDrawable>(grandParent, parent, shapes[(int) index]);
                        }
                    }
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return null;
        }

        private ShapeLocator(IContainer<TDrawable> grandParent, IContainer<TDrawable> parent, TDrawable shape)
        {
            GrandParent = grandParent;
            Parent = parent;
            Shape = shape;
        }

    }

}