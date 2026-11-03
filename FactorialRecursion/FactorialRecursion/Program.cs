/*
 * Bing Shaw Lim
 * CST-250
 * 11/03/2026
 * Factorial Recursion
 * Activity 3
 * Resources: CST-250 Activity 3 Guide (Grand Canyon University, 2025)
 */

using System.Numerics;
using FactorialRecursion.Services.BusinessLogicLayer;

//----------------------------------------------------------
// Start of the Main Method
//----------------------------------------------------------

// Declare and initialize
FactorialLogic factorialLogic = new FactorialLogic();
int input = 0;
BigInteger iterativeAns = 0, recursiveAns = 0;

// Prompt the user
Console.Write($"Enter a whole number (0 - {Utility.MaxFactorial}): ");
// Get the users input
input = Utility.ReadIntFromConsole();

// Solve the factorial using iteration
Console.WriteLine("Solving the factorial using iteration...");
iterativeAns = factorialLogic.SolveIterativeFactorial(input);
Console.WriteLine($"Answer: {iterativeAns}");

// Solve the factorial using recursion
Console.WriteLine("Solving the factorial using recursion...");
recursiveAns = factorialLogic.SolveRecursiveFactorial(input);
Console.WriteLine($"Answer: {recursiveAns}");

// Both methods should always give the same answer
Console.WriteLine($"Iteration and recursion give the same answer: {iterativeAns == recursiveAns}");

//----------------------------------------------------------
// End of the Main Method
//----------------------------------------------------------

//----------------------------------------------------------
// Start of Utility Class
//----------------------------------------------------------
static class Utility
{
    // The largest number accepted. Each recursive call adds a frame to the call stack,
    // so the input is limited to keep the recursion well inside the default stack size.
    internal const int MaxFactorial = 1000;

    /// <summary>
    /// Read integer input from the console
    /// </summary>
    /// <returns>A whole number from 0 to MaxFactorial entered by the user</returns>
    internal static int ReadIntFromConsole()
    {
        // Declare and initialize
        string input = "";
        int number = -1;

        // Get the current input
        input = Console.ReadLine() ?? "";
        // Check if the input is valid: a whole number from 0 to MaxFactorial
        while (!int.TryParse(input, out number) || number < 0 || number > MaxFactorial)
        {
            // Show the user an error message and prompt them again
            Console.Write($"Invalid input. Please enter a whole number from 0 to {MaxFactorial}: ");
            // Get the new input
            input = Console.ReadLine() ?? "";
        }
        // Return the resulting integer from the user
        return number;
    } // End of ReadIntFromConsole method
}
//----------------------------------------------------------
// End of Utility Class
//----------------------------------------------------------
