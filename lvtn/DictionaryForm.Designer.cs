namespace lvtn
{
    partial class DictionaryForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.lbDic = new System.Windows.Forms.Label();
            this.btnLoadDic = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.cbxTudien = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(486, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "Từ điển";
            // 
            // listBox1
            // 
            this.listBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 18;
            this.listBox1.Location = new System.Drawing.Point(13, 242);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(1055, 436);
            this.listBox1.TabIndex = 1;
            // 
            // lbDic
            // 
            this.lbDic.AutoSize = true;
            this.lbDic.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDic.Location = new System.Drawing.Point(12, 173);
            this.lbDic.Name = "lbDic";
            this.lbDic.Size = new System.Drawing.Size(85, 18);
            this.lbDic.TabIndex = 2;
            this.lbDic.Text = "Từ điển gốc";
            this.lbDic.Click += new System.EventHandler(this.label2_Click);
            // 
            // btnLoadDic
            // 
            this.btnLoadDic.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLoadDic.Location = new System.Drawing.Point(26, 131);
            this.btnLoadDic.Name = "btnLoadDic";
            this.btnLoadDic.Size = new System.Drawing.Size(132, 35);
            this.btnLoadDic.TabIndex = 3;
            this.btnLoadDic.Text = "Load từ điển";
            this.btnLoadDic.UseVisualStyleBackColor = true;
            this.btnLoadDic.Click += new System.EventHandler(this.btnLoadDic_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 206);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(449, 18);
            this.label2.TabIndex = 4;
            this.label2.Text = "Các chủ để: Thể Thao, Giáo dục, Kinh Doanh, Sức Khoẻ, Pháp luật";
            // 
            // cbxTudien
            // 
            this.cbxTudien.FormattingEnabled = true;
            this.cbxTudien.Items.AddRange(new object[] {
            "Tất Cả",
            "Thể Thao",
            "Kinh doanh",
            "Giáo dục",
            "Sức khoẻ",
            "Pháp luật",
            "Văn hoá"});
            this.cbxTudien.Location = new System.Drawing.Point(26, 104);
            this.cbxTudien.Name = "cbxTudien";
            this.cbxTudien.Size = new System.Drawing.Size(203, 21);
            this.cbxTudien.TabIndex = 5;
            this.cbxTudien.SelectedIndexChanged += new System.EventHandler(this.cbxTudien_SelectedIndexChanged);
            // 
            // DictionaryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1080, 720);
            this.Controls.Add(this.cbxTudien);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnLoadDic);
            this.Controls.Add(this.lbDic);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "DictionaryForm";
            this.Text = "Dictionary";
            this.Load += new System.EventHandler(this.DictionaryForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label lbDic;
        private System.Windows.Forms.Button btnLoadDic;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbxTudien;
    }
}