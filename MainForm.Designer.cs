namespace AStar_vs_Best_First_Analysis {
    partial class MainForm {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        private void InitializeComponent() {
            this.btnRun = new System.Windows.Forms.Button();
            this.picAStar = new System.Windows.Forms.PictureBox();
            this.picBFS = new System.Windows.Forms.PictureBox();
            this.lblAStar = new System.Windows.Forms.Label();
            this.lblBFS = new System.Windows.Forms.Label();
            
            this.lblIterationInput = new System.Windows.Forms.Label();
            this.nudIterations = new System.Windows.Forms.NumericUpDown();
            this.lblGridSize = new System.Windows.Forms.Label();
            this.lblLastResult = new System.Windows.Forms.Label();
            // New Control
            this.lblSolvedCount = new System.Windows.Forms.Label();
            
            ((System.ComponentModel.ISupportInitialize)(this.picAStar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBFS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudIterations)).BeginInit();
            this.SuspendLayout();
            
            // btnRun
            this.btnRun.BackColor = System.Drawing.Color.Gainsboro;
            this.btnRun.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRun.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnRun.Location = new System.Drawing.Point(640, 420);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(120, 40);
            this.btnRun.TabIndex = 0;
            this.btnRun.Text = "RUN VISUALIZER";
            this.btnRun.UseVisualStyleBackColor = false;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            
            // picAStar
            this.picAStar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.picAStar.Location = new System.Drawing.Point(20, 40);
            this.picAStar.Name = "picAStar";
            this.picAStar.Size = new System.Drawing.Size(360, 360);
            this.picAStar.TabIndex = 1;
            this.picAStar.TabStop = false;
            
            // picBFS
            this.picBFS.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.picBFS.Location = new System.Drawing.Point(400, 40);
            this.picBFS.Name = "picBFS";
            this.picBFS.Size = new System.Drawing.Size(360, 360);
            this.picBFS.TabIndex = 2;
            this.picBFS.TabStop = false;
            
            // lblAStar
            this.lblAStar.AutoSize = true;
            this.lblAStar.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblAStar.ForeColor = System.Drawing.Color.White;
            this.lblAStar.Location = new System.Drawing.Point(20, 10);
            this.lblAStar.Name = "lblAStar";
            this.lblAStar.Size = new System.Drawing.Size(135, 21);
            this.lblAStar.TabIndex = 3;
            this.lblAStar.Text = "A* Algorithm";
            
            // lblBFS
            this.lblBFS.AutoSize = true;
            this.lblBFS.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblBFS.ForeColor = System.Drawing.Color.White;
            this.lblBFS.Location = new System.Drawing.Point(400, 10);
            this.lblBFS.Name = "lblBFS";
            this.lblBFS.Size = new System.Drawing.Size(155, 21);
            this.lblBFS.TabIndex = 4;
            this.lblBFS.Text = "Best-First Search";

            // lblIterationInput
            this.lblIterationInput.AutoSize = true;
            this.lblIterationInput.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblIterationInput.ForeColor = System.Drawing.Color.White;
            this.lblIterationInput.Location = new System.Drawing.Point(20, 430);
            this.lblIterationInput.Name = "lblIterationInput";
            this.lblIterationInput.Size = new System.Drawing.Size(132, 19);
            this.lblIterationInput.TabIndex = 5;
            this.lblIterationInput.Text = "Labyrinths per size:";

            // nudIterations
            this.nudIterations.Location = new System.Drawing.Point(160, 430);
            this.nudIterations.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            this.nudIterations.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.nudIterations.Name = "nudIterations";
            this.nudIterations.Size = new System.Drawing.Size(80, 20);
            this.nudIterations.TabIndex = 6;
            this.nudIterations.Value = new decimal(new int[] { 50, 0, 0, 0 });

            // lblGridSize
            this.lblGridSize.AutoSize = true;
            this.lblGridSize.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblGridSize.ForeColor = System.Drawing.Color.LightSkyBlue;
            this.lblGridSize.Location = new System.Drawing.Point(20, 470);
            this.lblGridSize.Name = "lblGridSize";
            this.lblGridSize.Size = new System.Drawing.Size(138, 19);
            this.lblGridSize.TabIndex = 7;
            this.lblGridSize.Text = "Grid Size: Waiting...";

            // lblLastResult
            this.lblLastResult.AutoSize = true;
            this.lblLastResult.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblLastResult.ForeColor = System.Drawing.Color.LightGray;
            this.lblLastResult.Location = new System.Drawing.Point(20, 495);
            this.lblLastResult.Name = "lblLastResult";
            this.lblLastResult.Size = new System.Drawing.Size(141, 19);
            this.lblLastResult.TabIndex = 8;
            this.lblLastResult.Text = "Last Result: Waiting...";

            // lblSolvedCount
            this.lblSolvedCount.AutoSize = true;
            this.lblSolvedCount.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblSolvedCount.ForeColor = System.Drawing.Color.PaleGreen;
            this.lblSolvedCount.Location = new System.Drawing.Point(20, 520);
            this.lblSolvedCount.Name = "lblSolvedCount";
            this.lblSolvedCount.Size = new System.Drawing.Size(145, 19);
            this.lblSolvedCount.TabIndex = 9;
            this.lblSolvedCount.Text = "Labyrinths Solved: 0";

            // MainForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.ClientSize = new System.Drawing.Size(780, 560); // Increased height to fit new label
            this.Controls.Add(this.lblSolvedCount);
            this.Controls.Add(this.lblLastResult);
            this.Controls.Add(this.lblGridSize);
            this.Controls.Add(this.nudIterations);
            this.Controls.Add(this.lblIterationInput);
            this.Controls.Add(this.lblBFS);
            this.Controls.Add(this.lblAStar);
            this.Controls.Add(this.picBFS);
            this.Controls.Add(this.picAStar);
            this.Controls.Add(this.btnRun);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "A* vs Best-First Visualizer";
            ((System.ComponentModel.ISupportInitialize)(this.picAStar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBFS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudIterations)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion

        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.PictureBox picAStar;
        private System.Windows.Forms.PictureBox picBFS;
        private System.Windows.Forms.Label lblAStar;
        private System.Windows.Forms.Label lblBFS;
        
        private System.Windows.Forms.Label lblIterationInput;
        private System.Windows.Forms.NumericUpDown nudIterations;
        private System.Windows.Forms.Label lblGridSize;
        private System.Windows.Forms.Label lblLastResult;
        // New declaration
        private System.Windows.Forms.Label lblSolvedCount;
    }
}