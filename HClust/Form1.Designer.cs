namespace HClust
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
            this.picClust = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picClust)).BeginInit();
            this.SuspendLayout();
            // 
            // picClust
            // 
            this.picClust.Location = new System.Drawing.Point(12, 12);
            this.picClust.Name = "picClust";
            this.picClust.Size = new System.Drawing.Size(641, 481);
            this.picClust.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picClust.TabIndex = 0;
            this.picClust.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(666, 506);
            this.Controls.Add(this.picClust);
            this.Name = "Form1";
            this.Text = "MADII";
            ((System.ComponentModel.ISupportInitialize)(this.picClust)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox picClust;
    }
}

