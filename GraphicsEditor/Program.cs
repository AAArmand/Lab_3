using System;
using System.Net.Mime;
using ConsoleUI;
using DrawablesUI;
using System.Text;
using GraphicsEditor.Commands;
using GraphicsEditor.Commands.FiguresDataCommands;
using GraphicsEditor.Commands.FiguresInitCommands;
using GraphicsEditor.Commands.ShapesDataCommands;
using GraphicsEditor.Commands.ShapesDataCommands.FiguresDataCommands;

namespace GraphicsEditor
{
    class Program
    {
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
            app.AddCommand(new ListCommand(picture));
            app.AddCommand(new RemoveCommand(picture));
            app.AddCommand(new ColorCommand(picture));
            app.AddCommand(new WidthCommand(picture));
            app.AddCommand(new GroupCommand(picture));

            picture.Changed += ui.Refresh;
            ui.Start();
            app.Run(Console.In);
            ui.Stop();
        }
    }
}
