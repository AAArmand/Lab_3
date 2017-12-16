using ConsoleUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsEditor {
    class GroupCommand : CommandIndex, ICommand {
        public GroupCommand(Picture picture) : base(picture) {
        }

        public string Name { get { return "group"; } }

        public string Help { get { return "Группировка фигур"; } }
        public string Description { get { return "Переносит фигуры, идентификаторы которых перечислены в параметрах, в новую составную фигуру, которая добавляется на картинку"; } }
        public string[] Synonyms { get { return new string[] { "grouping", "gr" }; } }

       
        public void Execute(params string[] parameters) {
            try {
                if (parameters[0] == "") {
                    throw new FormatException("Команда должна принимать параметры");
                }

                if (picture.Shapes.Count() == 0) {
                    throw new NullReferenceException("Не нарисовано ни одной фигуры");
                }

                int[] indexes = ValidateIndexes(parameters);           
                if (indexes != null) {
                    CompoundShape compoundShape = new CompoundShape(picture, indexes);
                    picture.Add(compoundShape);
                } else {
                    throw new ArgumentException("Введите индексы заново");
                }
                
            } catch (FormatException error) {
                Console.WriteLine(error.Message);
            } catch (NullReferenceException error) {
                Console.WriteLine(error.Message);
            } catch (ArgumentException error) {
                Console.WriteLine(error.Message);
            }
        }
    }
}
