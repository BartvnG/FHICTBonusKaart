namespace FHICT_Bonus_kaart_froms_test
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.numUpDown_Streak0 = new System.Windows.Forms.NumericUpDown();
            this.numUpDown_TotalPoints0 = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.numUpDown_Streak1 = new System.Windows.Forms.NumericUpDown();
            this.numUpDown_TotalPoints1 = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.bttn_WriteToDB0 = new System.Windows.Forms.Button();
            this.rb_CheckedIn0 = new System.Windows.Forms.RadioButton();
            this.rb_CheckedIn1 = new System.Windows.Forms.RadioButton();
            this.bttn_WriteToDB1 = new System.Windows.Forms.Button();
            this.bttn_ReadFromDB0 = new System.Windows.Forms.Button();
            this.bttn_ReadFromDB1 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDown_Streak0)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDown_TotalPoints0)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDown_Streak1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDown_TotalPoints1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Totaal:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.bttn_ReadFromDB0);
            this.groupBox1.Controls.Add(this.rb_CheckedIn0);
            this.groupBox1.Controls.Add(this.bttn_WriteToDB0);
            this.groupBox1.Controls.Add(this.numUpDown_Streak0);
            this.groupBox1.Controls.Add(this.numUpDown_TotalPoints0);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(267, 142);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Punten Desmond";
            // 
            // numUpDown_Streak0
            // 
            this.numUpDown_Streak0.Location = new System.Drawing.Point(140, 51);
            this.numUpDown_Streak0.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numUpDown_Streak0.Name = "numUpDown_Streak0";
            this.numUpDown_Streak0.Size = new System.Drawing.Size(120, 22);
            this.numUpDown_Streak0.TabIndex = 1;
            // 
            // numUpDown_TotalPoints0
            // 
            this.numUpDown_TotalPoints0.Location = new System.Drawing.Point(9, 52);
            this.numUpDown_TotalPoints0.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numUpDown_TotalPoints0.Name = "numUpDown_TotalPoints0";
            this.numUpDown_TotalPoints0.Size = new System.Drawing.Size(120, 22);
            this.numUpDown_TotalPoints0.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(137, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Streak:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.bttn_ReadFromDB1);
            this.groupBox2.Controls.Add(this.rb_CheckedIn1);
            this.groupBox2.Controls.Add(this.bttn_WriteToDB1);
            this.groupBox2.Controls.Add(this.numUpDown_Streak1);
            this.groupBox2.Controls.Add(this.numUpDown_TotalPoints1);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(285, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(267, 142);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Punten Bart";
            // 
            // numUpDown_Streak1
            // 
            this.numUpDown_Streak1.Location = new System.Drawing.Point(140, 52);
            this.numUpDown_Streak1.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numUpDown_Streak1.Name = "numUpDown_Streak1";
            this.numUpDown_Streak1.Size = new System.Drawing.Size(120, 22);
            this.numUpDown_Streak1.TabIndex = 2;
            // 
            // numUpDown_TotalPoints1
            // 
            this.numUpDown_TotalPoints1.Location = new System.Drawing.Point(9, 52);
            this.numUpDown_TotalPoints1.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numUpDown_TotalPoints1.Name = "numUpDown_TotalPoints1";
            this.numUpDown_TotalPoints1.Size = new System.Drawing.Size(120, 22);
            this.numUpDown_TotalPoints1.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(137, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 16);
            this.label3.TabIndex = 1;
            this.label3.Text = "Streak:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 32);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 16);
            this.label4.TabIndex = 0;
            this.label4.Text = "Totaal:";
            // 
            // bttn_WriteToDB0
            // 
            this.bttn_WriteToDB0.Location = new System.Drawing.Point(135, 102);
            this.bttn_WriteToDB0.Name = "bttn_WriteToDB0";
            this.bttn_WriteToDB0.Size = new System.Drawing.Size(125, 29);
            this.bttn_WriteToDB0.TabIndex = 1;
            this.bttn_WriteToDB0.Text = "Opslaan";
            this.bttn_WriteToDB0.UseVisualStyleBackColor = true;
            this.bttn_WriteToDB0.Click += new System.EventHandler(this.bttn_WriteToDB_Click);
            // 
            // rb_CheckedIn0
            // 
            this.rb_CheckedIn0.AutoSize = true;
            this.rb_CheckedIn0.Location = new System.Drawing.Point(9, 80);
            this.rb_CheckedIn0.Name = "rb_CheckedIn0";
            this.rb_CheckedIn0.Size = new System.Drawing.Size(95, 20);
            this.rb_CheckedIn0.TabIndex = 3;
            this.rb_CheckedIn0.TabStop = true;
            this.rb_CheckedIn0.Text = "Checked In";
            this.rb_CheckedIn0.UseVisualStyleBackColor = true;
            // 
            // rb_CheckedIn1
            // 
            this.rb_CheckedIn1.AutoSize = true;
            this.rb_CheckedIn1.Location = new System.Drawing.Point(9, 80);
            this.rb_CheckedIn1.Name = "rb_CheckedIn1";
            this.rb_CheckedIn1.Size = new System.Drawing.Size(95, 20);
            this.rb_CheckedIn1.TabIndex = 5;
            this.rb_CheckedIn1.TabStop = true;
            this.rb_CheckedIn1.Text = "Checked In";
            this.rb_CheckedIn1.UseVisualStyleBackColor = true;
            // 
            // bttn_WriteToDB1
            // 
            this.bttn_WriteToDB1.Location = new System.Drawing.Point(135, 102);
            this.bttn_WriteToDB1.Name = "bttn_WriteToDB1";
            this.bttn_WriteToDB1.Size = new System.Drawing.Size(125, 29);
            this.bttn_WriteToDB1.TabIndex = 2;
            this.bttn_WriteToDB1.Text = "Opslaan";
            this.bttn_WriteToDB1.UseVisualStyleBackColor = true;
            this.bttn_WriteToDB1.Click += new System.EventHandler(this.bttn_WriteToDB_Click);
            // 
            // bttn_ReadFromDB0
            // 
            this.bttn_ReadFromDB0.Location = new System.Drawing.Point(6, 102);
            this.bttn_ReadFromDB0.Name = "bttn_ReadFromDB0";
            this.bttn_ReadFromDB0.Size = new System.Drawing.Size(125, 29);
            this.bttn_ReadFromDB0.TabIndex = 1;
            this.bttn_ReadFromDB0.Text = "Haal data op";
            this.bttn_ReadFromDB0.UseVisualStyleBackColor = true;
            this.bttn_ReadFromDB0.Click += new System.EventHandler(this.bttn_WriteToDB_Click);
            // 
            // bttn_ReadFromDB1
            // 
            this.bttn_ReadFromDB1.Location = new System.Drawing.Point(4, 102);
            this.bttn_ReadFromDB1.Name = "bttn_ReadFromDB1";
            this.bttn_ReadFromDB1.Size = new System.Drawing.Size(125, 29);
            this.bttn_ReadFromDB1.TabIndex = 2;
            this.bttn_ReadFromDB1.Text = "Haal data op";
            this.bttn_ReadFromDB1.UseVisualStyleBackColor = true;
            this.bttn_ReadFromDB1.Click += new System.EventHandler(this.bttn_WriteToDB_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Location = new System.Drawing.Point(558, 18);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(137, 136);
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.MediumOrchid;
            this.ClientSize = new System.Drawing.Size(707, 170);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "FHICT Bonus - Admin";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDown_Streak0)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDown_TotalPoints0)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDown_Streak1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDown_TotalPoints1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numUpDown_Streak0;
        private System.Windows.Forms.NumericUpDown numUpDown_TotalPoints0;
        private System.Windows.Forms.NumericUpDown numUpDown_Streak1;
        private System.Windows.Forms.NumericUpDown numUpDown_TotalPoints1;
        private System.Windows.Forms.RadioButton rb_CheckedIn0;
        private System.Windows.Forms.Button bttn_WriteToDB0;
        private System.Windows.Forms.RadioButton rb_CheckedIn1;
        private System.Windows.Forms.Button bttn_WriteToDB1;
        private System.Windows.Forms.Button bttn_ReadFromDB0;
        private System.Windows.Forms.Button bttn_ReadFromDB1;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

