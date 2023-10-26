namespace ExcelToSqlDBConverter
{
    partial class Form1
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
            groupBox1 = new GroupBox();
            button2 = new Button();
            inputTableName = new TextBox();
            label3 = new Label();
            inputDatabaseName = new TextBox();
            buttonConvert = new Button();
            button1 = new Button();
            label2 = new Label();
            excelFilePath = new TextBox();
            label1 = new Label();
            dataGridView1 = new DataGridView();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(button2);
            groupBox1.Controls.Add(inputTableName);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(inputDatabaseName);
            groupBox1.Controls.Add(buttonConvert);
            groupBox1.Controls.Add(button1);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(excelFilePath);
            groupBox1.Controls.Add(label1);
            groupBox1.Location = new Point(29, 30);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(746, 140);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Excel Converter";
            // 
            // button2
            // 
            button2.Location = new Point(626, 28);
            button2.Name = "button2";
            button2.Size = new Size(71, 23);
            button2.TabIndex = 8;
            button2.Text = "Clear All";
            button2.UseVisualStyleBackColor = true;
            button2.Click += OnClickClearAll;
            // 
            // inputTableName
            // 
            inputTableName.Location = new Point(372, 67);
            inputTableName.Name = "inputTableName";
            inputTableName.Size = new Size(158, 23);
            inputTableName.TabIndex = 7;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(294, 73);
            label3.Name = "label3";
            label3.Size = new Size(72, 15);
            label3.TabIndex = 6;
            label3.Text = "Table Name:";
            // 
            // inputDatabaseName
            // 
            inputDatabaseName.Location = new Point(116, 66);
            inputDatabaseName.Name = "inputDatabaseName";
            inputDatabaseName.Size = new Size(158, 23);
            inputDatabaseName.TabIndex = 3;
            // 
            // buttonConvert
            // 
            buttonConvert.Location = new Point(116, 95);
            buttonConvert.Name = "buttonConvert";
            buttonConvert.Size = new Size(158, 23);
            buttonConvert.TabIndex = 5;
            buttonConvert.Text = "Convert to database";
            buttonConvert.UseVisualStyleBackColor = true;
            buttonConvert.Click += OnClickConvertToDb;
            // 
            // button1
            // 
            button1.Location = new Point(549, 28);
            button1.Name = "button1";
            button1.Size = new Size(71, 23);
            button1.TabIndex = 4;
            button1.Text = "View Data";
            button1.UseVisualStyleBackColor = true;
            button1.Click += OnClickViewExcelFile;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(18, 74);
            label2.Name = "label2";
            label2.Size = new Size(93, 15);
            label2.TabIndex = 2;
            label2.Text = "Database Name:";
            // 
            // excelFilePath
            // 
            excelFilePath.Location = new Point(116, 28);
            excelFilePath.Name = "excelFilePath";
            excelFilePath.Size = new Size(427, 23);
            excelFilePath.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(18, 36);
            label1.Name = "label1";
            label1.Size = new Size(83, 15);
            label1.TabIndex = 0;
            label1.Text = "Excel file path:";
            // 
            // dataGridView1
            // 
            dataGridView1.BackgroundColor = SystemColors.Control;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(29, 206);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowTemplate.Height = 25;
            dataGridView1.Size = new Size(746, 232);
            dataGridView1.TabIndex = 4;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(dataGridView1);
            Controls.Add(groupBox1);
            Name = "Form1";
            Text = "Form1";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private TextBox excelFilePath;
        private Label label1;
        private TextBox inputDatabaseName;
        private Label label2;
        private Button buttonConvert;
        private Button button1;
        private DataGridView dataGridView1;
        private TextBox inputTableName;
        private Label label3;
        private Button button2;
    }
}