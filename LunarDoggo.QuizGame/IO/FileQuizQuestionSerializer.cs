using System.Collections.Generic;
using System.Text.Json;
using System.IO;
using System;

namespace LunarDoggo.QuizGame.IO
{
    public class FileQuizQuestionSerializer : IQuizQuestionSerializer
    {
        private readonly string filePath;
        public FileQuizQuestionSerializer(string filePath)
        {
            this.filePath = filePath;
        }

        public IEnumerable<QuizQuestion> DeserializeQuestions()
        {
            string content = this.GetFileContent(this.filePath);
            IEnumerable<QuizQuestion> questions = this.DeserializeJson(content);
            this.SetGuids(questions);
            return questions;
        }

        private void SetGuids(IEnumerable<QuizQuestion> questions)
        {
            foreach(QuizQuestion question in questions)
            {
                question.Id = Guid.NewGuid();
                foreach(QuizQuestionAnswer answer in question.Answers)
                {
                    answer.Id = Guid.NewGuid();
                }
            }
        }

        private IEnumerable<QuizQuestion> DeserializeJson(string content)
        {
            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                ReadCommentHandling = JsonCommentHandling.Skip,
                PropertyNameCaseInsensitive = true,
                AllowTrailingCommas = true,
                IgnoreNullValues = true
            };

            return JsonSerializer.Deserialize<QuizQuestion[]>(content, options);
        }

        private string GetFileContent(string filePath)
        {
            if(File.Exists(filePath))
            {
                return File.ReadAllText(filePath);
            }
            return String.Empty;
        }
    }
}
