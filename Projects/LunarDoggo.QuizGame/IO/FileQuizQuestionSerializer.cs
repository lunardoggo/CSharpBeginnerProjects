using System.Collections.Generic;
using System.Text.Json;
using System.IO;
using System;
using System.Text.Json.Serialization;

namespace LunarDoggo.QuizGame.IO
{
    /*
     * In this case, FileQuizQuestionSerializer implements the interface IQuizQuestionSerializer so that other
     * serializers (for example database) can be implemented to swap between the implementations
     */
    public class FileQuizQuestionSerializer : IQuizQuestionSerializer
    {
        private readonly string filePath;

        /// <param name="filePath">Absolute or relative file path to the quiz question json file</param>
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

        /// <summary>
        ///We don't trust the user to correctly set the Ids of the <see cref="QuizQuestion"/> and <see cref="QuizQuestionAnswer"/>. In order to prevent duplicate Ids
        ///this method assigns a unique <see cref="Guid"/> to every <see cref="QuizQuestion"/> and its <see cref="QuizQuestionAnswer"/>s
        /// </summary>
        private void SetGuids(IEnumerable<QuizQuestion> questions)
        {
            foreach (QuizQuestion question in questions)
            {
                //Guid.NewGuid() generates a new random Guid
                question.Id = Guid.NewGuid();
                foreach (QuizQuestionAnswer answer in question.Answers)
                {
                    answer.Id = Guid.NewGuid();
                }
            }
        }

        private IEnumerable<QuizQuestion> DeserializeJson(string content)
        {
            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull, //if a property has the value "null", it will be ignored
                ReadCommentHandling = JsonCommentHandling.Skip, //when the json content contains comments, they will be ignored
                PropertyNameCaseInsensitive = true, //the casing of propertynames will be ignored for the deserialization
                AllowTrailingCommas = true //if the json content contains a lonely ",", it will be ignored
            };

            //System.Text.Json.JsonSerializer deserializes the content string into a QuizQuestion array 
            return JsonSerializer.Deserialize<QuizQuestion[]>(content, options);
        }

        private string GetFileContent(string filePath)
        {
            //Check if the file exists, if not, return an empty string to prevent a FileNotFoundException, otherwise return the files content
            //Note that file access violations (e. g. another application has the file locked) are not handled and will lead to an exception
            if (File.Exists(filePath))
            {
                return File.ReadAllText(filePath);
            }
            return String.Empty;
        }
    }
}
