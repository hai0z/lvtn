namespace lvtn
{
    partial class FCM
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
            this.button_xuly = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.button_phanloai = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // button_xuly
            // 
            this.button_xuly.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_xuly.Location = new System.Drawing.Point(844, 104);
            this.button_xuly.Name = "button_xuly";
            this.button_xuly.Size = new System.Drawing.Size(208, 35);
            this.button_xuly.TabIndex = 2;
            this.button_xuly.Text = "Tiền xử lý";
            this.button_xuly.UseVisualStyleBackColor = true;
            this.button_xuly.Click += new System.EventHandler(this.button_xuly_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Consolas", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(314, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(262, 24);
            this.label1.TabIndex = 4;
            this.label1.Text = "Phân loại văn bản FCM";
            // 
            // button_phanloai
            // 
            this.button_phanloai.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_phanloai.Location = new System.Drawing.Point(844, 145);
            this.button_phanloai.Name = "button_phanloai";
            this.button_phanloai.Size = new System.Drawing.Size(208, 35);
            this.button_phanloai.TabIndex = 5;
            this.button_phanloai.Text = "Phân loại";
            this.button_phanloai.UseVisualStyleBackColor = true;
            this.button_phanloai.Click += new System.EventHandler(this.button_phanloai_Click);
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(25, 91);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(805, 578);
            this.textBox1.TabIndex = 6;
            this.textBox1.Text = "";
            // 
            // FCM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1064, 681);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button_phanloai);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button_xuly);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FCM";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DeTai";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button_xuly;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button_phanloai;
        private System.Windows.Forms.RichTextBox textBox1;
    }
}

