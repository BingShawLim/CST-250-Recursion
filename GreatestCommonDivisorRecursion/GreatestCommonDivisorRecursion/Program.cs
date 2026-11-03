/*
 * Bing Shaw Lim
 * CST-250
 * 11/03/2026
 * Greatest Common Divisor Recursion
 * Activity 3
 * Resources: CST-250 Activity 3 Guide (Grand Canyon University, 2025)
 */

using System.Diagnostics;

//----------------------------------------------------------
// Start of the Main Method
//----------------------------------------------------------

// Show the title of the program
Console.WriteLine("Greatest Common Divisor Recursion");

// Declare and initialize
int number1 = 440, number2 = 80, result = 0, choice = -1;
List<int> numbers = new List<int>();

// Keep showing the menu until the user chooses 0 to exit
while (choice != 0)
{
    // Show the menu and get the user's choice
    Utility.ShowMenu();
    choice = Utility.ReadIntFromConsole("Choose an option (0 - 6): ", 0, 6);

    switch (choice)
    {
        case 1:
            // The original example from the activity: 440 and 80
            number1 = 440;
            number2 = 80;
            // Call the GCD method
            result = Utility.GreatestCommonDivisor(number1, number2);
            // Print the result to the user
            Console.WriteLine($"The GCD of {number1} and {number2} is {result}");
            break;
        case 2:
            // Challenge 1: the user enters the two numbers
            number1 = Utility.ReadIntFromConsole("Enter the first number: ", int.MinValue + 1, int.MaxValue);
            number2 = Utility.ReadIntFromConsole("Enter the second number: ", int.MinValue + 1, int.MaxValue);
            result = Utility.GreatestCommonDivisor(number1, number2);
            Console.WriteLine($"The GCD of {number1} and {number2} is {result}");
            break;
        case 3:
            // Challenge 2: compare the recursive answer with the iterative answer
            number1 = Utility.ReadIntFromConsole($"Enter the first number (1 - {Utility.IterativeLimit}): ", 1, Utility.IterativeLimit);
            number2 = Utility.ReadIntFromConsole($"Enter the second number (1 - {Utility.IterativeLimit}): ", 1, Utility.IterativeLimit);
            Utility.CompareAnswers(number1, number2);
            break;
        case 4:
            // Challenge 3: the GCD of three or more numbers
            numbers = Utility.ReadNumberList("Enter three or more whole numbers separated by spaces: ", 3);
            result = Utility.GreatestCommonDivisorOfList(numbers);
            Console.WriteLine($"The GCD of {string.Join(", ", numbers)} is {result}");
            break;
        case 5:
            // Challenge 4: time the recursive and iterative methods
            number1 = Utility.ReadIntFromConsole($"Enter the first number (1 - {Utility.IterativeLimit}): ", 1, Utility.IterativeLimit);
            number2 = Utility.ReadIntFromConsole($"Enter the second number (1 - {Utility.IterativeLimit}): ", 1, Utility.IterativeLimit);
            Utility.CompareTimes(number1, number2);
            break;
        case 6:
            // Challenge 5: test the limits and the edge cases
            Utility.TestLimits();
            break;
        default:
            Console.WriteLine("Goodbye!");
            break;
    }
}

//----------------------------------------------------------
// End of the Main Method
//----------------------------------------------------------

//----------------------------------------------------------
// Start of the Utility Class
//----------------------------------------------------------
public class Utility
{
    // The largest number the iterative method may use. It loops once for every number
    // up to each input, so larger numbers would take too long.
    internal const int IterativeLimit = 10000000;

    /// <summary>
    /// Show the menu of GCD options
    /// </summary>
    internal static void ShowMenu()
    {
        Console.WriteLine();
        Console.WriteLine("1. GCD of 440 and 80 (original)");
        Console.WriteLine("2. GCD of two numbers you enter (Challenge 1)");
        Console.WriteLine("3. Recursive and iterative answers (Challenge 2)");
        Console.WriteLine("4. GCD of three or more numbers (Challenge 3)");
        Console.WriteLine("5. Time the recursive and iterative methods (Challenge 4)");
        Console.WriteLine("6. Test limits and edge cases (Challenge 5)");
        Console.WriteLine("0. Exit");
    } // End of ShowMenu method

