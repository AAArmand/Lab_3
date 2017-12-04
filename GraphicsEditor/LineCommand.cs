using ConsoleUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsEditor {
    class LineCommand : ICommand{

        private Picture picture;
        public string Name { get { return "line"; } }

        public string Help { get { return "Рисует отрезок в графическом интерфейсе"; } }
        public string Description { get { return "Рисует отрезок от одной точки до другой, в качестве параметра - две пары координат точек"; } }
        public string[] Synonyms { get { return new string[] { "segment", "offcut" }; } }

        public LineCommand(Picture picture) {
            this.picture = picture;
        }

        public void Execute(params string[] parameters) {
            try {
                if (parameters.Length > 4) {
                    throw new ArgumentException("Команда принимает только 4 параметра");
                }

                float[] X = { float.Parse(parameters[0]), float.Parse(parameters[2]) };
                float[] Y = { float.Parse(parameters[1]), float.Parse(parameters[3]) };
                
                Point point1 = new Point(X[0], Y[0]);
                Point point2 = new Point(X[1], Y[1]);
                Line line = new Line(point1, point2);
                picture.Add(line);
                ListCommand.Shapes.Add("Линия" +
                    "(Точка(" + X[0] + ", " + Y[0] + "), " +
                    "Точка(" + X[1] + ", " + Y[1] + "))", line);
            } catch (IndexOutOfRangeException) {
                Console.WriteLine("Вы ввели не все координаты");
            } catch (FormatException) {
                Console.WriteLine("Вы ввели координаты в неправильном формате");
            } catch (OverflowException) {
                Console.WriteLine("Вы ввели слишком большое число в качестве координаты");
            } catch (ArgumentException error) {
                Console.WriteLine(error.Message);
            }
        }
    }
}
