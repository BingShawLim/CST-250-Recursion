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
    /// One square on the flood fill board
    /// </summary>
    internal class CellModel
    {
        // CellModel Properties
        public int Row { get; set; }
        public int Column { get; set; }
        // "E" = empty, "W" = wall, "F" = filled
        public string Contents { get; set; }

        /// <summary>
        /// Parameterized constructor for CellModel
        /// </summary>
        /// <param name="row">The row of the cell</param>
        /// <param name="column">The column of the cell</param>
        /// <param name="contents">E for empty, W for wall, or F for filled</param>
        public CellModel(int row, int column, string contents)
        {
            Row = row;
            Column = column;
            Contents = contents;
        }
    }
}
