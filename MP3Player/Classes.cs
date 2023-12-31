﻿using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static MP3Player.Classes;

namespace MP3Player
{
    public class Classes
    {
        public class RoundedPictureBox : PictureBox
        {
            private int cornerRadius = 10; // Радиус закругления углов

            public int CornerRadius
            {
                get { return cornerRadius; }
                set
                {
                    cornerRadius = value;
                    Invalidate(); // Обновляем элемент при изменении радиуса закругления
                }
            }

            protected override void OnPaint(PaintEventArgs pe)
            {
                using (var path = new System.Drawing.Drawing2D.GraphicsPath())
                {
                    path.AddArc(0, 0, cornerRadius, cornerRadius, 180, 90); 
                    path.AddArc(Width - cornerRadius, 0, cornerRadius, cornerRadius, 270, 90); 
                    path.AddArc(Width - cornerRadius, Height - cornerRadius, cornerRadius, cornerRadius, 0, 90);
                    path.AddArc(0, Height - cornerRadius, cornerRadius, cornerRadius, 90, 90); 

                    pe.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    pe.Graphics.FillPath(new SolidBrush(BackColor), path); 

                    if (Image != null)
                    {
                        pe.Graphics.Clip = new Region(path); 
                        pe.Graphics.DrawImage(Image, ClientRectangle); 
                    }
                }
            }
        }

        public class RoundedButton : Button
        {
            private int cornerRadius = 10; // Радиус закругления углов
            public ButtonState ButtonState { get; set; } = ButtonState.Pause;

            public int CornerRadius
            {
                get { return cornerRadius; }
                set
                {
                    cornerRadius = value;
                    Invalidate(); // Обновляем элемент при изменении радиуса закругления
                }
            }

            protected override void OnPaint(PaintEventArgs e)
            {
                int radius = cornerRadius;
                GraphicsPath path = new GraphicsPath();
                // Создание пути с закругленными углами
                path.AddArc(0, 0, radius * 2, radius * 2, 180, 90);
                path.AddLine(radius, 0, Width - radius, 0);
                path.AddArc(Width - radius * 2, 0, radius * 2, radius * 2, 270, 90);
                path.AddLine(Width, radius, Width, Height - radius);
                path.AddArc(Width - radius * 2, Height - radius * 2, radius * 2, radius * 2, 0, 90);
                path.AddLine(Width - radius, Height, radius, Height);
                path.AddArc(0, Height - radius * 2, radius * 2, radius * 2, 90, 90);
                path.AddLine(0, Height - radius, 0, radius);

                this.Region = new Region(path); // Применение закругленного региона к кнопке

                base.OnPaint(e);
            }
        }        
    }

    public class Playlist
    {
        public string ImagePath { get; set; }
        public string FolderPath { get; set; }
        public string Title { get; set; }
        List<string> Exception { get; set; }

        public Playlist(string imagePath, string folderPath, string title) 
        {
            ImagePath = imagePath;
            FolderPath = folderPath;
            Title = title;
        }

        public void GetSongs()
        {
            // ПУТИ К ФАЙЛАМ ИЗ ПАПКИ ДОБАВЛЯЕТСЯ В Songs (только mp3)
        }
    }

    public enum ButtonState
    {
        Pause,
        Play
    }

    public enum ImageState
    {
        Pause,
        PauseEnter,
        PauseClick,
        Play,
        PlayEnter,
        PlayClick,
        Left,
        LeftEnter,
        LeftClick,
        Right,
        RightEnter,
        RightClick,
        Add,
        AddEnter,
        AddClick,
        AddFrom,
        AddFromEnter,
        AddFromClick,
        Settings,
        SettingsEnter,
        SettingsClick,
        Repeat,
        RepeatEnter,
        RepeatClick,
        RepeatEnabled,
        RepeatEnabledEnter,
        RepeatEnabledClick,
        Shuffle,
        ShuffleEnter,
        ShuffleClick,
        ShuffleEnabled,
        ShuffleEnabledEnter,
        ShuffleEnabledClick,
    }
}
