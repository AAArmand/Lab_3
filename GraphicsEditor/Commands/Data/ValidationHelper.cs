using System;
using System.IO;
using System.Linq;
using DrawablesUI;
using GraphicsEditor.Figures.Data.Interfaces;

namespace GraphicsEditor.Commands.Data
{
    class ValidationHelper
    {
        public static bool ParametsEmptyValidator(string[] parameters,
            string message = "Команда должна принимать параметры")
        {
            if (parameters.Length == 0)
            {
                Console.WriteLine(message);
                return false;
            }
            return true;
        }

        public static bool IndexesDistinctValidator(uint[][] indexes,
            string message = "Нельзя вводить одинаковые индексы несколько раз")
        {
            if (indexes.Distinct(new IndexesComparer()).Count() != indexes.Length)
            {
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