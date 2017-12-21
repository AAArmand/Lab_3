using ConsoleUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsEditor {
    class EllipseCommand : ICommand{
        private readonly Picture _picture;

        public string GetName() { return "ellipse"; }
        public string GetHelp() { return "Рисует эллипс в графическом интерфейсе"; }

        public string GetDescription() { return "Рисует эллипс. Параметры — координаты точки центра эллипса, размеры осей эллипса, угол поворота эллипса"; }

        public string[] Synonyms => new string[] { "oval", "egg" };
        public EllipseCommand(Picture picture) => this._picture = picture ?? throw new ArgumentNullException(nameof(picture));

        public void Execute(params string[] parameters) {
            try {
                if (parameters.Length > 5) {
                    throw new ArgumentException("Команда принимает только 5 параметров");
                }

                float x = float.Parse(parameters[0]);
                float y = float.Parse(parameters[1]);
                Point center = new Point(x, y);

                float height = float.Parse(parameters[2]);
                float width = float.Parse(parameters[3]);

                if ((height <= 0) || (width <= 0)) {
                    throw new InvalidOperationException("Длина оси эллипса должна быть больше 0");
                }

                float angle = float.Parse(parameters[4]);
                Ellipse ellipse = new Ellipse(center, width, height, angle);

                _picture.Add(ellipse);
            } catch (IndexOutOfRangeException) {
                Console.WriteLine("Вы ввели не все параметры");
            } catch (FormatException) {
                Console.WriteLine("Вы ввели параметры в неправильном формате");
            } catch (OverflowException) {
                Console.WriteLine("Вы ввели слишком большое числов в качестве координаты точки");
            } catch (InvalidOperationException error) {
                Console.WriteLine(error.Message);
            } catch (ArgumentException error) {
                Console.WriteLine(error.Message);
            }
        }
    }
}
