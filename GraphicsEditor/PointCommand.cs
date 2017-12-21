using ConsoleUI;
using DrawablesUI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsEditor {
    class PointCommand : ICommand {

        private readonly Picture _picture;

        public string GetName() { return "point"; }
        public string GetHelp() { return "Рисует точку в графическом интерфейсе"; }

        public string GetDescription() { return "Рисует точку с координатами, задаваемыми пользователем"; }

        public string[] Synonyms => new string[] { "dot", "pt" };
        public PointCommand(Picture picture) => this._picture = picture ?? throw new ArgumentNullException(nameof(picture));

        public void Execute(params string[] parameters)
        {
            try {
                if (parameters.Length > 2) {
                    throw new ArgumentException("Команда принимает только 2 параметра");
                }

                float x = float.Parse(parameters[0]);
                float y = float.Parse(parameters[1]);

                Point point = new Point(x, y);
                _picture.Add(point);
            } catch (IndexOutOfRangeException){
                Console.WriteLine("Вы ввели не все координаты");
            } catch (FormatException) {
                Console.WriteLine("Вы ввели координаты точки в неправильном формате");
            } catch (OverflowException) {
                Console.WriteLine("Вы ввели слишком большое число в качестве координаты точки");
            } catch (ArgumentException error) {
                Console.WriteLine(error.Message);
            }
        }
    }
}
