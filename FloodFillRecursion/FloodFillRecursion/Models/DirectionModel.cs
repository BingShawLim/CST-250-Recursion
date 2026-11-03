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
    /// One direction the flood fill can move (Challenges 1 and 3): how the row and column
    /// change, the name of the direction, and the color of its log message
    /// </summary>
    internal class DirectionModel
    {
        // DirectionModel Properties
        public string Name { get; set; }
        public int RowChange { get; set; }
        public int ColumnChange { get; set; }
        public ConsoleColor Color { get; set; }

        /// <summary>
        /// Parameterized constructor for DirectionModel
        /// </summary>
        /// <param name="name">The name of the direction, such as North</param>
        /// <param name="rowChange">How much the row changes (-1, 0, or 1)</param>
        /// <param name="columnChange">How much the column changes (-1, 0, or 1)</param>
        /// <param name="color">The color of the log message for this direction</param>
        public DirectionModel(string name, int rowChange, int columnChange, ConsoleColor color)
        {
            Name = name;
            RowChange = rowChange;
            ColumnChange = columnChange;
            Color = color;
        }
    }
}
