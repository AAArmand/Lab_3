using System;
using System.Linq;
using ConsoleUI;
using DrawablesUI;
using GraphicsEditor.Commands.Data;
using GraphicsEditor.Figures.Data.Interfaces;

namespace GraphicsEditor.Commands.FiguresDataCommands {
    class ListCommand<T> : ICommand where T : IFigure 
        { 
        private readonly IContainer<IDrawable> _picture;

        public string Name => "list"; public string Help => "Выводит список фигур на картинке";
        public string GetDescription() { return "Выводит список фигур на картинке, не принимает параметров"; }

        public string[] Synonyms => new [] { "bill", "roll" };
        public ListCommand(IContainer<IDrawable> picture) => this._picture = picture;

        public void Execute(params string[] parameters) {
            try {
                if (parameters.Length > 0) {
                    throw new FormatException("Команда не принимает параметров");
                }

                if (ValidationHelper.ContainsInContainerValidator<IFigure>(_picture, "Не нарисовано ни одной фигуры"))
                {
                    for (uint index = 0; index < _picture.GetAll<T>().Count; index++) {
                        Console.WriteLine(_picture.GetAll<T>()[(int)index].GenerateDescription(index));
                    }
                }      
                
            } catch (FormatException error) {
                Console.WriteLine(error.Message);
            } catch (NullReferenceException error) {
                Console.WriteLine(error.Message);
            }
        }
    }
}
