﻿namespace MyPaint
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
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.AddRegion.SuspendLayout();
            this.SuspendLayout();
            // 
            // AddRegion
            // 
            this.AddRegion.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.AddRegion.Name = "rightClickMenuStrip";
            this.AddRegion.Size = new System.Drawing.Size(174, 26);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(173, 22);
            this.toolStripMenuItem1.Text = "Добавить область";
            // 
            // DrawingZone
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(775, 361);
            this.DoubleBuffered = true;
            this.Name = "DrawingZone";
            this.Text = "DrawingZone";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.DrawingZone_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DrawingZone_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.DrawingZone_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.DrawingZone_MouseUp);
            this.AddRegion.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip AddRegion;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
    }
}