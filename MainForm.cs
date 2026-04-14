using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;

namespace AStar_vs_Best_First_Analysis {
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private async void btnRun_Click_1(object sender, EventArgs e) {
            btnRun.Enabled = false;
            txtLog.AppendText("Starting Analysis...\r\n");

            var config = new InputConfig {
                Sizes = new List<int> { 20, 30 },
                WallPercentages = new List<int> { 5, 10, 20, 25, 30 },
                Iterations = 50
            };

            var analyzer = new Analyzer(new LabyrinthGenerator(), new Validator());
            
            // Run on a background thread to keep UI responsive
            var results = await System.Threading.Tasks.Task.Run(() => analyzer.RunAnalysis(config));

            var comparator = new Comparator();
            string csv = comparator.GenerateCsv(results);

            string path = Path.Combine(Application.StartupPath, "Results.csv");
            File.WriteAllText(path, csv);

            txtLog.AppendText($"Analysis Complete. Saved to: {path}\r\n");
            btnRun.Enabled = true;
        }
    }
}