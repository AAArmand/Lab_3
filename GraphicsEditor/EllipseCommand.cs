using ConsoleUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsEditor {
    class EllipseCommand : ICommand{
        private Picture picture;
        public string Name { get { return "ellipse"; } }

        public string Help { get { return "Рисует эллипс в графическом интерфейсе"; } }
        public string Description { get { return "Рисует эллипс. Параметры — координаты точки центра эллипса, размеры осей эллипса, угол поворота эллипса"; } }
        public string[] Synonyms { get { return new string[] { "oval", "egg" }; } }

        public EllipseCommand(Picture picture) {
            this.picture = picture;
        }

        public void Execute(params string[] parameters) {
            try {
                if (parameters.Length > 5) {
                    throw new ArgumentException("Команда принимает только 5 параметров");
                }

                float X = float.Parse(parameters[0]);
                float Y = float.Parse(parameters[1]);
                Point Center = new Point(X, Y);

                float Height = float.Parse(parameters[2]);
                float Width = float.Parse(parameters[3]);

                if ((Height <= 0) || (Width <= 0)) {
                    throw new InvalidOperationException("Длина оси эллипса должна быть больше 0");
                }

                float Angle = float.Parse(parameters[4]);
                Ellipse ellipse = new Ellipse(Center, Width, Height, Angle);

                picture.Add(ellipse);
                ListCommand.Shapes.Add("Эллипс" +
                    "(Точка(" + X + ", " + Y + "), " +
                    "Ось a = " + Height + ", " +
                    "Ocь b = " + Width + ", " +
                    "Угол поворота = " + Angle + ")", ellipse);
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
