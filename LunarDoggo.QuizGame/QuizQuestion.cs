using System.Text.Json.Serialization;
using System.Linq;
using System;

namespace LunarDoggo.QuizGame
{
    public class QuizQuestion
    {
        public QuizQuestionAnswer[] Answers { get; set; }
        public string Question { get; set; }
        [JsonIgnore]
        public Guid Id { get; set; }

        public QuizQuestionAnswer CorrectAnswer { get { return this.Answers.Single(_answer => _answer.IsCorrect); } }
    }

    public class QuizQuestionAnswer
    {
        public bool IsCorrect { get; set; }
        public string Answer { get; set; }
        [JsonIgnore] 
        public Guid Id { get; set; }
    }
}
