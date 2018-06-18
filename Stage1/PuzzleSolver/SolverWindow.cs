using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PuzzleSolver
{

    public partial class SolverWindow : Form
    {
        public SolverWindow()
        {
            InitializeComponent();
        }
        
        PuzzleSolver ps;
        SpaceState returnState;

        private bool isRunning = false;

        private string message = "Click Solve to run the algorithm.";

        public SpaceState SolveGame(Game game)
        {
            ps = new PuzzleSolver(game);
            ShowDialog();
            return returnState;
        }
        
        private void btnRun_Click(object sender, EventArgs e)
        {
            if (isRunning)
            {
                MessageBox.Show("Solver already running.");
            }
            else
            {
                Thread solveThread = new Thread(SolveMethod);
                solveThread.Start();
            }
        }

        private void SolveMethod()
        {
            isRunning = true;
            returnState = ps.Solve();
            isRunning = false;

            Thread.Sleep(150);

            message = "Solution found\nStates Checked: " + ps.count;
        }

        private void tmrUpdateStatus_Tick(object sender, EventArgs e)
        {
            if (isRunning)
                lblStatus.Text = ps.count + " states checked.";
            else
                lblStatus.Text = message;
        }

    }

    public class PuzzleSolver
    {

        // Width and height of the puzzle.
        public int w;
        public int h;

        // Starting state of the puzzle.
        SpaceState startState;
        // Starting state of the puzzle.
        SpaceState winningState;

        // Blocks used for simplification.
        private List<Block> blocks = new List<Block>();

        // Count the amount a nodes tested.
        public int count = 0;

        // Overloaded constructor.
        public PuzzleSolver(Game game)
        {
            // Test if a game is playing.
            if (game == null)
                return;

            // Set width and height of the puzzle.
            w = game.w;
            h = game.h;

            // Set the starting state of the puzzle.
            startState = game.currentState;
            winningState = game.winningState;

            // Analyze the blocks present in the puzzle.
            AnalyzeBlocks();
        }

        // Method to populate the blocks that will be used for simplification.
        private void AnalyzeBlocks()
        {

            int charCount = 0;
            for (int i = 0; i < startState.blocks.Length; i++)
            {
                if (startState.blocks[i] != '0')
                {

                    string name = startState.blocks[i].ToString();
                    string type = ((char)(97 + charCount++)).ToString();

                    bool nn = true;

                    List<int> shape = new List<int>();
                    for (int p = i; p < startState.blocks.Length; p++)
                    {
                        if (startState.blocks[p] == startState.blocks[i])
                            shape.Add(p - i);
                    }

                    for (int o = 0; o < blocks.Count; o++)
                    {
                        if (blocks[o].name == name)
                        {
                            nn = false;
                            charCount--;
                            break;
                        }
                        if (shape.SequenceEqual(blocks[o].shape))
                        {
                            type = blocks[o].type;
                            charCount--;
                            break;
                        }
                    }

                    if (nn)
                        blocks.Add(new Block(name, type, shape));

                }

            }

        }

        // List to hold all the nodes yet to be explored.
        private List<SpaceState> open = new List<SpaceState>();

        // Lookup lists. Used to search visited states more quickly.
        private List<List<string>> lookup = new List<List<string>>();

        // Method for searching breadth fisrt for solution.
        public SpaceState Solve()
        {
            // Reset count.
            count = 0;

            // Populate lookup lists.
            for (int i = 0; i < 1048575; i++)
            {
                lookup.Add(new List<string>());
            }

            // Add first state in open list.
            open.Add(startState);

            // Index of lookup list to store string.
            int aIndex = int.Parse(Simplify(ref startState.blocks).Substring(0, 5), System.Globalization.NumberStyles.HexNumber);
            // Adding fist state in lookup list.
            lookup[aIndex].Add(Simplify(ref startState.blocks).Substring(5));

            // While loop to search for solution.
            while (open.Count > 0)
            {
                // Test first element on open stack.
                SpaceState test = open.First();
                // Remove element from stack.
                open.RemoveAt(0);

                // Test for winning condition.
                if (IsWin(test))
                {
                    return test;
                }

                // Increment count.
                count++;

                List<int> zeroBlocks = new List<int>();
                List<char> moveBlocks = new List<char>();

                for (int i = 0; i < test.blocks.Length; i++)
                {
                    if (test.blocks[i] == '0')
                    {
                        zeroBlocks.Add(i);
                        if (i / w > 0 && !moveBlocks.Contains(test.blocks[i - w]))
                            moveBlocks.Add(test.blocks[i - w]);
                        if (i / w < h - 1 && !moveBlocks.Contains(test.blocks[i + w]))
                            moveBlocks.Add(test.blocks[i + w]);
                        if (i % w > 0 && !moveBlocks.Contains(test.blocks[i - 1]))
                            moveBlocks.Add(test.blocks[i - 1]);
                        if (i % w < w - 1 && !moveBlocks.Contains(test.blocks[i + 1]))
                            moveBlocks.Add(test.blocks[i + 1]);
                    }
                }

                moveBlocks.Remove('0');

                for (int i = 0; i < moveBlocks.Count; i++)
                {
                    List<int> oldIndexes = new List<int>();
                    for (int oi = 0; oi < test.blocks.Length; oi++)
                    {
                        if (test.blocks[oi] == moveBlocks[i])
                        {
                            oldIndexes.Add(oi);
                        }
                    }

                    int diff = 0;
                    for (int iteration = 0; iteration < 4; iteration++)
                    {
                        #region Move

                        bool valid = true;
                        // Test old indexes.
                        for (int oi = 0; oi < oldIndexes.Count; oi++)
                        {
                            if (iteration == 0 && !(oldIndexes[oi] / w > 0))
                            {
                                valid = false; break;
                            }
                            if (iteration == 1 && !(oldIndexes[oi] / w < h - 1))
                            {
                                valid = false; break;
                            }
                            if (iteration == 2 && !(oldIndexes[oi] % w > 0))
                            {
                                valid = false; break;
                            }
                            if (iteration == 3 && !(oldIndexes[oi] % w < w - 1))
                            {
                                valid = false; break;
                            }
                        }

                        // Set diff for movement.
                        if (iteration == 0)
                            diff = -w;
                        else if (iteration == 1)
                            diff = +w;
                        else if (iteration == 2)
                            diff = -1;
                        else if (iteration == 3)
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
                            if (!(test.blocks[newIndexes[ni]] == '0' || test.blocks[newIndexes[ni]] == moveBlocks[i]))
                                valid = false;
                        }

                        // Apply move and add to list.
                        if (valid)
                        {
                            string newBlocks = String.Copy(test.blocks);
                            for (int oi = 0; oi < oldIndexes.Count; oi++)
                            {
                                newBlocks = newBlocks.Remove(oldIndexes[oi], 1);
                                newBlocks = newBlocks.Insert(oldIndexes[oi], "0");
                            }
                            for (int ni = 0; ni < newIndexes.Count; ni++)
                            {
                                newBlocks = newBlocks.Remove(newIndexes[ni], 1);
                                newBlocks = newBlocks.Insert(newIndexes[ni], moveBlocks[i].ToString());
                            }

                            int cIndex2 = int.Parse(Simplify(ref newBlocks).Substring(0, 5), System.Globalization.NumberStyles.HexNumber);
                            SpaceState sp = new SpaceState(newBlocks, test, moveBlocks[i].ToString(), iteration.ToString());

                            if (!(lookup[cIndex2].Contains(Simplify(ref newBlocks).Substring(5))))
                            {
                                open.Add(sp);
                                lookup[cIndex2].Add(Simplify(ref newBlocks).Substring(5));
                            }

                        }

                        #endregion
                    }

                }

            }

            return startState;

        }

        public string Simplify(ref string input)
        {
            char[] answerblocks = winningState.blocks.Distinct().ToArray();
            string output = String.Copy(input);
            foreach (Block b in blocks)
            {
                if (!answerblocks.Contains(b.name[0]))
                    output = output.Replace(b.name, b.type);
            }
            return output;
        }

        public bool IsWin(SpaceState currentState)
        {
            for (int i = 0; i < winningState.blocks.Count(); i++)
                if (winningState.blocks[i] != '0' && winningState.blocks[i] != currentState.blocks[i])
                    return false;

            return true;
        }

    }

}
