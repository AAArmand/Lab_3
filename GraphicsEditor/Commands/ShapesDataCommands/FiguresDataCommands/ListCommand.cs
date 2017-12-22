using System;
using System.Linq;
using ConsoleUI;
using GraphicsEditor.Figures.Data.Interfaces;

namespace GraphicsEditor.Commands.ShapesDataCommands.FiguresDataCommands {
    class ListCommand : ICommand{
        private readonly Picture _picture;

        public string Name => "list"; public string Help => "Выводит список фигур на картинке";
        public string GetDescription() { return "Выводит список фигур на картинке, не принимает параметров"; }

        public string[] Synonyms => new string[] { "bill", "roll" };
        public ListCommand(Picture picture) => this._picture = picture;

        public void Execute(params string[] parameters) {
            try {
                if (parameters.Length > 0) {
                    throw new FormatException("Команда не принимает параметров");
                }
                
                if (!_picture.Shapes.Any()) {
                   throw new NullReferenceException("Не нарисовано ни одной фигуры");
                }
                

                int i = 0;
                foreach (IFigure figure in _picture.Figures) {
                    Console.WriteLine(figure.GenerateDescription(i));
                    i++;
                }
               
            } catch (FormatException error) {
                Console.WriteLine(error.Message);
            } catch (NullReferenceException error) {
                Console.WriteLine(error.Message);
            }
        }
    }
}
