using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace Assignment3
{
    internal class Program
    {
        /********************************************************************************
         * Author: Camden Taylor
         * Assignment Name: Methods, Arrays and File I/O
         * Date: November 14th, 2025
         *******************************************************************************/
        /// <summary>
        /// Main method for Assignment 3.
        /// Program allows the user to enter/save/load/edit/view daily time tracking values from a file.
        /// Allows simple data analysis for a given month.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            bool continueProgram = true;

            // TODO: 
            // declare a constant to represent the maximum size of the arrays
            // arrays must be large enough to store data for an entire month 
            const int MONTH_DAYS = 31;
            // TODO:
            // create a string array named dates, using the max size constant you created above to specify the physical size of the array
            string[] dates = new string[MONTH_DAYS];
            // TODO:
            // create a double array named minutes, using the max size constant you created above to specify the physical size of the array
            double[] minutes = new double[MONTH_DAYS];
            // TODO:
            // create a variable to represent the logical size of the array
            int logicalMonth = 0;
            int count = 0;

            DisplayProgramIntro();

            // TODO: call DisplayMainMenu()
            DisplayMainMenu();

            while (continueProgram)
            {
                string mainMenuChoice = Prompt("Enter MAIN MENU option ('D' to display menu): ").ToUpper();
                Console.WriteLine();

                //MAIN MENU Switch statement
                switch (mainMenuChoice)
                {
                    case "N": //[N]ew Daily Entries

                        if (AcceptNewEntryDisclaimer())
                        {
                            // TODO: call EnterDailyValues & assign its return value
                            EnterDailyValues(dates, minutes);
                            Console.WriteLine($"\nEntries completed. {count} records in temporary memory.\n");
                        }
                        else
                        {
                            Console.WriteLine("Cancelling new data entry. Returning to MAIN MENU.");
                        }
                        break;
                    case "S": //[S]ave Entries to File
                        if (count == 0)
                        {
                            Console.WriteLine("Sorry, LOAD data or enter NEW data before SAVING.");
                        }
                        else if (AcceptSaveEntryDisclaimer())
                        {
                            string filename = PromptForFilename();
                            // TODO: call SaveToFile()
                            SaveToFile();
                        }
                        else
                        {
                            Console.WriteLine("Cancelling save operation. Returning to MAIN MENU.");
                        }

                        break;
                    case "E": //[E]dit Entries
                        if (count == 0)
                        {
                            Console.WriteLine("Sorry, LOAD data or enter NEW data before EDITING.");
                        }
                        else if (AcceptEditEntryDisclaimer())
                        {
                            //TODO: call EditEntries()
                            EditEntries();
                        }
                        else
                        {
                            Console.WriteLine("Cancelling EDIT operation. Returning to MAIN MENU.");
                        }
                        break;
                    case "L": //[L]oad  File
                        if (AcceptLoadEntryDisclaimer())
                        {
                            string filename = Prompt("Enter name of file to load: ");
                            // TODO: call LoadFromFile() and assign its return value
                            Console.WriteLine($"{count} records were loaded.\n");
                        }
                        else
                        {
                            Console.WriteLine("Cancelling LOAD operation. Returning to MAIN MENU.");
                        }
                        break;
                    case "V":
                        if (count == 0)
                        {
                            Console.WriteLine("Sorry, LOAD data or enter NEW data before VIEWING.");
                        }
                        else
                        {
                            // TODO: call DisplayEntries()
                            DisplayEntries();
                        }
                        break;
                    case "M": //[M]onthly Statistics
                        if (count == 0)
                        {
                            Console.WriteLine("Sorry, LOAD data or enter NEW data before ANALYSIS.");
                        }
                        else
                        {
                            RunAnalysisMenu(dates, minutes, count);
                        }
                        break;
                    case "D": //[D]isplay Main Menu
                        //TODO: call DisplayMainMenu()
                        DisplayMainMenu();
                        break;
                    case "Q": //[Q]uit Program
                        bool quit = Prompt("Are you sure you want to quit (y/n)? ").ToLower().Equals("y");
                        Console.WriteLine();
                        if (quit)
                        {
                            continueProgram = false;
                        }
                        break;
                    default: //invalid entry. Reprompt.
                        Console.WriteLine("Invalid reponse. Enter one of the letters to choose a menu option.");
                        break;
                }
            }

            DisplayProgramOutro();
        }

        /// <summary>
        /// Runs the analysis sub-menu to display summary metrics.
        /// </summary>
        /// <param name="dates">an array containing dates in YYYY-MM-DD format</param>
        /// <param name="numbers">an array containing numeric values</param>
        /// <param name="count">logical count of elements</param>
        static void RunAnalysisMenu(string[] dates, double[] numbers, int count)
        {
            bool runAnalysis = true;
            string year = dates[0].Substring(0, 4),
                month = dates[0].Substring(5, 3);

            while (runAnalysis)
            {
                string analysisMenuChoice;

                // TODO: call DisplayAnalysisMenu()
                DisplayAnalysisMenu();

                analysisMenuChoice = Prompt("Enter ANALYSIS sub-menu option: ").ToUpper();
                Console.WriteLine();

                switch (analysisMenuChoice)
                {
                    case "A": //[A]verage 
                        // TODO: uncomment the next 2 lines & call CalculateMean();
                        double mean = CalculateMean(numbers);
                        Console.WriteLine($"The mean value for {month} {year} is: {mean:N2}.\n");
                        break;
                    case "H": //[H]ighest 
                        // TODO: uncomment the next 2 lines & call CalculateLargest();
                        double largest = CalculateLargest(numbers);
                        Console.WriteLine($"The largest value for {month} {year} is: {largest:N2}.\n");
                        break;
                    case "L": //[L]owest 
                        //TODO: uncomment the next 2 lines & call CalculateSmallest();
                        double smallest = CalculateSmallest(numbers);
                        Console.WriteLine($"The smallest value for {month} {year} is: {smallest:N2}.\n");
                        break;
                    case "G": //[G]raph 
                        //TODO: call DisplayChart()
                        Prompt("Press <enter> to continue...");
                        break;
                    case "R": //[R]eturn to MAIN MENU
                        runAnalysis = false;
                        break;
                    default: //invalid entry. Reprompt.
                        Console.WriteLine("Invalid reponse. Enter one of the letters to choose a submenu option.");
                        break;
                }
            }
        }

        // ================================================================================================ //
        //                                                                                                  //
        //                                              METHODS                                             //
        //                                                                                                  //
        // ================================================================================================ //

        // ++++++++++++++++++++++++++++++++++++ Difficulty 1 ++++++++++++++++++++++++++++++++++++

        // TODO: create the DisplayMainMenu() method

        // Give choices to the user
        static void DisplayMainMenu()
        {
            Console.WriteLine("MAIN MENU");
            Console.WriteLine("--------------------------------");
            Console.WriteLine();
            Console.WriteLine("[N]ew Daily Entries");
            Console.WriteLine("[S]ave Entries to File");
            Console.WriteLine("[E]dit Entries]");
            Console.WriteLine("[L]oad File");
            Console.WriteLine("[V]iew Entered/Loaded Data");
            Console.WriteLine("[M]onthly Statistics");
            Console.WriteLine("[Q]uit Program");
            Console.WriteLine();
            Console.WriteLine("Enter MAIN MENU option ('D' to display menu): ");
        }

        // TODO: create the DisplayAnalysisMenu() method


        // More choices
        static void DisplayAnalysisMenu()
        {
            Console.WriteLine("ANALYSIS SUB-MENU");
            Console.WriteLine("-----------------------------------");
            Console.WriteLine("[A]verage");
            Console.WriteLine("[H]ighest");
            Console.WriteLine("[L]owest");
            Console.WriteLine("[G]raph");
            Console.WriteLine("[R]eturn to MAIN MENU");
            Console.WriteLine();
            Console.WriteLine("Enter ANALYSIS sub-menu option: ");
        }

        // TODO: create the Prompt method

        // Display message and save user response
        static string Prompt(string message)
        {
            Console.Write(message);
            return Console.ReadLine();
        }

        // TODO: create the PromptDouble() method

        // Display message and save user number response
        static double PromptDouble(string message)
        {
            Console.Write(message);
            return double.Parse(Console.ReadLine());
        }

        // optional TODO: create the PromptInt() method

        // Display message and save user int response
        static int PromptInt(string message)
        {
            Console.Write(message);
            return int.Parse(Console.ReadLine());
        }

        // TODO: create the CalculateLargest() method

        static double CalculateLargest(double[] numberArray )
        {
         // Loop through each value in the array and find the largest value
            double largestNumber = 0;
            for (int i = 0; i < numberArray.Length; i++)
            {
            // Check each value in the array and keep the largest one
                if (numberArray[i] > largestNumber)
                {
                    largestNumber = numberArray[i];
                }
            }
            return largestNumber;
        }

        // TODO: create the CalculateSmallest() method

        static double CalculateSmallest(double[] numberArray )
        {
        // Loop through each value in the array to find the smallest value
            double smallestNumber = 0;
            for (int i = 0; i < numberArray.Length; i++)
            {
            // Check each value in the array and keep the smallest one
                if (numberArray[i] < smallestNumber)
                {
                    smallestNumber = numberArray[i];
                }
            } 
            return smallestNumber;
        }

        // TODO: create the CalculateMean() method

        static double CalculateMean(double[] numberArray)
        {
        // Loop through each value in the array
            double meanNumber = 0;
            for (int i = 0; i < numberArray.Length;i++)
            {
            // Calculate average by adding each number in the array then divide by amount of numbers in array
                meanNumber = (meanNumber + numberArray[i] / numberArray.Length);
            }
            return meanNumber;
        }

        // ++++++++++++++++++++++++++++++++++++ Difficulty 2 ++++++++++++++++++++++++++++++++++++

        // TODO: create the EnterDailyValues method
        static void EnterDailyValues(string[] dates, double[] minutes)
        {
        // Prompt user for dates and display hints
            string month = Prompt("Enter the month (e.g. JAN): ").ToUpper();
            int year = PromptInt("Enter the year (yyyy): ");

            Console.WriteLine("\nHint: Enter -1 to cancel and exit.");

        // Request values for each day in the array
            for (int i = 0; i < dates.Length;i++)
            {
                double values = PromptDouble($"Enter the minutes for day {i}\nHint: Enter -1 to cancel and exit.");

                // Cancel if user inputs -1
                if (values != -1)
                {
                    minutes[i] = values;
                }
                // If they don't enter -1, end prompting for minutes
                else
                {
                    break;
                }
            }

            // TODO: create the LoadFromFile method
            static int LoadFromFile(string filename, string[] dates, double[] values)
            {
               
            }

            // TODO: create the SaveToFile method
            static void SaveToFile(string[]dates, double[] minutes)
            {
                // Create streamwriter to save responses to file
                StreamWriter writer = new StreamWriter(path);

                for (int i = 0; i < dates.Length; i++)
                    {
                        writer.WriteLine(dates[i]);
                        writer.WriteLine(minutes[i]);
                    }
            }

            // TODO: create the DisplayEntries method

            // ++++++++++++++++++++++++++++++++++++ Difficulty 3 ++++++++++++++++++++++++++++++++++++

            // TODO: create the EditEntries method

            // ++++++++++++++++++++++++++++++++++++ Difficulty 4 ++++++++++++++++++++++++++++++++++++

            // TODO: create the DisplayChart method

            // ********************************* Helper methods *********************************

            /// <summary>
            /// Displays the Program intro.
            /// </summary>
            static void DisplayProgramIntro()
        {
            Console.WriteLine("****************************************\n" +
                "*                                      *\n" +
                "*          Monthly  Game Time          *\n" +
                "*                                      *\n" +
                "****************************************\n");
        }

        /// <summary>
        /// Displays the Program outro.
        /// </summary>
        static void DisplayProgramOutro()
        {
            Console.Write("Program terminated. Press ENTER to exit program...");
            Console.ReadLine();
        }

        /// <summary>
        /// Displays a disclaimer for NEW entry option.
        /// </summary>
        /// <returns>Boolean, if user wishes to proceed (true) or not (false).</returns>
        static bool AcceptNewEntryDisclaimer()
        {
            bool response;
            Console.WriteLine("Disclaimer: proceeding will overwrite all unsaved data.\n" +
                "Hint: Select EDIT from the main menu instead, to change individual days.\n");
            response = Prompt("Do you wish to proceed anyway? (y/n) ").ToLower().Equals("y");
            Console.WriteLine();
            return response;
        }

        /// <summary>
        /// Displays a disclaimer for SAVE entry option.
        /// </summary>
        /// <returns>Boolean, if user wishes to proceed (true) or not (false).</returns>
        static bool AcceptSaveEntryDisclaimer()
        {
            bool response;
            Console.WriteLine("Disclaimer: saving to an EXISTING file will overwrite data currently on that file.\n" +
                "Hint: Files will be saved to this program's directory by default.\n" +
                "Hint: If the file does not yet exist, it will be created.\n");
            response = Prompt("Do you wish to proceed anyway? (y/n) ").ToLower().Equals("y");
            Console.WriteLine();
            return response;
        }

        /// <summary>
        /// Displays a disclaimer for EDIT entry option.
        /// </summary>
        /// <returns>Boolean, if user wishes to proceed (true) or not (false).</returns>
        static bool AcceptEditEntryDisclaimer()
        {
            bool response;
            Console.WriteLine("Disclaimer: editing will overwrite unsaved values.\n" +
                "Hint: Save to a file before editing.\n");
            response = Prompt("Do you wish to proceed anyway? (y/n ").ToLower().Equals("y");
            Console.WriteLine();
            return response;
        }

        /// <summary>
        /// Displays a disclaimer for LOAD entry option.
        /// </summary>
        /// <returns>Boolean, if user wishes to proceed (true) or not (false).</returns>
        static bool AcceptLoadEntryDisclaimer()
        {
            bool response;
            Console.WriteLine("Disclaimer: proceeding will overwrite all unsaved data.\n" +
                "Hint: If you entered New Daily entries, save them first!\n");
            response = Prompt("Do you wish to proceed anyway? (y/n) ").ToLower().Equals("y");
            Console.WriteLine();
            return response;
        }

        /// <summary>
        /// Displays prompt for a filename, and returns a valid filename. 
        /// Includes exception handling.
        /// </summary>
        /// <returns>User-entered string, representing valid filename (.txt or .csv)</returns>
        static string PromptForFilename()
        {
            string filename = "";
            bool isValidFilename = true;
            const string CSV_FILE_EXTENSION = ".csv";
            const string TXT_FILE_EXTENSION = ".txt";

            do
            {
                filename = Prompt("Enter name of .csv or .txt file to save to (e.g. JAN-2025-data.csv): ");
                if (filename == "")
                {
                    isValidFilename = false;
                    Console.WriteLine("Please try again. The filename cannot be blank or just spaces.");
                }
                else
                {
                    if (!filename.EndsWith(CSV_FILE_EXTENSION) && !filename.EndsWith(TXT_FILE_EXTENSION)) //if filename does not end with .txt or .csv.
                    {
                        filename = filename + CSV_FILE_EXTENSION; //append .csv to filename
                        Console.WriteLine("It looks like your filename does not end in .csv or .txt, so it will be treated as a .csv file.");
                        isValidFilename = true;
                    }
                    else
                    {
                        Console.WriteLine("It looks like your filename ends in .csv or .txt, which is good!");
                        isValidFilename = true;
                    }
                }
            } while (!isValidFilename);
            return filename;
        }

    }
}