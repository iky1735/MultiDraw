namespace Socket_MultiDraw_EvanYoon
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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.Status_Connection = new System.Windows.Forms.ToolStripStatusLabel();
            this.Status_Colour = new System.Windows.Forms.ToolStripStatusLabel();
            this.Status_Thickness = new System.Windows.Forms.ToolStripStatusLabel();
            this.Status_Frame = new System.Windows.Forms.ToolStripStatusLabel();
            this.Status_Fragment = new System.Windows.Forms.ToolStripStatusLabel();
            this.Status_Destack = new System.Windows.Forms.ToolStripStatusLabel();
            this.Status_Bytes = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.status_stress = new System.Windows.Forms.ToolStripStatusLabel();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Status_Connection,
            this.Status_Colour,
            this.Status_Thickness,
            this.Status_Frame,
            this.Status_Fragment,
            this.Status_Destack,
            this.Status_Bytes,
            this.toolStripStatusLabel1,
            this.status_stress});
            this.statusStrip1.Location = new System.Drawing.Point(0, 631);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1539, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // Status_Connection
            // 
            this.Status_Connection.BackColor = System.Drawing.Color.Red;
            this.Status_Connection.ForeColor = System.Drawing.Color.Black;
            this.Status_Connection.Margin = new System.Windows.Forms.Padding(5, 3, 5, 2);
            this.Status_Connection.Name = "Status_Connection";
            this.Status_Connection.Size = new System.Drawing.Size(79, 17);
            this.Status_Connection.Text = "Disconnected";
            this.Status_Connection.Click += new System.EventHandler(this.Status_Connection_Click);
            // 
            // Status_Colour
            // 
            this.Status_Colour.ForeColor = System.Drawing.Color.Red;
            this.Status_Colour.Margin = new System.Windows.Forms.Padding(5, 3, 5, 2);
            this.Status_Colour.Name = "Status_Colour";
            this.Status_Colour.Size = new System.Drawing.Size(43, 17);
            this.Status_Colour.Text = "Colour";
            this.Status_Colour.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Status_Colour_MouseDown);
            // 
            // Status_Thickness
            // 
            this.Status_Thickness.Margin = new System.Windows.Forms.Padding(5, 3, 5, 2);
            this.Status_Thickness.Name = "Status_Thickness";
            this.Status_Thickness.Size = new System.Drawing.Size(70, 17);
            this.Status_Thickness.Text = "Thickness: 1";
            // 
            // Status_Frame
            // 
            this.Status_Frame.Margin = new System.Windows.Forms.Padding(5, 3, 5, 2);
            this.Status_Frame.Name = "Status_Frame";
            this.Status_Frame.Size = new System.Drawing.Size(93, 17);
            this.Status_Frame.Text = "Frames RX\'ed : 0";
            // 
            // Status_Fragment
            // 
            this.Status_Fragment.Margin = new System.Windows.Forms.Padding(5, 3, 5, 2);
            this.Status_Fragment.Name = "Status_Fragment";
            this.Status_Fragment.Size = new System.Drawing.Size(78, 17);
            this.Status_Fragment.Text = "Fragments : 0";
            // 
            // Status_Destack
            // 
            this.Status_Destack.Margin = new System.Windows.Forms.Padding(5, 3, 5, 2);
            this.Status_Destack.Name = "Status_Destack";
            this.Status_Destack.Size = new System.Drawing.Size(84, 17);
            this.Status_Destack.Text = "Destack Avg. : ";
            // 
            // Status_Bytes
            // 
            this.Status_Bytes.Margin = new System.Windows.Forms.Padding(5, 3, 5, 2);
            this.Status_Bytes.Name = "Status_Bytes";
            this.Status_Bytes.Size = new System.Drawing.Size(80, 17);
            this.Status_Bytes.Text = "Bytes RXed : 0";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Margin = new System.Windows.Forms.Padding(5, 3, 5, 2);
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(34, 17);
            this.toolStripStatusLabel1.Text = "Clear";
            this.toolStripStatusLabel1.Click += new System.EventHandler(this.ToolStripStatusLabel1_Click);
            // 
            // status_stress
            // 
            this.status_stress.Margin = new System.Windows.Forms.Padding(5, 3, 5, 2);
            this.status_stress.Name = "status_stress";
            this.status_stress.Size = new System.Drawing.Size(60, 17);
            this.status_stress.Text = "Stress Test";
            this.status_stress.Click += new System.EventHandler(this.Status_stress_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1539, 653);
            this.Controls.Add(this.statusStrip1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseUp);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel Status_Connection;
        private System.Windows.Forms.ToolStripStatusLabel Status_Colour;
        private System.Windows.Forms.ToolStripStatusLabel Status_Thickness;
        private System.Windows.Forms.ToolStripStatusLabel Status_Frame;
        private System.Windows.Forms.ToolStripStatusLabel Status_Fragment;
        private System.Windows.Forms.ToolStripStatusLabel Status_Destack;
        private System.Windows.Forms.ToolStripStatusLabel Status_Bytes;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel status_stress;
    }
}

