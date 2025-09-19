using System.Runtime.ExceptionServices;

namespace Numb3rGu3ss3r
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Draw title screen, complete with pretty colours.
            Console.WriteLine("\t\tWelcome to");
            Console.BackgroundColor = ConsoleColor.Cyan;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("\t\t\tNumb3r Gu3ss3r");
            Console.ResetColor();
            Console.WriteLine("\t\t\t\tby Emma Karlholm");
            Console.WriteLine("\n\nWelcome! I'm thinking of a number. Care to guess which one? You get five tries.");

            // Set the correctNumber to be a number between 1 and 20
            Random random = new Random();
            int correctNumber = (random.Next(0, 20) + 1); // The 20 here could be made into a variable in future updates.


            // Prepare for user inputs.
            int attempts = 0;
            bool numberWasGuessed = false;
            while (numberWasGuessed == false)
            {
                while (attempts < 5)
                {
                    // Using this tuple makes the code unnecessarily difficult to read rather than just handling
                    // everything with a while loop within the NumberInput method like I have done in previous
                    // assignments. But I'm doing this to experiment and to learn just as much as  code to show off
                    // my code, so I want to handle a tuple today!
                    //
                    // First time I use "var" willingly as a consequnce of this tuple, because I need to read an
                    // integer and a boolean value returned from method. If the logic was handled within the
                    // NumberInput method instead, I wouldn't need to receive two data types (or two variables to
                    // begin with.
                    var (guess, wasConvertable) = NumberInput(attempts);
                    if (wasConvertable == true)
                    {
                        if (guess == correctNumber)
                        {
                            string attemptsString = "";
                            switch (attempts)
                            {
                                case 0: // attempt 1
                                    attemptsString = "first";
                                    break;
                                case 1: // attempt 2
                                    attemptsString = "second";
                                    break;
                                case 2: // attempt 3
                                    attemptsString = "third";
                                    break;
                                case 3: // attempt 4
                                    attemptsString = "fourth";
                                    break;
                                case 4: // attempt 5
                                    attemptsString = "fifth";
                                    break;
                            }
                            Console.WriteLine($"Congratulations! You guessed my number of {correctNumber} on your {attemptsString} try!\n");
                            return; // Exits program since the Main method is the top method.
                        }
                        else
                        {
                            if (guess < correctNumber)
                            {
                                Console.WriteLine("You have guessed a value below the correct number.\n");
                            }

                            if (guess > correctNumber)
                            {
                                Console.WriteLine("You have guessed a value above the correct number.\n");
                            }
                            
                            attempts++;
                        }

                    }
                }
                
            }
            //
            // User has found the correctNumber.
            //

            Console.WriteLine("End of code reached.");
        }



        // Method for user number inputs.
        public static (int guess, bool wasConvertable) NumberInput(int attempts)
        {
            Console.Write($"\tTry a number (try #{attempts+1}): ");
            string? userInput = Console.ReadLine();

            // Initialise variables.
            int guess = 0;
            bool wasConvertable = true;
            try
            {
                // Could have used TryParse, but I wanted to use try->catch since I just learnt to use it
                // and TryParse failing does not cause an exception.
                guess = int.Parse(userInput);
            }
            catch
            {
                Console.WriteLine("That is not a valid number. Please try again.\n");
                wasConvertable = false;
            }
            return (guess, wasConvertable);
        }

    }
}


