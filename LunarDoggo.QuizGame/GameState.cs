using System.Collections.Generic;
using System.Linq;
using System;

namespace LunarDoggo.QuizGame
{
    public class GameState
    {
        private readonly Dictionary<QuizQuestion, bool> givenAnswers = new Dictionary<QuizQuestion, bool>();
        private readonly List<QuizQuestion> unansweredQuestions = new List<QuizQuestion>();
        private readonly Random random = new Random();

        private int highlightedAnswerIndex;

        public GameState(IEnumerable<QuizQuestion> questions)
        {
            if (questions != null && questions.Any())
            {
                this.unansweredQuestions.AddRange(questions);
                this.TotalQuestionCount = this.unansweredQuestions.Count;
            }
        }

        public QuizQuestionAnswer HighlightedAnswer { get { return this.CurrentQuestion?.Answers[this.highlightedAnswerIndex]; } }
        public int CorrectAnswersCount { get { return this.givenAnswers.Where(_pair => _pair.Value).Count(); } }
        public bool HasUnansweredQuestions { get { return this.unansweredQuestions.Count > 0; } }
        public int AnsweredQuestionCount { get { return this.givenAnswers.Count; } }
        public QuizQuestionAnswer PreviousAnswer { get; private set; }
        public bool IsCurrentQuestionAnswered { get; private set; }
        public QuizQuestion CurrentQuestion { get; private set; }
        public int TotalQuestionCount { get; }

        public void MoveToNextQuestion()
        {
            if (this.HasUnansweredQuestions)
            {
                this.CurrentQuestion = this.unansweredQuestions[random.Next(0, this.unansweredQuestions.Count)];
                this.IsCurrentQuestionAnswered = false;
                this.highlightedAnswerIndex = 0;
                this.PreviousAnswer = null;
            }
        }

        public void HighlightNextAnswer()
        {
            this.ChangeHighlightedAnswer(1);
        }

        public void HighlightPreviousAnswer()
        {
            this.ChangeHighlightedAnswer(-1);
        }

        private void ChangeHighlightedAnswer(int indexIncrement)
        {
            int answerCount = this.CurrentQuestion.Answers.Length;
            this.highlightedAnswerIndex += indexIncrement;

            if(this.highlightedAnswerIndex >= answerCount)
            {
                this.highlightedAnswerIndex = 0;
            }
            else if(this.highlightedAnswerIndex < 0)
            {
                this.highlightedAnswerIndex = answerCount - 1;
            }
        }

        public void AnswerQuestion()
        {
            if(this.CurrentQuestion != null)
            {
                QuizQuestionAnswer givenAnswer = this.CurrentQuestion.Answers[highlightedAnswerIndex];
                this.unansweredQuestions.Remove(this.CurrentQuestion);
                this.givenAnswers.Add(this.CurrentQuestion, givenAnswer.IsCorrect);
                this.IsCurrentQuestionAnswered = true;
                this.PreviousAnswer = givenAnswer;
            }
        }
    }
}
