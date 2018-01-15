using System;
using System.Collections.Generic;
using System.Linq;
using DrawablesUI;
using GraphicsEditor.Figures.Data.Interfaces;

namespace GraphicsEditor.Commands.Data
{
    class ShapeLocator<T> where T : IDrawable
    {
        // фигура, которая соответствует идентификатору типа 0:1:1
        public T Shape { get; }

        // "родитель" фигуры, которая соответствует идентификатору, м.б. равен null
        public IContainer<T> GrandParent { get; set; }

        // "родитель" "родителя" фигуры которая, соответствует идентификатору, м.б. равен null
        public IContainer<T> Parent { get; set; }

        private static readonly object LockObject = new object();

        // метод, преобразующий строку-идентификатор, разделённый двоеточиями, в объект
        public static ShapeLocator<T> ParseOrFail(uint[] indexes, IContainer<IDrawable> picture)
        {
            lock (LockObject)
            {
                try
                {
                    IContainer<T> parent = (IContainer<T>) picture;
                    IContainer<T> grandParent = null;
                    for (uint count = 0; count < indexes.Length; count++)
                    {
                        IReadOnlyList<T> shapes = parent.GetAll<T>();
                        uint index = indexes[count];

                        if (count < indexes.Length - 1)
                        {
                            if (!shapes[(int) index].GetType().GetInterfaces().Contains(typeof(IContainer<T>)))
                            {
                                uint[] currentIndex = new uint[count + 1];
                                Array.Copy(indexes, 0, currentIndex, 0, count + 1);
                                throw new ArgumentException("Не существует сгруппированной фигуры с индексом " +
                                                            IndexHelper.IndexesToString(currentIndex) +
                                                            "(введенный индекс: " +
                                                            IndexHelper.IndexesToString(indexes) + ")");
                            }
                            grandParent = parent;
                            parent = (IContainer<T>) shapes[(int) index];
                        }
                        else
                        {
                            if (index >= shapes.Count)
                            {
                                throw new ArgumentException("Не существует фигуры с индексом " +
                                                            IndexHelper.IndexesToString(indexes));
                            }
                            return new ShapeLocator<T>(grandParent, parent, shapes[(int) index]);
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

        private ShapeLocator(IContainer<T> grandParent, IContainer<T> parent, T shape)
        {
            GrandParent = grandParent;
            Parent = parent;
            Shape = shape;
        }

    }

}