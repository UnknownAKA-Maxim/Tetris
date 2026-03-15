namespace Tetris
{
    partial class MenusScreen
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
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SoftDropTrackBar = new System.Windows.Forms.TrackBar();
            this.button2 = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.DelayAutoShiftTrackBar = new System.Windows.Forms.TrackBar();
            this.AutoRepeatRateTrackBar = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.SoftDropTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DelayAutoShiftTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AutoRepeatRateTrackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(894, 428);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(199, 285);
            this.button1.TabIndex = 0;
            this.button1.Text = "StartGame";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.StartButton);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Comic Sans MS", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(15, 524);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(192, 45);
            this.label1.TabIndex = 3;
            this.label1.Text = "DAS SENS";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Comic Sans MS", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(15, 620);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(187, 45);
            this.label2.TabIndex = 4;
            this.label2.Text = "ARR SENS";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Comic Sans MS", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(15, 428);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(208, 45);
            this.label3.TabIndex = 6;
            this.label3.Text = "SFD SENSE";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Comic Sans MS", 72F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(175, 52);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(732, 135);
            this.label4.TabIndex = 7;
            this.label4.Text = "Game Settings";
            // 
            // SoftDropTrackBar
            // 
            this.SoftDropTrackBar.AccessibleName = "SoftDropTrackBar";
            this.SoftDropTrackBar.LargeChange = 1;
            this.SoftDropTrackBar.Location = new System.Drawing.Point(12, 476);
            this.SoftDropTrackBar.Maximum = 20;
            this.SoftDropTrackBar.Minimum = 1;
            this.SoftDropTrackBar.Name = "SoftDropTrackBar";
            this.SoftDropTrackBar.Size = new System.Drawing.Size(854, 45);
            this.SoftDropTrackBar.TabIndex = 5;
            this.SoftDropTrackBar.Value = 1;
            this.SoftDropTrackBar.Scroll += new System.EventHandler(this.SFDSensitivity);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(23, 339);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(371, 86);
            this.button2.TabIndex = 8;
            this.button2.Text = "Default Settings(Recommended)";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.DefaultSettings);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(266, 453);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(39, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Values";
            // 
            // DelayAutoShiftTrackBar
            // 
            this.DelayAutoShiftTrackBar.LargeChange = 1;
            this.DelayAutoShiftTrackBar.Location = new System.Drawing.Point(12, 572);
            this.DelayAutoShiftTrackBar.Maximum = 20;
            this.DelayAutoShiftTrackBar.Minimum = 1;
            this.DelayAutoShiftTrackBar.Name = "DelayAutoShiftTrackBar";
            this.DelayAutoShiftTrackBar.Size = new System.Drawing.Size(854, 45);
            this.DelayAutoShiftTrackBar.TabIndex = 11;
            this.DelayAutoShiftTrackBar.Value = 1;
            this.DelayAutoShiftTrackBar.Scroll += new System.EventHandler(this.DASSensitivity);
            // 
            // AutoRepeatRateTrackBar
            // 
            this.AutoRepeatRateTrackBar.LargeChange = 1;
            this.AutoRepeatRateTrackBar.Location = new System.Drawing.Point(12, 668);
            this.AutoRepeatRateTrackBar.Maximum = 20;
            this.AutoRepeatRateTrackBar.Minimum = 1;
            this.AutoRepeatRateTrackBar.Name = "AutoRepeatRateTrackBar";
            this.AutoRepeatRateTrackBar.Size = new System.Drawing.Size(854, 45);
            this.AutoRepeatRateTrackBar.TabIndex = 12;
            this.AutoRepeatRateTrackBar.Value = 1;
            this.AutoRepeatRateTrackBar.Scroll += new System.EventHandler(this.ARRSensitivity);
            // 
            // MenusScreen
            // 
            this.AccessibleName = "DelayAuto";
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1105, 774);
            this.Controls.Add(this.AutoRepeatRateTrackBar);
            this.Controls.Add(this.DelayAutoShiftTrackBar);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.SoftDropTrackBar);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Name = "MenusScreen";
            this.Text = "Form2";
            ((System.ComponentModel.ISupportInitialize)(this.SoftDropTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DelayAutoShiftTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AutoRepeatRateTrackBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TrackBar SoftDropTrackBar;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TrackBar DelayAutoShiftTrackBar;
        private System.Windows.Forms.TrackBar AutoRepeatRateTrackBar;
    }
}