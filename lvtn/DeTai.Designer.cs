namespace DeTai
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
            this.button_tudien = new System.Windows.Forms.Button();
            this.button_chude = new System.Windows.Forms.Button();
            this.button_xuly = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button_phanloai = new System.Windows.Forms.Button();
            this.radioButton_Cmean = new System.Windows.Forms.RadioButton();
            this.radioButton_svm = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // button_tudien
            // 
            this.button_tudien.Location = new System.Drawing.Point(32, 33);
            this.button_tudien.Name = "button_tudien";
            this.button_tudien.Size = new System.Drawing.Size(107, 23);
            this.button_tudien.TabIndex = 0;
            this.button_tudien.Text = "Từ Điển";
            this.button_tudien.UseVisualStyleBackColor = true;
            // 
            // button_chude
            // 
            this.button_chude.Location = new System.Drawing.Point(32, 62);
            this.button_chude.Name = "button_chude";
            this.button_chude.Size = new System.Drawing.Size(107, 23);
            this.button_chude.TabIndex = 1;
            this.button_chude.Text = "Chủ đề";
            this.button_chude.UseVisualStyleBackColor = true;
            this.button_chude.Click += new System.EventHandler(this.button_chude_Click);
            // 
            // button_xuly
            // 
            this.button_xuly.Location = new System.Drawing.Point(32, 91);
            this.button_xuly.Name = "button_xuly";
            this.button_xuly.Size = new System.Drawing.Size(107, 23);
            this.button_xuly.TabIndex = 2;
            this.button_xuly.Text = "Tiền xử lý";
            this.button_xuly.UseVisualStyleBackColor = true;
            this.button_xuly.Click += new System.EventHandler(this.button_xuly_Click);
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.textBox1.Location = new System.Drawing.Point(166, 35);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(459, 305);
            this.textBox1.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(309, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(140, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "PHÂN LOẠI VĂN BẢN";
            // 
            // button_phanloai
            // 
            this.button_phanloai.Location = new System.Drawing.Point(32, 120);
            this.button_phanloai.Name = "button_phanloai";
            this.button_phanloai.Size = new System.Drawing.Size(107, 23);
            this.button_phanloai.TabIndex = 5;
            this.button_phanloai.Text = "Phân loại";
            this.button_phanloai.UseVisualStyleBackColor = true;
            this.button_phanloai.Click += new System.EventHandler(this.button_phanloai_Click);
            // 
            // radioButton_Cmean
            // 
            this.radioButton_Cmean.Location = new System.Drawing.Point(41, 154);
            this.radioButton_Cmean.Name = "radioButton_Cmean";
            this.radioButton_Cmean.Size = new System.Drawing.Size(86, 17);
            this.radioButton_Cmean.TabIndex = 6;
            this.radioButton_Cmean.TabStop = true;
            this.radioButton_Cmean.Text = "C-Means\r\n";
            this.radioButton_Cmean.UseVisualStyleBackColor = true;
            // 
            // radioButton_svm
            // 
            this.radioButton_svm.AutoSize = true;
            this.radioButton_svm.Location = new System.Drawing.Point(41, 177);
            this.radioButton_svm.Name = "radioButton_svm";
            this.radioButton_svm.Size = new System.Drawing.Size(48, 17);
            this.radioButton_svm.TabIndex = 7;
            this.radioButton_svm.TabStop = true;
            this.radioButton_svm.Text = "SVM\r\n";
            this.radioButton_svm.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ClientSize = new System.Drawing.Size(634, 347);
            this.Controls.Add(this.radioButton_svm);
            this.Controls.Add(this.radioButton_Cmean);
            this.Controls.Add(this.button_phanloai);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button_xuly);
            this.Controls.Add(this.button_chude);
            this.Controls.Add(this.button_tudien);
            this.Name = "Form1";
            this.Text = "DeTai";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_tudien;
        private System.Windows.Forms.Button button_chude;
        private System.Windows.Forms.Button button_xuly;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button_phanloai;
        private System.Windows.Forms.RadioButton radioButton_Cmean;
        private System.Windows.Forms.RadioButton radioButton_svm;
    }
}

