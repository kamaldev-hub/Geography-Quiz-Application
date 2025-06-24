namespace GeographyQuizApp.UI
{
    partial class frmHighScores
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.grpFilterScores = new System.Windows.Forms.GroupBox();
            this.rbMyScores = new System.Windows.Forms.RadioButton();
            this.rbAllPlayers = new System.Windows.Forms.RadioButton();
            this.dgvHighScores = new System.Windows.Forms.DataGridView();
            this.colPlayerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colScore = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colQuizType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTotalQuestions = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnClose = new System.Windows.Forms.Button();
            this.grpFilterScores.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHighScores)).BeginInit();
            this.SuspendLayout();
            //
            // grpFilterScores
            //
            this.grpFilterScores.AccessibleName = "Filter Scores";
            this.grpFilterScores.Controls.Add(this.rbMyScores);
            this.grpFilterScores.Controls.Add(this.rbAllPlayers);
            this.grpFilterScores.Location = new System.Drawing.Point(12, 12);
            this.grpFilterScores.Name = "grpFilterScores";
            this.grpFilterScores.Size = new System.Drawing.Size(460, 50);
            this.grpFilterScores.TabIndex = 0;
            this.grpFilterScores.TabStop = false;
            this.grpFilterScores.Text = "Filter Scores";
            //
            // rbMyScores
            //
            this.rbMyScores.AccessibleName = "Filter My Scores";
            this.rbMyScores.AutoSize = true;
            this.rbMyScores.Location = new System.Drawing.Point(120, 19);
            this.rbMyScores.Name = "rbMyScores";
            this.rbMyScores.Size = new System.Drawing.Size(76, 17);
            this.rbMyScores.TabIndex = 1;
            this.rbMyScores.Text = "My Scores";
            this.rbMyScores.UseVisualStyleBackColor = true;
            this.rbMyScores.CheckedChanged += new System.EventHandler(this.rbFilter_CheckedChanged);
            //
            // rbAllPlayers
            //
            this.rbAllPlayers.AccessibleName = "Filter All Players Scores";
            this.rbAllPlayers.AutoSize = true;
            this.rbAllPlayers.Checked = true;
            this.rbAllPlayers.Location = new System.Drawing.Point(15, 19);
            this.rbAllPlayers.Name = "rbAllPlayers";
            this.rbAllPlayers.Size = new System.Drawing.Size(73, 17);
            this.rbAllPlayers.TabIndex = 0;
            this.rbAllPlayers.TabStop = true;
            this.rbAllPlayers.Text = "All Players";
            this.rbAllPlayers.UseVisualStyleBackColor = true;
            this.rbAllPlayers.CheckedChanged += new System.EventHandler(this.rbFilter_CheckedChanged);
            //
            // dgvHighScores
            //
            this.dgvHighScores.AccessibleName = "High Scores Table";
            this.dgvHighScores.AllowUserToAddRows = false;
            this.dgvHighScores.AllowUserToDeleteRows = false;
            this.dgvHighScores.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHighScores.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colPlayerName,
            this.colScore,
            this.colDate,
            this.colQuizType,
            this.colTotalQuestions});
            this.dgvHighScores.Location = new System.Drawing.Point(12, 68);
            this.dgvHighScores.Name = "dgvHighScores";
            this.dgvHighScores.ReadOnly = true;
            this.dgvHighScores.Size = new System.Drawing.Size(460, 250);
            this.dgvHighScores.TabIndex = 1;
            this.dgvHighScores.AutoGenerateColumns = false; // Important for using defined columns
            //
            // colPlayerName
            //
            this.colPlayerName.DataPropertyName = "PlayerName"; // From DisplayHighScore class
            this.colPlayerName.HeaderText = "Player Name";
            this.colPlayerName.Name = "colPlayerName";
            this.colPlayerName.ReadOnly = true;
            this.colPlayerName.Width = 120;
            //
            // colScore
            //
            this.colScore.DataPropertyName = "Score";
            this.colScore.HeaderText = "Score";
            this.colScore.Name = "colScore";
            this.colScore.ReadOnly = true;
            this.colScore.Width = 60;
            //
            // colDate
            //
            this.colDate.DataPropertyName = "QuizTimestamp";
            this.colDate.HeaderText = "Date";
            this.colDate.Name = "colDate";
            this.colDate.ReadOnly = true;
            this.colDate.Width = 120;
            //
            // colQuizType
            //
            this.colQuizType.DataPropertyName = "QuizType";
            this.colQuizType.HeaderText = "Quiz Type";
            this.colQuizType.Name = "colQuizType";
            this.colQuizType.ReadOnly = true;
            this.colQuizType.Width = 120;
            //
            // colTotalQuestions
            //
            this.colTotalQuestions.DataPropertyName = "TotalQuestions";
            this.colTotalQuestions.HeaderText = "Questions";
            this.colTotalQuestions.Name = "colTotalQuestions";
            this.colTotalQuestions.ReadOnly = true;
            this.colTotalQuestions.Width = 70;
            //
            // btnClose
            //
            this.btnClose.AccessibleName = "Close High Scores Button";
            this.btnClose.Location = new System.Drawing.Point(200, 324);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(84, 23);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            //
            // frmHighScores
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 361);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.dgvHighScores);
            this.Controls.Add(this.grpFilterScores);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmHighScores";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "High Scores";
            this.Load += new System.EventHandler(this.frmHighScores_Load);
            this.grpFilterScores.ResumeLayout(false);
            this.grpFilterScores.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHighScores)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpFilterScores;
        private System.Windows.Forms.RadioButton rbMyScores;
        private System.Windows.Forms.RadioButton rbAllPlayers;
        private System.Windows.Forms.DataGridView dgvHighScores;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPlayerName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colScore;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colQuizType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTotalQuestions;
    }
}
