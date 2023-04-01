namespace TrainingDataChatGPTApp
{
    partial class MainManageForm
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
            this.label2 = new System.Windows.Forms.Label();
            this.contentTextBox = new System.Windows.Forms.TextBox();
            this.addRecordButton = new System.Windows.Forms.Button();
            this.trainingRecordDataGridView = new System.Windows.Forms.DataGridView();
            this.idTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.updateRecordButton = new System.Windows.Forms.Button();
            this.deleteRecordButton = new System.Windows.Forms.Button();
            this.clearRecordButton = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.registrosLabel = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.searchTextBox = new System.Windows.Forms.TextBox();
            this.searchButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.titleTextBox = new System.Windows.Forms.TextBox();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Title = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.trainingRecordDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(492, 118);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "Content";
            // 
            // contentTextBox
            // 
            this.contentTextBox.Location = new System.Drawing.Point(492, 136);
            this.contentTextBox.Multiline = true;
            this.contentTextBox.Name = "contentTextBox";
            this.contentTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.contentTextBox.Size = new System.Drawing.Size(604, 422);
            this.contentTextBox.TabIndex = 4;
            // 
            // addRecordButton
            // 
            this.addRecordButton.Location = new System.Drawing.Point(485, 563);
            this.addRecordButton.Name = "addRecordButton";
            this.addRecordButton.Size = new System.Drawing.Size(104, 23);
            this.addRecordButton.TabIndex = 5;
            this.addRecordButton.Text = "Add Record";
            this.addRecordButton.UseVisualStyleBackColor = true;
            this.addRecordButton.Click += new System.EventHandler(this.addRecordButton_Click);
            // 
            // trainingRecordDataGridView
            // 
            this.trainingRecordDataGridView.AllowUserToAddRows = false;
            this.trainingRecordDataGridView.AllowUserToDeleteRows = false;
            this.trainingRecordDataGridView.AllowUserToResizeColumns = false;
            this.trainingRecordDataGridView.AllowUserToResizeRows = false;
            this.trainingRecordDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.trainingRecordDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.Title});
            this.trainingRecordDataGridView.Location = new System.Drawing.Point(12, 31);
            this.trainingRecordDataGridView.MultiSelect = false;
            this.trainingRecordDataGridView.Name = "trainingRecordDataGridView";
            this.trainingRecordDataGridView.ReadOnly = true;
            this.trainingRecordDataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.trainingRecordDataGridView.RowTemplate.Height = 25;
            this.trainingRecordDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.trainingRecordDataGridView.Size = new System.Drawing.Size(462, 527);
            this.trainingRecordDataGridView.TabIndex = 6;
            this.trainingRecordDataGridView.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.trainingRecordDataGridView_CellMouseClick);
            // 
            // idTextBox
            // 
            this.idTextBox.Location = new System.Drawing.Point(490, 49);
            this.idTextBox.Name = "idTextBox";
            this.idTextBox.ReadOnly = true;
            this.idTextBox.Size = new System.Drawing.Size(606, 23);
            this.idTextBox.TabIndex = 8;
            this.idTextBox.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(491, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(18, 15);
            this.label3.TabIndex = 7;
            this.label3.Text = "ID";
            // 
            // updateRecordButton
            // 
            this.updateRecordButton.Location = new System.Drawing.Point(595, 563);
            this.updateRecordButton.Name = "updateRecordButton";
            this.updateRecordButton.Size = new System.Drawing.Size(106, 23);
            this.updateRecordButton.TabIndex = 9;
            this.updateRecordButton.Text = "Update Record";
            this.updateRecordButton.UseVisualStyleBackColor = true;
            this.updateRecordButton.Click += new System.EventHandler(this.updateRecordButton_Click);
            // 
            // deleteRecordButton
            // 
            this.deleteRecordButton.Location = new System.Drawing.Point(707, 563);
            this.deleteRecordButton.Name = "deleteRecordButton";
            this.deleteRecordButton.Size = new System.Drawing.Size(106, 23);
            this.deleteRecordButton.TabIndex = 10;
            this.deleteRecordButton.Text = "Delete Record";
            this.deleteRecordButton.UseVisualStyleBackColor = true;
            this.deleteRecordButton.Click += new System.EventHandler(this.deleteRecordButton_Click);
            // 
            // clearRecordButton
            // 
            this.clearRecordButton.Location = new System.Drawing.Point(819, 564);
            this.clearRecordButton.Name = "clearRecordButton";
            this.clearRecordButton.Size = new System.Drawing.Size(106, 23);
            this.clearRecordButton.TabIndex = 11;
            this.clearRecordButton.Text = "Clear Record";
            this.clearRecordButton.UseVisualStyleBackColor = true;
            this.clearRecordButton.Click += new System.EventHandler(this.clearRecordButton_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(110, 15);
            this.label4.TabIndex = 12;
            this.label4.Text = "Number of records:";
            // 
            // registrosLabel
            // 
            this.registrosLabel.AutoSize = true;
            this.registrosLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.registrosLabel.ForeColor = System.Drawing.Color.DarkRed;
            this.registrosLabel.Location = new System.Drawing.Point(121, 9);
            this.registrosLabel.Name = "registrosLabel";
            this.registrosLabel.Size = new System.Drawing.Size(31, 15);
            this.registrosLabel.TabIndex = 13;
            this.registrosLabel.Text = "XXX";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 572);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(45, 15);
            this.label5.TabIndex = 14;
            this.label5.Text = "Search:";
            // 
            // searchTextBox
            // 
            this.searchTextBox.Location = new System.Drawing.Point(57, 565);
            this.searchTextBox.Name = "searchTextBox";
            this.searchTextBox.Size = new System.Drawing.Size(302, 23);
            this.searchTextBox.TabIndex = 15;
            // 
            // searchButton
            // 
            this.searchButton.Location = new System.Drawing.Point(365, 564);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(103, 23);
            this.searchButton.TabIndex = 16;
            this.searchButton.Text = "Search";
            this.searchButton.UseVisualStyleBackColor = true;
            this.searchButton.Click += new System.EventHandler(this.searchButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(490, 75);
            this.label1.Name = "label1";
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label1.Size = new System.Drawing.Size(29, 15);
            this.label1.TabIndex = 18;
            this.label1.Text = "Title";
            // 
            // titleTextBox
            // 
            this.titleTextBox.Location = new System.Drawing.Point(492, 92);
            this.titleTextBox.Name = "titleTextBox";
            this.titleTextBox.Size = new System.Drawing.Size(603, 23);
            this.titleTextBox.TabIndex = 19;
            // 
            // ID
            // 
            this.ID.DataPropertyName = "Id";
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.Width = 30;
            // 
            // Title
            // 
            this.Title.DataPropertyName = "Title";
            this.Title.HeaderText = "Title";
            this.Title.Name = "Title";
            this.Title.ReadOnly = true;
            this.Title.Width = 390;
            // 
            // MainManageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1108, 600);
            this.Controls.Add(this.titleTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.searchButton);
            this.Controls.Add(this.searchTextBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.registrosLabel);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.clearRecordButton);
            this.Controls.Add(this.deleteRecordButton);
            this.Controls.Add(this.updateRecordButton);
            this.Controls.Add(this.idTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.trainingRecordDataGridView);
            this.Controls.Add(this.addRecordButton);
            this.Controls.Add(this.contentTextBox);
            this.Controls.Add(this.label2);
            this.Name = "MainManageForm";
            this.Text = "Main Form";
            ((System.ComponentModel.ISupportInitialize)(this.trainingRecordDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Label label2;
        private TextBox contentTextBox;
        private Button addRecordButton;
        private DataGridView trainingRecordDataGridView;
        private TextBox idTextBox;
        private Label label3;
        private Button updateRecordButton;
        private Button deleteRecordButton;
        private Button clearRecordButton;
        private Label label4;
        private Label registrosLabel;
        private Label label5;
        private TextBox searchTextBox;
        private Button searchButton;
        private Label label1;
        private TextBox titleTextBox;
        private DataGridViewTextBoxColumn ID;
        private DataGridViewTextBoxColumn Title;
    }
}