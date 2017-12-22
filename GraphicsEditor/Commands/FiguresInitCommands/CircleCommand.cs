using System;
using ConsoleUI;
using GraphicsEditor.Figures;

namespace GraphicsEditor.Commands.FiguresInitCommands {
    class CircleCommand : ICommand {
        private readonly Picture _picture;

        public string Name => "circle"; public string Help => "Рисует круг в графическом интерфейсе";
        public string GetDescription() { return "Рисует круг. Параметры — координаты центра круга и радиус"; }

        public string[] Synonyms => new string[] { "lap", "disk" };
        public CircleCommand(Picture picture) => this._picture = picture ?? throw new ArgumentNullException(nameof(picture));

        public void Execute(params string[] parameters) {
            try {
                if (parameters.Length > 3) {
                    throw new ArgumentException("Команда принимает только 3 параметра");
                }

                float x = float.Parse(parameters[0]);
                float y = float.Parse(parameters[1]);
                PointFigure center = new PointFigure(x, y);
                
                float radius = float.Parse(parameters[2]);

                if (radius <= 0) {
                    throw new InvalidOperationException("Радиус круга должен быть больше 0");
                }

                CircleFigure circle = new CircleFigure(center, radius);

                _picture.Add(circle);
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
