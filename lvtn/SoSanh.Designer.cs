namespace lvtn
{
    partial class SoSanh
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
            this.rtSoSanh = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // rtSoSanh
            // 
            this.rtSoSanh.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtSoSanh.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtSoSanh.Location = new System.Drawing.Point(0, 0);
            this.rtSoSanh.Name = "rtSoSanh";
            this.rtSoSanh.Size = new System.Drawing.Size(845, 576);
            this.rtSoSanh.TabIndex = 0;
            this.rtSoSanh.Text = "";
            // 
            // SoSanh
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(845, 576);
            this.Controls.Add(this.rtSoSanh);
            this.Name = "SoSanh";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SoSanh";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtSoSanh;
    }
}