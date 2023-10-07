using System.Collections.Generic;
using System.Linq;
using System;

namespace LunarDoggo.QuizGame
{
    /// <summary>
    /// This class is a data container that contains all data regarding the game state
    /// </summary>
    public class GameState
    {
        //all answered questions are stored with the flag if the given answer was correct or wrong
        private readonly Dictionary<QuizQuestion, bool> givenAnswers = new Dictionary<QuizQuestion, bool>();
        private readonly List<QuizQuestion> unansweredQuestions = new List<QuizQuestion>();
        //Random for choosing a random unanswered question from the list
        //Note: The default constructor of Random in most languages takes the current system time as a seed for this reason, 
        //Random-instances that are instantiated in loops can sometimes have the same seed and therefore return the same values
        private readonly Random random = new Random();

        //index of the currently highlighted answer in the answer array of the current question
        private int highlightedAnswerIndex;

        /// <summary>
        /// Instantiates a new <see cref="GameState"/>-object with the provided <see cref="QuizQuestion"/>s
        /// </summary>
        public GameState(IEnumerable<QuizQuestion> questions)
        {
            if (questions != null && questions.Any())
            {
                this.unansweredQuestions.AddRange(questions);
                this.TotalQuestionCount = this.unansweredQuestions.Count;
            }
        }

        /// <summary>
        /// Returns the currently hightlighted <see cref="QuizQuestionAnswer"/>
        /// </summary>
        public QuizQuestionAnswer HighlightedAnswer { get { return this.CurrentQuestion?.Answers[this.highlightedAnswerIndex]; } }

        /// <summary>
        /// Returns the count of answers the user got right
        /// </summary>
        public int CorrectAnswersCount
        {
            //With the extension methods provided by System.Linq.Enumerable you can write Lambda-Expressions which are like anonymous/nameless methods that operate on a 
            //given dataset (array, list, database, xml, ...). In this case we use the method "Where" which takes an expression that iterates the QuizQuestion-instances of givenAnswers, does some 
            //Format: ...Where({temporary variable name} => {expression that returns a boolean})
            //learn more about lambda-expressions: https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/lambda-expressions
            //learn more about System.Linq.Enumerable extension methods:
            // - available methods: https://docs.microsoft.com/en-us/dotnet/api/system.linq.enumerable?view=net-5.0
            // - how to use them: https://www.dotnetperls.com/ienumerable
            get { return this.givenAnswers.Where(_pair => _pair.Value).Count(); }
        }

        /// <summary>
        /// Returns whether there are unanswered <see cref="QuizQuestion"/>s left or not
        /// </summary>
        public bool HasUnansweredQuestions { get { return this.unansweredQuestions.Count > 0; } }

        /// <summary>
        /// Returns the count of <see cref="QuizQuestion"/>s the user has already answered
        /// </summary>
        public int AnsweredQuestionCount { get { return this.givenAnswers.Count; } }

        /// <summary>
        /// Returns the <see cref="QuizQuestionAnswer"/> the user has chosen for <see cref="CurrentQuestion"/>
        /// </summary>
        public QuizQuestionAnswer ChosenAnswer { get; private set; }

        /// <summary>
        /// Returns whether the user already answered <see cref="CurrentQuestion"/>
        /// </summary>
        public bool IsCurrentQuestionAnswered { get; private set; }

        /// <summary>
        /// Returns the currently active <see cref="QuizQuestion"/>
        /// </summary>
        public QuizQuestion CurrentQuestion { get; private set; }

        /// <summary>
        /// Returns the total count of <see cref="QuizQuestion"/>s in the game
        /// </summary>
        public int TotalQuestionCount { get; }

        /// <summary>
        /// Chooses the next <see cref="QuizQuestion"/> to be asked
        /// </summary>
        public void MoveToNextQuestion()
        {
            if (this.HasUnansweredQuestions) //if there are unanswered questions left, we want to choose the next one
            {
                int questionIndex = random.Next(0, this.unansweredQuestions.Count); //randomly choose the index of the next question from GameState.unansweredQuestions
                this.CurrentQuestion = this.unansweredQuestions[questionIndex];
                //Reset the data regarding the last question
                this.IsCurrentQuestionAnswered = false;
                this.highlightedAnswerIndex = 0;
                this.ChosenAnswer = null;
            }
        }

        /// <summary>
        /// This method sets the next answer to be highlighted, it wraps around if the last answer is passed
        /// </summary>
        public void HighlightNextAnswer()
        {
            this.ChangeHighlightedAnswer(1);
        }

        /// <summary>
        /// This method sets the previous answer to be highlighted, it wraps around if the first answer is passed
        /// </summary>
        public void HighlightPreviousAnswer()
        {
            this.ChangeHighlightedAnswer(-1);
        }

        /// <summary>
        /// This method sets the answer to be highlighted depending on the provided increment, it wraps around if the last or first
        /// answer if <see cref="CurrentQuestion"/> is passed
        /// </summary>
        private void ChangeHighlightedAnswer(int indexIncrement)
        {
            int answerCount = this.CurrentQuestion.Answers.Length;
            this.highlightedAnswerIndex += indexIncrement;

            if (this.highlightedAnswerIndex >= answerCount)
            {
                this.highlightedAnswerIndex = 0;
            }
            else if (this.highlightedAnswerIndex < 0)
            {
                this.highlightedAnswerIndex = answerCount - 1;
            }
        }

        /// <summary>
        /// Sets <see cref="CurrentQuestion"/> answered and updates the related data
        /// </summary>
        public void AnswerQuestion()
        {
            if (this.CurrentQuestion != null)
            {
                QuizQuestionAnswer givenAnswer = this.CurrentQuestion.Answers[highlightedAnswerIndex];
                //remove the current question from the list of unanswered questions so it isn't asked again
                this.unansweredQuestions.Remove(this.CurrentQuestion);
                //add the current question and whether the answer was correct to GameState.givenAnswers for the final result
                this.givenAnswers.Add(this.CurrentQuestion, givenAnswer.IsCorrect);
                this.IsCurrentQuestionAnswered = true;
                this.ChosenAnswer = givenAnswer;
            }
        }
    }
}
