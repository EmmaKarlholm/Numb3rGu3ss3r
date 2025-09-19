using System.ComponentModel.Design;
using System.Runtime.ExceptionServices;

namespace Numb3rGu3ss3r
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Set initial variables.
            bool testingMode = false; // Set to FALSE before upload to GitHub.
            int attempts = 0;
            bool numberWasGuessed = false;

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

            // Display the correct number for testing reasons?
            if (testingMode == true) { Console.WriteLine("correctNumber is " + correctNumber); }


            // Prepare for user inputs.
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
                            // Number was guessed, victory state reached!
                            Console.WriteLine($"Congratulations! You guessed my number of {correctNumber} on your {attemptsString} try!\n");
                            numberWasGuessed = true;
                            break;
                        }
                        else
                        {
                            if (guess < correctNumber)
                            {
                                if (guess == correctNumber - 1)
                                { GuessWasClose(); }
                                else { GuessWasTooLow(); }
                            }

                            if (guess > correctNumber)
                            {
                                if (guess == correctNumber + 1)
                                { GuessWasClose(); }
                                else { GuessWasTooHigh(); }
                            }
                            
                            attempts++;
                        }

                    }
                }

                // Five attempts are over, or the correct guess has been made
                if (numberWasGuessed == true)
                {
                    break;
                }
                else
                {
                Console.Write("\n\nI'm sorry, but.");
                Thread.Sleep(400);
                Console.Write(".");
                Thread.Sleep(400);
                Console.Write(".");
                Thread.Sleep(1000);
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("\n\t\t\t\t\tGame Over!");
                Console.ResetColor();
                break;
                }
            }
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



        // Display a response to user if the input was way too high.
        public static void GuessWasTooHigh()
        {
            Random random = new Random();
            int outputText = random.Next(0, 5);
            switch (outputText)
            {
                case 0:
                    Console.WriteLine("Your guess went too high.\n");
                    break;

                case 1:
                    Console.WriteLine("I'm afraid your number is up in the clouds.\n");
                    break;

                case 2:
                    Console.WriteLine("Try something a bit lower.\n");
                    break;

                case 3:
                    Console.WriteLine("Haha, that was a bit too high, wasn't it?\n");
                    break;

                case 4:
                    Console.WriteLine("Your guess went above the number I was thinking of.\n");
                    break;

            }
        }



        // Display a response to user if the input was way too low.
        public static void GuessWasTooLow()
        {
            Random random = new Random();
            int outputText = random.Next(0, 5);
            switch (outputText)
            {
                case 0:
                    Console.WriteLine("You'll have to try something higher than that.\n");
                    break;

                case 1:
                    Console.WriteLine("Come on. Aim for the top!\n");
                    break;

                case 2:
                    Console.WriteLine("You're lowballing this.\n");
                    break;

                case 3:
                    Console.WriteLine("Try something a bit higher.\n");
                    break;

                case 4:
                    Console.WriteLine("Your guess went below the number I was thinking of.\n");
                    break;
            }
        }



        // Display a response to user if the input was off by one in either direction.
        public static void GuessWasClose()
        {
            Random random = new Random();
            int outputText = random.Next(0, 5);
            switch (outputText)
            {
                case 0:
                    Console.WriteLine("You're getting really, really close now.\n");
                    break;

                case 1:
                    Console.WriteLine("That number of yours, it's almost it.\n");
                    break;

                case 2:
                    Console.WriteLine("Aaaaalmost there!\n");
                    break;

                case 3:
                    Console.WriteLine("You're nearly at the number, so push it!\n"); // This is a Scarface reference. Sue me.
                    break;

                case 4:
                    Console.WriteLine("The number I'm thinking of could be your neighbour.\n");
                    break;
            }
        }
    }
}


