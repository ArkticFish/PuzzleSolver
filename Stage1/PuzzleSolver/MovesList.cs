using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PuzzleSolver
{
    public partial class MovesList : Form
    {
        public MovesList()
        {
            InitializeComponent();
        }

        public void ShowMoves(List<Tuple<string, int>> moves)
        {
            this.moves = moves;
            Show();
        }

        public List<Tuple<string, int>> moves = new List<Tuple<string, int>>();

        private void MovesList_Paint(object sender, PaintEventArgs e)
        {
            if (moves.Count > 0)
            {
                if (moves.First().Item2 == 0)
                    e.Graphics.DrawImage(Properties.Resources.ArrowUp, this.DisplayRectangle);
                else if (moves.First().Item2 == 1)
                    e.Graphics.DrawImage(Properties.Resources.ArrowDown, this.DisplayRectangle);
                else if (moves.First().Item2 == 2)
                    e.Graphics.DrawImage(Properties.Resources.ArrowLeft, this.DisplayRectangle);
                else if (moves.First().Item2 == 3)
                    e.Graphics.DrawImage(Properties.Resources.ArrowRight, this.DisplayRectangle);

                e.Graphics.DrawString(moves.First().Item1, new Font("Ariel", 25), Brushes.Blue, this.DisplayRectangle, new StringFormat {Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });

                e.Graphics.DrawString(moves.Count + " moves left.", new Font("Ariel", 10), Brushes.Blue, this.DisplayRectangle, new StringFormat { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Near });
            }
        }

        public void MoveMade(Tuple<string, int> move)
        {
            if (moves.Count > 0)
            {
                if (moves.First().Equals(move))
                {
                    moves.RemoveAt(0);

                    if (moves.Count == 0)
                    {
                        Close();
                    }

                }
                else
                {
                    if (move.Item2 == 0)
                        moves.Insert(0, new Tuple<string, int>(move.Item1, 1));
                    else if (move.Item2 == 1)
                        moves.Insert(0, new Tuple<string, int>(move.Item1, 0));
                    else if (move.Item2 == 2)
                        moves.Insert(0, new Tuple<string, int>(move.Item1, 3));
                    else if (move.Item2 == 3)
                        moves.Insert(0, new Tuple<string, int>(move.Item1, 2));
                }
                Refresh();
            }
        }

    }
}
