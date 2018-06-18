using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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

        Game game;
        
        // Blocks used for simplification.
        private List<Block> blocks = new List<Block>();

        public int count = 0;

        // Overloaded constructor.
        public PuzzleSolver(Game game)
        {
            // Test if a game is playing.
            if (game == null)
                return;

            this.game = game;

            // Analyze the blocks present in the puzzle.
            AnalyzeBlocks();
        }

        // Method to populate the blocks that will be used for simplification.
        private void AnalyzeBlocks()
        {

            int charCount = 0;
            for (int i = 0; i < game.state.blocks.Length; i++)
            {
                if (game.state.blocks[i] != '0')
                {

                    string name = game.state.blocks[i].ToString();
                    string type = ((char)(97 + charCount++)).ToString();

                    bool nn = true;

                    List<int> shape = new List<int>();
                    for (int p = i; p < game.state.blocks.Length; p++)
                    {
                        if (game.state.blocks[p] == game.state.blocks[i])
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
        // List to hold all the nodes yet to be explored.
        private List<string> lookup = new List<string>();
        
        // Method for searching breadth fisrt for solution.
        public SpaceState Solve()
        {
            // Reset count.
            count = 0;
            
            // Add first state in open list.
            open.Add(game.state);

            // Adding fist state in lookup list.
            lookup.Add(Simplify(ref game.state.blocks));

            List<char> moveBlocks = new List<char>();
            moveBlocks.AddRange(game.state.blocks.Distinct());
            moveBlocks.Remove(' ');

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

                        // Set diff for movement.
                        if (iteration == 0)
                            diff = -game.w;
                        else if (iteration == 1)
                            diff = +game.w;
                        else if (iteration == 2)
                            diff = -1;
                        else if (iteration == 3)
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
                            else if (game.stage.blocks[newIndexes[ni]] == '-' && moveBlocks[i] != game.goalBlock)
                            {
                                valid = false;
                            }
                        }

                        // Test new indexes.
                        for (int ni = 0; valid && ni < newIndexes.Count; ni++)
                        {
                            char mb = moveBlocks[i];
                            char tb = test.blocks[newIndexes[ni]];

                            if (!(test.blocks[newIndexes[ni]] == ' ' || test.blocks[newIndexes[ni]] == moveBlocks[i]))
                                valid = false;
                        }

                        // Apply move and add to list.
                        if (valid)
                        {
                            string newBlocks = String.Copy(test.blocks);
                            for (int oi = 0; oi < oldIndexes.Count; oi++)
                            {
                                newBlocks = newBlocks.Remove(oldIndexes[oi], 1);
                                newBlocks = newBlocks.Insert(oldIndexes[oi], " ");
                            }
                            for (int ni = 0; ni < newIndexes.Count; ni++)
                            {
                                newBlocks = newBlocks.Remove(newIndexes[ni], 1);
                                newBlocks = newBlocks.Insert(newIndexes[ni], moveBlocks[i].ToString());
                            }

                            SpaceState sp = new SpaceState(newBlocks, test, moveBlocks[i].ToString(), iteration.ToString());

                            if (!(lookup.Contains(Simplify(ref newBlocks))))
                            {
                                open.Add(sp);
                                lookup.Add(Simplify(ref newBlocks));
                            }

                        }

                        #endregion
                    }

                }

            }

            return game.state;

        }

        public string Simplify(ref string input)
        {
            string output = String.Copy(input);
            foreach (Block b in blocks)
            {
                if (b.name[0] != game.goalBlock && b.name[0] != ' ')
                    output = output.Replace(b.name, b.type);
            }
            return output;
        }

        public bool IsWin(SpaceState state)
        {
            for (int i = 0; i < game.stage.blocks.Count(); i++)
                if ((game.stage.blocks[i] == '0'|| game.stage.blocks[i] == '-') && state.blocks[i] == game.goalBlock)
                    return false;

            return true;
        }

    }

}
