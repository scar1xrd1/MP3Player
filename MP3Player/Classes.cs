using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NAudio.Wave;

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
            private WaveOutEvent outputDevice; // Объект WaveOutEvent для управления воспроизведением

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

            public RoundedButton()
            {
                // Привязка обработчика события Click к кнопке
                this.Click += PausePlayButton_Click;
                outputDevice = new WaveOutEvent();
            }

            public void PlaySong(string songPath)
            {
                if (outputDevice.PlaybackState == PlaybackState.Paused) // Если воспроизведение на паузе, возобновляем его
                {
                    outputDevice.Play();
                }
                else // Иначе начинаем воспроизведение новой песни
                {
                    // Здесь вы можете добавить код для воспроизведения песни,
                    // используя переданный путь к песне (songPath).
                    // Например, вы можете использовать библиотеку NAudio для воспроизведения аудио.

                    // Пример кода для воспроизведения песни с использованием NAudio:
                    using (var audioFile = new AudioFileReader(songPath))
                    {
                        outputDevice.Init(audioFile);
                        outputDevice.Play();

                        // Можно добавить обработчики событий для отслеживания статуса воспроизведения,
                        // паузы, остановки и т. д.
                        // outputDevice.PlaybackStopped += (sender, args) => { /* Код обработки события остановки воспроизведения */ };
                        // outputDevice.Pause();
                        // outputDevice.Resume();
                        // и так далее...
                    }
                }
            }

            private void PausePlayButton_Click(object sender, EventArgs e)
            {
                // Переключение состояния кнопки
                if (ButtonState == ButtonState.Pause)
                {
                    ButtonState = ButtonState.Play;
                    //Text = "Пуск";
                    // Выполнение действия при пуске
                    // ...
                    // Проигрывание песни
                    string songPath = "C:\\Users\\LEOKE\\Downloads\\shadowraze.mp3";
                    PlaySong(songPath);
                }
                else
                {
                    ButtonState = ButtonState.Pause;
                    //Text = "Пауза";
                    // Выполнение действия при паузе
                    // Поставить воспроизведение на паузу
                    if (outputDevice.PlaybackState == PlaybackState.Playing)
                    {
                        outputDevice.Pause();
                    }
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


        public class Playlist
        {
            public string ImagePath { get; set; }
            public string FolderPath { get; set; }
            public string Title { get; set; }
            List<string> Songs { get; set; }

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
            PauseFocus,
            PauseClick,
            Play,
            PlayFocus,
            PlayClick,
            Left,
            LeftFocus,
            LeftClick,
            Right,
            RightFocus,
            RightClick,
            Add,
            AddFocus,
            AddClick,
            AddFrom,
            AddFromFocus,
            AddFromClick
        }
    }
}