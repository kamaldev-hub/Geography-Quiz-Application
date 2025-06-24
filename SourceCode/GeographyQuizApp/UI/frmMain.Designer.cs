namespace GeographyQuizApp.UI
{
    partial class frmMain
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
            this.lblPlayerNamePrompt = new System.Windows.Forms.Label();
            this.txtPlayerName = new System.Windows.Forms.TextBox();
            this.grpQuizMode = new System.Windows.Forms.GroupBox();
            this.rbFlagFromCapital = new System.Windows.Forms.RadioButton();
            this.rbCapitalFromFlag = new System.Windows.Forms.RadioButton();
            this.rbCountryFromCapital = new System.Windows.Forms.RadioButton();
            this.rbFlagFromCountry = new System.Windows.Forms.RadioButton();
            this.rbCapitalFromCountry = new System.Windows.Forms.RadioButton();
            this.rbCountryFromFlag = new System.Windows.Forms.RadioButton();
            this.lblContinentFilterPrompt = new System.Windows.Forms.Label();
            this.cmbContinent = new System.Windows.Forms.ComboBox();
            this.lblNumberOfQuestionsPrompt = new System.Windows.Forms.Label();
            this.numNumberOfQuestions = new System.Windows.Forms.NumericUpDown();
            this.btnStartQuiz = new System.Windows.Forms.Button();
            this.btnHighScores = new System.Windows.Forms.Button();
            this.btnStartFromConfig = new System.Windows.Forms.Button();
            this.grpQuizMode.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numNumberOfQuestions)).BeginInit();
            this.SuspendLayout();
            //
            // lblPlayerNamePrompt
            //
            this.lblPlayerNamePrompt.AutoSize = true;
            this.lblPlayerNamePrompt.Location = new System.Drawing.Point(12, 15);
            this.lblPlayerNamePrompt.Name = "lblPlayerNamePrompt";
            this.lblPlayerNamePrompt.Size = new System.Drawing.Size(93, 13);
            this.lblPlayerNamePrompt.TabIndex = 0;
            this.lblPlayerNamePrompt.Text = "Enter Your Name:";
            //
            // txtPlayerName
            //
            this.txtPlayerName.AccessibleName = "Player Name Input";
            this.txtPlayerName.Location = new System.Drawing.Point(111, 12);
            this.txtPlayerName.Name = "txtPlayerName";
            this.txtPlayerName.Size = new System.Drawing.Size(261, 20);
            this.txtPlayerName.TabIndex = 1;
            //
            // grpQuizMode
            //
            this.grpQuizMode.Controls.Add(this.rbFlagFromCapital);
            this.grpQuizMode.Controls.Add(this.rbCapitalFromFlag);
            this.grpQuizMode.Controls.Add(this.rbCountryFromCapital);
            this.grpQuizMode.Controls.Add(this.rbFlagFromCountry);
            this.grpQuizMode.Controls.Add(this.rbCapitalFromCountry);
            this.grpQuizMode.Controls.Add(this.rbCountryFromFlag);
            this.grpQuizMode.Location = new System.Drawing.Point(15, 45);
            this.grpQuizMode.Name = "grpQuizMode";
            this.grpQuizMode.Size = new System.Drawing.Size(357, 100);
            this.grpQuizMode.TabIndex = 2;
            this.grpQuizMode.TabStop = false;
            this.grpQuizMode.Text = "Select Quiz Mode";
            //
            // rbFlagFromCapital
            //
            this.rbFlagFromCapital.AutoSize = true;
            this.rbFlagFromCapital.Location = new System.Drawing.Point(180, 65);
            this.rbFlagFromCapital.Name = "rbFlagFromCapital";
            this.rbFlagFromCapital.Size = new System.Drawing.Size(105, 17);
            this.rbFlagFromCapital.TabIndex = 5;
            this.rbFlagFromCapital.Text = "Flag from Capital";
            this.rbFlagFromCapital.UseVisualStyleBackColor = true;
            //
            // rbCapitalFromFlag
            //
            this.rbCapitalFromFlag.AutoSize = true;
            this.rbCapitalFromFlag.Location = new System.Drawing.Point(180, 42);
            this.rbCapitalFromFlag.Name = "rbCapitalFromFlag";
            this.rbCapitalFromFlag.Size = new System.Drawing.Size(105, 17);
            this.rbCapitalFromFlag.TabIndex = 4;
            this.rbCapitalFromFlag.Text = "Capital from Flag";
            this.rbCapitalFromFlag.UseVisualStyleBackColor = true;
            //
            // rbCountryFromCapital
            //
            this.rbCountryFromCapital.AutoSize = true;
            this.rbCountryFromCapital.Location = new System.Drawing.Point(180, 19);
            this.rbCountryFromCapital.Name = "rbCountryFromCapital";
            this.rbCountryFromCapital.Size = new System.Drawing.Size(119, 17);
            this.rbCountryFromCapital.TabIndex = 3;
            this.rbCountryFromCapital.Text = "Country from Capital";
            this.rbCountryFromCapital.UseVisualStyleBackColor = true;
            //
            // rbFlagFromCountry
            //
            this.rbFlagFromCountry.AutoSize = true;
            this.rbFlagFromCountry.Location = new System.Drawing.Point(6, 65);
            this.rbFlagFromCountry.Name = "rbFlagFromCountry";
            this.rbFlagFromCountry.Size = new System.Drawing.Size(108, 17);
            this.rbFlagFromCountry.TabIndex = 2;
            this.rbFlagFromCountry.Text = "Flag from Country";
            this.rbFlagFromCountry.UseVisualStyleBackColor = true;
            //
            // rbCapitalFromCountry
            //
            this.rbCapitalFromCountry.AutoSize = true;
            this.rbCapitalFromCountry.Location = new System.Drawing.Point(6, 42);
            this.rbCapitalFromCountry.Name = "rbCapitalFromCountry";
            this.rbCapitalFromCountry.Size = new System.Drawing.Size(120, 17);
            this.rbCapitalFromCountry.TabIndex = 1;
            this.rbCapitalFromCountry.Text = "Capital from Country";
            this.rbCapitalFromCountry.UseVisualStyleBackColor = true;
            //
            // rbCountryFromFlag
            //
            this.rbCountryFromFlag.AutoSize = true;
            this.rbCountryFromFlag.Checked = true;
            this.rbCountryFromFlag.Location = new System.Drawing.Point(6, 19);
            this.rbCountryFromFlag.Name = "rbCountryFromFlag";
            this.rbCountryFromFlag.Size = new System.Drawing.Size(108, 17);
            this.rbCountryFromFlag.TabIndex = 0;
            this.rbCountryFromFlag.TabStop = true;
            this.rbCountryFromFlag.Text = "Country from Flag";
            this.rbCountryFromFlag.UseVisualStyleBackColor = true;
            //
            // lblContinentFilterPrompt
            //
            this.lblContinentFilterPrompt.AutoSize = true;
            this.lblContinentFilterPrompt.Location = new System.Drawing.Point(12, 154);
            this.lblContinentFilterPrompt.Name = "lblContinentFilterPrompt";
            this.lblContinentFilterPrompt.Size = new System.Drawing.Size(145, 13);
            this.lblContinentFilterPrompt.TabIndex = 3;
            this.lblContinentFilterPrompt.Text = "Filter by Continent (Optional):";
            //
            // cmbContinent
            //
            this.cmbContinent.AccessibleName = "Continent Filter";
            this.cmbContinent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbContinent.FormattingEnabled = true;
            this.cmbContinent.Items.AddRange(new object[] {
            "Worldwide",
            "Africa",
            "Asia",
            "Europe",
            "North America",
            "Oceania",
            "South America"});
            this.cmbContinent.Location = new System.Drawing.Point(163, 151);
            this.cmbContinent.Name = "cmbContinent";
            this.cmbContinent.Size = new System.Drawing.Size(209, 21);
            this.cmbContinent.TabIndex = 4;
            //
            // lblNumberOfQuestionsPrompt
            //
            this.lblNumberOfQuestionsPrompt.AutoSize = true;
            this.lblNumberOfQuestionsPrompt.Location = new System.Drawing.Point(12, 181);
            this.lblNumberOfQuestionsPrompt.Name = "lblNumberOfQuestionsPrompt";
            this.lblNumberOfQuestionsPrompt.Size = new System.Drawing.Size(112, 13);
            this.lblNumberOfQuestionsPrompt.TabIndex = 5;
            this.lblNumberOfQuestionsPrompt.Text = "Number of Questions:";
            //
            // numNumberOfQuestions
            //
            this.numNumberOfQuestions.AccessibleName = "Number of Questions";
            this.numNumberOfQuestions.Location = new System.Drawing.Point(163, 179);
            this.numNumberOfQuestions.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numNumberOfQuestions.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numNumberOfQuestions.Name = "numNumberOfQuestions";
            this.numNumberOfQuestions.Size = new System.Drawing.Size(75, 20);
            this.numNumberOfQuestions.TabIndex = 6;
            this.numNumberOfQuestions.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            //
            // btnStartQuiz
            //
            this.btnStartQuiz.AccessibleName = "Start Quiz Button";
            this.btnStartQuiz.Location = new System.Drawing.Point(15, 215);
            this.btnStartQuiz.Name = "btnStartQuiz";
            this.btnStartQuiz.Size = new System.Drawing.Size(110, 30);
            this.btnStartQuiz.TabIndex = 7;
            this.btnStartQuiz.Text = "Start Quiz";
            this.btnStartQuiz.UseVisualStyleBackColor = true;
            this.btnStartQuiz.Click += new System.EventHandler(this.btnStartQuiz_Click);
            //
            // btnHighScores
            //
            this.btnHighScores.AccessibleName = "View High Scores Button";
            this.btnHighScores.Location = new System.Drawing.Point(136, 215);
            this.btnHighScores.Name = "btnHighScores";
            this.btnHighScores.Size = new System.Drawing.Size(120, 30);
            this.btnHighScores.TabIndex = 8;
            this.btnHighScores.Text = "View High Scores";
            this.btnHighScores.UseVisualStyleBackColor = true;
            this.btnHighScores.Click += new System.EventHandler(this.btnHighScores_Click);
            //
            // btnStartFromConfig
            //
            this.btnStartFromConfig.AccessibleName = "Start from Config File Button";
            this.btnStartFromConfig.Location = new System.Drawing.Point(262, 215);
            this.btnStartFromConfig.Name = "btnStartFromConfig";
            this.btnStartFromConfig.Size = new System.Drawing.Size(110, 30);
            this.btnStartFromConfig.TabIndex = 9;
            this.btnStartFromConfig.Text = "Start from Config";
            this.btnStartFromConfig.UseVisualStyleBackColor = true;
            this.btnStartFromConfig.Click += new System.EventHandler(this.btnStartFromConfig_Click);
            //
            // frmMain
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 261);
            this.Controls.Add(this.btnStartFromConfig);
            this.Controls.Add(this.btnHighScores);
            this.Controls.Add(this.btnStartQuiz);
            this.Controls.Add(this.numNumberOfQuestions);
            this.Controls.Add(this.lblNumberOfQuestionsPrompt);
            this.Controls.Add(this.cmbContinent);
            this.Controls.Add(this.lblContinentFilterPrompt);
            this.Controls.Add(this.grpQuizMode);
            this.Controls.Add(this.txtPlayerName);
            this.Controls.Add(this.lblPlayerNamePrompt);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Geography Quiz - Main Menu";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.grpQuizMode.ResumeLayout(false);
            this.grpQuizMode.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numNumberOfQuestions)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblPlayerNamePrompt;
        private System.Windows.Forms.TextBox txtPlayerName;
        private System.Windows.Forms.GroupBox grpQuizMode;
        private System.Windows.Forms.RadioButton rbFlagFromCapital;
        private System.Windows.Forms.RadioButton rbCapitalFromFlag;
        private System.Windows.Forms.RadioButton rbCountryFromCapital;
        private System.Windows.Forms.RadioButton rbFlagFromCountry;
        private System.Windows.Forms.RadioButton rbCapitalFromCountry;
        private System.Windows.Forms.RadioButton rbCountryFromFlag;
        private System.Windows.Forms.Label lblContinentFilterPrompt;
        private System.Windows.Forms.ComboBox cmbContinent;
        private System.Windows.Forms.Label lblNumberOfQuestionsPrompt;
        private System.Windows.Forms.NumericUpDown numNumberOfQuestions;
        private System.Windows.Forms.Button btnStartQuiz;
        private System.Windows.Forms.Button btnHighScores;
        private System.Windows.Forms.Button btnStartFromConfig;
    }
}
