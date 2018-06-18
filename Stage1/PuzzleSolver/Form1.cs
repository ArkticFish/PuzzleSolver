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

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        public Game game;
        
        private void normalGameToolStripMenuItem_Click(object sender, EventArgs e)
        {

            game = new Game(new SpaceState(
                "1223" +
                "1223" +
                "4556" +
                "4786" +
                "900A"),
                new SpaceState(
                "0000" +
                "0000" +
                "0000" +
                "0220" +
                "0220"),
                4,
                5);
            
            Refresh();

        }

        private void easyPeasyGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            game = new Game(new SpaceState(
                "1020" +
                "3425" +
                "6770" +
                "6088"),
                new SpaceState(
                "0000" +
                "0003" +
                "0000" +
                "0000"),
                4,
                4);
            Refresh();
        }

        private void game1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            game = new Game(new SpaceState(
                "1122" +
                "1134" +
                "0034" +
                "5667" +
                "599A"),
                new SpaceState(
                "0011" +
                "0011" +
                "0000" +
                "0000" +
                "0000"),
                4,
                5);
            Refresh();
        }

        private void game3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            game = new Game(new SpaceState(
                "102233" +
                "104405" +
                "667005" +
                "007889" +
                "AA7B09" +
                "CCCB00"),
                new SpaceState(
                "000000" +
                "000000" +
                "000066" +
                "000000" +
                "000000" +
                "000000"),
                6,
                6);
            Refresh();
        }

        private void customGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Not yet implemented.");
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

                        // Draw winning blocks.
                        if (game.winningState.blocks[y * game.w + x] != '0')
                        {
                            e.Graphics.DrawImage(Properties.Resources.done, block);
                            e.Graphics.DrawString(game.winningState.blocks[y * game.w + x].ToString(), new Font("Ariel", 10), new SolidBrush(Color.AliceBlue), block, tf);
                        }

                        // If the block is not empty, a block should be drawn.
                        if (game.currentState.blocks[y * game.w + x] != '0')
                        {
                            // Draw selected overlay first if block is selected.
                            if (game.currentState.blocks[y * game.w + x] == game.selectedBlock)
                                e.Graphics.DrawImage(Properties.Resources.Overlay, block);

                            // Draw standard block if block is not empty.
                            e.Graphics.DrawImage(Properties.Resources.Block, block);

                            // Draw bridge blocks.
                            // If left block is the same block, draw bridge block.
                            if (x > 0 && game.currentState.blocks[y * game.w + x] == game.currentState.blocks[y * game.w + (x - 1)])
                                e.Graphics.DrawImage(Properties.Resources.Left, block);

                            // If right block is the same block, draw bridge block.
                            if (x < game.w - 1 && game.currentState.blocks[y * game.w + x] == game.currentState.blocks[y * game.w + (x + 1)])
                                e.Graphics.DrawImage(Properties.Resources.Right, block);

                            // If top block is the same block, draw bridge block.
                            if (y > 0 && game.currentState.blocks[y * game.w + x] == game.currentState.blocks[(y - 1) * game.w + x])
                                e.Graphics.DrawImage(Properties.Resources.Top, block);

                            // If bottum block is the same block, draw bridge block.
                            if (y < game.h - 1 && game.currentState.blocks[y * game.w + x] == game.currentState.blocks[(y + 1) * game.w + x])
                                e.Graphics.DrawImage(Properties.Resources.Bottum, block);

                            // Draw the name of the block on the block.
                            e.Graphics.DrawString(game.currentState.blocks[y * game.w + x].ToString(), new Font("Ariel", 20), new SolidBrush(Color.AliceBlue), block, sf);

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
                            if (game.currentState.blocks[y * game.w + x] == '0')
                            {

                            }
                            else
                            {
                                game.selectedBlock = game.currentState.blocks[y * game.w + x];
                            }
                        }

                    }
                }
            }
            Refresh();
        }

        private void gamePanel_MouseMove(object sender, MouseEventArgs e)
        {

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
            for (int oi = 0; oi < game.currentState.blocks.Length; oi++)
            {
                if (game.currentState.blocks[oi] == game.selectedBlock)
                {
                    oldIndexes.Add(oi);
                }
            }

            bool valid = true;
            // Test old indexes.
            for (int oi = 0; oi < oldIndexes.Count; oi++)
            {
                if (direction == 0 && !(oldIndexes[oi] / game.w > 0))
                {
                    valid = false; break;
                }
                if (direction == 1 && !(oldIndexes[oi] / game.w < game.h - 1))
                {
                    valid = false; break;
                }
                if (direction == 2 && !(oldIndexes[oi] % game.w > 0))
                {
                    valid = false; break;
                }
                if (direction == 3 && !(oldIndexes[oi] % game.w < game.w - 1))
                {
                    valid = false; break;
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

            // Create new indexes.
            List<int> newIndexes = new List<int>();
            for (int oi = 0; oi < oldIndexes.Count; oi++)
            {
                newIndexes.Add(oldIndexes[oi] + diff);
            }

            // Test new  indexes.
            for (int ni = 0; valid && ni < newIndexes.Count; ni++)
            {
                if (!(game.currentState.blocks[newIndexes[ni]] == '0' || game.currentState.blocks[newIndexes[ni]] == game.selectedBlock))
                    valid = false;
            }

            // Apply move and add to list.
            if (valid)
            {
                string newBlocks = String.Copy(game.currentState.blocks);
                for (int oi = 0; oi < oldIndexes.Count; oi++)
                {
                    newBlocks = newBlocks.Remove(oldIndexes[oi], 1);
                    newBlocks = newBlocks.Insert(oldIndexes[oi], "0");
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

        public SpaceState currentState;
        public SpaceState winningState;

        public Stack<SpaceState> stateHistory = new Stack<SpaceState>();
        
        public Game(SpaceState state, SpaceState winstate, int w, int h)
        {
            this.w = w;
            this.h = h;
            currentState = state;
            winningState = winstate;
        }

        public void Undo()
        {
            if (stateHistory.Count > 0)
                currentState = stateHistory.Pop();
        }

        public void NewState(SpaceState state)
        {
            stateHistory.Push(currentState);
            currentState = state;
        }

        public bool IsWin()
        {
            for (int i = 0; i < winningState.blocks.Count(); i++)
                if (winningState.blocks[i] != '0' && winningState.blocks[i] != currentState.blocks[i])
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
