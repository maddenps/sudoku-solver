using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SudokuSolverApp {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        private void solveButton_Click(object sender, EventArgs e) {
            SolveMethod();
        }
        private void Testbutton_Click(object sender, EventArgs e) {
            testMethod();
        }

        public void SolveMethod() {
            try {
                Board board = new Board();
                int ALLSOLVED = 81;
                do {
                    if (board.isFirstPass == true) {
                        testInfoBox.Items.Add("Still in the if");
                        foreach (Control n in this.Controls) {
                            if (n is TextBox && n.Text != "") {
                                board.FirstPassClears(int.Parse(n.Name[1].ToString()), int.Parse(n.Name[3].ToString()), int.Parse(n.Text));
                            }
                            else if (n.Name[1].Equals("9") && n.Name[3].Equals("9")) {
                                board.isFirstPass = false;
                                testInfoBox.Items.Add("Finished first pass!");
                            }
                        }
                    }

                    else {
                        testInfoBox.Items.Add("Got to the else");
                        foreach (Control n in this.Controls) {
                            if (n is TextBox) {
                                board.SolveCheck(int.Parse(n.Name[1].ToString()), int.Parse(n.Name[3].ToString()), int.Parse(n.Text));
                            }
                        }
                    }
                }
                while (board.Squares.Count(x => !x.IsSolved).Equals(ALLSOLVED));
                testInfoBox.Items.Add("Everything finished");
            }
            catch {
                testInfoBox.Items.Add("Unable to solve, ran into infinite loop without solving any more squares");
            }
        }

        public void testMethod() {
            Board board = new Board();
            foreach (Control n in this.Controls) {
                if (n is TextBox) {
                    if (n.Text.Any(char.IsDigit)) {
                        testInfoBox.Items.Add("(" + n.Name[1] + ", " + n.Name[3] + ") V: " + n.Text);
                    }
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e) { }
    }
}
