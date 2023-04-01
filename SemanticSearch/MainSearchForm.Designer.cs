namespace TrainingDataChatGPTApp
{
    partial class MainSearchForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label6 = new System.Windows.Forms.Label();
            this.askChatGPTTextBox = new System.Windows.Forms.TextBox();
            this.askChatGPTButton = new System.Windows.Forms.Button();
            this.chatGPTAnswerTextBox = new System.Windows.Forms.TextBox();
            this.lowDetailRadioButton = new System.Windows.Forms.RadioButton();
            this.lotsDetailRadioButton = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label6.Location = new System.Drawing.Point(465, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(217, 25);
            this.label6.TabIndex = 20;
            this.label6.Text = "Ask ChatGPT A Question";
            // 
            // askChatGPTTextBox
            // 
            this.askChatGPTTextBox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.askChatGPTTextBox.Location = new System.Drawing.Point(12, 51);
            this.askChatGPTTextBox.Name = "askChatGPTTextBox";
            this.askChatGPTTextBox.Size = new System.Drawing.Size(703, 29);
            this.askChatGPTTextBox.TabIndex = 21;
            this.askChatGPTTextBox.TextChanged += new System.EventHandler(this.askChatGPTTextBox_TextChanged);
            // 
            // askChatGPTButton
            // 
            this.askChatGPTButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.askChatGPTButton.Location = new System.Drawing.Point(955, 50);
            this.askChatGPTButton.Name = "askChatGPTButton";
            this.askChatGPTButton.Size = new System.Drawing.Size(124, 30);
            this.askChatGPTButton.TabIndex = 22;
            this.askChatGPTButton.Text = "Ask ChatGPT";
            this.askChatGPTButton.UseVisualStyleBackColor = true;
            this.askChatGPTButton.Click += new System.EventHandler(this.askChatGPTButton_Click);
            // 
            // chatGPTAnswerTextBox
            // 
            this.chatGPTAnswerTextBox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.chatGPTAnswerTextBox.Location = new System.Drawing.Point(12, 86);
            this.chatGPTAnswerTextBox.Multiline = true;
            this.chatGPTAnswerTextBox.Name = "chatGPTAnswerTextBox";
            this.chatGPTAnswerTextBox.ReadOnly = true;
            this.chatGPTAnswerTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.chatGPTAnswerTextBox.Size = new System.Drawing.Size(1083, 502);
            this.chatGPTAnswerTextBox.TabIndex = 23;
            // 
            // lowDetailRadioButton
            // 
            this.lowDetailRadioButton.AutoSize = true;
            this.lowDetailRadioButton.Checked = true;
            this.lowDetailRadioButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lowDetailRadioButton.Location = new System.Drawing.Point(721, 55);
            this.lowDetailRadioButton.Name = "lowDetailRadioButton";
            this.lowDetailRadioButton.Size = new System.Drawing.Size(103, 25);
            this.lowDetailRadioButton.TabIndex = 24;
            this.lowDetailRadioButton.TabStop = true;
            this.lowDetailRadioButton.Text = "Brief Reply";
            this.lowDetailRadioButton.UseVisualStyleBackColor = true;
            this.lowDetailRadioButton.CheckedChanged += new System.EventHandler(this.lowDetailRadioButton_CheckedChanged);
            // 
            // lotsDetailRadioButton
            // 
            this.lotsDetailRadioButton.AutoSize = true;
            this.lotsDetailRadioButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lotsDetailRadioButton.Location = new System.Drawing.Point(830, 55);
            this.lotsDetailRadioButton.Name = "lotsDetailRadioButton";
            this.lotsDetailRadioButton.Size = new System.Drawing.Size(119, 25);
            this.lotsDetailRadioButton.TabIndex = 25;
            this.lotsDetailRadioButton.Text = "Lots of Detail";
            this.lotsDetailRadioButton.UseVisualStyleBackColor = true;
            this.lotsDetailRadioButton.CheckedChanged += new System.EventHandler(this.lotsOfDetailRadioButton_CheckedChanged);
            // 
            // MainSearchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1108, 600);
            this.Controls.Add(this.lotsDetailRadioButton);
            this.Controls.Add(this.lowDetailRadioButton);
            this.Controls.Add(this.chatGPTAnswerTextBox);
            this.Controls.Add(this.askChatGPTButton);
            this.Controls.Add(this.askChatGPTTextBox);
            this.Controls.Add(this.label6);
            this.Name = "MainSearchForm";
            this.Text = "Main Form";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Label label6;
        private TextBox askChatGPTTextBox;
        private Button askChatGPTButton;
        private TextBox chatGPTAnswerTextBox;
        private RadioButton lowDetailRadioButton;
        private RadioButton lotsDetailRadioButton;
    }
}