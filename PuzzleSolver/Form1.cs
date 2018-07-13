using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

using System.Windows.Forms;

namespace PuzzleSolver
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        public Game game;
        
        private void normalGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            game = new Game(new SpaceState(
                "      " +
                " 1223 " +
                " 1223 " +
                " 4556 " +
                " 4786 " +
                " 9  A " +
                "      " +
                "      " +
                "      "),
                new SpaceState(
                "######" +
                "#0000#" +
                "#0000#" +
                "#0000#" +
                "#0000#" +
                "#0000#" +
                "##--##" +
                "      " +
                "      "),
                6,
                9,
                '2');
            Refresh();
        }

        private void blockCheckToolStripMenuItem_Click(object sender, EventArgs e)
        {
            game = new Game(new SpaceState(
                "      " +
                " 1122 " +
                " 3344 " +
                " 5566 " +
                " 7789 " +
                "  7A  " +
                "      " +
                "      " +
                "      "),
                new SpaceState(
                "######" +
                "#0000#" +
                "#0000#" +
                "#0000#" +
                "#0000#" +
                "#0000#" +
                "##--##" +
                "      " +
                "      "),
                6,
                9,
                '7');
            Refresh();
        }

        private void walkingPuzzleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            game = new Game(new SpaceState(
                "           " +
                " 1         " +
                "           " +
                "           " +
                "           " +
                "           " +
                "           " +
                "           " +
                "           " +
                "           " +
                "           "),
                new SpaceState(
                "###        " +
                "#00#       " +
                " #00#      " +
                "  #00#     " +
                "   #00#    " +
                "    #00#   " +
                "     #00#  " +
                "      #00# " +
                "       #00#" +
                "        #-#" +
                "           "),
                11,
                11,
                '1');
            Refresh();
        }

        StringFormat sf = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
        StringFormat tf = new StringFormat() { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Near };

        private void gamePanel_Paint(object sender, PaintEventArgs e)
        {
            // Test if there is a game playing.
            if (game != null)
            {

                // Get the width and height of each block in the grid.
                int width = gamePanel.Width / game.w;
                int height = gamePanel.Height / game.h;

                // Loop through the blocks.
                for (int y = 0; y < game.h; y++)
                {
                    for (int x = 0; x < game.w; x++)
                    {
                        Rectangle block = new Rectangle(x * width, y * height, width, height);
                        
                        // Draw the stage.
                        if (game.stage.blocks[y * game.w + x] != ' ')
                        {
                            // Draw the edge of the stage.
                            if (game.stage.blocks[y * game.w + x] == '#')
                                e.Graphics.DrawImage(Properties.Resources.hash, block);
                            if (game.stage.blocks[y * game.w + x] == '-')
                                e.Graphics.DrawImage(Properties.Resources.gate, block);

                        }

                        // If the block is not empty, a block should be drawn.
                        if (game.state.blocks[y * game.w + x] != ' ')
                        {
                            // Draw selected overlay first if block is selected.
                            if (game.state.blocks[y * game.w + x] == game.selectedBlock)
                                e.Graphics.DrawImage(Properties.Resources.Overlay, block);

                            // Draw standard block if block is not empty.
                            e.Graphics.DrawImage(Properties.Resources.Block, block);

                            // Draw bridge blocks.
                            // If left block is the same block, draw bridge block.
                            if (x > 0 && game.state.blocks[y * game.w + x] == game.state.blocks[y * game.w + (x - 1)])
                                e.Graphics.DrawImage(Properties.Resources.Left, block);

                            // If right block is the same block, draw bridge block.
                            if (x < game.w - 1 && game.state.blocks[y * game.w + x] == game.state.blocks[y * game.w + (x + 1)])
                                e.Graphics.DrawImage(Properties.Resources.Right, block);

                            // If top block is the same block, draw bridge block.
                            if (y > 0 && game.state.blocks[y * game.w + x] == game.state.blocks[(y - 1) * game.w + x])
                                e.Graphics.DrawImage(Properties.Resources.Top, block);

                            // If bottum block is the same block, draw bridge block.
                            if (y < game.h - 1 && game.state.blocks[y * game.w + x] == game.state.blocks[(y + 1) * game.w + x])
                                e.Graphics.DrawImage(Properties.Resources.Bottum, block);

                            // Draw the name of the block on the block.
                            if (game.goalBlock == game.state.blocks[y * game.w + x])
                                e.Graphics.DrawString(game.state.blocks[y * game.w + x].ToString(), new Font("Ariel", 20), new SolidBrush(Color.GreenYellow), block, sf);
                            else
                                e.Graphics.DrawString(game.state.blocks[y * game.w + x].ToString(), new Font("Ariel", 20), new SolidBrush(Color.AliceBlue), block, sf);


                        } // End if block != 0.

                    } // End for x.
                } // End for y.
            } // End if game != null.
        }
        
        private void gamePanel_MouseClick(object sender, MouseEventArgs e)
        {
            
        }
        
        int mx = 0;
        int my = 0;

        private void gamePanel_MouseDown(object sender, MouseEventArgs e)
        {
            mx = e.X;
            my = e.Y;
            if (game != null)
            {
                int width = gamePanel.Width / game.w;
                int height = gamePanel.Height / game.h;

                for (int y = 0; y < game.h; y++)
                {
                    for (int x = 0; x < game.w; x++)
                    {
                        Rectangle box = new Rectangle(x * width, y * height, width, height);
                        if (e.X >= box.X && e.X <= box.X + box.Width && e.Y >= box.Y && e.Y <= box.Y + box.Height)
                        {
                            if (game.state.blocks[y * game.w + x] != ' ')
                                game.selectedBlock = game.state.blocks[y * game.w + x];
                        }

                    }
                }
            }
            Refresh();
        }
        
        private void gamePanel_MouseUp(object sender, MouseEventArgs e)
        {

            int xDif = mx - e.X;
            int yDif = my - e.Y;

            if (Math.Abs(xDif) > Math.Abs(yDif) && xDif > 100)
                MoveBlocks(2);
            else if (Math.Abs(xDif) > Math.Abs(yDif) && xDif < -100)
                MoveBlocks(3);
            else if (Math.Abs(yDif) > Math.Abs(xDif) && yDif > 100)
                MoveBlocks(0);
            else if (Math.Abs(yDif) > Math.Abs(xDif) && yDif < -100)
                MoveBlocks(1);

            Refresh();

        }

        private void MoveBlocks(int direction)
        {

            if (game == null)
                return;

            List<int> oldIndexes = new List<int>();
            for (int oi = 0; oi < game.state.blocks.Length; oi++)
            {
                if (game.state.blocks[oi] == game.selectedBlock)
                {
                    oldIndexes.Add(oi);
                }
            }

            // Set diff for movement.
            int diff = 1;
            if (direction == 0)
                diff = -game.w;
            else if (direction == 1)
                diff = +game.w;
            else if (direction == 2)
                diff = -1;
            else if (direction == 3)
                diff = +1;

            bool valid = true;
            
            // Create new indexes.
            List<int> newIndexes = new List<int>();
            for (int oi = 0; oi < oldIndexes.Count; oi++)
            {
                newIndexes.Add(oldIndexes[oi] + diff);
            }

            // Test against stage.
            for (int ni = 0; valid && ni < newIndexes.Count; ni++)
            {
                if (game.stage.blocks[newIndexes[ni]] == '#')
                {
                    valid = false;
                }
                else if (game.stage.blocks[newIndexes[ni]] == '-' && game.selectedBlock != game.goalBlock)
                {
                    valid = false;
                }
            }

            // Test new  indexes.
            for (int ni = 0; valid && ni < newIndexes.Count; ni++)
            {
                if (!(game.state.blocks[newIndexes[ni]] == ' ' || game.state.blocks[newIndexes[ni]] == game.selectedBlock))
                    valid = false;
            }

            // Apply move and add to list.
            if (valid)
            {
                string newBlocks = String.Copy(game.state.blocks);
                for (int oi = 0; oi < oldIndexes.Count; oi++)
                {
                    newBlocks = newBlocks.Remove(oldIndexes[oi], 1);
                    newBlocks = newBlocks.Insert(oldIndexes[oi], " ");
                }
                for (int ni = 0; ni < newIndexes.Count; ni++)
                {
                    newBlocks = newBlocks.Remove(newIndexes[ni], 1);
                    newBlocks = newBlocks.Insert(newIndexes[ni], game.selectedBlock.ToString());
                }

                SpaceState sp = new SpaceState(newBlocks, null, "", "");
                game.NewState(sp);

                if (game.IsWin())
                    MessageBox.Show("You have won the game. It took you " + game.stateHistory.Count + " moves.");

                try { ml.MoveMade(new Tuple<string, int>(game.selectedBlock.ToString(), direction)); }
                catch (Exception e) { }

            }

        }

        private List<Tuple<string, int>> moves = new List<Tuple<string, int>>();

        private MovesList ml = new MovesList();

        private void solveToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (game == null)
                return;

            moves.Clear();

            SolverWindow sw = new SolverWindow();
            SpaceState sp = sw.SolveGame(game);
            if (sp != null)
            {
                while (sp.parent != null)
                {
                    moves.Add(new Tuple<string, int>(sp.moved, int.Parse(sp.direction)));
                    sp = sp.parent;
                }

                moves.Reverse();

                try { ml.Close(); }
                catch (Exception ex) { }

                ml = new MovesList();
                ml.ShowMoves(moves);
            }
        }

    }

    public class Game
    {
        public int w, h;

        public char selectedBlock = '1';
        public char goalBlock = '1';

        public SpaceState state;
        public SpaceState stage;

        public Stack<SpaceState> stateHistory = new Stack<SpaceState>();
        
        public Game(SpaceState state, SpaceState stage, int w, int h, char goalBlock)
        {
            this.w = w;
            this.h = h;
            this.state = state;
            this.stage = stage;
            this.goalBlock = goalBlock;
        }

        public void Undo()
        {
            if (stateHistory.Count > 0)
                state = stateHistory.Pop();
        }

        public void NewState(SpaceState state)
        {
            stateHistory.Push(this.state);
            this.state = state;
        }

        public bool IsWin()
        {
            for (int i = 0; i < stage.blocks.Count(); i++)
                if ((stage.blocks[i] == '0' || stage.blocks[i] == '-') && state.blocks[i] == goalBlock)
                    return false;

            return true;
        }

    }

    public class SpaceState
    {

        public string blocks;

        public string direction = "null";
        public string moved = "null";

        public SpaceState parent;

        public SpaceState(string blocks)
        {
            this.blocks = blocks;
        }

        public SpaceState(string blocks, SpaceState parent, string moved, string direction)
        {
            this.blocks = blocks;
            this.parent = parent;
            this.moved = moved;
            this.direction = direction;
        }

        public override bool Equals(object obj)
        {
            return blocks.Equals(((SpaceState)obj).blocks);
        }

    }

    public class Block
    {
        public string name;
        public string type;
        public List<int> shape = new List<int>();

        public Block(string name, string type, List<int> shape)
        {
            this.name = name;
            this.type = type;
            this.shape = shape;
        }

    }

    class MyPanel : Panel
    {
        public MyPanel()
        {
            this.DoubleBuffered = true;
            this.ResizeRedraw = true;
        }
    }

}
