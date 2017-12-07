﻿using ConsoleUI;
using DrawablesUI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsEditor {
    class ListCommand : ICommand{
        private Picture picture;
        public string Name { get { return "list"; } }

        public string Help { get { return "Выводит список фигур на картинке"; } }
        public string Description { get { return "Выводит список фигур на картинке, не принимает параметров"; } }
        public string[] Synonyms { get { return new string[] { "bill", "roll" }; } }
        
        public ListCommand(Picture picture) {
            this.picture = picture;
        }

        public void Execute(params string[] parameters) {
            try {
                if (parameters.Length > 0) {
                    throw new FormatException("Команда не принимает параметров");
                }
                
                if (picture.Shapes.Count() == 0) {
                   throw new NullReferenceException("Не нарисовано ни одной фигуры");
                }
                
                int i = 0;
                foreach (IShape shape in picture.Shapes ) {
                    Console.WriteLine("[{0}] {1}", i, shape.Description);
                    i++;
                }
               
            } catch (FormatException error) {
                Console.WriteLine(error.Message);
            } catch (NullReferenceException error) {
                Console.WriteLine(error.Message);
            }
        }
    }
}
