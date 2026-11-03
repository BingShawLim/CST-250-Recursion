/*
 * Bing Shaw Lim
 * CST-250
 * 11/03/2026
 * Flood Fill Recursion
 * Activity 3
 * Resources: CST-250 Activity 3 Guide (Grand Canyon University, 2025)
 */

namespace FloodFillRecursion.Models
{
    /// <summary>
    /// The square flood fill board. It is made of a 2D array of CellModel objects.
    /// </summary>
    internal class BoardModel
    {
        // BoardModel Properties
        public int Size { get; set; }
        public CellModel[,] Grid { get; set; }
        public int NumShapes { get; set; }

        /// <summary>
        /// Parameterized constructor for BoardModel
        /// </summary>
        /// <param name="size">The length and width of the board</param>
        /// <param name="numShapes">How many shapes to place on the board</param>
        public BoardModel(int size, int numShapes)
        {
            Size = size;
            NumShapes = numShapes;
            Grid = new CellModel[Size, Size];
            // Set up the Grid
            for (int row = 0; row < Size; row++)
            {
                for (int col = 0; col < Size; col++)
                {
                    Grid[row, col] = new CellModel(row, col, "E");
                }
            }
            // Place random shapes on the board
            PlaceShapes();
        }

        /// <summary>
        /// Create shapes to place on the board. It is private, so only the
        /// constructor can call it. Updated for Challenge 2: the shapes take turns
        /// being a square, a triangle, a diamond, and a set of random points.
        /// </summary>
        private void PlaceShapes()
        {
            // Declare and initialize
            // Random object to generate numbers
            Random random = new Random();
            int shapeSize = Size / 2;

            // Create the shapes
            for (int shapes = 0; shapes < NumShapes; shapes++)
            {
                // Choose the next shape in turn
                switch (shapes % 4)
                {
                    case 0:
                        PlaceSquare(random, shapeSize);
                        break;
                    case 1:
                        PlaceTriangle(random, shapeSize);
                        break;
                    case 2:
                        PlaceDiamond(random, shapeSize);
                        break;
                    default:
                        PlaceRandomPoints(random, Size / 2);
                        break;
                }
            }
        } // End of PlaceShapes method

        /// <summary>
        /// Place the outline of a square (the original shape from the activity)
        /// </summary>
        /// <param name="random">The random number generator</param>
        /// <param name="shapeSize">The length of each side</param>
        private void PlaceSquare(Random random, int shapeSize)
        {
            // Generate the row and col for the
            // top left corner of the square
            int row = random.Next(0, Size - shapeSize + 1);
            int col = random.Next(0, Size - shapeSize + 1);
            for (int offset = 0; offset < shapeSize; offset++)
            {
                // Top Wall
                Grid[row, col + offset].Contents = "W";
                // Bottom Wall
                Grid[row + shapeSize - 1, col + offset].Contents = "W";
                // Left Wall
                Grid[row + offset, col].Contents = "W";
                // Right Wall
                Grid[row + offset, col + shapeSize - 1].Contents = "W";
            }
        } // End of PlaceSquare method

        /// <summary>
        /// Challenge 2: place the outline of a right triangle. Its long side is a
        /// diagonal line of walls.
        /// </summary>
        /// <param name="random">The random number generator</param>
        /// <param name="shapeSize">The length of the two short sides</param>
        private void PlaceTriangle(Random random, int shapeSize)
        {
            // Generate the row and col for the top left corner of the triangle
            int row = random.Next(0, Size - shapeSize + 1);
            int col = random.Next(0, Size - shapeSize + 1);
            for (int offset = 0; offset < shapeSize; offset++)
            {
                // Left Wall
                Grid[row + offset, col].Contents = "W";
                // Bottom Wall
                Grid[row + shapeSize - 1, col + offset].Contents = "W";
                // Diagonal Wall from the top left corner to the bottom right corner
                Grid[row + offset, col + offset].Contents = "W";
            }
        } // End of PlaceTriangle method

        /// <summary>
        /// Challenge 2: place the outline of a diamond. All four sides are diagonal
        /// lines of walls, so an eight-way flood fill can jump through them.
        /// </summary>
        /// <param name="random">The random number generator</param>
        /// <param name="shapeSize">The width of the diamond</param>
        private void PlaceDiamond(Random random, int shapeSize)
        {
            // The diamond reaches radius cells from its center in each direction
            int radius = shapeSize / 2;
            // Generate the row and col for the center of the diamond
            int centerRow = random.Next(radius, Size - radius);
            int centerCol = random.Next(radius, Size - radius);
            for (int offset = 0; offset <= radius; offset++)
            {
                // Top left and top right sides: one cell wider on each row going down
                Grid[centerRow - radius + offset, centerCol - offset].Contents = "W";
                Grid[centerRow - radius + offset, centerCol + offset].Contents = "W";
                // Bottom left and bottom right sides: one cell wider on each row going up
                Grid[centerRow + radius - offset, centerCol - offset].Contents = "W";
                Grid[centerRow + radius - offset, centerCol + offset].Contents = "W";
            }
        } // End of PlaceDiamond method

        /// <summary>
        /// Challenge 2: place walls on single random cells
        /// </summary>
        /// <param name="random">The random number generator</param>
        /// <param name="count">How many random walls to place</param>
        private void PlaceRandomPoints(Random random, int count)
        {
            for (int point = 0; point < count; point++)
            {
                Grid[random.Next(0, Size), random.Next(0, Size)].Contents = "W";
            }
        } // End of PlaceRandomPoints method
    }
}
