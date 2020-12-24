using LunarDoggo.QuizGame.Visuals;
using System.Collections.Generic;
using LunarDoggo.QuizGame.IO;
using System.Linq;
using System;

namespace LunarDoggo.QuizGame
{
    public class Program
    {
        public static IVisualizer Visualizer { get; } = new ConsoleVisualizer();

        private static void Main()
        {
            bool run;

            do
            {
                IEnumerable<QuizQuestion> questions = Program.GetQuestions();
                if (questions.Any())
                {
                    Program.RunGameLoop(questions);
                }
                else
                {
                    Program.Visualizer.DrawNoQuestions();
                }

                run = Console.ReadKey().Key == ConsoleKey.Y;
            } while (run);
        }

        private static void RunGameLoop(IEnumerable<QuizQuestion> questions)
        {
            GameLoop gameLoop = new GameLoop(Program.Visualizer, questions);
            while (!gameLoop.IsFinished)
            {
                gameLoop.DoTick();
            }

            Program.Visualizer.DrawPlayAgain();
        }

        private static IEnumerable<QuizQuestion> GetQuestions()
        {
            string filePath = ".\\game_questions.json";
            IQuizQuestionSerializer serializer = new FileQuizQuestionSerializer(filePath);
            return serializer.DeserializeQuestions();
        }
    }
}
