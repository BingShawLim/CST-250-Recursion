/*
 * Bing Shaw Lim
 * CST-250
 * 11/03/2026
 * Flood Fill Recursion
 * Activity 3
 * Resources: CST-250 Activity 3 Guide (Grand Canyon University, 2025)
 */

using FloodFillRecursion.Models;

//----------------------------------------------------------
// Start of the Main Method
//----------------------------------------------------------

// Declare and initialize
// Create a new BoardModel
BoardModel board = new BoardModel(20, 3);

// Print the board to the console
Utility.PrintBoard(board);

//----------------------------------------------------------
// End of the Main Method
//----------------------------------------------------------

//----------------------------------------------------------
// Define a utility class
//----------------------------------------------------------
static class Utility
{
    /// <summary>
    /// Print the board to the console
    /// </summary>
    /// <param name="board">The board to print</param>
    internal static void PrintBoard(BoardModel board)
    {
        // Make sure the color of the column numbers is white
        Console.ForegroundColor = ConsoleColor.White;
        // Start the column numbers row with a space to keep the numbers aligned
        Console.Write(" ");
        // Loop to add column numbers for the board
        for (int colNum = 0; colNum < board.Size; colNum++)
        {
            // Print the colNum with a 2-character width
            Console.Write($" {colNum + 1,2}");
        }
        Console.WriteLine();

        // Loop through the rows of the board
        for (int row = 0; row < board.Size; row++)
        {
            // Print each row number in white
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"{row + 1,2}");

            // Loop through the columns of the board
            for (int col = 0; col < board.Size; col++)
            {
                // Check if the current cell is a wall
                if (board.Grid[row, col].Contents == "W")
                {
                    // Change the text color to red
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(" W ");
                }
                else if (board.Grid[row, col].Contents == "E")
                {
                    // Change the text color to white
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(" . ");
                }
                else
                {
                    Console.Write("   ");
                }
            }
            // Use a WriteLine to start a new row
            Console.WriteLine();
        }
        // Go back to the console's normal colors
        Console.ResetColor();
    } // End of PrintBoard method
}
//----------------------------------------------------------
// End of the utility class
//----------------------------------------------------------
