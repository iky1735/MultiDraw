namespace Socket_MultiDraw_EvanYoon
{
    partial class Alpha
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
            this.trackBar2 = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.lbl_current = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).BeginInit();
            this.SuspendLayout();
            // 
            // trackBar2
            // 
            this.trackBar2.Location = new System.Drawing.Point(12, 12);
            this.trackBar2.Maximum = 255;
            this.trackBar2.Name = "trackBar2";
            this.trackBar2.Size = new System.Drawing.Size(430, 45);
            this.trackBar2.TabIndex = 1;
            this.trackBar2.TickFrequency = 15;
            this.trackBar2.Value = 255;
            this.trackBar2.Scroll += new System.EventHandler(this.trackBar2_Scroll);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(21, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "0%";
            // 
            // lbl_current
            // 
            this.lbl_current.AutoSize = true;
            this.lbl_current.Location = new System.Drawing.Point(213, 44);
            this.lbl_current.Name = "lbl_current";
            this.lbl_current.Size = new System.Drawing.Size(48, 13);
            this.lbl_current.TabIndex = 3;
            this.lbl_current.Text = "100.00%";
            this.lbl_current.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(414, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "100%";
            // 
            // Alpha
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(454, 69);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lbl_current);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.trackBar2);
            this.Name = "Alpha";
            this.Text = "Alpha";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Alpha_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar trackBar2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbl_current;
        private System.Windows.Forms.Label label3;
    }
}