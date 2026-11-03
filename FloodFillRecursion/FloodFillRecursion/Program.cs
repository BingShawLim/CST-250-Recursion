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
// Create a new BoardModel with four shapes: a square, a triangle, a diamond, and random points
BoardModel board = new BoardModel(20, 4);
List<CellModel> filledCells = new List<CellModel>();
int choice = -1, startRow = -1, startCol = -1;
bool showSteps = false;

Console.WriteLine("Flood Fill Recursion");
// Print the board to the console
Utility.PrintBoard(board);

// Keep showing the menu until the user chooses 0 to exit
while (choice != 0)
{
    // Show the menu and get the user's choice
    Utility.ShowMenu();
    choice = Utility.ReadIntFromConsole("Choose an option (0 - 5): ", 0, 5);

    // Options 1 - 4 need a starting cell
    if (choice >= 1 && choice <= 4)
    {
        // Prompt the user for the starting row and column (1 - 20).
        // Remove 1 from each to get the 0 - 19 range used by the grid.
        startRow = Utility.ReadIntFromConsole($"Enter the row to start the flood fill at (1 - {board.Size}): ", 1, board.Size) - 1;
        startCol = Utility.ReadIntFromConsole($"Enter the column to start the flood fill at (1 - {board.Size}): ", 1, board.Size) - 1;
        // Clear the last fill so every fill starts with the same walls
        Utility.ResetFill(board);
        filledCells.Clear();
    }

    switch (choice)
    {
        case 1:
            // The original four-way fill: North, East, South, West
            showSteps = Utility.ReadYesNo("Show each step? (y/n): ");
            board = Utility.FloodFill(board, startRow, startCol, Utility.FourWayOrder, showSteps, filledCells);
            Utility.ShowFillResult(board, filledCells, "Four-way fill (North, East, South, West)");
            break;
        case 2:
            // Challenge 1: a four-way fill that starts West instead of North
            showSteps = Utility.ReadYesNo("Show each step? (y/n): ");
            board = Utility.FloodFill(board, startRow, startCol, Utility.WestFirstOrder, showSteps, filledCells);
            Utility.ShowFillResult(board, filledCells, "Four-way fill (West, South, East, North)");
            break;
        case 3:
            // Challenge 3: an eight-way fill that also moves diagonally
            showSteps = Utility.ReadYesNo("Show each step? (y/n): ");
            board = Utility.FloodFill(board, startRow, startCol, Utility.EightWayOrder, showSteps, filledCells);
            Utility.ShowFillResult(board, filledCells, "Eight-way fill (adds the diagonals)");
            break;
        case 4:
            // Challenge 1: compare the North-first and West-first orders from the same cell
            Utility.CompareFillOrders(board, startRow, startCol);
            break;
        case 5:
            // Make a new board with new random shapes
            board = new BoardModel(board.Size, board.NumShapes);
            Utility.PrintBoard(board);
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
// Define a utility class
//----------------------------------------------------------
static class Utility
{
    // How long to pause between steps when each step is shown (milliseconds)
    internal const int SleepCount = 25;

    // The four main directions and the four diagonals
    internal static readonly DirectionModel North = new DirectionModel("North", -1, 0, ConsoleColor.Blue);
    internal static readonly DirectionModel East = new DirectionModel("East", 0, 1, ConsoleColor.Yellow);
    internal static readonly DirectionModel South = new DirectionModel("South", 1, 0, ConsoleColor.Magenta);
    internal static readonly DirectionModel West = new DirectionModel("West", 0, -1, ConsoleColor.Cyan);
    internal static readonly DirectionModel NorthEast = new DirectionModel("North East", -1, 1, ConsoleColor.Green);
    internal static readonly DirectionModel SouthEast = new DirectionModel("South East", 1, 1, ConsoleColor.DarkYellow);
    internal static readonly DirectionModel SouthWest = new DirectionModel("South West", 1, -1, ConsoleColor.DarkCyan);
    internal static readonly DirectionModel NorthWest = new DirectionModel("North West", -1, -1, ConsoleColor.DarkMagenta);

    // The order used in the activity: North, East, South, West
    internal static readonly DirectionModel[] FourWayOrder = { North, East, South, West };
    // Challenge 1: start with West instead of North (West, South, East, North)
    internal static readonly DirectionModel[] WestFirstOrder = { West, South, East, North };
    // Challenge 3: eight-way fill adds the four diagonal directions
    internal static readonly DirectionModel[] EightWayOrder =
        { North, NorthEast, East, SouthEast, South, SouthWest, West, NorthWest };

    /// <summary>
    /// Show the menu of flood fill options
    /// </summary>
    internal static void ShowMenu()
    {
        Console.WriteLine();
        Console.WriteLine("1. Four-way fill: North, East, South, West (original order)");
        Console.WriteLine("2. Four-way fill: West, South, East, North (Challenge 1)");
        Console.WriteLine("3. Eight-way fill: adds the diagonals (Challenge 3)");
        Console.WriteLine("4. Compare the North-first and West-first fill orders (Challenge 1)");
        Console.WriteLine("5. Make a new board");
        Console.WriteLine("0. Exit");
    } // End of ShowMenu method

    /// <summary>
    /// Print the board to the console. Updated for Challenge 4: walls, empty cells,
    /// and filled cells each have their own characters and colors.
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
                PrintCell(board.Grid[row, col].Contents, "");
            }
            // Use a WriteLine to start a new row
            Console.WriteLine();
        }
        // Go back to the console's normal colors
        Console.ResetColor();
    } // End of PrintBoard method

    /// <summary>
    /// Challenge 4: print one cell with its own character, text color, and background
    /// </summary>
    /// <param name="contents">E for empty, W for wall, or F for filled</param>
    /// <param name="label">Optional number (up to 2 digits) to show on a filled cell</param>
    private static void PrintCell(string contents, string label)
    {
        if (contents == "W")
        {
            // Walls are solid gray blocks
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.Write(" # ");
        }
        else if (contents == "F")
        {
            // Filled cells look like water: cyan waves (or a label) on a dark blue background
            Console.ForegroundColor = label == "" ? ConsoleColor.Cyan : ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.Write(label == "" ? " ~ " : $"{label,2} ");
        }
        else
        {
            // Empty cells are small dark gray dots
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write(" . ");
        }
        // Go back to the normal colors so the next cell starts clean
        Console.ResetColor();
    } // End of PrintCell method

    /// <summary>
    /// Perform a flood fill algorithm on the given row and col. Updated for the
    /// challenges: the directions (and their order) are passed in, so the same method
    /// does the North-first fill, the West-first fill (Challenge 1), and the eight-way
    /// fill (Challenge 3). Each filled cell is saved in the order it was filled.
    /// </summary>
    /// <param name="board">The board to fill</param>
    /// <param name="row">The row of the current cell</param>
    /// <param name="col">The column of the current cell</param>
    /// <param name="directions">The directions to move, in the order to try them</param>
    /// <param name="showSteps">True to log each step and redraw the board with a short pause</param>
    /// <param name="filledCells">Each filled cell is added here in the order it was filled</param>
    /// <returns>The board after the fill</returns>
    internal static BoardModel FloodFill(BoardModel board, int row, int col, DirectionModel[] directions,
        bool showSteps, List<CellModel> filledCells)
    {
        if (showSteps)
        {
            // Print the current cell (with the same 1-based numbers as the board)
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"Filling at {row + 1}, {col + 1} ");
            // Pause the program for SleepCount number of milliseconds
            Thread.Sleep(SleepCount);
        }

        // Base case 1: the cell is not on the board
        if (row < 0 || row >= board.Size || col < 0 || col >= board.Size)
        {
            ShowStop(showSteps, "Out of bounds. Stop");
            return board;
        }
        // Base case 2: the cell is a wall
        if (board.Grid[row, col].Contents == "W")
        {
            ShowStop(showSteps, "Hit a wall. Stop");
            return board;
        }
        // Base case 3: the cell has already been filled
        if (board.Grid[row, col].Contents == "F")
        {
            ShowStop(showSteps, "Already Filled. Stop");
            return board;
        }

        // Fill the cell and remember the order it was filled in
        board.Grid[row, col].Contents = "F";
        filledCells.Add(board.Grid[row, col]);

        if (showSteps)
        {
            // Pause, then clear the console and print the current board
            Thread.Sleep(SleepCount);
            Console.Clear();
            Console.WriteLine();
            PrintBoard(board);
        }

        // Recursive step: call the flood fill method in each direction, in order
        foreach (DirectionModel direction in directions)
        {
            if (showSteps)
            {
                // Print a message indicating the next flood fill direction
                Console.ForegroundColor = direction.Color;
                Console.Write($"{direction.Name}: ");
            }
            board = FloodFill(board, row + direction.RowChange, col + direction.ColumnChange,
                directions, showSteps, filledCells);
        }

        // Return the board
        return board;
    } // End of FloodFill method

    /// <summary>
    /// Print why the flood fill stopped at a cell, then pause (only when steps are shown)
    /// </summary>
    /// <param name="showSteps">True to print the message</param>
    /// <param name="message">Why this call stopped</param>
    private static void ShowStop(bool showSteps, string message)
    {
        if (showSteps)
        {
            Console.WriteLine(message);
            Thread.Sleep(SleepCount);
        }
    } // End of ShowStop method

    /// <summary>
    /// Turn every filled cell back into an empty cell so a new fill can start
    /// </summary>
    /// <param name="board">The board to reset</param>
    internal static void ResetFill(BoardModel board)
    {
        foreach (CellModel cell in board.Grid)
        {
            if (cell.Contents == "F")
            {
                cell.Contents = "E";
            }
        }
    } // End of ResetFill method

    /// <summary>
    /// Print the board after a fill and how many cells were filled
    /// </summary>
    /// <param name="board">The filled board</param>
    /// <param name="filledCells">The cells that were filled, in order</param>
    /// <param name="fillName">The name of the fill that was used</param>
    internal static void ShowFillResult(BoardModel board, List<CellModel> filledCells, string fillName)
    {
        Console.WriteLine();
        PrintBoard(board);
        if (filledCells.Count == 0)
        {
            Console.WriteLine($"{fillName}: nothing was filled because the starting cell is a wall.");
        }
        else
        {
            Console.WriteLine($"{fillName}: filled {filledCells.Count} cells starting at " +
                $"{filledCells[0].Row + 1}, {filledCells[0].Column + 1}.");
        }
    } // End of ShowFillResult method

    /// <summary>
    /// Challenge 1: print the board with the first 99 filled cells numbered in the
    /// order they were filled, so the path of the recursion can be seen
    /// </summary>
    /// <param name="board">The filled board</param>
    /// <param name="filledCells">The cells that were filled, in order</param>
    internal static void PrintFillOrder(BoardModel board, List<CellModel> filledCells)
    {
        // Print the column numbers in white. The row numbers in this map get an extra
        // space so a two-digit fill number never touches its row number.
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write("  ");
        for (int colNum = 0; colNum < board.Size; colNum++)
        {
            Console.Write($" {colNum + 1,2}");
        }
        Console.WriteLine();

        for (int row = 0; row < board.Size; row++)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"{row + 1,2} ");
            for (int col = 0; col < board.Size; col++)
            {
                // A filled cell shows its fill number (cells after 99 show a wave)
                int order = filledCells.IndexOf(board.Grid[row, col]) + 1;
                PrintCell(board.Grid[row, col].Contents, order >= 1 && order <= 99 ? order.ToString() : "");
            }
            Console.WriteLine();
        }
        Console.ResetColor();
    } // End of PrintFillOrder method

    /// <summary>
    /// Challenge 1: fill from the same cell twice, first starting North (North, East,
    /// South, West) and then starting West (West, South, East, North), and compare them
    /// </summary>
    /// <param name="board">The board to fill</param>
    /// <param name="row">The starting row</param>
    /// <param name="col">The starting column</param>
    internal static void CompareFillOrders(BoardModel board, int row, int col)
    {
        // Declare and initialize
        List<CellModel> northFirst = new List<CellModel>();
        List<CellModel> westFirst = new List<CellModel>();
        bool sameCells = true;

        // Fill starting North and show the order of the fill
        FloodFill(board, row, col, FourWayOrder, false, northFirst);
        Console.WriteLine();
        Console.WriteLine("Starting North (North, East, South, West). Numbers show the fill order:");
        PrintFillOrder(board, northFirst);

        // Clear the fill, then fill again starting West
        ResetFill(board);
        FloodFill(board, row, col, WestFirstOrder, false, westFirst);
        Console.WriteLine();
        Console.WriteLine("Starting West (West, South, East, North). Numbers show the fill order:");
        PrintFillOrder(board, westFirst);

        // Compare the results: did both orders fill exactly the same cells?
        foreach (CellModel cell in northFirst)
        {
            if (!westFirst.Contains(cell))
            {
                sameCells = false;
            }
        }
        sameCells = sameCells && northFirst.Count == westFirst.Count;
        Console.WriteLine($"North first filled {northFirst.Count} cells and West first filled {westFirst.Count} cells.");
        Console.WriteLine($"Both orders filled the same cells: {sameCells}. Only the path of the recursion changed.");
    } // End of CompareFillOrders method

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
        int num = -1;

        // Prompt the user
        Console.Write(prompt);
        // Check if the current input is valid and inside the range
        while (!int.TryParse(Console.ReadLine(), out num) || num < min || num > max)
        {
            // Inform the user of invalid input and prompt the user again
            Console.Write($"Invalid input. Please enter a whole number from {min} to {max}: ");
        }
        // Return the integer from the user
        return num;
    } // End of ReadIntFromConsole method

    /// <summary>
    /// Ask a yes or no question
    /// </summary>
    /// <param name="prompt">The question to ask</param>
    /// <returns>True for y and false for n</returns>
    internal static bool ReadYesNo(string prompt)
    {
        // Declare and initialize
        string answer = "";

        // Prompt the user and keep asking until the answer is y or n
        Console.Write(prompt);
        answer = (Console.ReadLine() ?? "").Trim().ToLower();
        while (answer != "y" && answer != "n")
        {
            Console.Write("Please enter y or n: ");
            answer = (Console.ReadLine() ?? "").Trim().ToLower();
        }
        return answer == "y";
    } // End of ReadYesNo method
}
//----------------------------------------------------------
// End of the utility class
//----------------------------------------------------------
