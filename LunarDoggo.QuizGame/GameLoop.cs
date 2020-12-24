using LunarDoggo.QuizGame.Visuals;
using System.Collections.Generic;
using System;

namespace LunarDoggo.QuizGame
{
    public class GameLoop
    {
        private readonly IVisualizer visualizer;
        private readonly GameState state;
        
        private bool isStarted;

        public bool IsFinished { get; private set; }

        public GameLoop(IVisualizer visualizer, IEnumerable<QuizQuestion> questions)
        {
            this.state = new GameState(questions);
            this.visualizer = visualizer;
        }

        public void DoTick()
        {
            bool finished = !this.state.HasUnansweredQuestions;
            this.UpdateScreen();
            this.ProcessInput();
            this.IsFinished = finished;
        }

        private void ProcessInput()
        {
            ConsoleKeyInfo key = Console.ReadKey();

            switch (key.Key)
            {
                case ConsoleKey.UpArrow:
                    this.ChangeHighlightedAnswer(true);
                    break;
                case ConsoleKey.DownArrow:
                    this.ChangeHighlightedAnswer(false);
                    break;
                case ConsoleKey.Enter:
                    this.ProcessEnterPress();
                    break;
            }
        }

        private void UpdateScreen()
        {
            if (!this.isStarted)
            {
                this.visualizer.DrawGameStart(this.state.TotalQuestionCount);
            }

            QuizQuestion question = this.state.CurrentQuestion;
            if (question != null)
            {
                this.visualizer.DrawQuizQuestion(question, this.state.HighlightedAnswer.Id);
            }
            if(this.state.IsCurrentQuestionAnswered)
            {
                this.visualizer.DrawAnswerStatus(this.state.PreviousAnswer.IsCorrect, question.CorrectAnswer);
            }

            if (!this.state.HasUnansweredQuestions)
            {
                this.visualizer.DrawGameResult(this.state.TotalQuestionCount, this.state.CorrectAnswersCount);
            }
        }

        private void ChangeHighlightedAnswer(bool up)
        {
            if (this.state.CurrentQuestion != null && !this.state.IsCurrentQuestionAnswered)
            {
                if (up)
                {
                    this.state.HighlightPreviousAnswer();
                }
                else
                {
                    this.state.HighlightNextAnswer();
                }
            }
        }

        private void ProcessEnterPress()
        {
            if(this.isStarted)
            {
                if(this.state.IsCurrentQuestionAnswered)
                {
                    this.state.MoveToNextQuestion();
                }
                else
                {
                    this.state.AnswerQuestion();
                }
            }
            else
            {
                this.isStarted = true;
                this.state.MoveToNextQuestion();
            }
        }
    }
}
