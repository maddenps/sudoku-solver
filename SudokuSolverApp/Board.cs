using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Windows.Forms;
    

namespace SudokuSolverApp {
    public class Board {
        static Form1 mainForm = Application.OpenForms.OfType<Form1>().FirstOrDefault(); // Create an object for form1 so this can make changes to the suares
        static int MAXVAL = 10;
        public List<int> count = new List<int> { };
        public List<Square> Squares { get; private set; } // Create a list that will copntain squares that will be filled later
        public bool isFirstPass = true;

        public Board() { // Constructor for the class
            Squares = new List<Square>(); // Initiallizing the list
            /*
            using (StreamReader readIn = new StreamReader("C:/CODE/PersonalProjects/SudokuSolverApp/sudoku/test.txt")) {
                while(readIn.Peek() >= 0) {
                    puzzle += readIn.ReadLine();
                }
            }*/

            // For block that fills the list with squares that know what row/column they are
            for (int row = 1; row < 10; row++) {
                for (int column = 1; column < 10; column++) {
                    Squares.Add(new Square(row, column));

                    Control[] square = mainForm.Controls.Find("r" + row.ToString() + "c" + column.ToString(), true);
                    string puzzle = "004300209005009001070060043006002087190007400050083000600000105003508690042910300";
                    int rcVal = puzzle.ElementAt(((row - 1) * 9) + column);
                    if (square[0] is TextBox) { // If the control object is a textbox, continue
                        TextBox tb = (TextBox)square[0]; // Weird conversion I need so that I can later change the text of textbox
                        if (rcVal == 0) {
                            tb.Text = "";
                        }
                        else {
                            tb.Text = rcVal.ToString();
                        }
                    }
                }
            }
        }

        public void FirstPassClears(int row, int column, int value) {
            Square currentSquare = Squares.Single(x => (x.Row == row) && (x.Column == column)); // We will use current square to get rid of the potential values in same row, column, and quadrant later

            currentSquare.Value = value; // Set the value for the current square
            currentSquare.PotentialValues.Clear(); // Clear all potential values so that the rest of the code doesn't waste time on this
            currentSquare.IsSolved = true; // We need to mark this as solved so that the foreach loops later don't try to access this square anymore

            // Get rid of the current square's potential value from the same row
            foreach (Square square in Squares.Where(s => !s.IsSolved && (s.Row == row))) {
                square.PotentialValues.Remove(value);
            }

            // Get rid of the current square's potential value from the same column
            foreach (Square square in Squares.Where(s => !s.IsSolved && (s.Column == column))) {
                square.PotentialValues.Remove(value);
            }

            // Get rid of the current square's potential value from the same quadrant
            foreach (Square square in Squares.Where(s => !s.IsSolved && (s.Quadrant == currentSquare.Quadrant))) {
                square.PotentialValues.Remove(value);
            }
        }

        public void SolveCheck(int row, int column, int value) {
            Square currentSquare = Squares.Single(x => (x.Row == row) && (x.Column == column)); // We will use current square to get rid of the potential values in same row, column, and quadrant later

            // Set the Value for Naked Singles 
            foreach (Square square in Squares.Where(s => !s.IsSolved && (s.PotentialValues.Count == 1))) {
                SetSquareValue(square.Row, square.Column, square.PotentialValues[0]);
            }

            // Set the Value for a Hidden Single 
            foreach (Square square in Squares.Where(s => s.Quadrant == currentSquare.Quadrant)) {
                for (int i = 1; i < MAXVAL; i++) {
                    if (square.PotentialValues.Contains(i)) {
                        count.Add(i);
                    }
                }
            }
            for (int i = 1; i < MAXVAL; i++) {
                if (count.SingleOrDefault(s => s.Equals(i)) == i) {

                }
            }
            count.Clear();
            // Set the Value for a square that has the only potentialvalue for a given number in the quadrant
            foreach (Square square in Squares.Where(s => s.Row == currentSquare.Row)) {
                for (int i = 1; i < MAXVAL; i++) {
                    if (square.PotentialValues.Contains(i)) {
                        count.Add(i);
                    }
                }
            }
            count.Clear();
            // Set the Value for a square that has the only potentialvalue for a given number in the quadrant
            foreach (Square square in Squares.Where(s => s.Column == currentSquare.Column)) {
                for (int i = 1; i < MAXVAL; i++) {
                    if (square.PotentialValues.Contains(i)) {
                        count.Add(i);
                    }
                }
            }
            count.Clear();
        }

        private void SetSquareValue(int row, int column, int value) {
            Square currentSquare = Squares.Single(x => (x.Row == row) && (x.Column == column)); // We will use current square to get rid of the potential values in same row, column, and quadrant later
            Control[] matches = mainForm.Controls.Find("r" + currentSquare.Row.ToString() + "c" + currentSquare.Column.ToString(), true); // Creates an object for the control in the form with the same name as the text box currently active

            if (matches[0] is TextBox) { // If the control object is a textbox, continue
                TextBox tb = (TextBox)matches[0]; // Weird conversion I need so that I can later change the text of textbox

                currentSquare.Value = value; // Set the value for the current square
                currentSquare.PotentialValues.Clear(); // Clear all potential values so that the rest of the code doesn't waste time on this
                currentSquare.IsSolved = true; // We need to mark this as solved so that the foreach loops later don't try to access this square anymore
                tb.Text = currentSquare.Value.ToString(); // Use the Value we set earlier to change the text in the corresponding 


                // Get rid of the current square's potential value from the same row
                foreach (Square square in Squares.Where(s => !s.IsSolved && (s.Row == row))) {
                    square.PotentialValues.Remove(value);
                }

                // Get rid of the current square's potential value from the same column
                foreach (Square square in Squares.Where(s => !s.IsSolved && (s.Column == column))) {
                    square.PotentialValues.Remove(value);
                }

                // Get rid of the current square's potential value from the same quadrant
                foreach (Square square in Squares.Where(s => !s.IsSolved && (s.Quadrant == currentSquare.Quadrant))) {
                    square.PotentialValues.Remove(value);
                }                
            }
        }
    }
}