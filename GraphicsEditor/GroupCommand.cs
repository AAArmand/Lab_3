using ConsoleUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsEditor {
    class GroupCommand : CommandIndex, ICommand {
        
        public string GetName() { return "group"; }
        public string GetHelp() { return "Группировка фигур"; }

        public string GetDescription() { return "Переносит фигуры, идентификаторы которых перечислены в параметрах, в новую составную фигуру, которая добавляется на картинку"; }

        public string[] Synonyms => new string[] { "grouping", "gr" };

        public GroupCommand(Picture picture) : base(picture) { }
    
        public void Execute(params string[] parameters) {
            try {
                if (parameters[0] == "") {
                    throw new FormatException("Команда должна принимать параметры");
                }

                if (!Picture.Shapes.Any()) {
                    throw new NullReferenceException("Не нарисовано ни одной фигуры");
                }

                int[] indexes = ValidateIndexes(parameters);           
                if (indexes != null) {
                    CompoundShape compoundShape = new CompoundShape(Picture, indexes);
                    Picture.Add(compoundShape);
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
