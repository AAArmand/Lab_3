﻿using System;
using System.Linq;
using ConsoleUI;
using GraphicsEditor.Commands.Data;
using GraphicsEditor.Figures;

namespace GraphicsEditor.Commands.FiguresInitCommands {
    class GroupCommand : CommandIndex, ICommand {

        public string Name => "group"; public string Help => "Группировка фигур";
        public string GetDescription() { return "Переносит фигуры, идентификаторы которых перечислены в параметрах, в новую составную фигуру, которая добавляется на картинку"; }

        public string[] Synonyms => new string[] { "grouping", "gr" };

        public GroupCommand(Picture picture) : base(picture) { }
    
        public void Execute(params string[] parameters) {
            
            try {
                if (parameters[0] == "") {
                    throw new FormatException("Команда должна принимать параметры");
                }

                if (!Picture.Figures.Any()) {
                    throw new NullReferenceException("Не нарисовано ни одной фигуры");
                }

                int[] indexes = ValidateIndexes(parameters);           
                if (indexes != null) {
                    Picture.Add(new CompoundFigure(Picture, indexes));
                } else {
                    throw new ArgumentException("Введите индексы заново");
                }
                
            } catch (FormatException error) {
                Console.WriteLine(error.Message);
            } catch (NullReferenceException error) {
                Console.WriteLine(error.Message);
            } catch (ArgumentException error) {
                Console.WriteLine(error.Message);
            }
        }
    }
}