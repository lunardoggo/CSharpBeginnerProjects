using System.Text.Json.Serialization;
using System.Linq;
using System;

namespace LunarDoggo.QuizGame
{
    /*
     * These are simple data containers
     * 
     * Note: you can define multiple classes in the same file, in theory you could define your whole application in a single file, for
     * small classes this is ok, for big classes with a lot of logic, it is advisable to split the classes into their own files to increase
     * the readability and clarity of your code
     */
    public class QuizQuestion
    {
        public QuizQuestionAnswer[] Answers { get; set; }
        public string Question { get; set; }
        /*
         * members of a class can have attributes that define metadata of that member. The metadata can be directed towards the compiler
         * (e.g. System.Runtime.CompilerServices.CallerMemberName) or can be used at runtime (e.g. JsonIgnore)
         * 
         * a notable example is the attribute "System.ComponentModel.DataAnnotations.Display" which in many ui-frameworks is used to give
         * properties speaking names in the UI instead of the name the property has in the code
         * 
         * The JsonIgnore-attribute tells the JsonSerializer if it is used by the application to ignore this attribute during serialization 
         * and deserialization. If the application doesn't use the JsonSerializer, the attribute is still defined but never used
         */
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
