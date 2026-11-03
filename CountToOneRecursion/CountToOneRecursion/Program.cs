/*
 * Bing Shaw Lim
 * CST-250
 * 11/03/2026
 * Count To One Recursion
 * Activity 3
 * Resources: CST-250 Activity 3 Guide (Grand Canyon University, 2025)
 */

//----------------------------------------------------------
// Start of the Main Method
//----------------------------------------------------------

// Declare and initialize
int choice = -1, number = 0, result = 0, oddRule = 0;

Console.WriteLine("Count To One Recursion");

// Keep showing the menu until the user chooses 0 to exit
while (choice != 0)
{
    // Show the menu and get the user's choice
    Utility.ShowMenu();
    choice = Utility.ReadIntFromConsole("Choose an option (0 - 7): ", 0, 7);

    switch (choice)
    {
        case 1:
            // Original version: even numbers are divided by 2, odd numbers have 1 added
            number = Utility.ReadIntFromConsole($"Enter a positive number (1 - {Utility.MaxInput}): ", 1, Utility.MaxInput);
            result = Utility.CountToOne(number);
            Console.WriteLine($"The end number is {result}");
            break;
        case 2:
            // Challenge 1: count how many recursive steps it takes to reach 1
            number = Utility.ReadIntFromConsole($"Enter a positive number (1 - {Utility.MaxInput}): ", 1, Utility.MaxInput);
            result = Utility.CountToOneWithCounter(number, 0, true);
            Utility.PrintStepCount(number, result);
            break;
        case 3:
            // Challenge 2: change what happens to odd numbers
            number = Utility.ReadIntFromConsole($"Enter a positive number (1 - {Utility.MaxInput}): ", 1, Utility.MaxInput);
            oddRule = Utility.ReadIntFromConsole("For odd numbers, enter 1 to add 2 or 2 to subtract 1: ", 1, 2);
            result = Utility.CountToOneOddChange(number, oddRule == 1 ? 2 : -1, 0, true);
            Utility.PrintStepCount(number, result);
            break;
        case 4:
            // Challenge 3: divide by 4 or 3 when the number allows it
            number = Utility.ReadIntFromConsole($"Enter a positive number (1 - {Utility.MaxInput}): ", 1, Utility.MaxInput);
            result = Utility.CountToOneLargerDivisors(number, 0, true);
            Utility.PrintStepCount(number, result);
            break;
        case 5:
            // Challenge 4: odd multiples of 5 are multiplied by 3 (and then 1 is added)
            number = Utility.ReadIntFromConsole($"Enter a positive number (1 - {Utility.MaxInput}): ", 1, Utility.MaxInput);
            result = Utility.CountToOneWithMultiplier(number, 0, true);
            Utility.PrintStepCount(number, result);
            break;
        case 6:
            // Challenge 5: zero and negative numbers are allowed
            number = Utility.ReadIntFromConsole($"Enter any whole number ({-Utility.MaxInput} - {Utility.MaxInput}): ",
                -Utility.MaxInput, Utility.MaxInput);
            result = Utility.CountToOne(number);
            Console.WriteLine($"The end number is {result}");
            break;
        case 7:
            // Use the step counters to compare every rule for the same number
            number = Utility.ReadIntFromConsole($"Enter a positive number (1 - {Utility.MaxInput}): ", 1, Utility.MaxInput);
            Utility.CompareRules(number);
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
// Start of the Utility class
//----------------------------------------------------------
static class Utility
{
    // The largest number the menu accepts (keeps every rule inside the int range)
    internal const int MaxInput = 1000000;
    // The most steps a rule may take. A rule that never reaches the base case would
    // otherwise call itself until the program crashes with a stack overflow.
    internal const int MaxSteps = 100;
    // Challenge 4: the factor that odd multiples of 5 are multiplied by
    internal const int MultiplyFactor = 3;

    /// <summary>
    /// Show the menu of count to one versions
    /// </summary>
    internal static void ShowMenu()
    {
        Console.WriteLine();
        Console.WriteLine("1. Count to one (original: odd + 1, even / 2)");
        Console.WriteLine("2. Count the steps (Challenge 1)");
        Console.WriteLine("3. Change the odd rule: add 2 or subtract 1 (Challenge 2)");
        Console.WriteLine("4. Divide by 4, 3, or 2 (Challenge 3)");
        Console.WriteLine("5. Multiply odd multiples of 5 by 3 (Challenge 4)");
        Console.WriteLine("6. Count to one from zero or a negative number (Challenge 5)");
        Console.WriteLine("7. Compare the steps for every rule");
        Console.WriteLine("0. Exit");
    } // End of ShowMenu method

    /// <summary>
    /// Read a whole number in a range from the console
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
    /// Count to one using recursion. Updated for Challenge 5 so that zero and
    /// negative numbers also reach the base case.
    /// </summary>
    /// <param name="num">The current number</param>
    /// <returns>The end number, which is always 1</returns>
    internal static int CountToOne(int num)
    {
        // Print out the current number
        Console.WriteLine($"The current number is {num}");
        // Check if the number is 1 (base case: stop the recursion)
        if (num == 1)
        {
            return 1;
        }
        // Challenge 5: 0 / 2 is still 0, so zero would never reach the base case.
        // Add 1 instead so zero becomes 1.
        else if (num == 0)
        {
            Console.WriteLine("The number is zero. Add 1");
            return CountToOne(num + 1);
        }
        // Challenge 5: a negative number is made positive so it can count down to 1
        else if (num < 0)
        {
            Console.WriteLine("The number is negative. Make it positive");
            return CountToOne(-num);
        }
        // Check if the number is even
        else if ((num % 2) == 0)
        {
            Console.WriteLine("The number is even. Divide by 2");
            // Divide the number by 2 and call the function
            return CountToOne(num / 2);
        }
        else
        {
            Console.WriteLine("The number is odd. Add 1");
            // Add 1 and call the function
            return CountToOne(num + 1);
        }
    } // End of CountToOne method

    /// <summary>
    /// Challenge 1: count to one and count how many recursive steps it takes
    /// </summary>
    /// <param name="num">The current number</param>
    /// <param name="steps">The steps taken so far (start with 0)</param>
    /// <param name="showSteps">True to print each step</param>
    /// <returns>The number of steps it took to reach 1</returns>
    internal static int CountToOneWithCounter(int num, int steps, bool showSteps)
    {
        // Base case: the number is 1, so return the number of steps it took
        if (num == 1)
        {
            return steps;
        }
        // Even numbers are divided by 2
        if (num % 2 == 0)
        {
            PrintStep(showSteps, steps + 1, $"{num} is even. Divide by 2 = {num / 2}");
            return CountToOneWithCounter(num / 2, steps + 1, showSteps);
        }
        // Odd numbers have 1 added
        PrintStep(showSteps, steps + 1, $"{num} is odd. Add 1 = {num + 1}");
        return CountToOneWithCounter(num + 1, steps + 1, showSteps);
    } // End of CountToOneWithCounter method

    /// <summary>
    /// Challenge 2: count to one with a different change for odd numbers
    /// (add 2 or subtract 1 instead of adding 1)
    /// </summary>
    /// <param name="num">The current number</param>
    /// <param name="oddChange">The amount added to an odd number (2 or -1)</param>
    /// <param name="steps">The steps taken so far (start with 0)</param>
    /// <param name="showSteps">True to print each step</param>
    /// <returns>The number of steps, or -1 when the rule never reaches 1</returns>
    internal static int CountToOneOddChange(int num, int oddChange, int steps, bool showSteps)
    {
        // Base case: the number is 1
        if (num == 1)
        {
            return steps;
        }
        // Safety stop: adding 2 to an odd number gives another odd number, so that
        // rule never reaches 1. Stop instead of recursing until the stack overflows.
        if (steps >= MaxSteps)
        {
            PrintStopped(showSteps, $"Stopped after {MaxSteps} steps. This rule is not reaching 1.");
            return -1;
        }
        // Even numbers are still divided by 2
        if (num % 2 == 0)
        {
            PrintStep(showSteps, steps + 1, $"{num} is even. Divide by 2 = {num / 2}");
            return CountToOneOddChange(num / 2, oddChange, steps + 1, showSteps);
        }
        // Odd numbers change by oddChange
        string change = oddChange >= 0 ? $"Add {oddChange}" : $"Subtract {-oddChange}";
        PrintStep(showSteps, steps + 1, $"{num} is odd. {change} = {num + oddChange}");
        return CountToOneOddChange(num + oddChange, oddChange, steps + 1, showSteps);
    } // End of CountToOneOddChange method

    /// <summary>
    /// Challenge 3: count to one by dividing by 4 or 3 when the number is
    /// divisible by them, and by 2 otherwise
    /// </summary>
    /// <param name="num">The current number</param>
    /// <param name="steps">The steps taken so far (start with 0)</param>
    /// <param name="showSteps">True to print each step</param>
    /// <returns>The number of steps, or -1 when the rule never reaches 1</returns>
    internal static int CountToOneLargerDivisors(int num, int steps, bool showSteps)
    {
        // Base case: the number is 1
        if (num == 1)
        {
            return steps;
        }
        // Safety stop in case a rule never reaches 1
        if (steps >= MaxSteps)
        {
            PrintStopped(showSteps, $"Stopped after {MaxSteps} steps. This rule is not reaching 1.");
            return -1;
        }
        // Use the largest divisor (4, 3, or 2) that divides the number evenly
        if (num % 4 == 0)
        {
            PrintStep(showSteps, steps + 1, $"{num} is divisible by 4. Divide by 4 = {num / 4}");
            return CountToOneLargerDivisors(num / 4, steps + 1, showSteps);
        }
        if (num % 3 == 0)
        {
            PrintStep(showSteps, steps + 1, $"{num} is divisible by 3. Divide by 3 = {num / 3}");
            return CountToOneLargerDivisors(num / 3, steps + 1, showSteps);
        }
        if (num % 2 == 0)
        {
            PrintStep(showSteps, steps + 1, $"{num} is even. Divide by 2 = {num / 2}");
            return CountToOneLargerDivisors(num / 2, steps + 1, showSteps);
        }
        // Any other odd number has 1 added
        PrintStep(showSteps, steps + 1, $"{num} is odd. Add 1 = {num + 1}");
        return CountToOneLargerDivisors(num + 1, steps + 1, showSteps);
    } // End of CountToOneLargerDivisors method

    /// <summary>
    /// Challenge 4: count to one, but an odd multiple of 5 is multiplied by 3 and
    /// then 1 is added (like the 3n + 1 problem), so the number grows before it shrinks
    /// </summary>
    /// <param name="num">The current number</param>
    /// <param name="steps">The steps taken so far (start with 0)</param>
    /// <param name="showSteps">True to print each step</param>
    /// <returns>The number of steps, or -1 when the rule is stopped</returns>
    internal static int CountToOneWithMultiplier(int num, int steps, bool showSteps)
    {
        // Base case: the number is 1
        if (num == 1)
        {
            return steps;
        }
        // Safety stop in case the number keeps growing
        if (steps >= MaxSteps)
        {
            PrintStopped(showSteps, $"Stopped after {MaxSteps} steps. This rule is not reaching 1.");
            return -1;
        }
        // Even numbers are divided by 2
        if (num % 2 == 0)
        {
            PrintStep(showSteps, steps + 1, $"{num} is even. Divide by 2 = {num / 2}");
            return CountToOneWithMultiplier(num / 2, steps + 1, showSteps);
        }
        // Odd multiples of 5 are multiplied by the factor, then 1 is added
        if (num % 5 == 0)
        {
            // Stop before the multiplication would go past the largest int
            if (num > (int.MaxValue - 1) / MultiplyFactor)
            {
                PrintStopped(showSteps, $"Stopped. {num} x {MultiplyFactor} + 1 is too large for an int.");
                return -1;
            }
            PrintStep(showSteps, steps + 1,
                $"{num} is an odd multiple of 5. Multiply by {MultiplyFactor} and add 1 = {num * MultiplyFactor + 1}");
            return CountToOneWithMultiplier(num * MultiplyFactor + 1, steps + 1, showSteps);
        }
        // Any other odd number has 1 added
        PrintStep(showSteps, steps + 1, $"{num} is odd. Add 1 = {num + 1}");
        return CountToOneWithMultiplier(num + 1, steps + 1, showSteps);
    } // End of CountToOneWithMultiplier method

    /// <summary>
    /// Use the step counters to compare every rule for the same starting number
    /// </summary>
    /// <param name="number">The starting number</param>
    internal static void CompareRules(int number)
    {
        Console.WriteLine($"Steps to reduce {number} to 1 with each rule:");
        PrintComparison("Original (odd + 1, even / 2)", CountToOneWithCounter(number, 0, false));
        PrintComparison("Odd numbers subtract 1", CountToOneOddChange(number, -1, 0, false));
        PrintComparison("Odd numbers add 2", CountToOneOddChange(number, 2, 0, false));
        PrintComparison("Divide by 4, 3, or 2", CountToOneLargerDivisors(number, 0, false));
        PrintComparison("Odd multiples of 5: x 3 + 1", CountToOneWithMultiplier(number, 0, false));
    } // End of CompareRules method

    /// <summary>
    /// Print how many steps a rule took, or that it never reached 1
    /// </summary>
    /// <param name="number">The starting number</param>
    /// <param name="steps">The step count returned by the rule (-1 = stopped)</param>
    internal static void PrintStepCount(int number, int steps)
    {
        if (steps >= 0)
        {
            Console.WriteLine($"It took {steps} steps to reduce {number} to 1.");
        }
        else
        {
            Console.WriteLine($"{number} never reached 1 with this rule.");
        }
    } // End of PrintStepCount method

    /// <summary>
    /// Print one step of a rule when steps are being shown
    /// </summary>
    /// <param name="showSteps">True to print the step</param>
    /// <param name="stepNumber">The step number</param>
    /// <param name="message">What happened in this step</param>
    private static void PrintStep(bool showSteps, int stepNumber, string message)
    {
        if (showSteps)
        {
            Console.WriteLine($"Step {stepNumber,3}: {message}");
        }
    } // End of PrintStep method

    /// <summary>
    /// Print why a rule was stopped by a safety check, when steps are being shown
    /// </summary>
    /// <param name="showSteps">True to print the message</param>
    /// <param name="message">Why the rule was stopped</param>
    private static void PrintStopped(bool showSteps, string message)
    {
        if (showSteps)
        {
            Console.WriteLine(message);
        }
    } // End of PrintStopped method

    /// <summary>
    /// Print one row of the rule comparison table
    /// </summary>
    /// <param name="rule">The name of the rule</param>
    /// <param name="steps">The step count (-1 = never reached 1)</param>
    private static void PrintComparison(string rule, int steps)
    {
        string outcome = steps >= 0 ? $"{steps} steps" : $"never reaches 1 (stopped at {MaxSteps})";
        Console.WriteLine($"  {rule,-30} {outcome}");
    } // End of PrintComparison method
}
//----------------------------------------------------------
// End of the Utility class
//----------------------------------------------------------
