namespace WarehouseAccountingGUI
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
            groupBox2 = new GroupBox();
            sectionCB = new ComboBox();
            label2 = new Label();
            dataGridView1 = new DataGridView();
            editingGB_none = new GroupBox();
            label1 = new Label();
            editingGB_general = new GroupBox();
            takeBTN = new Button();
            putInBTN = new Button();
            selectedTB = new TextBox();
            label3 = new Label();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            editingGB_none.SuspendLayout();
            editingGB_general.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(sectionCB);
            groupBox2.Controls.Add(label2);
            groupBox2.Location = new Point(12, 12);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(195, 83);
            groupBox2.TabIndex = 2;
            groupBox2.TabStop = false;
            groupBox2.Text = "Filter settings";
            // 
            // sectionCB
            // 
            sectionCB.FormattingEnabled = true;
            sectionCB.Location = new Point(61, 16);
            sectionCB.Name = "sectionCB";
            sectionCB.Size = new Size(122, 23);
            sectionCB.TabIndex = 1;
            sectionCB.SelectedIndexChanged += sectionCB_SelectedIndexChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(6, 19);
            label2.Name = "label2";
            label2.Size = new Size(49, 15);
            label2.TabIndex = 0;
            label2.Text = "Section:";
            // 
            // dataGridView1
            // 
            dataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(12, 101);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowTemplate.Height = 25;
            dataGridView1.Size = new Size(1077, 397);
            dataGridView1.TabIndex = 3;
            // 
            // editingGB_none
            // 
            editingGB_none.Controls.Add(label1);
            editingGB_none.Location = new Point(213, 12);
            editingGB_none.Name = "editingGB_none";
            editingGB_none.Size = new Size(389, 83);
            editingGB_none.TabIndex = 4;
            editingGB_none.TabStop = false;
            editingGB_none.Text = "Editing";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(6, 24);
            label1.Name = "label1";
            label1.Size = new Size(302, 15);
            label1.TabIndex = 0;
            label1.Text = "Select a specific section to be able to change something";
            // 
            // editingGB_general
            // 
            editingGB_general.Controls.Add(takeBTN);
            editingGB_general.Controls.Add(putInBTN);
            editingGB_general.Controls.Add(selectedTB);
            editingGB_general.Controls.Add(label3);
            editingGB_general.Location = new Point(213, 12);
            editingGB_general.Name = "editingGB_general";
            editingGB_general.Size = new Size(220, 83);
            editingGB_general.TabIndex = 5;
            editingGB_general.TabStop = false;
            editingGB_general.Text = "Editing";
            // 
            // takeBTN
            // 
            takeBTN.Enabled = false;
            takeBTN.Location = new Point(109, 45);
            takeBTN.Name = "takeBTN";
            takeBTN.Size = new Size(100, 23);
            takeBTN.TabIndex = 4;
            takeBTN.Text = "Take";
            takeBTN.UseVisualStyleBackColor = true;
            takeBTN.Click += takeBTN_Click;
            // 
            // putInBTN
            // 
            putInBTN.Enabled = false;
            putInBTN.Location = new Point(6, 45);
            putInBTN.Name = "putInBTN";
            putInBTN.Size = new Size(100, 23);
            putInBTN.TabIndex = 3;
            putInBTN.Text = "Put in";
            putInBTN.UseVisualStyleBackColor = true;
            putInBTN.Click += putInBTN_Click;
            // 
            // selectedTB
            // 
            selectedTB.Location = new Point(66, 16);
            selectedTB.Name = "selectedTB";
            selectedTB.ReadOnly = true;
            selectedTB.Size = new Size(143, 23);
            selectedTB.TabIndex = 1;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(6, 19);
            label3.Name = "label3";
            label3.Size = new Size(54, 15);
            label3.TabIndex = 0;
            label3.Text = "Selected:";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1101, 510);
            Controls.Add(editingGB_general);
            Controls.Add(editingGB_none);
            Controls.Add(dataGridView1);
            Controls.Add(groupBox2);
            Name = "Form1";
            Text = "Warehouse accounting GUI";
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            editingGB_none.ResumeLayout(false);
            editingGB_none.PerformLayout();
            editingGB_general.ResumeLayout(false);
            editingGB_general.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox2;
        private ComboBox sectionCB;
        private Label label2;
        private DataGridView dataGridView1;
        private GroupBox editingGB_none;
        private Label label1;
        private GroupBox editingGB_general;
        private TextBox selectedTB;
        private Label label3;
        private Button putInBTN;
        private Button takeBTN;
    }
}