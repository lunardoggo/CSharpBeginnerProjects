using System;

namespace LunarDoggo.QuizGame.Visuals
{
    /*
     * Interfaces in C# are like blueprints for classes, every class that "implements" this interface must also define all
     * methods and properties of this interface as public members.
     * Interfaces can be used to define common methods and properties of multiple classes in order to be able to change
     * the instance of a concrete class in the source code without having to rewrite the application.
     * In the real world interfaces are for example used, when you have multiple datasources like databases, files, ...
     * or rendering-libraries like DirectX, OpenGL, Vulcan, ... which can be swapped in and out at runtime
     * 
     * In this case, all classes that implement IQuizQuestionSerializer must declare the following public methods as voids:
     * DrawAnswerStatus(bool correct, QuizQuestionAnswer correctAnswer)
     * DrawQuizQuestion(QuizQuestion question, Guid highlitedAnswerId)
     * DrawGameResult(int totalQuestionCount, int correctAnswersCount)
     * DrawGameStart(int totalQuestionCount)
     * DrawNoQuestions()
     * DrawPlayAgain()
     */
    public interface IVisualizer
    {
        /// <summary>
        /// Draws if the answer the user gave was correct or wrong
        /// </summary>
        /// <param name="correct">bool-indicator if the last given answer was correct</param>
        /// <param name="correctAnswer">answer to be displayed if correct is set to false</param>
        void DrawAnswerStatus(bool correct, QuizQuestionAnswer correctAnswer);

        /// <summary>
        /// Draws a <see cref="QuizQuestion"/> to the display and highlights the currenctly selected <see cref="QuizQuestionAnswer"/>
        /// where <see cref="QuizQuestionAnswer.Id"/> equals <paramref name="highlitedAnswerId"/>
        /// </summary>
        /// <param name="question">question to be shown</param>
        /// <param name="highlitedAnswerId"><see cref="QuizQuestionAnswer.Id"/> to be highlighted</param>
        void DrawQuizQuestion(QuizQuestion question, Guid highlitedAnswerId);

        /// <summary>
        /// After the game is finished, this method draws the overall result
        /// </summary>
        /// <param name="totalQuestionCount">Count of all asked questions</param>
        /// <param name="correctAnswersCount">Cound of questions the user got correct</param>
        void DrawGameResult(int totalQuestionCount, int correctAnswersCount);

        /// <summary>
        /// Before the game starts, this method draws the count of answers loaded from the file
        /// </summary>
        void DrawGameStart(int totalQuestionCount);

        /// <summary>
        /// When no questions could be loaded from the file, this method tells the user to update the json question file
        /// </summary>
        void DrawNoQuestions();

        /// <summary>
        /// When the game is finished, this method asks the user if they want to play another round of the game
        /// </summary>
        void DrawPlayAgain();
    }
}
