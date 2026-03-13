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
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.trackBar2 = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.trackBar3 = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar3)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(537, 292);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(238, 121);
            this.button1.TabIndex = 0;
            this.button1.Text = "StartGame";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.StartButton);
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(12, 292);
            this.trackBar1.Maximum = 20;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(519, 45);
            this.trackBar1.TabIndex = 1;
            this.trackBar1.Scroll += new System.EventHandler(this.DASSensitivity);
            // 
            // trackBar2
            // 
            this.trackBar2.Location = new System.Drawing.Point(12, 368);
            this.trackBar2.Maximum = 20;
            this.trackBar2.Name = "trackBar2";
            this.trackBar2.Size = new System.Drawing.Size(519, 45);
            this.trackBar2.TabIndex = 2;
            this.trackBar2.Scroll += new System.EventHandler(this.ARRSensitivity);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Comic Sans MS", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(21, 244);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(192, 45);
            this.label1.TabIndex = 3;
            this.label1.Text = "DAS SENS";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Comic Sans MS", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(21, 320);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(187, 45);
            this.label2.TabIndex = 4;
            this.label2.Text = "ARR SENS";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Comic Sans MS", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(21, 148);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(208, 45);
            this.label3.TabIndex = 6;
            this.label3.Text = "SFD SENSE";
            // 
            // trackBar3
            // 
            this.trackBar3.Location = new System.Drawing.Point(12, 196);
            this.trackBar3.Maximum = 20;
            this.trackBar3.Name = "trackBar3";
            this.trackBar3.Size = new System.Drawing.Size(519, 45);
            this.trackBar3.TabIndex = 5;
            this.trackBar3.Scroll += new System.EventHandler(this.trackBar3_Scroll);
            // 
            // MenusScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.trackBar3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.trackBar2);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.button1);
            this.Name = "MenusScreen";
            this.Text = "Form2";
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.TrackBar trackBar2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TrackBar trackBar3;
    }
}