    /// <summary>
    /// Challenge 1: read a whole number in a range from the console
    /// </summary>
    /// <param name="prompt">The message that asks for the number</param>
    /// <param name="min">The smallest number allowed</param>
    /// <param name="max">The largest number allowed</param>
    /// <returns>A valid whole number from min to max</returns>
    internal static int ReadIntFromConsole(string prompt, int min, int max)
    {
        // Declare and initialize
        int number = 0;

        // Prompt the user
        Console.Write(prompt);
        // Keep asking until the input is a whole number inside the range
        while (!int.TryParse(Console.ReadLine(), out number) || number < min || number > max)
        {
            Console.Write($"Invalid number. Please enter a whole number from {min} to {max}: ");
        }
        // Return the valid number
        return number;
    } // End of ReadIntFromConsole method

    /// <summary>
    /// Challenge 3: read a list of whole numbers typed on one line
    /// </summary>
    /// <param name="prompt">The message that asks for the numbers</param>
    /// <param name="minCount">The fewest numbers allowed</param>
    /// <returns>The list of numbers the user entered</returns>
    internal static List<int> ReadNumberList(string prompt, int minCount)
    {
        // Declare and initialize
        List<int> numbers = new List<int>();
        bool isValid = false;

        // Keep asking until every value is a whole number and there are enough of them
        while (!isValid)
        {
            Console.Write(prompt);
            string[] parts = (Console.ReadLine() ?? "").Split(new[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
            numbers.Clear();
            isValid = parts.Length >= minCount;
            foreach (string part in parts)
            {
                if (int.TryParse(part, out int number) && number != int.MinValue)
                {
                    numbers.Add(number);
                }
                else
                {
                    isValid = false;
                }
            }
            if (!isValid)
            {
                Console.WriteLine($"Please enter at least {minCount} whole numbers, separated by spaces.");
            }
        }
        return numbers;
    } // End of ReadNumberList method

    /// <summary>
    /// Calculate the GCD using recursion (Euclidean algorithm). Updated for the
    /// challenges: the steps can be hidden for timing, and zero and negative numbers work.
    /// </summary>
    /// <param name="num1">The first number</param>
    /// <param name="num2">The second number</param>
    /// <param name="showSteps">True to print each remainder (false when timing)</param>
    /// <returns>The greatest common divisor of num1 and num2 (never negative)</returns>
    internal static int GreatestCommonDivisor(int num1, int num2, bool showSteps = true)
    {
        // Declare and initialize
        int remainder = 0;

        // Base case: num2 is 0, so num1 is the greatest common divisor.
        // Challenge 5: the original also stopped when num1 was 0 and returned 0, but
        // GCD(0, 25) is 25, so only num2 is checked now. Math.Abs keeps the answer
        // positive when negative numbers are entered.
        if (num2 == 0)
        {
            // Return the greatest common divisor
            return Math.Abs(num1);
        }
        else
        {
            // Get the remainder of dividing num1 and num2
            remainder = num1 % num2;
            // Print an update to the user
            if (showSteps)
            {
                Console.WriteLine($"Not yet. The remainder of {num1} and {num2} is {remainder}");
            }
            // Call the recursive function of the second number and the remainder
            return GreatestCommonDivisor(num2, remainder, showSteps);
        }
    } // End of GreatestCommonDivisor method

    /// <summary>
    /// Count how many calls the recursive GCD method makes for two numbers
    /// (this is also a recursive method)
    /// </summary>
    /// <param name="num1">The first number</param>
    /// <param name="num2">The second number</param>
    /// <returns>The number of calls, including the base case call</returns>
    internal static int CountRecursiveCalls(int num1, int num2)
    {
        // Base case: the GCD method stops when num2 is 0
        if (num2 == 0)
        {
            return 1;
        }
        // This call plus every call that comes after it
        return 1 + CountRecursiveCalls(num2, num1 % num2);
    } // End of CountRecursiveCalls method

    /// <summary>
    /// Challenge 2: calculate the GCD using iteration. Every divisor of each number is
    /// saved in a list, then the largest number found in both lists is the answer.
    /// </summary>
    /// <param name="num1">The first number</param>
    /// <param name="num2">The second number</param>
    /// <returns>The greatest common divisor of num1 and num2</returns>
    internal static int GreatestCommonDivisorIterative(int num1, int num2)
    {
        // Declare and initialize
        List<int> divisors1 = new List<int>();
        List<int> divisors2 = new List<int>();

        // Work with the size of each number so negative numbers work too
        num1 = Math.Abs(num1);
        num2 = Math.Abs(num2);

        // Every number divides 0, so the GCD with 0 is the other number
        if (num1 == 0)
        {
            return num2;
        }
        if (num2 == 0)
        {
            return num1;
        }

        // Loop from 1 to the first number and save each divisor in a list
        divisors1 = GetDivisors(num1);
        // Repeat the process for the second number
        divisors2 = GetDivisors(num2);

        // The lists go from smallest to largest, so search the first list from the end
        // for the largest number that is also in the second list
        for (int i = divisors1.Count - 1; i >= 0; i--)
        {
            if (divisors2.Contains(divisors1[i]))
            {
                return divisors1[i];
            }
        }
        // 1 divides every number, so the loop always returns before this line
        return 1;
    } // End of GreatestCommonDivisorIterative method

    /// <summary>
    /// Challenge 2: find every divisor of a positive number using a loop
    /// </summary>
    /// <param name="number">A positive number</param>
    /// <returns>The divisors from smallest to largest</returns>
    internal static List<int> GetDivisors(int number)
    {
        // Declare and initialize
        List<int> divisors = new List<int>();

        // Loop from 1 to the number and keep each value that divides it evenly
        for (int i = 1; i <= number; i++)
        {
            if (number % i == 0)
            {
                divisors.Add(i);
            }
        }
        return divisors;
    } // End of GetDivisors method

    /// <summary>
    /// Challenge 2: show the recursive and iterative answers side by side
    /// </summary>
    /// <param name="num1">The first number</param>
    /// <param name="num2">The second number</param>
    internal static void CompareAnswers(int num1, int num2)
    {
        // Declare and initialize
        List<int> divisors1 = GetDivisors(num1);
        List<int> divisors2 = GetDivisors(num2);
        List<int> common = new List<int>();
        int recursiveAnswer = 0, iterativeAnswer = 0;

        // Find the divisors that are in both lists
        foreach (int divisor in divisors1)
        {
            if (divisors2.Contains(divisor))
            {
                common.Add(divisor);
            }
        }

        // Show the lists the iterative method builds (only when they are short)
        if (divisors1.Count <= 30 && divisors2.Count <= 30)
        {
            Console.WriteLine($"Divisors of {num1}: {string.Join(", ", divisors1)}");
            Console.WriteLine($"Divisors of {num2}: {string.Join(", ", divisors2)}");
            Console.WriteLine($"Common divisors: {string.Join(", ", common)}");
        }
        else
        {
            Console.WriteLine($"{num1} has {divisors1.Count} divisors, {num2} has {divisors2.Count}, and they share {common.Count}.");
        }

        // Solve the problem both ways
        Console.WriteLine("Recursive steps:");
        recursiveAnswer = GreatestCommonDivisor(num1, num2);
        iterativeAnswer = GreatestCommonDivisorIterative(num1, num2);
        Console.WriteLine($"Recursive answer: {recursiveAnswer}");
        Console.WriteLine($"Iterative answer: {iterativeAnswer} (the largest common divisor)");
        Console.WriteLine($"The answers match: {recursiveAnswer == iterativeAnswer}");
    } // End of CompareAnswers method

    /// <summary>
    /// Challenge 3: the GCD of three or more numbers. The GCD of the first two numbers
    /// is found, then that answer is used with the next number, and so on.
    /// </summary>
    /// <param name="numbers">The numbers (at least two)</param>
    /// <returns>The greatest common divisor of every number in the list</returns>
    internal static int GreatestCommonDivisorOfList(List<int> numbers)
    {
        // Declare and initialize: start with the first number
        int result = Math.Abs(numbers[0]);

        // Combine the answer so far with each next number
        for (int i = 1; i < numbers.Count; i++)
        {
            int previous = result;
            result = GreatestCommonDivisor(result, numbers[i], false);
            Console.WriteLine($"GCD({previous}, {numbers[i]}) = {result}");
        }
        return result;
    } // End of GreatestCommonDivisorOfList method

    /// <summary>
    /// Challenge 4: time the recursive and iterative methods with a Stopwatch
    /// </summary>
    /// <param name="num1">The first number</param>
    /// <param name="num2">The second number</param>
    internal static void CompareTimes(int num1, int num2)
    {
        // Declare and initialize
        const int RecursiveRuns = 100000;
        const int IterativeRuns = 20;
        Stopwatch stopwatch = new Stopwatch();
        int answer = 0;
        double recursiveMicroseconds = 0, iterativeMicroseconds = 0;

        // Run each method once first so the timing does not include start-up time
        GreatestCommonDivisor(num1, num2, false);
        GreatestCommonDivisorIterative(num1, num2);

        // Time the recursive method (the steps are hidden so printing is not timed)
        stopwatch.Start();
        for (int i = 0; i < RecursiveRuns; i++)
        {
            answer = GreatestCommonDivisor(num1, num2, false);
        }
        stopwatch.Stop();
        recursiveMicroseconds = stopwatch.Elapsed.TotalMilliseconds * 1000 / RecursiveRuns;

        // Time the iterative method
        stopwatch.Restart();
        for (int i = 0; i < IterativeRuns; i++)
        {
            answer = GreatestCommonDivisorIterative(num1, num2);
        }
        stopwatch.Stop();
        iterativeMicroseconds = stopwatch.Elapsed.TotalMilliseconds * 1000 / IterativeRuns;

        // Print the results
        Console.WriteLine($"The GCD of {num1} and {num2} is {answer}");
        Console.WriteLine($"Recursive (Euclid):        {recursiveMicroseconds,14:N3} microseconds per call ({RecursiveRuns:N0} runs)");
        Console.WriteLine($"Iterative (divisor lists): {iterativeMicroseconds,14:N3} microseconds per call ({IterativeRuns:N0} runs)");
        if (recursiveMicroseconds < iterativeMicroseconds)
        {
            Console.WriteLine($"The recursive method was about {iterativeMicroseconds / recursiveMicroseconds:N0} times faster.");
        }
        else
        {
            Console.WriteLine($"The iterative method was about {recursiveMicroseconds / iterativeMicroseconds:N0} times faster.");
        }
        Console.WriteLine($"The recursive method made {CountRecursiveCalls(num1, num2)} calls. " +
            $"The iterative method looped {(long)num1 + num2:N0} times.");
    } // End of CompareTimes method

    /// <summary>
    /// Challenge 5: test the limits of the GCD methods with zero, negative,
    /// very small, and very large numbers
    /// </summary>
    internal static void TestLimits()
    {
        Console.WriteLine($"{"First",12} {"Second",12} {"Recursive",11} {"Calls",6} {"Iterative",10}  Note");
        TestOneCase(440, 80, "the example from the activity");
        TestOneCase(0, 25, "the original method returned 0 here");
        TestOneCase(25, 0, "every number divides 0");
        TestOneCase(0, 0, "no largest divisor, so 0 is returned");
        TestOneCase(-48, 18, "negative first number");
        TestOneCase(48, -18, "the original method returned -6 here");
        TestOneCase(1, 1, "the smallest positive numbers");
        TestOneCase(17, 13, "two primes only share 1");
        TestOneCase(int.MaxValue, int.MaxValue - 1, "the largest int values");
        TestOneCase(1836311903, 1134903170, "Fibonacci numbers need the most calls");
        TestOneCase(int.MinValue, 0, "2,147,483,648 does not fit in an int");
    } // End of TestLimits method

    /// <summary>
    /// Challenge 5: run one limit test and print a row of the results table
    /// </summary>
    /// <param name="num1">The first number</param>
    /// <param name="num2">The second number</param>
    /// <param name="note">What the test shows</param>
    private static void TestOneCase(int num1, int num2, string note)
    {
        // Declare and initialize
        string recursive = "", iterative = "";

        // The recursive method: an answer that does not fit in an int throws an exception
        try
        {
            recursive = GreatestCommonDivisor(num1, num2, false).ToString();
        }
        catch (OverflowException)
        {
            recursive = "overflow";
        }

        // The iterative method is only run when it will finish quickly
        if (Math.Abs((long)num1) <= IterativeLimit && Math.Abs((long)num2) <= IterativeLimit)
        {
            iterative = GreatestCommonDivisorIterative(num1, num2).ToString();
        }
        else
        {
            iterative = "too slow";
        }
        Console.WriteLine($"{num1,12} {num2,12} {recursive,11} {CountRecursiveCalls(num1, num2),6} {iterative,10}  {note}");
    } // End of TestOneCase method
}
//----------------------------------------------------------
// End of the Utility Class
//----------------------------------------------------------
