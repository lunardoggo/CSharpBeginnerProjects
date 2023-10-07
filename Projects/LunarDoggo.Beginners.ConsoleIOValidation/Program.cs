using System.Collections.Generic;
using System;

namespace LunarDoggo.ConsoleIOValidation
{
    class Program
    {
        /*
         * you can declare and initialize variables outside of methods. Note, that a variable must be static
         * if you want to use it in a static method
         * a List is a collection of objects, in this case strings
         * if a variable is readonly, it can only be assigned in a constructor or directly like here
         * The type "List" is contained inside of the namespace "System.Collections.Generic"
         */
        private readonly static List<string> games = new List<string>();

        private static void Main(string[] args)
        {
            //Similarily to variables, you can also call your methods by their names. Program just provides 
            //further information for you, where to find the method
            Program.AddGames();

            Program.PrintGameSelection();

            //the output of a method can directly be assigned to a variable
            int gameIndex = Program.GetSelectedGame();

            //we get the value at the users selected index by using the indexer of our list:
            //Program.games[index]; note that an IndexOutOfRangeException is thrown if you try to access an invalid index (eg. less than 0)
            string gameName = Program.games[gameIndex];

            //parameters are input in the same order as they are declared in the method-signature
            Program.PrintGameChoice(gameName);

            Console.ReadKey();
        }

        /*
         * you can add your custom static methods by using the format:
         * [visibility-modifier] static <return-type> <name>([parameters])
         * this method is defined as private and therefore can only be accessed inside of the class Program, empty 
         * parenthesis mean, that the method doesn't take any parameters
         */
        private static void AddGames()
        {
            /*
             * static variables, that are declared outside of any method can be accessed by writing their name. It is advisable 
             * that you write the class-name before the variable-name in order to give you more information about its context.
             * here we just add some entries to the games-dictionary
             */
            Program.games.Add("The Wither 3 - Wild Hunt");
            Program.games.Add("Baba is you");
            Program.games.Add("Factorio");
            Program.games.Add("Cities Skylines");
            Program.games.Add("Kerbal space program");
        }

        private static void PrintGameSelection()
        {
            /*
             * a for-loop executes the code inside of its body as many times as the loop-condition is met and can be defined with the following format:
             * for(<counter-variable initialization and declaration>; <loop-condition>; <decrement/increment>) { <code to be executed by the loop> }
             * 
             * In this case we declare a new integer-variable with the name "i" and assign the value "0" to it. For the condition we check if the
             * coutner-variable is less than the current count of games in our previously defined list of games. If the condition is not met,
             * the counter-variable is incremented by one (i++) otherwise the loop is exited
             */
            for (int i = 0; i < Program.games.Count; i++)
            {
                //We just print the index of the game and the name of it to the console
                Console.WriteLine("[" + i + "]: " + Program.games[i]);
            }
        }

        //If your method should return a value, just add the name of the return-type instead of void to the signature
        private static int GetSelectedGame()
        {
            //variables can be declared without assigning a value to it, this should only be done if you assign a value shortly afterwards
            int chosenIndex;
            string choice;

            //a do-while-loop first enters its body, executes the contained code and afterwards checks the condition
            do
            {
                //Note that in C# collections (array, list, dictionary, ...) start at the index 0, therefore the last index 
                //is the length of the collection minus one
                Console.Write("Please select a game by it's index-number (between 0 and " + (Program.games.Count - 1) + "): ");
                choice = Console.ReadLine();
                //Int32.TryParse is a method to check if a string is a number and if this is the case to put out the parsed value to the out-parameter-variable
                //similar methods exist for double, bool, DateTime, and many others
                //in order to prevent errors further on, we also check if the chosen index is a valid index of our list
                //with the "!"-operator, the whole boolean-expression inside the parenthesis is negated
            } while (!(Int32.TryParse(choice, out chosenIndex) && chosenIndex >= 0 && chosenIndex < Program.games.Count));

            //the return-value of your custom method is returned via a return-statement:
            //return <value>;
            return chosenIndex;
        }

        //If you want to add paramters to your methods, use the format "<parameter-type> <parameter-name>" for each parameter,
        //if you want to add multiple parameters, separate them with a colon
        private static void PrintGameChoice(string gameName)
        {
            Console.WriteLine();
            //As you normally cant directly write doublequotes into C#-strings, we use the escape-character backslash "\",
            //which tells the compiler, that the following character is to be literally inserted into the string.
            Console.WriteLine("So you have chosen \"" + gameName + "\". GLHF :)");
        }
    }
}
