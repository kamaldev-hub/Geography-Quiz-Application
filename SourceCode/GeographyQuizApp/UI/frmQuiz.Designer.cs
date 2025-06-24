namespace GeographyQuizApp.UI
{
    partial class frmQuiz
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
            this.components = new System.ComponentModel.Container();
            this.lblQuestionNumber = new System.Windows.Forms.Label();
            this.lblScore = new System.Windows.Forms.Label();
            this.picQuestionImage = new System.Windows.Forms.PictureBox();
            this.lblQuestionText = new System.Windows.Forms.Label();
            this.btnOption1 = new System.Windows.Forms.Button();
            this.btnOption2 = new System.Windows.Forms.Button();
            this.btnOption3 = new System.Windows.Forms.Button();
            this.btnOption4 = new System.Windows.Forms.Button();
            this.lblFeedback = new System.Windows.Forms.Label();
            this.progressBarQuiz = new System.Windows.Forms.ProgressBar();
            this.timerFeedback = new System.Windows.Forms.Timer(this.components);
            this.pnlOptions = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.picQuestionImage)).BeginInit();
            this.pnlOptions.SuspendLayout();
            this.SuspendLayout();
            //
            // lblQuestionNumber
            //
            this.lblQuestionNumber.AccessibleName = "Question Progress";
            this.lblQuestionNumber.AutoSize = true;
            this.lblQuestionNumber.Location = new System.Drawing.Point(12, 9);
            this.lblQuestionNumber.Name = "lblQuestionNumber";
            this.lblQuestionNumber.Size = new System.Drawing.Size(79, 13);
            this.lblQuestionNumber.TabIndex = 0;
            this.lblQuestionNumber.Text = "Question: 0 / 0";
            //
            // lblScore
            //
            this.lblScore.AccessibleName = "Current Score";
            this.lblScore.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblScore.AutoSize = true;
            this.lblScore.Location = new System.Drawing.Point(420, 9);
            this.lblScore.Name = "lblScore";
            this.lblScore.Size = new System.Drawing.Size(52, 13);
            this.lblScore.TabIndex = 1;
            this.lblScore.Text = "Score: 0";
            this.lblScore.TextAlign = System.Drawing.ContentAlignment.TopRight;
            //
            // picQuestionImage
            //
            this.picQuestionImage.AccessibleName = "Question Image (Flag)";
            this.picQuestionImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picQuestionImage.Location = new System.Drawing.Point(15, 60);
            this.picQuestionImage.Name = "picQuestionImage";
            this.picQuestionImage.Size = new System.Drawing.Size(457, 150);
            this.picQuestionImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picQuestionImage.TabIndex = 2;
            this.picQuestionImage.TabStop = false;
            //
            // lblQuestionText
            //
            this.lblQuestionText.AccessibleName = "Question Text";
            this.lblQuestionText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQuestionText.Location = new System.Drawing.Point(15, 220);
            this.lblQuestionText.Name = "lblQuestionText";
            this.lblQuestionText.Size = new System.Drawing.Size(457, 40);
            this.lblQuestionText.TabIndex = 3;
            this.lblQuestionText.Text = "Question text will appear here.";
            this.lblQuestionText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // pnlOptions
            //
            this.pnlOptions.Controls.Add(this.btnOption1);
            this.pnlOptions.Controls.Add(this.btnOption2);
            this.pnlOptions.Controls.Add(this.btnOption3);
            this.pnlOptions.Controls.Add(this.btnOption4);
            this.pnlOptions.Location = new System.Drawing.Point(15, 270);
            this.pnlOptions.Name = "pnlOptions";
            this.pnlOptions.Size = new System.Drawing.Size(457, 120);
            this.pnlOptions.TabIndex = 4;
            //
            // btnOption1
            //
            this.btnOption1.AccessibleName = "Answer Option 1";
            this.btnOption1.Location = new System.Drawing.Point(3, 3);
            this.btnOption1.Name = "btnOption1";
            this.btnOption1.Size = new System.Drawing.Size(220, 50);
            this.btnOption1.TabIndex = 0;
            this.btnOption1.Text = "Option 1";
            this.btnOption1.UseVisualStyleBackColor = true;
            this.btnOption1.Click += new System.EventHandler(this.btnOption_Click);
            //
            // btnOption2
            //
            this.btnOption2.AccessibleName = "Answer Option 2";
            this.btnOption2.Location = new System.Drawing.Point(234, 3);
            this.btnOption2.Name = "btnOption2";
            this.btnOption2.Size = new System.Drawing.Size(220, 50);
            this.btnOption2.TabIndex = 1;
            this.btnOption2.Text = "Option 2";
            this.btnOption2.UseVisualStyleBackColor = true;
            this.btnOption2.Click += new System.EventHandler(this.btnOption_Click);
            //
            // btnOption3
            //
            this.btnOption3.AccessibleName = "Answer Option 3";
            this.btnOption3.Location = new System.Drawing.Point(3, 59);
            this.btnOption3.Name = "btnOption3";
            this.btnOption3.Size = new System.Drawing.Size(220, 50);
            this.btnOption3.TabIndex = 2;
            this.btnOption3.Text = "Option 3";
            this.btnOption3.UseVisualStyleBackColor = true;
            this.btnOption3.Click += new System.EventHandler(this.btnOption_Click);
            //
            // btnOption4
            //
            this.btnOption4.AccessibleName = "Answer Option 4";
            this.btnOption4.Location = new System.Drawing.Point(234, 59);
            this.btnOption4.Name = "btnOption4";
            this.btnOption4.Size = new System.Drawing.Size(220, 50);
            this.btnOption4.TabIndex = 3;
            this.btnOption4.Text = "Option 4";
            this.btnOption4.UseVisualStyleBackColor = true;
            this.btnOption4.Click += new System.EventHandler(this.btnOption_Click);
            //
            // lblFeedback
            //
            this.lblFeedback.AccessibleName = "Answer Feedback";
            this.lblFeedback.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFeedback.Location = new System.Drawing.Point(15, 400);
            this.lblFeedback.Name = "lblFeedback";
            this.lblFeedback.Size = new System.Drawing.Size(457, 23);
            this.lblFeedback.TabIndex = 5;
            this.lblFeedback.Text = "Feedback";
            this.lblFeedback.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblFeedback.Visible = false;
            //
            // progressBarQuiz
            //
            this.progressBarQuiz.AccessibleName = "Quiz Progress Bar";
            this.progressBarQuiz.Location = new System.Drawing.Point(15, 30);
            this.progressBarQuiz.Name = "progressBarQuiz";
            this.progressBarQuiz.Size = new System.Drawing.Size(457, 23);
            this.progressBarQuiz.TabIndex = 6;
            //
            // timerFeedback
            //
            this.timerFeedback.Interval = 1500;
            this.timerFeedback.Tick += new System.EventHandler(this.timerFeedback_Tick);
            //
            // frmQuiz
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 431);
            this.Controls.Add(this.progressBarQuiz);
            this.Controls.Add(this.lblFeedback);
            this.Controls.Add(this.pnlOptions);
            this.Controls.Add(this.lblQuestionText);
            this.Controls.Add(this.picQuestionImage);
            this.Controls.Add(this.lblScore);
            this.Controls.Add(this.lblQuestionNumber);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmQuiz";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Geography Quiz";
            this.Load += new System.EventHandler(this.frmQuiz_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picQuestionImage)).EndInit();
            this.pnlOptions.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblQuestionNumber;
        private System.Windows.Forms.Label lblScore;
        private System.Windows.Forms.PictureBox picQuestionImage;
        private System.Windows.Forms.Label lblQuestionText;
        private System.Windows.Forms.Button btnOption1;
        private System.Windows.Forms.Button btnOption2;
        private System.Windows.Forms.Button btnOption3;
        private System.Windows.Forms.Button btnOption4;
        private System.Windows.Forms.Label lblFeedback;
        private System.Windows.Forms.ProgressBar progressBarQuiz;
        private System.Windows.Forms.Timer timerFeedback;
        private System.Windows.Forms.Panel pnlOptions;
    }
}
