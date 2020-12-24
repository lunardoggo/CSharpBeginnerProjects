using System;

namespace LunarDoggo.QuizGame.Visuals
{
    public interface IVisualizer
    {
        void DrawAnswerStatus(bool correct, QuizQuestionAnswer correctAnswer);
        void DrawQuizQuestion(QuizQuestion question, Guid highlitedAnswerId);
        void DrawGameResult(int totalQuestionCount, int correctAnswersCount);
        void DrawGameStart(int totalQuestionCount);
        void DrawNoQuestions();
        void DrawPlayAgain();
    }
}
