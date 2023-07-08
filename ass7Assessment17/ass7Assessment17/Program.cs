using System;

public class CustomException : Exception
{
    public CustomException(string message) : base(message)
    {
    }
}

public class Game
{
    private static int numGamesPlayed = 0;

    public static void Main(string[] args)
    {
        try
        {
            while (true)
            {
                Step3();
                int userInput = GetUserInput();
                Step4(userInput);
                numGamesPlayed++;

                if (numGamesPlayed >= 5)
                {
                    Console.WriteLine("You have played this game for 5 times.");
                    break;
                }
            }
        }
        catch (CustomException ex)
        {
            Console.WriteLine("Error: " + ex.Message);
            Main(args); // Restart the game
        }
    }

    private static void Step3()
    {
        Console.WriteLine("Enter any number from 1-5:");
    }

    private static int GetUserInput()
    {
        string input = Console.ReadLine();
        int userInput;
        bool isValid = int.TryParse(input, out userInput);

        if (!isValid || userInput < 1 || userInput > 5)
        {
            throw new CustomException("Invalid input. Please enter a number from 1-5.");
        }

        return userInput;
    }

    private static void Step4(int userInput)
    {
        switch (userInput)
        {
            case 1:
                Console.WriteLine("Enter even number.");
                break;
            case 2:
                Console.WriteLine("Enter odd number.");
                break;
            case 3:
                Console.WriteLine("Enter a prime number.");
                break;
            case 4:
                Console.WriteLine("Enter a negative number.");
                break;
            case 5:
                Console.WriteLine("Enter zero.");
                break;
            default:
                throw new CustomException("Invalid input. Please enter a number from 1-5.");
        }

        int number = GetNumberInput();

        switch (userInput)
        {
            case 1:
                if (number % 2 != 0)
                {
                    throw new CustomException("Error: Not an even number.");
                }
                break;
            case 2:
                if (number % 2 == 0)
                {
                    throw new CustomException("Error: Not an odd number.");
                }
                break;
            case 3:
                if (!IsPrime(number))
                {
                    throw new CustomException("Error: Not a prime number.");
                }
                break;
            case 4:
                if (number >= 0)
                {
                    throw new CustomException("Error: Not a negative number.");
                }
                break;
            case 5:
                if (number != 0)
                {
                    throw new CustomException("Error: Not zero.");
                }
                break;
        }

        Console.WriteLine("Success!");
    }

    private static int GetNumberInput()
    {
        string input = Console.ReadLine();
        int number;
        bool isValid = int.TryParse(input, out number);

        if (!isValid)
        {
            throw new CustomException("Invalid number input.");
        }

        return number;
    }

    private static bool IsPrime(int number)
    {
        if (number < 2)
        {
            return false;
        }

        for (int i = 2; i * i <= number; i++)
        {
            if (number % i == 0)
            {
                return false;
            }
        }

        return true;
    }
}
