namespace Blender_Model_Selector
{
    partial class Form1
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
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.btn_StoreProject = new System.Windows.Forms.Button();
            this.btn_GenerateProject = new System.Windows.Forms.Button();
            this.btn_GetTableValues = new System.Windows.Forms.Button();
            this.listBox_Tables = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.comboBox6 = new System.Windows.Forms.ComboBox();
            this.comboBox4 = new System.Windows.Forms.ComboBox();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.comboBox5 = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lbl_1 = new System.Windows.Forms.Label();
            this.lbl_6 = new System.Windows.Forms.Label();
            this.lbl_3 = new System.Windows.Forms.Label();
            this.lbl_4 = new System.Windows.Forms.Label();
            this.lbl_5 = new System.Windows.Forms.Label();
            this.lbl_2 = new System.Windows.Forms.Label();
            this.btn_Save = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView
            // 
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Location = new System.Drawing.Point(373, 84);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.Size = new System.Drawing.Size(910, 354);
            this.dataGridView.TabIndex = 0;
            this.dataGridView.DataSourceChanged += new System.EventHandler(this.dataGridView_DataSourceChanged);
            this.dataGridView.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellValueChanged);
            this.dataGridView.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.dataGridView_UserDeletingRow);
            // 
            // btn_StoreProject
            // 
            this.btn_StoreProject.Location = new System.Drawing.Point(199, 105);
            this.btn_StoreProject.Name = "btn_StoreProject";
            this.btn_StoreProject.Size = new System.Drawing.Size(116, 77);
            this.btn_StoreProject.TabIndex = 1;
            this.btn_StoreProject.Text = "Store Project";
            this.btn_StoreProject.UseVisualStyleBackColor = true;
            this.btn_StoreProject.Click += new System.EventHandler(this.btn_StoreProject_Click);
            // 
            // btn_GenerateProject
            // 
            this.btn_GenerateProject.Location = new System.Drawing.Point(27, 105);
            this.btn_GenerateProject.Name = "btn_GenerateProject";
            this.btn_GenerateProject.Size = new System.Drawing.Size(116, 77);
            this.btn_GenerateProject.TabIndex = 2;
            this.btn_GenerateProject.Text = "Generate";
            this.btn_GenerateProject.UseVisualStyleBackColor = true;
            this.btn_GenerateProject.Click += new System.EventHandler(this.btn_GenerateProject_Click);
            // 
            // btn_GetTableValues
            // 
            this.btn_GetTableValues.Location = new System.Drawing.Point(60, 350);
            this.btn_GetTableValues.Name = "btn_GetTableValues";
            this.btn_GetTableValues.Size = new System.Drawing.Size(116, 77);
            this.btn_GetTableValues.TabIndex = 3;
            this.btn_GetTableValues.Text = "Open ";
            this.btn_GetTableValues.UseVisualStyleBackColor = true;
            this.btn_GetTableValues.Click += new System.EventHandler(this.btn_GetTableValues_Click);
            // 
            // listBox_Tables
            // 
            this.listBox_Tables.FormattingEnabled = true;
            this.listBox_Tables.Location = new System.Drawing.Point(27, 208);
            this.listBox_Tables.Name = "listBox_Tables";
            this.listBox_Tables.Size = new System.Drawing.Size(288, 95);
            this.listBox_Tables.TabIndex = 4;
            this.listBox_Tables.SelectedIndexChanged += new System.EventHandler(this.listBox_Tables_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(112, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Modeling Idea Generator";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 30);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(84, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "Exit";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(255, 30);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(84, 23);
            this.button2.TabIndex = 7;
            this.button2.Text = "AI Generate";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.aiGenerateButton_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(389, 41);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 8;
            //this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // comboBox6
            // 
            this.comboBox6.FormattingEnabled = true;
            this.comboBox6.Location = new System.Drawing.Point(1158, 41);
            this.comboBox6.Name = "comboBox6";
            this.comboBox6.Size = new System.Drawing.Size(121, 21);
            this.comboBox6.TabIndex = 9;
            //this.comboBox6.SelectedIndexChanged += new System.EventHandler(this.comboBox6_SelectedIndexChanged);
            // 
            // comboBox4
            // 
            this.comboBox4.FormattingEnabled = true;
            this.comboBox4.Location = new System.Drawing.Point(848, 41);
            this.comboBox4.Name = "comboBox4";
            this.comboBox4.Size = new System.Drawing.Size(121, 21);
            this.comboBox4.TabIndex = 11;
            //this.comboBox4.SelectedIndexChanged += new System.EventHandler(this.comboBox4_SelectedIndexChanged);
            // 
            // comboBox3
            // 
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Location = new System.Drawing.Point(693, 41);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(121, 21);
            this.comboBox3.TabIndex = 10;
            //this.comboBox3.SelectedIndexChanged += new System.EventHandler(this.comboBox3_SelectedIndexChanged);
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(541, 41);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(121, 21);
            this.comboBox2.TabIndex = 13;
            //this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // comboBox5
            // 
            this.comboBox5.FormattingEnabled = true;
            this.comboBox5.Location = new System.Drawing.Point(1000, 41);
            this.comboBox5.Name = "comboBox5";
            this.comboBox5.Size = new System.Drawing.Size(121, 21);
            this.comboBox5.TabIndex = 12;
            //this.comboBox5.SelectedIndexChanged += new System.EventHandler(this.comboBox5_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label2.Location = new System.Drawing.Point(355, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(2, 449);
            this.label2.TabIndex = 15;
            // 
            // lbl_1
            // 
            this.lbl_1.AutoSize = true;
            this.lbl_1.Location = new System.Drawing.Point(386, 20);
            this.lbl_1.Name = "lbl_1";
            this.lbl_1.Size = new System.Drawing.Size(68, 13);
            this.lbl_1.TabIndex = 16;
            this.lbl_1.Text = "Accent Color";
            // 
            // lbl_6
            // 
            this.lbl_6.AutoSize = true;
            this.lbl_6.Location = new System.Drawing.Point(1155, 20);
            this.lbl_6.Name = "lbl_6";
            this.lbl_6.Size = new System.Drawing.Size(71, 13);
            this.lbl_6.TabIndex = 17;
            this.lbl_6.Text = "World Theme";
            // 
            // lbl_3
            // 
            this.lbl_3.AutoSize = true;
            this.lbl_3.Location = new System.Drawing.Point(690, 20);
            this.lbl_3.Name = "lbl_3";
            this.lbl_3.Size = new System.Drawing.Size(106, 13);
            this.lbl_3.TabIndex = 18;
            this.lbl_3.Text = "Emotional Undertone";
            // 
            // lbl_4
            // 
            this.lbl_4.AutoSize = true;
            this.lbl_4.Location = new System.Drawing.Point(845, 20);
            this.lbl_4.Name = "lbl_4";
            this.lbl_4.Size = new System.Drawing.Size(39, 13);
            this.lbl_4.TabIndex = 19;
            this.lbl_4.Text = "Quality";
            // 
            // lbl_5
            // 
            this.lbl_5.AutoSize = true;
            this.lbl_5.Location = new System.Drawing.Point(997, 20);
            this.lbl_5.Name = "lbl_5";
            this.lbl_5.Size = new System.Drawing.Size(31, 13);
            this.lbl_5.TabIndex = 20;
            this.lbl_5.Text = "Type";
            // 
            // lbl_2
            // 
            this.lbl_2.AutoSize = true;
            this.lbl_2.Location = new System.Drawing.Point(538, 20);
            this.lbl_2.Name = "lbl_2";
            this.lbl_2.Size = new System.Drawing.Size(46, 13);
            this.lbl_2.TabIndex = 21;
            this.lbl_2.Text = "Art Style";
            // 
            // btn_Save
            // 
            this.btn_Save.Location = new System.Drawing.Point(227, 317);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(75, 23);
            this.btn_Save.TabIndex = 22;
            this.btn_Save.Text = "Save";
            this.btn_Save.UseVisualStyleBackColor = true;
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1303, 467);
            this.Controls.Add(this.btn_Save);
            this.Controls.Add(this.lbl_2);
            this.Controls.Add(this.lbl_5);
            this.Controls.Add(this.lbl_4);
            this.Controls.Add(this.lbl_3);
            this.Controls.Add(this.lbl_6);
            this.Controls.Add(this.lbl_1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.comboBox5);
            this.Controls.Add(this.comboBox4);
            this.Controls.Add(this.comboBox3);
            this.Controls.Add(this.comboBox6);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listBox_Tables);
            this.Controls.Add(this.btn_GetTableValues);
            this.Controls.Add(this.btn_GenerateProject);
            this.Controls.Add(this.btn_StoreProject);
            this.Controls.Add(this.dataGridView);
            this.Name = "Form1";
            this.Text = "Modeling Idea Generator";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Button btn_StoreProject;
        private System.Windows.Forms.Button btn_GenerateProject;
        private System.Windows.Forms.Button btn_GetTableValues;
        private System.Windows.Forms.ListBox listBox_Tables;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ComboBox comboBox6;
        private System.Windows.Forms.ComboBox comboBox4;
        private System.Windows.Forms.ComboBox comboBox3;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.ComboBox comboBox5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbl_1;
        private System.Windows.Forms.Label lbl_6;
        private System.Windows.Forms.Label lbl_3;
        private System.Windows.Forms.Label lbl_4;
        private System.Windows.Forms.Label lbl_5;
        private System.Windows.Forms.Label lbl_2;
        private System.Windows.Forms.Button btn_Save;
    }
}

