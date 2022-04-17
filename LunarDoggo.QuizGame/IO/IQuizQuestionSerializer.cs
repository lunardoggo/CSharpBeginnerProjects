using System.Collections.Generic;

namespace LunarDoggo.QuizGame.IO
{
    /*
     * Interfaces in C# are like blueprints for classes, every class that "implements" this interface must also define all
     * methods and properties of this interface as public members.
     * Interfaces can be used to define common methods and properties of multiple classes in order to be able to change
     * the instance of a concrete class in the source code without having to rewrite the application.
     * In the real world interfaces are for example used, when you have multiple datasources like databases, files, ...
     * or rendering-libraries like DirectX, OpenGL, Vulcan, ... which can be swapped in and out at runtime
     * 
     * In this case, all classes that implement IQuizQuestionSerializer must declare the public method DeserializeQuestions() 
     * that returns a IEnumerable<QuizQuestion>
     * 
     * Serialization is the process of converting a data structure/object into a storable format (Json, XML, BIN, ...)
     * Deserialization is the reverse, content of a storable format is converted into a data structure/object to be used in your application
     */
    public interface IQuizQuestionSerializer
    {
        /// <summary>
        /// Load all <see cref="QuizQuestion"/>s from the datasource
        /// </summary>
        IEnumerable<QuizQuestion> DeserializeQuestions();
    }
}
