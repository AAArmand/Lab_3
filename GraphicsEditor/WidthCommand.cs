﻿using ConsoleUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsEditor {
    class WidthCommand : ICommand{
        private Picture picture;
        public string Name { get { return "width"; } }

        public string Help { get { return "Изменяет ширину линий фигуры на заданный"; } }
        public string Description { get { return "Изменяет ширину линий фигуры. Первый параметр - ширина, последующие - индексы фигур"; } }
        public string[] Synonyms { get { return new string[] { "breadth", "w" }; } }

        public WidthCommand(Picture picture) {
            this.picture = picture;
        }

        private int[] ValidateIndexes(string[] parameters) {
            try {
                List<int> indexes = new List<int>();
                int index;
                foreach (string parametr in parameters.Skip(1)) {
                    index = int.Parse(parametr);
                    if (index < 0) {
                        throw new ArgumentException("Индекс " + index + " не может быть отрицательным");
                    }

                    if (index >= picture.Shapes.Count()) {
                        throw new ArgumentException("Не существует фигуры с индексом " + index);
                    }

                    if (indexes.Contains(index)) {
                        throw new ArgumentException("Индекс " + index + " повторяется");
                    } else {
                        indexes.Add(index);
                    }
                }

                return indexes.ToArray();

            } catch (FormatException) {
                Console.WriteLine("Вы ввели индексы в неверном формате");
                return null;
            } catch (OverflowException) {
                Console.WriteLine("Вы ввели слишком большое число в качестве индекса");
                return null;
            } catch (ArgumentException error) {
                Console.WriteLine(error.Message);
                return null;
            }

        }

        public void Execute(params string[] parameters) {
            try {

                int[] indexes = ValidateIndexes(parameters);

                if (indexes != null) {
                    int i = 0;
                    foreach (IShape shape in picture.Shapes) {
                        if (indexes.Contains(i)) {
                            shape.Format.Width = uint.Parse(parameters[0]);
                        }
                        i++;
                    }
                    
                    picture.OnChanged();
                } else {
                    throw new ArgumentException("Повторите ввод индексов фигур");
                }

            } catch (ArgumentException error) {
                Console.WriteLine(error.Message);
            } catch (FormatException) {
                Console.WriteLine("Вы ввели ширину в неверном формате");
            } catch (OverflowException) {
                Console.WriteLine("Вы ввели слишком большое число в качестве ширины");
            }
    }
}
