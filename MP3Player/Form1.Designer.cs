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
            this.components = new System.ComponentModel.Container();
            this.trackBarSongTime = new System.Windows.Forms.TrackBar();
            this.labelCurrentSongTime = new System.Windows.Forms.Label();
            this.labelSongTime = new System.Windows.Forms.Label();
            this.flowLayoutPanelPlaylist = new System.Windows.Forms.FlowLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.asdToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.panel2 = new System.Windows.Forms.Panel();
            this.flowLayoutPanelSongs = new System.Windows.Forms.FlowLayoutPanel();
            this.labelNameSong = new System.Windows.Forms.Label();
            this.trackBarVolume = new System.Windows.Forms.TrackBar();
            this.pictureBoxVolume = new System.Windows.Forms.PictureBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.openFileDialog2 = new System.Windows.Forms.OpenFileDialog();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.trackBarSongTime)).BeginInit();
            this.panel1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarVolume)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxVolume)).BeginInit();
            this.SuspendLayout();
            // 
            // trackBarSongTime
            // 
            this.trackBarSongTime.Location = new System.Drawing.Point(533, 36);
            this.trackBarSongTime.Name = "trackBarSongTime";
            this.trackBarSongTime.Size = new System.Drawing.Size(104, 45);
            this.trackBarSongTime.TabIndex = 1;
            this.trackBarSongTime.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBarSongTime.Scroll += new System.EventHandler(this.trackBarSongTime_Scroll);
            // 
            // labelCurrentSongTime
            // 
            this.labelCurrentSongTime.AutoSize = true;
            this.labelCurrentSongTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.labelCurrentSongTime.ForeColor = System.Drawing.Color.White;
            this.labelCurrentSongTime.Location = new System.Drawing.Point(492, 106);
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
            this.labelSongTime.Location = new System.Drawing.Point(492, 139);
            this.labelSongTime.Name = "labelSongTime";
            this.labelSongTime.Size = new System.Drawing.Size(36, 17);
            this.labelSongTime.TabIndex = 3;
            this.labelSongTime.Text = "0:00";
            // 
            // flowLayoutPanelPlaylist
            // 
            this.flowLayoutPanelPlaylist.AutoSize = true;
            this.flowLayoutPanelPlaylist.BackColor = System.Drawing.Color.Transparent;
            this.flowLayoutPanelPlaylist.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanelPlaylist.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanelPlaylist.Name = "flowLayoutPanelPlaylist";
            this.flowLayoutPanelPlaylist.Size = new System.Drawing.Size(227, 149);
            this.flowLayoutPanelPlaylist.TabIndex = 4;
            this.flowLayoutPanelPlaylist.WrapContents = false;
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.panel1.Controls.Add(this.flowLayoutPanelPlaylist);
            this.panel1.Location = new System.Drawing.Point(49, 69);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(331, 373);
            this.panel1.TabIndex = 5;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.asdToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(181, 26);
            // 
            // asdToolStripMenuItem
            // 
            this.asdToolStripMenuItem.Name = "asdToolStripMenuItem";
            this.asdToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.asdToolStripMenuItem.Text = "Изменить обложку";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "PNG|*.png|All Files|*.*|JPG|*.jpg";
            this.openFileDialog1.FilterIndex = 2;
            // 
            // panel2
            // 
            this.panel2.AutoScroll = true;
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.panel2.Controls.Add(this.flowLayoutPanelSongs);
            this.panel2.Location = new System.Drawing.Point(792, 72);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(331, 373);
            this.panel2.TabIndex = 6;
            // 
            // flowLayoutPanelSongs
            // 
            this.flowLayoutPanelSongs.AutoSize = true;
            this.flowLayoutPanelSongs.BackColor = System.Drawing.Color.Transparent;
            this.flowLayoutPanelSongs.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanelSongs.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanelSongs.Name = "flowLayoutPanelSongs";
            this.flowLayoutPanelSongs.Size = new System.Drawing.Size(312, 81);
            this.flowLayoutPanelSongs.TabIndex = 4;
            this.flowLayoutPanelSongs.WrapContents = false;
            // 
            // labelNameSong
            // 
            this.labelNameSong.AutoSize = true;
            this.labelNameSong.Font = new System.Drawing.Font("Segoe UI Black", 20F, System.Drawing.FontStyle.Bold);
            this.labelNameSong.ForeColor = System.Drawing.Color.White;
            this.labelNameSong.Location = new System.Drawing.Point(402, 125);
            this.labelNameSong.Name = "labelNameSong";
            this.labelNameSong.Size = new System.Drawing.Size(151, 37);
            this.labelNameSong.TabIndex = 7;
            this.labelNameSong.Text = "Нет трека";
            // 
            // trackBarVolume
            // 
            this.trackBarVolume.Location = new System.Drawing.Point(713, 274);
            this.trackBarVolume.Maximum = 100;
            this.trackBarVolume.Name = "trackBarVolume";
            this.trackBarVolume.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBarVolume.Size = new System.Drawing.Size(45, 104);
            this.trackBarVolume.TabIndex = 8;
            this.trackBarVolume.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBarVolume.Scroll += new System.EventHandler(this.trackBarVolume_Scroll);
            // 
            // pictureBoxVolume
            // 
            this.pictureBoxVolume.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxVolume.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBoxVolume.Location = new System.Drawing.Point(725, 418);
            this.pictureBoxVolume.Name = "pictureBoxVolume";
            this.pictureBoxVolume.Size = new System.Drawing.Size(25, 25);
            this.pictureBoxVolume.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxVolume.TabIndex = 9;
            this.pictureBoxVolume.TabStop = false;
            this.pictureBoxVolume.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBoxVolume_MouseClick);
            // 
            // openFileDialog2
            // 
            this.openFileDialog2.FileName = "openFileDialog2";
            this.openFileDialog2.Filter = "MP3|*.mp3";
            // 
            // timer1
            // 
            this.timer1.Interval = 10;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.ClientSize = new System.Drawing.Size(1176, 534);
            this.Controls.Add(this.pictureBoxVolume);
            this.Controls.Add(this.trackBarVolume);
            this.Controls.Add(this.labelNameSong);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.labelSongTime);
            this.Controls.Add(this.labelCurrentSongTime);
            this.Controls.Add(this.trackBarSongTime);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.trackBarSongTime)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarVolume)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxVolume)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TrackBar trackBarSongTime;
        private System.Windows.Forms.Label labelCurrentSongTime;
        private System.Windows.Forms.Label labelSongTime;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelPlaylist;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem asdToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelSongs;
        private System.Windows.Forms.Label labelNameSong;
        private System.Windows.Forms.TrackBar trackBarVolume;
        private System.Windows.Forms.PictureBox pictureBoxVolume;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog2;
        private System.Windows.Forms.Timer timer1;
    }
}

