using System.ComponentModel.Design;
using System.Runtime.ExceptionServices;

namespace Numb3rGu3ss3r
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool testingMode = false; // Set to FALSE before upload to GitHub or compilation.
            bool quittingGame = false;
            while (quittingGame == false)
            {
                // Set initial gameplay variables which will reset when the game resets.
                int attempts = 0;
                bool acceptedMaximumNumber = false;
                bool numberWasGuessed = false;

                // Draw title screen, complete with pretty colours.
                Console.WriteLine("\n\t\tWelcome to");
                Console.BackgroundColor = ConsoleColor.Cyan;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("\t\t\tNumb3r Gu3ss3r");
                Console.ResetColor();
                Console.WriteLine("\t\t\t\tby Emma Karlholm");
                Console.WriteLine("\n\nWelcome! I will be thinking of a number in a range between one(1) and " +
                    "a number of your choosing.\n\nYou will have five(5) tries to guess the number I chose.\n");


                // Allow user to input number to set their own difficulty as gradual as they wish.
                int maximumNumber = 0;
                while (acceptedMaximumNumber == false)
                {
                    Console.Write("Please tell me the highest number I can go for. My recommendation is 20: ");
                    string? userInputMaxGuess = Console.ReadLine();
                    int.TryParse(userInputMaxGuess, out maximumNumber);
                    if (maximumNumber < 2 )
                    {
                        Console.WriteLine("Sorry, try a number over one(1).\n");
                    }
                    else if (maximumNumber >= 2)
                    {
                        acceptedMaximumNumber = true;
                    }
                }



                //Console.WriteLine("\n\nWelcome! I'm thinking of a number. Care to guess which one? You get five tries.");

                // Set the correctNumber to be a number between 1 and 20
                Random random = new Random();
                int correctNumber = (random.Next(0, maximumNumber) + 1); // The 20 here could be made into a variable in future updates.

                // Display the correct number for testing reasons?
                if (testingMode == true) { Console.WriteLine("correctNumber is " + correctNumber); }


                // Prepare for user inputs.
                while (numberWasGuessed == false)
                {
                    while (attempts < 5)
                    {
                        // Using this tuple likely makes the code more difficult to read rather than just handling
                        // everything with a while loop within the NumberInput method like I have done in previous
                        // assignments. But I'm doing this to experiment and to learn, not just to show off
                        // my code, so I want to handle a tuple today!

                        (int guess, bool wasConvertable) = NumberInput(attempts);
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
                                Console.Clear();
                                Console.WriteLine($"\n\tCongratulations! You guessed my number of {correctNumber} on your {attemptsString} try!\n\n");
                                Thread.Sleep(500);
                                PressAnyKeyToContinue();
                                numberWasGuessed = true;
                                break;
                            }
                            else
                            {
                                bool wasFairGuess = true;

                                // If the user guesses above the stated maximum guessing number, the guess
                                // was not fair and the user should not be penalised by losing a turn.
                                if (guess > maximumNumber)
                                {
                                    GuessWasOutOfRange(maximumNumber);
                                    wasFairGuess = false;
                                }

                                if (guess <= 0)
                                {
                                    GuessWasBelowRange();
                                    wasFairGuess = false;
                                }

                                if ((guess < correctNumber) && (wasFairGuess == true))
                                {
                                    if (guess == correctNumber - 1)
                                    { GuessWasClose(); }
                                    else { GuessWasTooLow(); }
                                }

                                if ((guess > correctNumber) && (wasFairGuess == true))
                                {
                                    if (guess == correctNumber + 1)
                                    { GuessWasClose(); }
                                    else { GuessWasTooHigh(); }
                                }

                                // Lower the amount of guesses the user has available since their guess
                                // was fair but wrong.
                                if (wasFairGuess == true)
                                { 
                                    attempts++;
                                }
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
                        Console.Clear();
                        Console.Write("\n\nI'm sorry, but.");
                        Thread.Sleep(400);
                        Console.Write(".");
                        Thread.Sleep(400);
                        Console.Write(".");
                        Thread.Sleep(1000);
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("\n\t\t\t\t\tGame Over!\n\n\n");
                        Console.ResetColor();
                        Thread.Sleep(1500);
                        PressAnyKeyToContinue();
                        break;
                    }
                }

                // End of gameplay state reached. Try again?
                while (true)
                {
                    Console.Clear();
                    Console.Write("\n\n\n\t\tWould you like to play again? (y/n) ");
                    string? quitGameQuestion = Console.ReadLine();
                    if (quitGameQuestion == "y" || quitGameQuestion == "yes")
                    {
                        Console.Clear();
                        break;
                    }
                    else if (quitGameQuestion == "n" || quitGameQuestion == "no")
                    {
                        quittingGame = true;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Sorry, I did not understand your input.");
                        Thread.Sleep(1500);
                    }

                }
            }

            // User has chosen not to continue playing.
            return; // Exiting in Main exits the application.
        }



        // Method for user number inputs.
        public static (int guess, bool wasConvertable) NumberInput(int attempts)
        {
            Console.Write($"\tTry a number (try #{attempts+1}): ");
            string? userInput = Console.ReadLine();
            Console.WriteLine();
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


        
        // Display a response to user if the input was higher than the maximum number.
        public static void GuessWasOutOfRange(int maximumNumber)
        {
            Random random = new Random();
            int outputText = random.Next(0, 5);
            switch (outputText)
            {
                case 0:
                    Console.WriteLine($"That number is above what you let me guess! You told me to guess a number between 1-{maximumNumber}\n"); 
                    break;

                case 1:
                    Console.WriteLine($"Weren't you guessing a number between 1 and {maximumNumber}?\n");
                    break;

                case 2:
                    Console.WriteLine($"You didn't let me go beyond {maximumNumber}, friend.\n");
                    break;

                case 3:
                    Console.WriteLine($"{maximumNumber} is as far as we go. Try again!\n");
                    break;

                case 4:
                    Console.WriteLine($"Your guess was higher than the possible maximum. If you forgot, you told me to pick a number between one and {maximumNumber}.\n");
                    break;
            }
            
        }



        // Display a response to user if the input was lower than 1.
        public static void GuessWasBelowRange()
        {
            Random random = new Random();
            int outputText = random.Next(0, 5);
            switch (outputText)
            {
                case 0:
                    Console.WriteLine("Try a number of one or above.\n");
                    break;

                case 1:
                    Console.WriteLine("That's below one.\n");
                    break;

                case 2:
                    Console.WriteLine("Listen, friend, do you know what between 1 and another number means?\n");
                    break;

                case 3:
                    Console.WriteLine("You can't go for zero or below.\n");
                    break;

                case 4:
                    Console.WriteLine("Please choose a legal number. That was below the number range we agreed upon.\n");
                    break;
            }

        }



        // Simple method to await user input for pausing purposes.
        public static void PressAnyKeyToContinue()
        {
            Console.Write("Press any key to continue...");
            Console.ReadKey();
        }
    }
}


