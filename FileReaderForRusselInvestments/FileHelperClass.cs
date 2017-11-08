using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileReaderForRusselInvestments
{
    /// <summary>
    /// This is the file helper class and contains methods for 
    /// creating and manipulating files
    /// </summary>
    public static class FileHelperClass
    {
        /// <summary>
        /// This method creates the initial .txt file if it does not already exist;
        /// if it does exist the method clears the file from any text.
        /// </summary>
        public static bool CreateInitialTextFile(string path)
        {
            try
            {
                if (!File.Exists(path))
                {
                    FileStream fStream = File.Create(path);
                    fStream.Close();
                }
                else
                {
                    ClearFileContent(path);
                }
                return true;
            }
            //display error 
            catch(Exception ex)
            {
                ExceptionHandler(ex, "Error occured while Creating Text File");
            }
            return false;
        }

        /// <summary>
        /// Exceptions are passed through this method with a message which formats, handles, and displays 
        /// Errors in a consistent and easy to maintain location. 
        /// </summary>
        /// <param name="ex">exception being thrown</param>
        /// <param name="errorMessage">discription of where this error is occuring</param>
        public static void ExceptionHandler(Exception ex, string errorMessage)
        {
            Console.WriteLine("Error occurred : Exception type of " + ex.GetType());
            Console.WriteLine(" :: " + errorMessage);

            //I would consider adding an email feature right here to the development team
            //with the exception and error message

        }

        /// <summary>
        /// Creates a path for new .txt file
        /// </summary>
        /// <returns>fully qualified path as a string</returns>
        public static string CreatePath()
        {
            string path = @"c:\DennisBFolder\";

            try
            {
                System.IO.Directory.CreateDirectory(path);
            }
            catch (Exception ex)
            {
                ExceptionHandler(ex, "Error occured while Creating Path");
            }
        

            string txtFileName = "DennisBProject.txt";
            return path + txtFileName;
        }

        /// <summary>
        /// Adds a line to a .txt file
        /// </summary>
        /// <param name="path">path of the file</param>
        /// <param name="line">the line you want added to file</param>
        public static void AppendFile(string path, string line)
        {
            StreamWriter strmWriter = null;

            try
            {
                using (strmWriter = (File.Exists(path)) ? File.AppendText(path) : File.CreateText(path))
                {
                    strmWriter.WriteLine(line);
                }
                
            }
            catch(Exception ex)
            {
                
            }
            finally
            {
                if(strmWriter != null)
                {
                    strmWriter.Dispose();
                }
            }
            
        }

        /// <summary>
        /// overwrites existing file with empty file
        /// </summary>
        /// <param name="path">path to file</param>
        public static void ClearFileContent(string path)
        {
            var fl = File.Create(path);
            fl.Close();
        }

        /// <summary>
        /// gets all lines from file, sorts by line length and displays longest line, 
        /// longest lines with the same length or nothing if file is empty
        /// </summary>
        /// <param name="path">path to file</param>
        public static void DisplayLongestLineInFile(string path)
        {
            try
            {
                // consume file into array
                string[] textFileLines = File.ReadAllLines(path);
                
                if(textFileLines.Count() > 0)
                {
                    //get lines of longest Length
                    textFileLines = textFileLines.Where(x => x.Length == textFileLines.Max(m => m.Length)).ToArray();

                    // display lines with longest string length
                    foreach (var line in textFileLines)
                    {
                        Console.WriteLine(line);
                    }
                    
                    AddEmptyLine();
                }
                
            }
            //add error message if access to file fails
            catch(Exception ex)
            {
                ExceptionHandler(ex, "Error occurred while attempting to display longest line");
            }
        }

        /// <summary>
        /// allows user to add lines to a .txt file.
        /// </summary>
        /// <param name="path">path to file</param>
        public static void CreateFileContent(string path)
        {
            //message to user
            string displayAddContentInstructions = "Type a line of text and click ENTER to add a line of text to your file.";
            Console.WriteLine(displayAddContentInstructions);

            bool addAnotherLine = true;

            //allow user to add mulitple lines
            while (addAnotherLine)
            {
                string input = Console.ReadLine();
                FileHelperClass.AppendFile(path, input);
                AddEmptyLine();

                //if user response is invalid: retry
                bool invalidInput = true;

                while (invalidInput)
                {
                    Console.WriteLine("Add another Line?(Y/N)");

                    //read user input and convert it to lower
                    string continueInput = Console.ReadLine().ToLower();
                    AddEmptyLine();

                    if (continueInput == "y")
                    {
                        addAnotherLine = true;
                        invalidInput = false;
                        Console.WriteLine(displayAddContentInstructions);
                    }
                    else if (continueInput == "n")
                    {
                        addAnotherLine = false;
                        invalidInput = false;
                    }
                    else
                    {
                        Console.WriteLine(continueInput + " is invalid input: RETRY");
                    }
                }
            }
        }

        /// <summary>
        /// Adds empty line to console window
        /// </summary>
        public static void AddEmptyLine()
        {
            Console.WriteLine();
        }
    }
}
