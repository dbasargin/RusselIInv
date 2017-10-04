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
                Console.WriteLine("Error occurred creating file: Exception type of " + ex.GetType() );
            }
            return false;
        }

        /// <summary>
        /// Creates a path for new .txt file
        /// </summary>
        /// <returns>fully qualified path as a string</returns>
        public static string CreatePath()
        {
            string txtFileName = "DennisBProject.txt";
            string path = @"c\";
            var folder = Path.Combine(path, "DennisBFolder");
            Directory.CreateDirectory(folder);
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
                Console.WriteLine(ex.GetType() + "::Error adding line to: " + path);
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
            //initialize stream
            StreamReader strmReader = null;

            //try accessing and reading file
            try
            {
                using (strmReader = new StreamReader(path))
                {
                    List<String> textFileLines = new List<string>();

                    while (!strmReader.EndOfStream)
                    {
                        string newLine = strmReader.ReadLine();
                        textFileLines.Add(newLine);
                    }

                    //Orders Lines by string length with LINQ(insures best practice)
                    textFileLines = textFileLines.OrderByDescending(x => x.Length).ToList();

                    //return without displaying lines if file is empty
                    if (textFileLines.Count() == 0)
                    {
                        strmReader.Dispose();
                        return;
                    }
                    // display lines with longest string length
                    else
                    {
                        for (int i = 0; i < textFileLines.Count(); i++)
                        {
                            if (i == 0 || textFileLines[i].Length == textFileLines[0].Length)
                            {
                                Console.WriteLine(textFileLines[i]);
                            }
                        }

                        AddEmptyLine();
                    }
                }
            }
            //add error message if access to file fails
            catch
            {
                Console.WriteLine("error accessing path: " + path);
            }
            //Dispose stream
            finally
            {
                if (strmReader != null)
                {
                    strmReader.Dispose();
                }

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
