using System;
using ConsoleUI;
using DrawablesUI;
using GraphicsEditor.Figures;
using GraphicsEditor.Figures.Data.Interfaces;

namespace GraphicsEditor.Commands.FiguresInitCommands {
    class LineCommand : ICommand{

        private readonly IContainer<IDrawable> _picture;

        public string Name => "line"; public string Help => "Рисует отрезок в графическом интерфейсе";
        public string GetDescription() { return "Рисует отрезок от одной точки до другой, в качестве параметра - две пары координат точек"; }

        public string[] Synonyms => new string[] { "segment", "offcut" };
        public LineCommand(IContainer<IDrawable> picture) => _picture = picture;

        public void Execute(params string[] parameters) {
            try {
                if (parameters.Length > 4) {
                    throw new ArgumentException("Команда принимает только 4 параметра");
                }

                float[] x = { float.Parse(parameters[0]), float.Parse(parameters[2]) };
                float[] y = { float.Parse(parameters[1]), float.Parse(parameters[3]) };

                LineFigure line = new LineFigure(new PointFigure(x[0], y[0]), new PointFigure(x[1], y[1]));
                _picture.Add(line);
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
