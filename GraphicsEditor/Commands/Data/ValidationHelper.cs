using System;
using System.IO;
using System.Linq;
using DrawablesUI;
using GraphicsEditor.Figures.Data.Interfaces;

namespace GraphicsEditor.Commands.Data
{
    class ValidationHelper
    {
        public static bool ParametersEmptyValidator(string[] parameters,
            string message = "Команда должна принимать параметры")
        {
            if (parameters.Length == 0)
            {
                Console.WriteLine(message);
                return false;
            }
            return true;
        }

        public static bool ParametersCountValidator(string[] parameters, uint count,
            string message = "Неверное число параметров.") {
            if (parameters.Length != count) {
                Console.WriteLine(message);
                return false;
            }
            return true;
        }

       
        public static bool ContainsInContainerValidator<TItem>(IContainer<TItem> container,string message) where TItem :IDrawable
            {
            if (!container.GetAll<TItem>().Any()) {
                Console.WriteLine(message);
                return false;
            }
            return true;
        }
    }
}