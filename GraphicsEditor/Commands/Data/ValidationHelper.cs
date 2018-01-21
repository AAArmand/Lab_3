using System;
using System.IO;
using System.Linq;
using DrawablesUI;
using GraphicsEditor.Figures.Data.Interfaces;

namespace GraphicsEditor.Commands.Data
{
    class ValidationHelper
    {
        //разве indexHelper и indexesComparer не должны расширять ValidationHelper
        protected const string EMPTY_ERROR = "Команда должна принимать параметры";
        protected const string SAME_INDEX_ERROR = "Нельзя вводить одинаковые индексы несколько раз";

        public static bool ParametsEmptyValidator(string[] parameters,
            string message = EMPTY_ERROR)
        {
            if (parameters.Length == 0)
            {
                Console.WriteLine(message);
                return false;
            }
            return true;
        }

        public static bool IndexesDistinctValidator(uint[][] indexes,
            string message = SAME_INDEX_ERROR)
        {
            if (indexes.Distinct(new IndexesComparer()).Count() != indexes.Length)
            {
                Console.WriteLine(message);
                return false;
            }
            return true;
        }

        //не понял
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