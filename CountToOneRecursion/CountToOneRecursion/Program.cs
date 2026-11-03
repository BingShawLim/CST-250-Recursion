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
int choice = 0, result = 0;
string input = "";

// Prompt the user for a number
Console.Write("Enter a positive number: ");
// Get the users input
input = Console.ReadLine() ?? "";
// See if the user entered valid input: keep asking until the input
// is a whole number greater than 0
while (!int.TryParse(input, out choice) || choice <= 0)
{
    Console.WriteLine("Invalid number");
    // Re-Prompt the user for a number
    Console.Write("Enter a positive number: ");
    // Get the users input
    input = Console.ReadLine() ?? "";
}
// Call the CountToOne function
result = Utility.CountToOne(choice);
Console.WriteLine($"The end number is {result}");

//----------------------------------------------------------
// End of the Main Method
//----------------------------------------------------------

//----------------------------------------------------------
// Start of the Utility class
//----------------------------------------------------------
static class Utility
{
    /// <summary>
    /// Count to one using recursion
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
        else
        {
            // Check if the number is even
            if ((num % 2) == 0)
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
        }
    } // End of CountToOne method
}
//----------------------------------------------------------
// End of the Utility class
//----------------------------------------------------------
