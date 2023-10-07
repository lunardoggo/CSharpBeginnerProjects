using System;

namespace LunarDoggo.QuizGame.Visuals
{
    public class ConsoleVisualizer : IVisualizer
    {
        public ConsoleVisualizer()
        {
            //this call hides the white underscore indicator of the cursor
            Console.CursorVisible = false;
        }

        public void DrawNoQuestions()
        {
            Console.WriteLine("No questions were loaded, please enter some questions into the json file in the applications folder.\n\nReload game? (Y/N)");
        }

        public void DrawAnswerStatus(bool correct, QuizQuestionAnswer correctAnswer)
        {
            if (correct)
            {
                Console.WriteLine("Your answer is correct. Continue with \"enter\".");
            }
            else
            {
                Console.WriteLine("Your answer isn't correct. The correct answer is: \"{0}\". Continue with \"enter\".", correctAnswer.Answer);
            }
        }

        public void DrawQuizQuestion(QuizQuestion question, Guid highlitedAnswerId)
        {
            Console.Clear();
            Console.WriteLine(question.Question);
            Console.WriteLine();
            foreach (QuizQuestionAnswer answer in question.Answers)
            {
                this.DrawQuizQuestionAnswer(answer, answer.Id == highlitedAnswerId);
            }
            Console.WriteLine();
            Console.WriteLine();
        }

        private void DrawQuizQuestionAnswer(QuizQuestionAnswer answer, bool highlited)
        {
            Console.SetCursorPosition(1, Console.CursorTop);
            //you can write on line conditional statements in the format: {boolean expression} ? {action when the condition is met} : {action when the condition isn't met}
            Console.WriteLine("({0}) {1}", highlited ? "*" : " ", answer.Answer);
            //in this case, the previous line could also be written as:
            //if(highlighted) { Console.WriteLine("({0}) {1}", "*", answer.Answer); } else { Console.WriteLine("({0}) {1}", " ", answer.Answer); }
        }

        public void DrawGameStart(int totalQuestionCount)
        {
            Console.Clear();
            Console.WriteLine("{0} question{1} {2} loaded, press \"enter\" to start the game.", totalQuestionCount, totalQuestionCount > 1 ? "s" : "", totalQuestionCount > 1 ? "were" : "was");
        }

        public void DrawGameResult(int totalQuestionCount, int correctAnswersCount)
        {
            Console.WriteLine("You got {0} out of {1} question right. Continue with \"enter\".", correctAnswersCount, totalQuestionCount);
            Console.WriteLine();
        }

        public void DrawPlayAgain()
        {
            Console.WriteLine("Do you like to play again? (Y/N)");
        }
    }
}
