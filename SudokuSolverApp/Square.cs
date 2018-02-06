using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolverApp {
    public class Square {

        public int Row { get; private set; }
        public int Column { get; private set; }
        internal List<int> PotentialValues { get; private set; }
        private List<int> potentialValues = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

        public bool IsSolved { get; set; }
        public int? Value { get; set; }

        internal Square(int row, int column) {
            Row = row;
            Column = column;
            PotentialValues = potentialValues;
            IsSolved = false;
        }

        internal enum Quadrants {
            UpperLeft, UpperCenter, UpperRight, CenterLeft, Center, CenterRight, LowerLeft, LowerCenter, LowerRight
        }    

        internal Quadrants Quadrant {
            get {                
                if (Row < 4) {
                    if (Column < 4) { return Quadrants.UpperLeft; }
                    return Column < 7 ? Quadrants.UpperCenter : Quadrants.UpperRight;
                }

                else if (Row < 7) {
                    if (Column < 4) { return Quadrants.CenterLeft; }
                    return Column < 7 ? Quadrants.Center : Quadrants.CenterRight;
                }

                else if (Column < 4) { return Quadrants.LowerLeft; }
                else { return Column < 7 ? Quadrants.LowerCenter : Quadrants.LowerRight; }
            }
        }      
    }    
}
