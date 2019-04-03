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
            this.buttonEditRegion = new System.Windows.Forms.Button();
            this.buttonSelectRegion = new System.Windows.Forms.Button();
            this.buttonDeleteRegion = new System.Windows.Forms.Button();
            this.listBox = new System.Windows.Forms.ListBox();
            this.CreatingAddRegion = new System.Windows.Forms.Button();
            this.ModeDrawComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBoxSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxAdditionalCommands
            // 
            this.groupBoxAdditionalCommands.Location = new System.Drawing.Point(323, 12);
            this.groupBoxAdditionalCommands.Name = "groupBoxAdditionalCommands";
            this.groupBoxAdditionalCommands.Size = new System.Drawing.Size(200, 100);
            this.groupBoxAdditionalCommands.TabIndex = 0;
            this.groupBoxAdditionalCommands.TabStop = false;
            this.groupBoxAdditionalCommands.Text = "Additional commands";
            // 
            // groupBoxSettings
            // 
            this.groupBoxSettings.Controls.Add(this.buttonSelectRegion);
            this.groupBoxSettings.Controls.Add(this.buttonEditRegion);
            this.groupBoxSettings.Controls.Add(this.buttonDeleteRegion);
            this.groupBoxSettings.Controls.Add(this.listBox);
            this.groupBoxSettings.Controls.Add(this.CreatingAddRegion);
            this.groupBoxSettings.Controls.Add(this.ModeDrawComboBox);
            this.groupBoxSettings.Controls.Add(this.label1);
            this.groupBoxSettings.Location = new System.Drawing.Point(12, 12);
            this.groupBoxSettings.Name = "groupBoxSettings";
            this.groupBoxSettings.Size = new System.Drawing.Size(233, 211);
            this.groupBoxSettings.TabIndex = 1;
            this.groupBoxSettings.TabStop = false;
            this.groupBoxSettings.Text = "Альтернативные конфигурации";
            // 
            // buttonEditRegion
            // 
            this.buttonEditRegion.Location = new System.Drawing.Point(133, 81);
            this.buttonEditRegion.Name = "buttonEditRegion";
            this.buttonEditRegion.Size = new System.Drawing.Size(92, 25);
            this.buttonEditRegion.TabIndex = 10;
            this.buttonEditRegion.Text = "Редактировать";
            this.buttonEditRegion.UseVisualStyleBackColor = true;
            this.buttonEditRegion.Click += new System.EventHandler(this.buttonEditRegion_Click);
            // 
            // buttonSelectRegion
            // 
            this.buttonSelectRegion.Location = new System.Drawing.Point(133, 112);
            this.buttonSelectRegion.Name = "buttonSelectRegion";
            this.buttonSelectRegion.Size = new System.Drawing.Size(92, 25);
            this.buttonSelectRegion.TabIndex = 9;
            this.buttonSelectRegion.Text = "Применить";
            this.buttonSelectRegion.UseVisualStyleBackColor = true;
            this.buttonSelectRegion.Click += new System.EventHandler(this.buttonSelectRegion_Click);
            // 
            // buttonDeleteRegion
            // 
            this.buttonDeleteRegion.Location = new System.Drawing.Point(133, 50);
            this.buttonDeleteRegion.Name = "buttonDeleteRegion";
            this.buttonDeleteRegion.Size = new System.Drawing.Size(92, 25);
            this.buttonDeleteRegion.TabIndex = 8;
            this.buttonDeleteRegion.Text = "Удалить";
            this.buttonDeleteRegion.UseVisualStyleBackColor = true;
            this.buttonDeleteRegion.Click += new System.EventHandler(this.buttonDeleteRegion_Click);
            // 
            // listBox
            // 
            this.listBox.FormattingEnabled = true;
            this.listBox.Location = new System.Drawing.Point(7, 19);
            this.listBox.Name = "listBox";
            this.listBox.Size = new System.Drawing.Size(120, 134);
            this.listBox.TabIndex = 7;
            // 
            // CreatingAddRegion
            // 
            this.CreatingAddRegion.Location = new System.Drawing.Point(133, 19);
            this.CreatingAddRegion.Name = "CreatingAddRegion";
            this.CreatingAddRegion.Size = new System.Drawing.Size(92, 25);
            this.CreatingAddRegion.TabIndex = 2;
            this.CreatingAddRegion.Text = "Новая";
            this.CreatingAddRegion.UseVisualStyleBackColor = true;
            this.CreatingAddRegion.Click += new System.EventHandler(this.button1_Click);
            // 
            // ModeDrawComboBox
            // 
            this.ModeDrawComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ModeDrawComboBox.FormattingEnabled = true;
            this.ModeDrawComboBox.Items.AddRange(new object[] {
            "Классический",
            "Альтернативный"});
            this.ModeDrawComboBox.Location = new System.Drawing.Point(9, 172);
            this.ModeDrawComboBox.Name = "ModeDrawComboBox";
            this.ModeDrawComboBox.Size = new System.Drawing.Size(121, 21);
            this.ModeDrawComboBox.TabIndex = 4;
            this.ModeDrawComboBox.SelectedIndexChanged += new System.EventHandler(this.ModeDrawComboBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 156);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Режим отрисовки";
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(631, 354);
            this.Controls.Add(this.groupBoxSettings);
            this.Controls.Add(this.groupBoxAdditionalCommands);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Settings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            this.groupBoxSettings.ResumeLayout(false);
            this.groupBoxSettings.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxAdditionalCommands;
        private System.Windows.Forms.GroupBox groupBoxSettings;
        private System.Windows.Forms.Button CreatingAddRegion;
        private System.Windows.Forms.ComboBox ModeDrawComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox listBox;
        private System.Windows.Forms.Button buttonSelectRegion;
        private System.Windows.Forms.Button buttonDeleteRegion;
        private System.Windows.Forms.Button buttonEditRegion;
    }
}