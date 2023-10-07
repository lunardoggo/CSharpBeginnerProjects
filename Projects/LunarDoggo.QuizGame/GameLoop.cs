using LunarDoggo.QuizGame.Visuals;
using System.Collections.Generic;
using System;

namespace LunarDoggo.QuizGame
{
    /// <summary>
    /// This class contains the logic to run the game
    /// </summary>
    public class GameLoop
    {
        private readonly IVisualizer visualizer; //swappable IVisualizer-instance due to interface-usage
        private readonly GameState state;

        private bool isStarted; //Indicator if the game has started yet

        public bool IsFinished { get; private set; }

        /// <summary>
        /// Creates a new instance of <see cref="GameLoop"/> with the provided <see cref="IVisualizer"/>-instance and
        /// provided <see cref="QuizQuestion"/>s
        /// </summary>
        public GameLoop(IVisualizer visualizer, IEnumerable<QuizQuestion> questions)
        {
            this.state = new GameState(questions);
            this.visualizer = visualizer;
        }

        /// <summary>
        /// Runs one cycle of the game (rendering, input processing)
        /// </summary>
        public void DoTick()
        {
            //the is finished-flag is stored in this local variable and set after all other game loop related
            //tasks have finished because otherwise the status (correct, wrong) of the last answer of the game
            //isn't displayed to the user because the game is finished before the cycle, the status would be printed
            bool finished = !this.state.HasUnansweredQuestions;
            //First draw the content to the screen, afterwards process the input
            this.UpdateScreen();
            this.ProcessInput();
            this.IsFinished = finished;
        }

        /// <summary>
        /// Queries and processes the current input
        /// </summary>
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

        /// <summary>
        /// Updates the screen according to the current game state
        /// </summary>
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
            if (this.state.IsCurrentQuestionAnswered)
            {
                this.visualizer.DrawAnswerStatus(this.state.ChosenAnswer.IsCorrect, question.CorrectAnswer);
            }

            if (!this.state.HasUnansweredQuestions)
            {
                this.visualizer.DrawGameResult(this.state.TotalQuestionCount, this.state.CorrectAnswersCount);
            }
        }

        /// <summary>
        /// Moves the cursor up or down to highlight the previous or next question respectively
        /// </summary>
        /// <param name="up">flag whether the cursor should be moved upwards to the previous answer</param>
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

        /// <summary>
        /// Takes the suitable action according to the game state if the enter-key was pressed
        /// </summary>
        private void ProcessEnterPress()
        {
            if (this.isStarted)
            {
                if (this.state.IsCurrentQuestionAnswered)
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
