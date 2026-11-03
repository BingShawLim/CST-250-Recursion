/*
 * Bing Shaw Lim
 * CST-250
 * 11/03/2026
 * Greatest Common Divisor Recursion
 * Activity 3
 * Resources: CST-250 Activity 3 Guide (Grand Canyon University, 2025)
 */

//----------------------------------------------------------
// Start of the Main Method
//----------------------------------------------------------

// Show the title of the program
Console.WriteLine("Greatest Common Divisor Recursion");

// Declare and initialize
int number1 = 440, number2 = 80, result = 0;

// Call the GCD method
result = Utility.GreatestCommonDivisor(number1, number2);

// Print the result to the user
Console.WriteLine($"The GCD of {number1} and {number2} is {result}");

//----------------------------------------------------------
// End of the Main Method
//----------------------------------------------------------

//----------------------------------------------------------
// Start of the Utility Class
//----------------------------------------------------------
public class Utility
{
    /// <summary>
    /// Calculate the GCD using recursion (Euclidean algorithm)
    /// </summary>
    /// <param name="num1">The first number</param>
    /// <param name="num2">The second number</param>
    /// <returns>The greatest common divisor of num1 and num2</returns>
    internal static int GreatestCommonDivisor(int num1, int num2)
    {
        // Declare and initialize
        int remainder = 0;

        // Base case: num2 is 0
        if (num1 == 0 || num2 == 0)
        {
            // Return the greatest common divisor
            return num1;
        }
        else
        {
            // Get the remainder of dividing num1 and num2
            remainder = num1 % num2;
            // Print an update to the user
            Console.WriteLine($"Not yet. The remainder of {num1} and {num2} is {remainder}");
            // Call the recursive function of the second number and the remainder
            return GreatestCommonDivisor(num2, remainder);
        }
    } // End of GreatestCommonDivisor method
}
//----------------------------------------------------------
// End of the Utility Class
//----------------------------------------------------------
