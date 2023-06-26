﻿using NAudio.Wave;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text;
//using System.Reflection.Emit;
//using System.Reflection.Emit;
using System.Windows.Forms;
using static MP3Player.Classes;

namespace MP3Player
{
    public partial class Form1 : Form
    {
        private int cornerRadiusButtons = 8; // Радиус закругления кнопок

        // ЭЛЕМЕНТЫ
        RoundedPictureBox roundedPictureBox;
        RoundedButton roundedButtonPause;
        RoundedButton roundedButtonLeft;
        RoundedButton roundedButtonRight;
        RoundedButton roundedButtonAddPlaylist;
        RoundedButton roundedButtonAddSong;
        RoundedButton roundedButtonAddPlaylistFrom;

        List<Playlist> playlists = new List<Playlist>();
        List<string> songs;
        int currentPlaylist = 0;
        int currentSong = 0;

        int volume;
        bool muted;

        FontFamily[] fontFamilies = FontFamily.Families;

        string[] formats = { ".png" };

        //bool stopThreads = false;
        bool loadedData = false;

        public Form1()
        {
            InitializeComponent();
            MyInitialize();

            AllowDrop = true;

            //AddPlaylist("Images/NoSong.png", "MyPlaylist", "Playlists/MyPlaylist");

            //GetTextDialog getTextDialog = new GetTextDialog();
            //getTextDialog.ShowDialog();

            if(!Directory.Exists("Data")) Directory.CreateDirectory("Data");
            if (!Directory.Exists("Playlists")) Directory.CreateDirectory("Playlists");
            if (!File.Exists("Data/MyData.txt")) using (FileStream fileStream = File.Create("Data/MyData.txt"));
            if (!File.Exists("Data/Playlists.txt")) using (FileStream fileStream = File.Create("Data/Playlists.txt"));

            //SaveData();
            LoadData();
        }

        private void SaveData()
        {
            MyData myData = new MyData(currentPlaylist, currentSong, volume, muted);

            //File.Delete("Data/MyData.txt");
            //File.Delete("Data/Playlists.txt");
            //Directory.Delete("Data");

            //Directory.CreateDirectory("Data");
            //File.Create("Data/MyData.txt");
            //File.Create("Data/Playlists.txt");

            using (FileStream fs = new FileStream("Data/MyData.txt", FileMode.OpenOrCreate))
            {
                byte[] bytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(myData)); // Преобразование строки в массив байтов
                fs.Write(bytes, 0, bytes.Length); // Запись массива байтов в файл
            }

            File.WriteAllText("Data/Playlists.txt", "");
            using (FileStream fs = new FileStream("Data/Playlists.txt", FileMode.OpenOrCreate))
            {
                byte[] bytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(playlists)); // Преобразование строки в массив байтов
                fs.Write(bytes, 0, bytes.Length); // Запись массива байтов в файл
            }

