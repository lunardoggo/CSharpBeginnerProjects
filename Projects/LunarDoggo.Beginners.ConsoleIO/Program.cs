//You can include namespaces with "using" in order to get access to the classes you need. using-directives always must be before any class declarations in your C#-file
//The namespace "System" contains many things you could need, in this example we need "Console" from this namespace
using System;

namespace LunarDoggo.ConsoleIO
{
    class Program
    {
        /// <summary>
        /// This is the entrypoint to your application. When you execute your application, this method is called with the parameters
        /// you specified in the application-call passed into "args". void just means, the Method doesn't return anything.
        /// static isn't important as of now, just know, that your Main-method must always be static
        /// </summary>
        static void Main(string[] args)
        {
            //Console.WriteLine() writes the provided string to the console-window and sets the cursor to the next line
            Console.WriteLine("Hi, I'm a simple console application written in C# that can prompt the user for input.");
            //Console.WriteLine() without any provided string just adds an empty line to the console output
            Console.WriteLine();
            //Console.Write writes the provided string to the console-window, the cursor will be on the same line
            Console.Write("Please tell me your name: ");

            /*
             * variables are declared in the format:
             * <datatype> <variablename> = <value>
             * In this case we declare a new string-variable with the name "name" and assign the return value of Console.ReadLine() to it
             * Console.ReadLine() waits for the user to input characters to the console and returns these characters after the Return-key is pressed
             */
            string name = Console.ReadLine();

            Console.WriteLine();

            /*
             * if-statements allow you to execute a portion of your code only when a condition is met. In combination with "else" or "else if" you
             * can also branch your code into multiple execution-paths
             * 
             * an if-statement follows the following format:
             * if(<expression that returns a bool>) { <the code to execute when the condition is met> }
             * 
             * an else-if-statement follows a similar format and can be appended after the closing "}" of an if-statements body:
             * else if(<expression that returns a bool>) { <the code to execute when the condition is met> }
             * 
             * an else-statement can be appended after an if- or else-if-statement, it doesn't have a condition and is executed, when none of the previous 
             * statements-conditions are met. It follows the format:
             * else { <the code to execute when no condition was met> }
             * 
             * in this case the condition of the if-statement is String.IsNullOrEmpty(name) which checks if the value of name-variable consists of any
             * non-space-character and returns a false if this is the case, true is returned, when no non-space-chcaracters are contained or the strings length is 0
             */
            if (String.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("So you don't have a name? All right, then. Keep your secrets.");
            }
            else
            {
                //multiple trings can be concatenated into one by adding the "+"-operator between them
                //with the Trim()-method we remove all leading and trailing spaces in order to get a nice output without too many spaces between the text and the name
                Console.WriteLine("So your name is " + name.Trim() + ", nice to meet you. Have a great day :)");
            }

            //Console.ReadKey() waits for any key, that is not a control-key (Ctrl, Shift, Alt, ...), to be pressed and returns the pressed key (which we don't use in this example)
            Console.ReadKey();
            //after any key was pressed, the Main-Method is exited and the application terminated (= closed)
        }
    }
}