using LunarDoggo.QuizGame.Visuals;
using System.Collections.Generic;
using LunarDoggo.QuizGame.IO;
using System.Linq;
using System;

namespace LunarDoggo.QuizGame
{
    public class Program
    {
        //The declaration with the interface IVisualizer instead of declaring it as a ConsoleVisualizer has the advantage that we,
        //if we were to implement another visualizer for, example with OpenGL, we could swap it without rewriting the application
        public static IVisualizer Visualizer { get; } = new ConsoleVisualizer();

        private static void Main()
        {
            bool run;

            do
            {
                //First load the questions from the datasource, if the questions change between the do-while-loop-iteration,
                //the changes are reflected in the next run of the loop
                IEnumerable<QuizQuestion> questions = Program.GetQuestions();
                //if questions were loaded, start the game, otherwise prompt the user to update the question-datasource
                if (questions.Any()) //Note: .Any() is part of the extension methods from System.Linq and returns whether there are any entries in the questions collection
                {
                    Program.RunGameLoop(questions);
                }
                else
                {
                    Program.Visualizer.DrawNoQuestions();
                }

                //If the user presses the "Y"-Key, we want to run the loop again, otherwise, the application terminates
                run = Console.ReadKey().Key == ConsoleKey.Y;
            } while (run);
        }

        /// <summary>
        /// Instantiate and run the <see cref="GameLoop"/> with the provided <see cref="QuizQuestion"/>s
        /// </summary>
        private static void RunGameLoop(IEnumerable<QuizQuestion> questions)
        {
            GameLoop gameLoop = new GameLoop(Program.Visualizer, questions);
            //while the game isn't finished, we want to draw to the screen run updates to the gamestate and process user input
            while (!gameLoop.IsFinished)
            {
                gameLoop.DoTick();
            }
            //when the game is finished, we want to ask the user if they want to play again
            Program.Visualizer.DrawPlayAgain();
        }

        /// <summary>
        /// Load all questions from the datasource
        /// </summary>
        private static IEnumerable<QuizQuestion> GetQuestions()
        {
            string filePath = ".\\game_questions.json";
            //if we were to implement another datasource (e.g. MySQLQuizQuestionSerializer) we could swap it here
            IQuizQuestionSerializer serializer = new FileQuizQuestionSerializer(filePath);
            return serializer.DeserializeQuestions();
        }
    }
}
