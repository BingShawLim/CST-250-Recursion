/*
 * Bing Shaw Lim
 * CST-250
 * 11/03/2026
 * Factorial Recursion
 * Activity 3
 * Resources: CST-250 Activity 3 Guide (Grand Canyon University, 2025)
 */

using System.Numerics;

namespace FactorialRecursion.Services.BusinessLogicLayer
{
    /// <summary>
    /// Business logic that solves the factorial problem two ways: iteration and recursion
    /// </summary>
    internal class FactorialLogic
    {
        /// <summary>
        /// Solve the factorial problem using iteration
        /// </summary>
        /// <param name="factorial">The number to find the factorial of (0 or more)</param>
        /// <returns>factorial! as a BigInteger</returns>
        internal BigInteger SolveIterativeFactorial(int factorial)
        {
            // A factorial is only defined for 0 and positive numbers
            if (factorial < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(factorial), "A factorial needs a number that is 0 or more.");
            }

            // Declare and initialize
            BigInteger result = 1;

            // For loop to solve the factorial
            for (int i = factorial; i >= 1; i--)
            {
                // Multiply the result by i
                result *= i;
            }
            // Return the result
            return result;
        } // End of SolveIterativeFactorial method

        /// <summary>
        /// Solve the factorial problem using recursion
        /// </summary>
        /// <param name="factorial">The number to find the factorial of (0 or more)</param>
        /// <returns>factorial! as a BigInteger</returns>
        internal BigInteger SolveRecursiveFactorial(int factorial)
        {
            // A factorial is only defined for 0 and positive numbers. Without this check a
            // negative number would never reach the base case.
            if (factorial < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(factorial), "A factorial needs a number that is 0 or more.");
            }

            // Base case: factorial is 0 or 1
            if (factorial == 0 || factorial == 1)
            {
                return 1;
            }
            // Perform the recursion: n! = n * (n - 1)!
            return factorial * SolveRecursiveFactorial(factorial - 1);
        } // End of SolveRecursiveFactorial method
    }
}
