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
            this.CreatingNewArea = new System.Windows.Forms.Button();
            this.ModeDrawComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
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
            // CreatingNewArea
            // 
            this.CreatingNewArea.Location = new System.Drawing.Point(790, 55);
            this.CreatingNewArea.Name = "CreatingNewArea";
            this.CreatingNewArea.Size = new System.Drawing.Size(121, 37);
            this.CreatingNewArea.TabIndex = 2;
            this.CreatingNewArea.Text = "Новая область";
            this.CreatingNewArea.UseVisualStyleBackColor = true;
            this.CreatingNewArea.Click += new System.EventHandler(this.button1_Click);
            // 
            // ModeDrawComboBox
            // 
            this.ModeDrawComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ModeDrawComboBox.FormattingEnabled = true;
            this.ModeDrawComboBox.Items.AddRange(new object[] {
            "Классический",
            "Альтернативный"});
            this.ModeDrawComboBox.Location = new System.Drawing.Point(790, 28);
            this.ModeDrawComboBox.Name = "ModeDrawComboBox";
            this.ModeDrawComboBox.Size = new System.Drawing.Size(121, 21);
            this.ModeDrawComboBox.TabIndex = 4;
            this.ModeDrawComboBox.SelectedIndexChanged += new System.EventHandler(this.ModeDrawComboBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(787, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Режим отрисовки";
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1128, 463);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ModeDrawComboBox);
            this.Controls.Add(this.CreatingNewArea);
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
        private System.Windows.Forms.Button CreatingNewArea;
        private System.Windows.Forms.ComboBox ModeDrawComboBox;
        private System.Windows.Forms.Label label1;
    }
}