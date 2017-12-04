using ConsoleUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsEditor {
    class CircleCommand : ICommand {
        private Picture picture;
        public string Name { get { return "circle"; } }

        public string Help { get { return "Рисует круг в графическом интерфейсе"; } }
        public string Description { get { return "Рисует круг. Параметры — координаты центра круга и радиус"; } }
        public string[] Synonyms { get { return new string[] { "lap", "disk" }; } }

        public CircleCommand(Picture picture) {
            this.picture = picture;
        }

        public void Execute(params string[] parameters) {
            try {
                if (parameters.Length > 3) {
                    throw new ArgumentException("Команда принимает только 3 параметра");
                }

                float X = float.Parse(parameters[0]);
                float Y = float.Parse(parameters[1]);
                Point Center = new Point(X, Y);
                
                float Radius = float.Parse(parameters[2]);

                if (Radius <= 0) {
                    throw new InvalidOperationException("Радиус круга должен быть больше 0");
                }

                Circle circle = new Circle(Center, Radius);

                picture.Add(circle);
                //ListCommand.Shapes.Add("Круг" +
                    "(Точка(" + X + ", " + Y + "), " +
                    "Радиус = " + Radius + ")", circle);
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
