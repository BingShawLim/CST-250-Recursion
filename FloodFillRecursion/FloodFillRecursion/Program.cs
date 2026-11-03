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

// Call the flood fill method using the board
board = Utility.FloodFill(board, 0, 0);

// Print the new board
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
                else if (board.Grid[row, col].Contents == "F")
                {
                    // Filled cells are dark blue waves
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    Console.Write(" ~ ");
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

    /// <summary>
    /// Perform a flood fill algorithm on the given row and col
    /// </summary>
    /// <param name="board">The board to fill</param>
    /// <param name="row">The row of the current cell</param>
    /// <param name="col">The column of the current cell</param>
    /// <returns>The board after the fill</returns>
    internal static BoardModel FloodFill(BoardModel board, int row, int col)
    {
        // Declare and initialize
        int sleepCount = 25; // milliseconds

        // Change the text color to white
        Console.ForegroundColor = ConsoleColor.White;
        // Print the current cell to the console (with the same 1-based numbers as the board)
        Console.Write($"Filling at {row + 1}, {col + 1} ");
        // Pause the program for sleepCount number of milliseconds
        Thread.Sleep(sleepCount);

        // Check if the cell is on the board
        if (row < 0 || row >= board.Size || col < 0 || col >= board.Size)
        {
            // Print a message indicating the cell is out of bounds
            Console.WriteLine("Out of bounds. Stop");
            // Pause the program for sleepCount number of milliseconds
            Thread.Sleep(sleepCount);

            // If the cell is not on the board, end the method
            return board;
        }

        // If the cell is a wall, end the method
        if (board.Grid[row, col].Contents == "W")
        {
            // Print a message indicating the cell is a wall
            Console.WriteLine("Hit a wall. Stop");
            // Pause the program for sleepCount number of milliseconds
            Thread.Sleep(sleepCount);

            return board;
        }
        // If the cell has already been filled, end the method
        else if (board.Grid[row, col].Contents == "F")
        {
            // Print a message indicating the cell is already filled
            Console.WriteLine("Already Filled. Stop");
            // Pause the program for sleepCount number of milliseconds
            Thread.Sleep(sleepCount);

            return board;
        }
        // Else, fill the cell
        else
        {
            board.Grid[row, col].Contents = "F";
            // Pause the program for sleepCount number of milliseconds
            Thread.Sleep(sleepCount);
        }

        // Improve the visual effect of the flood fill
        // Comment out to have program history
        Console.Clear();
        // Print the current board
        Console.WriteLine();
        PrintBoard(board);

        // Print a message indicating the next flood fill direction
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.Write("North: ");
        // Call the flood fill method to the north
        board = FloodFill(board, row - 1, col);

        // Print a message indicating the next flood fill direction
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("East: ");
        // Call the flood fill method to the east
        board = FloodFill(board, row, col + 1);

        // Print a message indicating the next flood fill direction
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.Write("South: ");
        // Call the flood fill method to the south
        board = FloodFill(board, row + 1, col);

        // Print a message indicating the next flood fill direction
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Write("West: ");
        // Call the flood fill method to the west
        board = FloodFill(board, row, col - 1);

        // Return the board
        return board;
    } // End of FloodFill method
}
//----------------------------------------------------------
// End of the utility class
//----------------------------------------------------------
