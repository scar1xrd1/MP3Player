namespace MP3Player
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.labelCurrentSongTime = new System.Windows.Forms.Label();
            this.labelSongTime = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.pictureBox1.Location = new System.Drawing.Point(115, 70);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 50);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(111, 196);
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(104, 45);
            this.trackBar1.TabIndex = 1;
            this.trackBar1.TickStyle = System.Windows.Forms.TickStyle.None;
            // 
            // labelCurrentSongTime
            // 
            this.labelCurrentSongTime.AutoSize = true;
            this.labelCurrentSongTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.labelCurrentSongTime.ForeColor = System.Drawing.Color.White;
            this.labelCurrentSongTime.Location = new System.Drawing.Point(70, 266);
            this.labelCurrentSongTime.Name = "labelCurrentSongTime";
            this.labelCurrentSongTime.Size = new System.Drawing.Size(36, 17);
            this.labelCurrentSongTime.TabIndex = 2;
            this.labelCurrentSongTime.Text = "0:00";
            // 
            // labelSongTime
            // 
            this.labelSongTime.AutoSize = true;
            this.labelSongTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.labelSongTime.ForeColor = System.Drawing.Color.White;
            this.labelSongTime.Location = new System.Drawing.Point(70, 299);
            this.labelSongTime.Name = "labelSongTime";
            this.labelSongTime.Size = new System.Drawing.Size(36, 17);
            this.labelSongTime.TabIndex = 3;
            this.labelSongTime.Text = "0:00";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.ClientSize = new System.Drawing.Size(1174, 516);
            this.Controls.Add(this.labelSongTime);
            this.Controls.Add(this.labelCurrentSongTime);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Label labelCurrentSongTime;
        private System.Windows.Forms.Label labelSongTime;
    }
}

