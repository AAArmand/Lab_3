/*using ConsoleUI;
using GraphicsEditor.Commands.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsEditor.Commands.FiguresInitCommands {
    class UngroupCommand : CommandIndex, ICommand {
        public string Name => "ungroup"; public string Help => "Разгруппировка фигур";
        public string GetDescription() { return "Разгруппировка фигур. Параметр - идентификатор составной фигуры, которую нужно разгруппировать"; }

        public string[] Synonyms => new string[] { "ungrouping", "ungr" };

        public UngroupCommand(Picture picture) : base(picture) { }

        
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
                    Picture.Add(new CompoundFigure(Picture, indexes));
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
*/