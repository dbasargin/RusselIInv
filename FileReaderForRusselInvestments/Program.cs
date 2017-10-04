using System;
using System.IO;

namespace FileReaderForRusselInvestments
{
    class Program
    {

        /// <summary>
        /// This Program creates a .txt file on Users machine which allows the user to add lines of text to the file from standard input.
        ///  User can choose an option to reads lines from standard  text file  and, upon end of file, writes the longest line to standard output. 
        ///  If there are ties for the longest line, the program writes out all the lines that tie. If there is no input, the program should produce no output.
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            DisplayProgramHeader();
            string path = FileHelperClass.CreatePath();
            bool fileCreated =  FileHelperClass.CreateInitialTextFile(path);

            //initialize menuOption to get started
            string menuOption = "0";

            if (fileCreated)
            { 
                //Continue program until user selects option 3)Quit
                while (menuOption != "3")
                {
                    DisplayMenuOptions();
                    menuOption =  SelectMenuOption();

                    if(menuOption == "1")
                    {
                        FileHelperClass.CreateFileContent(path);
                    }
                    if (menuOption == "2")
                    {
                        FileHelperClass.DisplayLongestLineInFile(path);
                    }
                }
            }
        }


        /// <summary>
        /// Reads and returns users input for selected menu options
        /// </summary>
        /// <returns></returns>
        private static string SelectMenuOption()
        {
            string input = "0";

            try
            {
                input = Console.ReadLine();

                if (input != "1" && input != "2" && input != "3")
                {
                    Console.WriteLine("Invalid Menu Option: try again");
                }
            }
            //if user response is invalid: retry
            catch
            {
                Console.WriteLine("Invalid Menu Option: try again");
            }

            AddEmptyLine();

            return input;

        }


        /// <summary>
        /// This is the program header displayed at the beggining of program
        /// </summary>
        private static void DisplayProgramHeader()
        {
            Console.WriteLine("Welcome! This program will read the longest lines of a .txt file.");
            Console.WriteLine("If there are ties for the longest line, the program writes out all the lines that tie.");
            Console.WriteLine("If there is no input, the program will produce no output.");
            AddEmptyLine();
        }

        /// <summary>
        /// Displays menu to console window
        /// </summary>
        private static void DisplayMenuOptions()
        {
            Console.WriteLine("Please type an option number below and press enter:");
            Console.WriteLine("1) Add content to a file");
            Console.WriteLine("2) Display file line with longest text");
            Console.WriteLine("3) Quit");
        }

        /// <summary>
        /// Adds empty line to console window:
        /// Makes program easier to read
        /// </summary>
        public static void AddEmptyLine()
        {
            Console.WriteLine();
        }

    }
}