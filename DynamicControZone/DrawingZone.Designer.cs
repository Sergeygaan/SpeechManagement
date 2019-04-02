namespace MyPaint
{
    partial class DrawingZone
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
            this.components = new System.ComponentModel.Container();
            this.AddRegion = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.SuspendLayout();
            // 
            // AddRegion
            // 
            this.AddRegion.Name = "rightClickMenuStrip";
            this.AddRegion.Size = new System.Drawing.Size(181, 26);
            // 
            // DrawingZone
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(195, 94);
            this.DoubleBuffered = true;
            this.Name = "DrawingZone";
            this.Opacity = 0.65D;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "DrawingZone";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.DrawingZone_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DrawingZone_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.DrawingZone_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.DrawingZone_MouseUp);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip AddRegion;
    }
}