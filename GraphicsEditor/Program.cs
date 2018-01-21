using System;
using ConsoleUI;
using DrawablesUI;
using System.Text;
using GraphicsEditor.Commands.FiguresDataCommands;
using GraphicsEditor.Commands.FiguresInitCommands;
using GraphicsEditor.Figures;
using GraphicsEditor.Figures.Data.Interfaces;

namespace GraphicsEditor
{
    class Program
    {
        private static Picture _instance;
        public static Picture GetInstance()
        {
            return _instance ?? (_instance = new Picture());
        }
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            var picture = new Picture();
            var ui = new DrawableGUI(picture);
            var app = new Application();          

            app.AddCommand(new ExitCommand(app));
            app.AddCommand(new ExplainCommand(app));
            app.AddCommand(new HelpCommand(app));
            app.AddCommand(new PointCommand(picture));
            app.AddCommand(new LineCommand(picture));
            app.AddCommand(new EllipseCommand(picture));
            app.AddCommand(new CircleCommand(picture));
            app.AddCommand(new ListCommand<IFigure>(picture));
            app.AddCommand(new RemoveCommand<IFigure>(picture));
            app.AddCommand(new ColorCommand<IFigure>(picture));
            app.AddCommand(new WidthCommand<IFigure>(picture));
            app.AddCommand(new GroupFigureCommand(picture));
            app.AddCommand(new UngroupCommand<IFigure>(picture));

            picture.Changed += ui.Refresh;
            ui.Start();
            app.Run(Console.In);
            ui.Stop();
        }
    }
}
