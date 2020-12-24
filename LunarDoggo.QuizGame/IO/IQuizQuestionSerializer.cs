using System.Collections.Generic;

namespace LunarDoggo.QuizGame.IO
{
    public interface IQuizQuestionSerializer
    {
        IEnumerable<QuizQuestion> DeserializeQuestions();
    }
}