            //ForSave();
        }

        private void LoadData()
        {
            try
            {
                MyData myData = JsonConvert.DeserializeObject<MyData>(File.ReadAllText("Data/MyData.txt"));
                List<Playlist> PlayLists = JsonConvert.DeserializeObject<List<Playlist>>(File.ReadAllText("Data/Playlists.txt"));

                if (PlayLists != null)
                {
                    foreach (var playlist in PlayLists) AddPlaylist(playlist.ImagePath, playlist.Title, playlist.FolderPath);

                    currentPlaylist = myData.CurrentPlaylist;
                    currentSong = myData.CurrentSong;
                    volume = myData.Volume;
                    muted = myData.Muted;

                    AddSongsFromPlaylist(playlists[currentPlaylist]);
                    for (int i = 0; i < flowLayoutPanelPlaylist.Controls.Count; i++) flowLayoutPanelPlaylist.Controls[i].Controls[2].ForeColor = Color.White;
                    flowLayoutPanelPlaylist.Controls[currentPlaylist].Controls[2].ForeColor = Color.FromArgb(33, 191, 90);

                    SetVolume(volume);
                    SetSongFromIndex(currentSong);
                    loadedData = true;
                }
            }
            catch (Exception) { }
        }

        private void SetSongFromIndex(int index)
        {
            using (var audioFile = new AudioFileReader(songs[index]))
            {
                var tagLib = TagLib.File.Create(songs[index]);

                labelNameSong.Text = tagLib.Tag.Title;

                if (audioFile.TotalTime.Hours > 0)
                {
                    if (audioFile.TotalTime.Seconds > 9) labelSongTime.Text = $"{audioFile.TotalTime.Hours}:{audioFile.TotalTime.Minutes}:{audioFile.TotalTime.Seconds}";
                    else labelSongTime.Text = $"{audioFile.TotalTime.Hours}:{audioFile.TotalTime.Minutes}:0{audioFile.TotalTime.Seconds}";
                }
                else
                {
                    if (audioFile.TotalTime.Seconds > 9) labelSongTime.Text = $"{audioFile.TotalTime.Minutes}:{audioFile.TotalTime.Seconds}";
                    else labelSongTime.Text = $"{audioFile.TotalTime.Minutes}:0{audioFile.TotalTime.Seconds}";
                }

                trackBarSongTime.Maximum = (int)Math.Round(audioFile.TotalTime.TotalSeconds, 0);
                trackBarSongTime.Minimum = 0;
                trackBarSongTime.Value = 0;

                roundedPictureBox.Image = GetAlbumArtFromMp3(songs[index]);
                if (roundedPictureBox.Image == null) roundedPictureBox.Image = Image.FromFile("Images/NoSong.png");

                SizeF textSize = TextRenderer.MeasureText(labelNameSong.Text, labelNameSong.Font);
                int labelWidth = (int)textSize.Width;
                int labelHeight = (int)textSize.Height;
                int pictureBoxCenterX = roundedPictureBox.Location.X + roundedPictureBox.Width / 2;
                //int pictureBoxCenterY = roundedPictureBox.Location.Y + roundedPictureBox.Height / 2;
                labelNameSong.Size = new Size(labelWidth, labelHeight);
                labelNameSong.Location = new Point(pictureBoxCenterX - labelWidth / 2, roundedPictureBox.Top - labelNameSong.Height - 10);
            }
        }

        private void SetVolume(int volume)
        {
            if (volume == -1) muted = true;
            else if (volume == -2) muted = false;

            if (muted)
            {
                pictureBoxVolume.Image = Image.FromFile("Images/VolumeMute.png");
            }

            if (!muted)
            {
                if (volume != -2) this.volume = volume;
                else { /*поставить прежнюю громкость*/}

                pictureBoxVolume.Image = Image.FromFile("Images/VolumeMax.png");
            }

            SaveData();
        }

        private void AddSongsFromPlaylist(Playlist playlist)
        {
            flowLayoutPanelSongs.Controls.Clear();
            songs = new List<string>();

            string[] songs_ = Directory.GetFiles(playlist.FolderPath);

            foreach (var song in songs_) if (Path.GetExtension(song) == ".mp3") songs.Add(song);

            foreach (var song in songs)
            {
                RoundedPictureBox tag = new RoundedPictureBox();
                Panel objectContainer = new Panel();
                Label title = new Label();
                Label performer = new Label();
                Label songLen = new Label();

                objectContainer.BackColor = Color.Transparent;
                objectContainer.Size = new Size(300, 50);

                tag.Image = GetAlbumArtFromMp3(song);
                if (tag.Image == null) tag.Image = Image.FromFile("Images/NoSong.png");
                tag.BackColor = Color.Transparent;
                tag.Size = new Size(50, 50);

                title.ForeColor = Color.White;
                //title.Font = GetFont("Font1-Light", 10);
                title.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                title.Location = new Point(60, 0);
                title.AutoSize = false;
                title.Width = flowLayoutPanelSongs.Width;
                title.MouseClick += Title_MouseClick;

                performer.ForeColor = Color.Gray;
                //performer.Font = GetFont("Font1-Light", 10);
                performer.Font = new Font("Segoe UI", 10, FontStyle.Regular);
                performer.Location = new Point(60, title.Location.Y + title.Height);
                performer.AutoSize = false;
                performer.Width = flowLayoutPanelSongs.Width;

                songLen.ForeColor = Color.White;
                songLen.Font = GetFont("Visby-Medium", 10);
                songLen.Location = new Point(title.Right - 100, title.Location.Y + songLen.Height + 10);

                using (var audioFile = new AudioFileReader(song))
                {
                    var tagLib = TagLib.File.Create(song);
                    title.Text = tagLib.Tag.Title;
                    performer.Text = tagLib.Tag.FirstPerformer;

                    if (audioFile.TotalTime.Hours > 0)
                    {
                        if (audioFile.TotalTime.Seconds > 9) songLen.Text = $"{audioFile.TotalTime.Hours}:{audioFile.TotalTime.Minutes}:{audioFile.TotalTime.Seconds}";
                        else songLen.Text = $"{audioFile.TotalTime.Hours}:{audioFile.TotalTime.Minutes}:0{audioFile.TotalTime.Seconds}";
                    }
                    else
                    {
                        if (audioFile.TotalTime.Seconds > 9) songLen.Text = $"{audioFile.TotalTime.Minutes}:{audioFile.TotalTime.Seconds}";
                        else songLen.Text = $"{audioFile.TotalTime.Minutes}:0{audioFile.TotalTime.Seconds}";
                    }
                }

                if (title.Text == string.Empty) title.Text = Path.GetFileNameWithoutExtension(song);//title.Text = "Без названия";
                if (performer.Text == string.Empty) performer.Text = "Без исполнителя";

                objectContainer.Controls.Add(tag);
                //objectContainer.Controls.Add(songLen);
                objectContainer.Controls.Add(title);
                objectContainer.Controls.Add(performer);

                try { currentSong = JsonConvert.DeserializeObject<MyData>(File.ReadAllText("Data/MyData.txt")).CurrentSong; } catch (Exception){ currentSong = 0; }
                //currentSong = 0;
                flowLayoutPanelSongs.Controls.Add(objectContainer);
            }
        }

        private void Title_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Label label = (Label)sender;

                int iter = 0;
                foreach (var song in songs)
                {
                    using (var audioFile = new AudioFileReader(song))
                    {
                        var tagLib = TagLib.File.Create(song);

                        if (label.Text == tagLib.Tag.Title || label.Text == Path.GetFileNameWithoutExtension(song))
                        {
                            labelCurrentSongTime.Text = "0:00";

                            labelNameSong.Text = tagLib.Tag.Title;
                            if (label.Text == Path.GetFileNameWithoutExtension(song)) labelNameSong.Text = Path.GetFileNameWithoutExtension(song);

                            if (audioFile.TotalTime.Hours > 0)
                            {
                                if (audioFile.TotalTime.Seconds > 9) labelSongTime.Text = $"{audioFile.TotalTime.Hours}:{audioFile.TotalTime.Minutes}:{audioFile.TotalTime.Seconds}";
                                else labelSongTime.Text = $"{audioFile.TotalTime.Hours}:{audioFile.TotalTime.Minutes}:0{audioFile.TotalTime.Seconds}";
                            }
                            else
                            {
                                if (audioFile.TotalTime.Seconds > 9) labelSongTime.Text = $"{audioFile.TotalTime.Minutes}:{audioFile.TotalTime.Seconds}";
                                else labelSongTime.Text = $"{audioFile.TotalTime.Minutes}:0{audioFile.TotalTime.Seconds}";
                            }

                            trackBarSongTime.Maximum = (int)Math.Round((double)audioFile.TotalTime.TotalSeconds, 0);

                            // MessageBox.Show(audioFile.TotalTime.TotalSeconds.ToString());

                            trackBarSongTime.Minimum = 0;
                            trackBarSongTime.Value = 0;

                            roundedPictureBox.Image = GetAlbumArtFromMp3(song);
                            if (roundedPictureBox.Image == null) roundedPictureBox.Image = Image.FromFile("Images/NoSong.png");
                            currentSong = iter;

                            SizeF textSize = TextRenderer.MeasureText(labelNameSong.Text, labelNameSong.Font);
                            int labelWidth = (int)textSize.Width;
                            int labelHeight = (int)textSize.Height;
                            int pictureBoxCenterX = roundedPictureBox.Location.X + roundedPictureBox.Width / 2;
                            //int pictureBoxCenterY = roundedPictureBox.Location.Y + roundedPictureBox.Height / 2;
                            labelNameSong.Size = new Size(labelWidth, labelHeight);
                            labelNameSong.Location = new Point(pictureBoxCenterX - labelWidth / 2, roundedPictureBox.Top - labelNameSong.Height - 10);

                            break;
                        }
                    }
                    iter++;
                }
            }
            SaveData();
        }

        private void AddPlaylist(string imagePath, string name, string path)
        {
            if (loadedData)
            {
                List<Playlist> PlayLists = JsonConvert.DeserializeObject<List<Playlist>>(File.ReadAllText("Data/Playlists.txt"));
                foreach (var playlist in PlayLists) if (playlist.Title == name) return;
            }

            if (flowLayoutPanelPlaylist.Controls.Count > 0) if (flowLayoutPanelPlaylist.Controls[currentPlaylist].Controls[2].ForeColor == Color.FromArgb(33, 191, 90)) flowLayoutPanelPlaylist.Controls[currentPlaylist].Controls[2].ForeColor = Color.White;

            RoundedPictureBox cover = new RoundedPictureBox();
            Panel objectContainer = new Panel();
            Label title = new Label();
            Label songsCount = new Label();

            objectContainer.BackColor = Color.Transparent;
            objectContainer.Size = new Size(300, objectContainer.Height);

            if (!File.Exists(imagePath)) imagePath = "Images/NoSong.png";

            cover.Image = Image.FromFile(imagePath);
            cover.BackColor = Color.Transparent;
            cover.Size = new Size(100, 100);
            cover.DragEnter += Cover_DragEnter;
            cover.MouseUp += Cover_MouseUp;
            cover.DragDrop += Cover_DragDrop;

            title.AutoSize = false;
            title.Width = flowLayoutPanelSongs.Width;
            title.Text = name;
            title.ForeColor = Color.White;
            //title.Font = GetFont("Visby-Heavy", 15);
            //title.Font = GetFont("Font1-Light", 15);
            title.Font = new Font("Segoe UI", 15, FontStyle.Bold);
            title.Location = new Point(110, 0);
            title.MouseClick += Title1_MouseClick;
            title.Height = 100;

            songsCount.Text = $"Треки: {Directory.GetFiles(path).Length.ToString()}";
            songsCount.ForeColor = Color.White;
            //songsCount.Font = GetFont("Visby-Medium", 10);
            //songsCount.Font = GetFont("Font1-Medium", 10);
            songsCount.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            songsCount.Location = new Point(110, title.Location.Y + songsCount.Height + 15);

            objectContainer.Controls.Add(cover);
            objectContainer.Controls.Add(songsCount);
            objectContainer.Controls.Add(title);

            playlists.Add(new Playlist(imagePath, path, name));
            currentPlaylist = playlists.Count - 1;
            AddSongsFromPlaylist(playlists[currentPlaylist]);
            flowLayoutPanelPlaylist.Controls.Add(objectContainer);
            flowLayoutPanelPlaylist.Controls[currentPlaylist].Controls[2].ForeColor = Color.FromArgb(33, 191, 90);
        }

        private void Title1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Label label = (Label)sender;

                int iter = 0;
                foreach (var playlist in playlists)
                {
                    if (playlist.Title == label.Text)
                    {
                        flowLayoutPanelPlaylist.Controls[currentPlaylist].Controls[2].ForeColor = Color.White;
                        currentPlaylist = iter;
                        flowLayoutPanelPlaylist.Controls[currentPlaylist].Controls[2].ForeColor = Color.FromArgb(33, 191, 90);
                        AddSongsFromPlaylist(playlist);
                        break;
                    }
                    iter++;
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                Label label = (Label)sender;

                GetTextDialog getTextDialog = new GetTextDialog();
                getTextDialog.StartPosition = FormStartPosition.Manual;
                //getTextDialog.Location = PointToScreen(new Point(e.Location.X, e.Location.Y));

                //GetText.Location = PointToScreen(new Point(e.Location.X, e.Location.Y));
                //Screen screen = Screen.FromHandle(Handle);
                //getTextDialog.Left = screen.Bounds.Left + e.X;
                //getTextDialog.Top = screen.Bounds.Top + e.Y;
                //MessageBox.Show(e.Y.ToString());
                getTextDialog.Location = new Point(Cursor.Position.X - e.X, Cursor.Position.Y - e.Y);
                getTextDialog.ShowDialog();

                if (GetText.Text != string.Empty)
                {
                    foreach (var playlist in playlists)
                    {
                        if (GetText.Text == playlist.Title) return;
                    }
                    for (int i = 0; i < playlists.Count; i++)
                    {
                        if (playlists[i].Title == label.Text)
                        {
                            label.Text = GetText.Text;
                            playlists[i].Title = label.Text;
                            break;
                        }
                    }
                }
            }
            SaveData();
        }

        private void Cover_DragDrop(object sender, DragEventArgs e)
        {
            RoundedPictureBox picture = (RoundedPictureBox)sender;

            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (files.Length > 0)
                {
                    string imagePath = files[0];

                    try
                    {
                        if (!formats.Contains(Path.GetExtension(imagePath))) { throw new Exception("Неверный формат"); }
                        System.Drawing.Image image = System.Drawing.Image.FromFile(imagePath);
                        picture.Image = image;
                    }
                    catch (Exception ex) { MessageBox.Show("Не удалось загрузить изображение: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }
        }

        private void Cover_DragEnter(object sender, DragEventArgs e) { if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy; }

        private void Cover_MouseUp(object sender, MouseEventArgs e)
        {
            RoundedPictureBox pictureBox = (RoundedPictureBox)sender;

            if (e.Button == MouseButtons.Right)
            {
                //contextMenuStrip1.Show(roundedPictureBox, e.Location);

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    Control container = pictureBox.Parent;

                    foreach (var playlist in playlists)
                    {
                        if (container.Controls[2].Text == playlist.Title)
                        {
                            if (!Directory.Exists("Playlists/Images")) Directory.CreateDirectory("Playlists/Images");
                            string path = $"Playlists/Images/{Path.GetFileName(openFileDialog1.FileName)}";
                            File.Copy(openFileDialog1.FileName, path);

                            playlist.ImagePath = path;
                            break;
                        }
                    }
                    SaveData();

                    pictureBox.Image = Image.FromFile(openFileDialog1.FileName);
                }
            }
        }

        private Image GetAlbumArtFromMp3(string filePath)
        {
            try
            {
                using (var file = TagLib.File.Create(filePath))
                {
                    var tag = file.Tag;
                    if (tag.Pictures.Length >= 1)
                    {
                        var picture = tag.Pictures[0];
                        using (var stream = new MemoryStream(picture.Data.Data)) { return Image.FromStream(stream); }
                    }
                }
            }
            catch (Exception) { return Image.FromFile("Images/NoSong.png"); }

            return null;
        }

        private void MyInitialize()
        {
            // Form1
            MinimumSize = new Size(800, 470);
            // roundedPictureBox
            roundedPictureBox = new RoundedPictureBox();
            roundedPictureBox.Size = new Size(200, 200);
            roundedPictureBox.Image = Image.FromFile("Images/NoSong.png");
            roundedPictureBox.BackColor = Color.Transparent; //Color.FromArgb(16, 255,255,255);
            roundedPictureBox.CornerRadius = 40;
            // roundedButtonPause
            roundedButtonPause = new RoundedButton();
            roundedButtonPause.BackColor = Color.Transparent;
            roundedButtonPause.BackgroundImage = Image.FromFile("Images/ButtonImages/ButtonStatePause.png");
            roundedButtonPause.BackgroundImageLayout = ImageLayout.Zoom;
            roundedButtonPause.Size = new Size(75, 75);
            roundedButtonPause.FlatStyle = FlatStyle.Flat;
            roundedButtonPause.FlatAppearance.BorderSize = 0;
            roundedButtonPause.CornerRadius = cornerRadiusButtons;
            roundedButtonPause.MouseEnter += RoundedButton1_MouseEnter;
            roundedButtonPause.MouseUp += RoundedButton1_MouseEnter;
            roundedButtonPause.MouseLeave += RoundedButton1_MouseLeave;
            roundedButtonPause.MouseClick += RoundedButton1_MouseClick;
            roundedButtonPause.MouseDown += RoundedButton1_MouseDown;
            // roundedButtonLeft
            roundedButtonLeft = new RoundedButton();
            roundedButtonLeft.BackColor = Color.Transparent;
            roundedButtonLeft.BackgroundImage = Image.FromFile("Images/ButtonImages/Left.png");
            roundedButtonLeft.BackgroundImageLayout = ImageLayout.Zoom;
            roundedButtonLeft.Size = new Size(50, 50);
            roundedButtonLeft.FlatStyle = FlatStyle.Flat;
            roundedButtonLeft.FlatAppearance.BorderSize = 0;
            roundedButtonLeft.CornerRadius = cornerRadiusButtons;
            roundedButtonLeft.MouseDown += RoundedButtonLeft_MouseDown;
            roundedButtonLeft.MouseLeave += RoundedButtonLeft_MouseLeave;
            roundedButtonLeft.MouseEnter += RoundedButtonLeft_MouseEnter;
            roundedButtonLeft.MouseUp += RoundedButtonLeft_MouseEnter;
            roundedButtonLeft.MouseClick += RoundedButtonLeft_MouseClick;
            // roundedButtonRight
            roundedButtonRight = new RoundedButton();
            roundedButtonRight.BackColor = Color.Transparent;
            roundedButtonRight.BackgroundImage = Image.FromFile("Images/ButtonImages/Right.png");
            roundedButtonRight.BackgroundImageLayout = ImageLayout.Zoom;
            roundedButtonRight.Size = new Size(50, 50);
            roundedButtonRight.FlatStyle = FlatStyle.Flat;
            roundedButtonRight.FlatAppearance.BorderSize = 0;
            roundedButtonRight.CornerRadius = cornerRadiusButtons;
            roundedButtonRight.MouseDown += RoundedButtonRight_MouseDown;
            roundedButtonRight.MouseLeave += RoundedButtonRight_MouseLeave;
            roundedButtonRight.MouseEnter += RoundedButtonRight_MouseEnter;
            roundedButtonRight.MouseUp += RoundedButtonRight_MouseEnter;
            roundedButtonRight.MouseClick += RoundedButtonRight_MouseClick;
            // roundedButtonAddPlaylist
            roundedButtonAddPlaylist = new RoundedButton();
            roundedButtonAddPlaylist.BackColor = Color.Transparent;
            roundedButtonAddPlaylist.BackgroundImage = Image.FromFile("Images/ButtonImages/Add.png");
            roundedButtonAddPlaylist.BackgroundImageLayout = ImageLayout.Zoom;
            roundedButtonAddPlaylist.Size = new Size(50, 50);
            roundedButtonAddPlaylist.FlatStyle = FlatStyle.Flat;
            roundedButtonAddPlaylist.FlatAppearance.BorderSize = 0;
            roundedButtonAddPlaylist.CornerRadius = cornerRadiusButtons;
            roundedButtonAddPlaylist.MouseDown += RoundedButtonAddPlaylist_MouseDown;
            roundedButtonAddPlaylist.MouseLeave += RoundedButtonAddPlaylist_MouseLeave;
            roundedButtonAddPlaylist.MouseEnter += RoundedButtonAddPlaylist_MouseEnter;
            roundedButtonAddPlaylist.MouseUp += RoundedButtonAddPlaylist_MouseEnter;
            roundedButtonAddPlaylist.MouseClick += RoundedButtonAddPlaylist_MouseClick;
            // roundedButtonAddSong
            roundedButtonAddSong = new RoundedButton();
            roundedButtonAddSong.BackColor = Color.Transparent;
            roundedButtonAddSong.BackgroundImage = Image.FromFile("Images/ButtonImages/Add.png");
            roundedButtonAddSong.BackgroundImageLayout = ImageLayout.Zoom;
            roundedButtonAddSong.Size = new Size(50, 50);
            roundedButtonAddSong.FlatStyle = FlatStyle.Flat;
            roundedButtonAddSong.FlatAppearance.BorderSize = 0;
            roundedButtonAddSong.CornerRadius = cornerRadiusButtons;
            roundedButtonAddSong.MouseDown += RoundedButtonAddSong_MouseDown;
            roundedButtonAddSong.MouseLeave += RoundedButtonAddSong_MouseLeave;
            roundedButtonAddSong.MouseEnter += RoundedButtonAddSong_MouseEnter;
            roundedButtonAddSong.MouseUp += RoundedButtonAddSong_MouseEnter;
            roundedButtonAddSong.MouseClick += RoundedButtonAddSong_MouseClick;
            // roundedButtonAddPlaylistFrom
            roundedButtonAddPlaylistFrom = new RoundedButton();
            roundedButtonAddPlaylistFrom.BackColor = Color.Transparent;
            roundedButtonAddPlaylistFrom.BackgroundImage = Image.FromFile("Images/ButtonImages/AddFrom.png");
            roundedButtonAddPlaylistFrom.BackgroundImageLayout = ImageLayout.Zoom;
            roundedButtonAddPlaylistFrom.Size = new Size(50, 50);
            roundedButtonAddPlaylistFrom.FlatStyle = FlatStyle.Flat;
            roundedButtonAddPlaylistFrom.FlatAppearance.BorderSize = 0;
            roundedButtonAddPlaylistFrom.CornerRadius = cornerRadiusButtons;
            roundedButtonAddPlaylistFrom.MouseDown += RoundedButtonAddPlaylistFrom_MouseDown;
            roundedButtonAddPlaylistFrom.MouseLeave += RoundedButtonAddPlaylistFrom_MouseLeave;
            roundedButtonAddPlaylistFrom.MouseEnter += RoundedButtonAddPlaylistFrom_MouseEnter;
            roundedButtonAddPlaylistFrom.MouseUp += RoundedButtonAddPlaylistFrom_MouseEnter;
            roundedButtonAddPlaylistFrom.MouseClick += RoundedButtonAddPlaylistFrom_MouseClick;
            // trackBarSongTime
            trackBarSongTime.Size = new Size(roundedPictureBox.Width * 2 - trackBarSongTime.Width, trackBarSongTime.Size.Height);
            // trackBarVolume
            trackBarVolume.Size = new Size(trackBarVolume.Width, roundedPictureBox.Height - pictureBoxVolume.Height - 3);
            //labelNameSong
            //labelNameSong.TextAlign = ContentAlignment.MiddleCenter;
            //labelNameSong.AutoSize = false;
            // Controls
            Controls.Add(roundedPictureBox);
            Controls.Add(roundedButtonPause);
            Controls.Add(roundedButtonLeft);
            Controls.Add(roundedButtonRight);
            Controls.Add(roundedButtonAddPlaylist);
            Controls.Add(roundedButtonAddSong);
            Controls.Add(roundedButtonAddPlaylistFrom);
            // other 
            MoveControls();
        }

        private void RoundedButtonAddPlaylistFrom_MouseEnter(object sender, EventArgs e) => SetButtonImage(roundedButtonAddPlaylistFrom, ImageState.AddFromFocus);
        private void RoundedButtonAddPlaylistFrom_MouseLeave(object sender, EventArgs e) => SetButtonImage(roundedButtonAddPlaylistFrom, ImageState.AddFrom);
        private void RoundedButtonAddPlaylistFrom_MouseDown(object sender, MouseEventArgs e) => SetButtonImage(roundedButtonAddPlaylistFrom, ImageState.AddFromClick);
        private void RoundedButtonAddSong_MouseEnter(object sender, EventArgs e) => SetButtonImage(roundedButtonAddSong, ImageState.AddFocus);
        private void RoundedButtonAddSong_MouseLeave(object sender, EventArgs e) => SetButtonImage(roundedButtonAddSong, ImageState.Add);
        private void RoundedButtonAddSong_MouseDown(object sender, MouseEventArgs e) => SetButtonImage(roundedButtonAddSong, ImageState.AddClick);
        private void RoundedButtonAddPlaylist_MouseEnter(object sender, EventArgs e) => SetButtonImage(roundedButtonAddPlaylist, ImageState.AddFocus);
        private void RoundedButtonAddPlaylist_MouseLeave(object sender, EventArgs e) => SetButtonImage(roundedButtonAddPlaylist, ImageState.Add);
        private void RoundedButtonAddPlaylist_MouseDown(object sender, MouseEventArgs e) => SetButtonImage(roundedButtonAddPlaylist, ImageState.AddClick);
        private void RoundedButtonRight_MouseEnter(object sender, EventArgs e) => SetButtonImage(roundedButtonRight, ImageState.RightFocus);
        private void RoundedButtonRight_MouseLeave(object sender, EventArgs e) => SetButtonImage(roundedButtonRight, ImageState.Right);
        private void RoundedButtonRight_MouseDown(object sender, MouseEventArgs e) => SetButtonImage(roundedButtonRight, ImageState.RightClick);
        private void RoundedButtonLeft_MouseEnter(object sender, EventArgs e) => SetButtonImage(roundedButtonLeft, ImageState.LeftFocus);
        private void RoundedButtonLeft_MouseLeave(object sender, EventArgs e) => SetButtonImage(roundedButtonLeft, ImageState.Left);
        private void RoundedButtonLeft_MouseDown(object sender, MouseEventArgs e) => SetButtonImage(roundedButtonLeft, ImageState.LeftClick);

        private void RoundedButtonAddPlaylist_MouseClick(object sender, MouseEventArgs e)
        {
            SetButtonImage(roundedButtonAddPlaylist, ImageState.Add);

            if (!Directory.Exists("Playlists/Playlist"))
            {
                Directory.CreateDirectory("Playlists/Playlist");
                AddPlaylist("", "Playlist", "Playlists/Playlist");
            }
            else
            {
                int iter = 1;
                while (true)
                {
                    if (!Directory.Exists($"Playlists/Playlist {iter}"))
                    {
                        Directory.CreateDirectory($"Playlists/Playlist {iter}");
                        AddPlaylist("", $"Playlist {iter}", $"Playlists/Playlist {iter}");
                        break;
                    }
                    else iter++;
                }

            }

            // ДОБАВИТЬ НОВЫЙ ПЛЕЙЛИСТ
        }

        private void RoundedButtonAddPlaylistFrom_MouseClick(object sender, MouseEventArgs e)
        {
            SetButtonImage(roundedButtonAddPlaylistFrom, ImageState.AddFrom);

            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                AddPlaylist("", Path.GetFileName(folderBrowserDialog1.SelectedPath), folderBrowserDialog1.SelectedPath);
            }

            // ДОБАВИТЬ ПЛЕЙЛИСТ ИЗ ПАПКИ
        }

        private void RoundedButtonAddSong_MouseClick(object sender, MouseEventArgs e)
        {
            SetButtonImage(roundedButtonAddSong, ImageState.Add);

            if(playlists.Count > 0)
            {
                if (openFileDialog2.ShowDialog() == DialogResult.OK)
                {
                    if (!File.Exists(Path.Combine(playlists[currentPlaylist].FolderPath, Path.GetFileName(openFileDialog2.FileName))))
                    {
                        File.Copy(openFileDialog2.FileName, Path.Combine(playlists[currentPlaylist].FolderPath, Path.GetFileName(openFileDialog2.FileName)));
                        AddSongsFromPlaylist(playlists[currentPlaylist]);
                    }
                }
            }            

            // ДОБАВИТЬ ПЕСНЮ В ПЛЕЙЛИСТ
        }


        private void RoundedButtonLeft_MouseClick(object sender, MouseEventArgs e)
        {
            SetButtonImage(roundedButtonLeft, ImageState.Left);

            // ТРЕК ПЕРЕКЛЮЧАЕТСЯ НАЗАД
        }

        private void RoundedButtonRight_MouseClick(object sender, MouseEventArgs e)
        {
            SetButtonImage(roundedButtonRight, ImageState.Right);

            // ТРЕК ПЕРЕКЛЮЧАЕТСЯ ВПЕРЁД
        }

        private void SetButtonImage(Button button, ImageState image)
        {
            if (image == ImageState.Pause) button.BackgroundImage = Image.FromFile("Images/ButtonImages/ButtonStatePause.png");
            else if (image == ImageState.PauseFocus) button.BackgroundImage = Image.FromFile("Images/ButtonImages/ButtonStatePauseFocusEnter.png");
            else if (image == ImageState.PauseClick) button.BackgroundImage = Image.FromFile("Images/ButtonImages/ButtonStatePauseClick.png");
            else if (image == ImageState.Play) button.BackgroundImage = Image.FromFile("Images/ButtonImages/ButtonStatePlay.png");
            else if (image == ImageState.PlayFocus) button.BackgroundImage = Image.FromFile("Images/ButtonImages/ButtonStatePlayFocusEnter.png");
            else if (image == ImageState.PlayClick) button.BackgroundImage = Image.FromFile("Images/ButtonImages/ButtonStatePlayClick.png");
            else if (image == ImageState.Left) button.BackgroundImage = Image.FromFile("Images/ButtonImages/Left.png");
            else if (image == ImageState.LeftFocus) button.BackgroundImage = Image.FromFile("Images/ButtonImages/LeftEnter.png");
            else if (image == ImageState.LeftClick) button.BackgroundImage = Image.FromFile("Images/ButtonImages/LeftClick.png");
            else if (image == ImageState.Right) button.BackgroundImage = Image.FromFile("Images/ButtonImages/Right.png");
            else if (image == ImageState.RightFocus) button.BackgroundImage = Image.FromFile("Images/ButtonImages/RightEnter.png");
            else if (image == ImageState.RightClick) button.BackgroundImage = Image.FromFile("Images/ButtonImages/RightClick.png");
            else if (image == ImageState.Add) button.BackgroundImage = Image.FromFile("Images/ButtonImages/Add.png");
            else if (image == ImageState.AddFocus) button.BackgroundImage = Image.FromFile("Images/ButtonImages/AddEnter.png");
            else if (image == ImageState.AddClick) button.BackgroundImage = Image.FromFile("Images/ButtonImages/AddClick.png");
            else if (image == ImageState.AddFrom) button.BackgroundImage = Image.FromFile("Images/ButtonImages/AddFrom.png");
            else if (image == ImageState.AddFromFocus) button.BackgroundImage = Image.FromFile("Images/ButtonImages/AddFromEnter.png");
            else if (image == ImageState.AddFromClick) button.BackgroundImage = Image.FromFile("Images/ButtonImages/AddFromClick.png");
        }

        private void RoundedButton1_MouseDown(object sender, MouseEventArgs e)
        {
            if (roundedButtonPause.ButtonState == ButtonState.Pause) SetButtonImage(roundedButtonPause, ImageState.PauseClick);
            else SetButtonImage(roundedButtonPause, ImageState.PlayClick);
        }
        private void RoundedButton1_MouseLeave(object sender, EventArgs e)
        {
            if (roundedButtonPause.ButtonState == ButtonState.Pause) SetButtonImage(roundedButtonPause, ImageState.Pause);
            else SetButtonImage(roundedButtonPause, ImageState.Play);
        }

        private void RoundedButton1_MouseEnter(object sender, EventArgs e)
        {
            if (roundedButtonPause.ButtonState == ButtonState.Pause) SetButtonImage(roundedButtonPause, ImageState.PauseFocus);
            else SetButtonImage(roundedButtonPause, ImageState.PlayFocus);
        }

        private void RoundedButton1_MouseClick(object sender, MouseEventArgs e)
        {
            if (roundedButtonPause.ButtonState == ButtonState.Pause)
            {
                SetButtonImage(roundedButtonPause, ImageState.Play);
                roundedButtonPause.ButtonState = ButtonState.Play;

                // ПЕСНЯ НАЧИНАЕТ ИГРАТЬ
            }
            else
            {
                SetButtonImage(roundedButtonPause, ImageState.Pause);
                roundedButtonPause.ButtonState = ButtonState.Pause;

                // ПЕСНЯ СТАВИТСЯ НА ПАУЗУ
            }
        }

        private void MoveControls()
        {
            roundedPictureBox.Location = new Point(Width / 2 - roundedPictureBox.Width / 2, Height - roundedPictureBox.Height * 2 + 10/*Height - roundedPictureBox.Height * 2*/);
            roundedButtonPause.Location = new Point(roundedPictureBox.Location.X + roundedButtonPause.Width * 2 - 87, roundedPictureBox.Location.Y + roundedPictureBox.Height + 60);
            roundedButtonLeft.Location = new Point(roundedButtonPause.Left - roundedButtonLeft.Width - 10, roundedButtonPause.Location.Y + roundedButtonLeft.Height / 4 + 2);
            roundedButtonRight.Location = new Point(roundedButtonPause.Right + 10, roundedButtonPause.Location.Y + roundedButtonRight.Height / 4 + 2);
            trackBarSongTime.Location = new Point(roundedPictureBox.Location.X - trackBarSongTime.Width / 6, roundedPictureBox.Bottom + 16);
            labelCurrentSongTime.Location = new Point(trackBarSongTime.Left - labelCurrentSongTime.Width - 2, trackBarSongTime.Location.Y);
            labelSongTime.Location = new Point(trackBarSongTime.Right, trackBarSongTime.Location.Y);
            pictureBoxVolume.Location = new Point(roundedPictureBox.Right + 15, roundedPictureBox.Bottom - pictureBoxVolume.Height);
            trackBarVolume.Location = new Point(roundedPictureBox.Right + 15, roundedPictureBox.Location.Y);

            //pictureBox1.Location = new Point(labelSongTime.Right, labelSongTime.Location.Y);
            //labelNameSong.Location = new Point(roundedPictureBox.Location.X, roundedPictureBox.Top - labelNameSong.Height - 10);
            SizeF textSize = TextRenderer.MeasureText(labelNameSong.Text, labelNameSong.Font);
            int labelWidth = (int)textSize.Width;
            int labelHeight = (int)textSize.Height;
            int pictureBoxCenterX = roundedPictureBox.Location.X + roundedPictureBox.Width / 2;
            labelNameSong.Size = new Size(labelWidth, labelHeight);
            labelNameSong.Location = new Point(pictureBoxCenterX - labelWidth / 2, roundedPictureBox.Top - labelNameSong.Height - 10);
            //panel1.Location = new Point(roundedPictureBox.Left- panel1.Width - panel1.Width /2 , roundedPictureBox.Location.Y);

            panel1.Location = new Point(roundedPictureBox.Left - panel1.Width - panel1.Width / 2, 0);
            panel1.Size = new Size(panel1.Width, Height - 40);
            panel2.Location = new Point(roundedPictureBox.Right + panel2.Width / 2, 0);
            panel2.Size = new Size(panel2.Width, Height - 40);
            //panel1.Dock = DockStyle.Left;
            roundedButtonAddPlaylist.Location = new Point(panel1.Location.X + panel1.Width + roundedButtonAddPlaylist.Width / 4, panel1.Location.Y + roundedButtonAddPlaylist.Height / 4);
            roundedButtonAddSong.Location = new Point(Convert.ToInt32(panel2.Location.X - roundedButtonAddSong.Width * 1.2), panel2.Location.Y + roundedButtonAddSong.Height / 4);
            roundedButtonAddPlaylistFrom.Location = new Point(roundedButtonAddPlaylist.Location.X, roundedButtonAddPlaylist.Bottom + roundedButtonAddPlaylistFrom.Height / 4);
        }

        private Font GetFont(string fontName, float fontSize)
        {
            PrivateFontCollection fontCollection = new PrivateFontCollection();

            if (fontName == "Visby-Bold") { fontCollection.AddFontFile("Fonts/VisbyCF-Bold.ttf"); return new Font(fontCollection.Families[0], fontSize); }
            else if (fontName == "Visby-Heavy") { fontCollection.AddFontFile("Fonts/VisbyCF-Heavy.ttf"); return new Font(fontCollection.Families[0], fontSize); }
            else if (fontName == "Visby-Medium") { fontCollection.AddFontFile("Fonts/VisbyCF-Medium.ttf"); return new Font(fontCollection.Families[0], fontSize); }
            else if (fontName == "Font1-Medium") { fontCollection.AddFontFile("Fonts/Font1-Medium.otf"); return new Font(fontCollection.Families[0], fontSize); }
            else if (fontName == "Font1-Bold") { fontCollection.AddFontFile("Fonts/Font1-Bold.otf"); return new Font(fontCollection.Families[0], fontSize); }
            else if (fontName == "Font1-Light") { fontCollection.AddFontFile("Fonts/Font1-Light.otf"); return new Font(fontCollection.Families[0], fontSize); }

            return new Font(DefaultFont, FontStyle.Bold);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // VisbyCF-Medium  Тонкий
            // VisbyCF-Heavy   Для заголовков
            // VisbyCF-Bold    Для текста

            labelCurrentSongTime.Font = GetFont("Visby-Bold", 10);
            labelSongTime.Font = GetFont("Visby-Bold", 10);
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            Text = $"{Width}x{Height}";
            MoveControls();
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            //if (Size.Width < 1080 || Size.Height < 300)
            //{
            //    // Устанавливаем минимальный размер формы на текущий размер
            //    MinimumSize = ;
            //}
            //else
            //{
            //    // Снимаем ограничение на минимальный размер формы
            //    MinimumSize = new Size(0, 0);
            //}
        }

        private void pictureBoxVolume_MouseClick(object sender, MouseEventArgs e)
        {
            if (!muted) SetVolume(-1);
            else SetVolume(-2);

        }

        private void trackBarSongTime_Scroll(object sender, EventArgs e)
        {
            //if (trackBarSongTime.Value < 10) labelCurrentSongTime.Text = $"0:0{trackBarSongTime.Value}";
            //else if (trackBarSongTime.Value < 60) labelCurrentSongTime.Text = $"0:{trackBarSongTime.Value}";
            //else if (trackBarSongTime.Value - trackBarSongTime.Value / 2 * 60 < 10) labelCurrentSongTime.Text = $"{trackBarSongTime.Value / 60}:0{trackBarSongTime.Value - trackBarSongTime.Value / 2 * 60}";
            //else labelCurrentSongTime.Text = $"{trackBarSongTime.Value / 60}:{trackBarSongTime.Value - trackBarSongTime.Value / 2 * 60}";
            TimeSpan timeSpan = TimeSpan.FromSeconds(trackBarSongTime.Value);

            if (timeSpan.Seconds < 10) labelCurrentSongTime.Text = $"{trackBarSongTime.Value / 60}:0{timeSpan.Seconds}";
            else labelCurrentSongTime.Text = $"{trackBarSongTime.Value / 60}:{timeSpan.Seconds}";
        }
    }

    public class MyData
    {
        public int CurrentPlaylist { get; set; }
        public int CurrentSong { get; set; }
        public int Volume { get; set; }
        public bool Muted { get; set; }

        public MyData(int currentPlaylist, int currentSong, int volume, bool muted)
        {
            CurrentPlaylist = currentPlaylist;
            CurrentSong = currentSong;
            Volume = volume;
            Muted = muted;
        }
    }

    public class GetText
    {
        public static string Text { get; set; }
        public static Point Location { get; set; }
    }
}