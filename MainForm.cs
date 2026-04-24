using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AStar_vs_Best_First_Analysis {
    public partial class MainForm : Form {
        private Bitmap bmpAStar;
        private Bitmap bmpBFS;
        private Graphics gA;
        private Graphics gB;
        private int gridSize = 30;
        private int cellSize;
        private bool isRunning = false;
        private int totalSolved = 0;

        public MainForm() {
            InitializeComponent();
            
            try {
                if (File.Exists("app_icon.ico")) {
                    this.Icon = new Icon("app_icon.ico");
                }
            } catch (Exception) {
                // Fallback or silent fail if icon is missing
            }
            
            picAStar.SizeMode = PictureBoxSizeMode.Zoom;
            picBFS.SizeMode = PictureBoxSizeMode.Zoom;
        }

        // Extracted reusable method to save the results in a CSV
        private async Task SaveResultsAsync(int userIterations) {
            try {
                await Task.Run(() => {
                    var config = new InputConfig {
                        Sizes = new List<int> { 20, 30 },
                        WallPercentages = new List<int> { 5, 10, 20, 25, 30 },
                        Iterations = userIterations
                    };
                    
                    var analyzer = new Analyzer(new LabyrinthGenerator(), new Validator());
                    var analysisResults = analyzer.RunAnalysis(config);
                    
                    var csvOutput = new Comparator().GenerateCsv(analysisResults);
                    File.WriteAllText("results.csv", csvOutput);
                });

                lblLastResult.Text = $"Last Result: Batch CSV saved ({userIterations} per size).";
                DarkMessageBox.Show("Successfully saved batch analysis to results.csv!", "Results Saved");
            } catch (Exception ex) {
                lblLastResult.Text = "Last Result: Error generating batch.";
                DarkMessageBox.Show("Error saving results: " + ex.Message, "Error");
            }
        }

        private async void btnRun_Click(object sender, EventArgs e) {
            int userIterations = (int)nudIterations.Value;

            if (isRunning) {
                isRunning = false;
                btnRun.Text = "RUN VISUALIZER";
                btnRun.Enabled = false; 
                
                await SaveResultsAsync(userIterations);
                
                btnRun.Enabled = true;
                return;
            }

            // Start logic
            isRunning = true;
            btnRun.Text = "STOP";
            
            totalSolved = 0;
            btnRun.Refresh(); 
            
            int maxIterations = (int)nudIterations.Value;
            
            var sizesToRun = new List<int> { 20, 30 };
            var wallPercents = new List<int> { 5, 10, 20, 25, 30 };

            foreach (int size in sizesToRun) {
                gridSize = size;
                foreach (int wp in wallPercents) {
                    for (int i = 1; i <= maxIterations && isRunning; i++) {
                        await RunSingleIteration(i, maxIterations, wp);
                        await Task.Delay(1); 
                    }
                    if (!isRunning) break;
                }
                if (!isRunning) break;
            }

            // If it finishes on its own (loop completes without pressing STOP)
            if (isRunning) {
                isRunning = false;
                btnRun.Text = "RUN VISUALIZER";
                btnRun.Enabled = false;
                lblLastResult.Text = "Last Result: Visualizer sequence completed. Saving...";
                
                await SaveResultsAsync(userIterations);
                
                btnRun.Enabled = true;
            }
        }

        private async Task RunSingleIteration(int currentIter, int maxIter, int wallPercent) {
            lblGridSize.Text = $"Grid Size: {gridSize}x{gridSize} | Walls: {wallPercent}%";

            var gen = new LabyrinthGenerator();
            var grid = gen.Generate(gridSize, wallPercent, out bool forced); 
            
            // depending on gridSize, and SizeMode.Zoom will scale it smoothly to fit the UI.
            cellSize = 20; 
            int imgSize = gridSize * cellSize;
            
            bmpAStar = new Bitmap(imgSize, imgSize);
            bmpBFS = new Bitmap(imgSize, imgSize);
            gA = Graphics.FromImage(bmpAStar);
            gB = Graphics.FromImage(bmpBFS);

            gA.Clear(Color.FromArgb(40, 40, 40));
            gB.Clear(Color.FromArgb(40, 40, 40));
            
            Pen gridPen = new Pen(Color.FromArgb(60, 60, 60));

            for (int x = 0; x < gridSize; x++) {
                for (int y = 0; y < gridSize; y++) {
                    Rectangle rect = new Rectangle(x * cellSize, y * cellSize, cellSize, cellSize);
                    if (grid.IsWall[x, y]) {
                        gA.FillRectangle(Brushes.Black, rect);
                        gB.FillRectangle(Brushes.Black, rect);
                    } else {
                        gA.DrawRectangle(gridPen, rect);
                        gB.DrawRectangle(gridPen, rect);
                    }
                }
            }

            Brush startBrush = Brushes.MediumPurple;
            Brush endBrush = Brushes.Orange;
            gA.FillRectangle(startBrush, 0, 0, cellSize, cellSize);
            gA.FillRectangle(endBrush, (gridSize - 1) * cellSize, (gridSize - 1) * cellSize, cellSize, cellSize);
            gB.FillRectangle(startBrush, 0, 0, cellSize, cellSize);
            gB.FillRectangle(endBrush, (gridSize - 1) * cellSize, (gridSize - 1) * cellSize, cellSize, cellSize);

            picAStar.Image = bmpAStar;
            picBFS.Image = bmpBFS;

            lblAStar.Text = "A*: Searching...";
            lblBFS.Text = "Best-First: Searching...";

            var astar = new AStar();
            var bfs = new BestFirstSearch();

            var taskA = Task.Run(() => astar.Search(grid, (x, y, s) => UpdateCell(picAStar, gA, x, y, s)));
            var taskB = Task.Run(() => bfs.Search(grid, (x, y, s) => UpdateCell(picBFS, gB, x, y, s)));

            await Task.WhenAll(taskA, taskB);

            bool isSolved = taskA.Result.PathFound;
            
            string resA = isSolved ? "Solved" : "Blocked";
            string resB = taskB.Result.PathFound ? "Solved" : "Blocked";
            
            lblAStar.Text = $"A*: {resA} ({taskA.Result.VisitedCells} nodes)";
            lblBFS.Text = $"Best-First: {resB} ({taskB.Result.VisitedCells} nodes)";

            if (isRunning) {
                if (isSolved) {
                    totalSolved++; 
                }
                
                lblLastResult.Text = $"Last Result: A* {resA} | Best-First {resB}";
                lblSolvedCount.Text = $"Solved: {totalSolved} (Iter: {currentIter}/{maxIter})";
            }
        }

        private void UpdateCell(PictureBox pic, Graphics g, int x, int y, int state) {
            if (!isRunning || pic.IsDisposed) return;

            // Keep the Start node purple and End node orange until the final path overrides it
            if (x == 0 && y == 0) return; 
            if (x == gridSize - 1 && y == gridSize - 1 && state != 2) return; 

            try {
                pic.Invoke(new Action(() => {
                    Brush b = Brushes.YellowGreen; 
                    if (state == 1) b = Brushes.LightCoral; 
                    if (state == 2) b = Brushes.DodgerBlue; 

                    g.FillRectangle(b, x * cellSize + 1, y * cellSize + 1, cellSize - 2, cellSize - 2);
                    pic.Invalidate();
                }));
            } catch { /* Handle closure mid-task */ }
        }
    }

    /// <summary>
    /// Custom message box to match the dark theme and font styles of the main application.
    /// </summary>
    public static class DarkMessageBox {
        public static void Show(string message, string title) {
            using (Form form = new Form()) {
                form.Text = title;
                form.BackColor = Color.FromArgb(25, 25, 25);
                form.ForeColor = Color.White;
                form.FormBorderStyle = FormBorderStyle.FixedDialog;
                form.StartPosition = FormStartPosition.CenterParent;
                form.MaximizeBox = false;
                form.MinimizeBox = false;
                form.ShowIcon = false;
                form.ClientSize = new Size(350, 150);

                Label lblMessage = new Label() {
                    Text = message,
                    AutoSize = false,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Dock = DockStyle.Fill,
                    Font = new Font("Segoe UI", 10F)
                };
                
                Button btnOk = new Button() {
                    Text = "OK",
                    DialogResult = DialogResult.OK,
                    BackColor = Color.Gainsboro,
                    ForeColor = Color.Black,
                    FlatStyle = FlatStyle.Flat,
                    Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                    Size = new Size(100, 35)
                };
                btnOk.FlatAppearance.BorderSize = 0;

                Panel panelBottom = new Panel() {
                    Dock = DockStyle.Bottom,
                    Height = 60
                };
                
                // Center the button dynamically
                btnOk.Left = (form.ClientSize.Width - btnOk.Width) / 2;
                btnOk.Top = (panelBottom.Height - btnOk.Height) / 2;
                panelBottom.Controls.Add(btnOk);

                form.Controls.Add(lblMessage);
                form.Controls.Add(panelBottom);
                
                form.ShowDialog();
            }
        }
    }
}