namespace VoiceControl
{
    partial class Settings
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
            this.groupBoxAdditionalCommands = new System.Windows.Forms.GroupBox();
            this.groupBoxSettings = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.ModeDraw = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // groupBoxAdditionalCommands
            // 
            this.groupBoxAdditionalCommands.Location = new System.Drawing.Point(916, 12);
            this.groupBoxAdditionalCommands.Name = "groupBoxAdditionalCommands";
            this.groupBoxAdditionalCommands.Size = new System.Drawing.Size(200, 100);
            this.groupBoxAdditionalCommands.TabIndex = 0;
            this.groupBoxAdditionalCommands.TabStop = false;
            this.groupBoxAdditionalCommands.Text = "Additional commands";
            // 
            // groupBoxSettings
            // 
            this.groupBoxSettings.Location = new System.Drawing.Point(12, 12);
            this.groupBoxSettings.Name = "groupBoxSettings";
            this.groupBoxSettings.Size = new System.Drawing.Size(200, 100);
            this.groupBoxSettings.TabIndex = 1;
            this.groupBoxSettings.TabStop = false;
            this.groupBoxSettings.Text = "Settings";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(474, 119);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(236, 87);
            this.button1.TabIndex = 2;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ModeDraw
            // 
            this.ModeDraw.AutoSize = true;
            this.ModeDraw.Location = new System.Drawing.Point(793, 21);
            this.ModeDraw.Name = "ModeDraw";
            this.ModeDraw.Size = new System.Drawing.Size(117, 17);
            this.ModeDraw.TabIndex = 3;
            this.ModeDraw.Text = "Режим отрисовки";
            this.ModeDraw.UseVisualStyleBackColor = true;
            this.ModeDraw.MouseCaptureChanged += new System.EventHandler(this.ModeDraw_MouseCaptureChanged);
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1128, 463);
            this.Controls.Add(this.ModeDraw);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBoxSettings);
            this.Controls.Add(this.groupBoxAdditionalCommands);
            this.Name = "Settings";
            this.Text = "Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxAdditionalCommands;
        private System.Windows.Forms.GroupBox groupBoxSettings;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox ModeDraw;
    }
}