using System;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using static MP3Player.Classes;

namespace MP3Player
{
    public partial class Form1 : Form
    {
        private bool isDraggingForm = false; // Для перетаскивания формы
        private Point dragStartPointForm;

        private int cornerRadiusButtons = 8; // Радиус закругления кнопок

        // ЭЛЕМЕНТЫ
        RoundedPictureBox roundedPictureBox;
        RoundedButton roundedButtonPause;
        RoundedButton roundedButtonLeft;
        RoundedButton roundedButtonRight;

        public Form1()
        {
            InitializeComponent();
            MyInitialize();
        }


        private void MyInitialize()
        {
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
            // roundedButtonRight
            roundedButtonRight = new RoundedButton();
            roundedButtonRight.BackColor = Color.Transparent;
            roundedButtonRight.BackgroundImage = Image.FromFile("Images/ButtonImages/Right.png");
            roundedButtonRight.BackgroundImageLayout = ImageLayout.Zoom;
            roundedButtonRight.Size = new Size(50, 50);
            roundedButtonRight.FlatStyle = FlatStyle.Flat;
            roundedButtonRight.FlatAppearance.BorderSize = 0;
            roundedButtonRight.CornerRadius = cornerRadiusButtons;
            // trackBar1
            trackBar1.Size = new Size(pictureBox1.Width * 3, trackBar1.Size.Height);
            // Controls
            Controls.Add(roundedPictureBox);
            Controls.Add(roundedButtonPause);
            Controls.Add(roundedButtonLeft);
            Controls.Add(roundedButtonRight);
            // other 
            MoveControls();
        }

        private void SetButtonImage(Button button, ImageState image)
        {
            if (image == ImageState.Pause) button.BackgroundImage = Image.FromFile("Images/ButtonImages/ButtonStatePause.png");
            else if (image == ImageState.PauseFocus) button.BackgroundImage = Image.FromFile("Images/ButtonImages/ButtonStatePauseFocusEnter.png");
            else if (image == ImageState.PauseClick) button.BackgroundImage = Image.FromFile("Images/ButtonImages/ButtonStatePauseClick.png");
            else if (image == ImageState.Play) button.BackgroundImage = Image.FromFile("Images/ButtonImages/ButtonStatePlay.png");
            else if (image == ImageState.PlayFocus) button.BackgroundImage = Image.FromFile("Images/ButtonImages/ButtonStatePlayFocusEnter.png");
            else if (image == ImageState.PlayClick) button.BackgroundImage = Image.FromFile("Images/ButtonImages/ButtonStatePlayClick.png");
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
            roundedPictureBox.Location = new Point(Width / 2 - roundedPictureBox.Width / 2, Height - roundedPictureBox.Height * 2);
            roundedButtonPause.Location = new Point(roundedPictureBox.Location.X + roundedButtonPause.Width * 2 - 86, roundedPictureBox.Location.Y + roundedPictureBox.Height + 60);
            roundedButtonLeft.Location = new Point(roundedButtonPause.Left - roundedButtonLeft.Width - 10, roundedButtonPause.Location.Y + roundedButtonLeft.Height / 4 + 2);
            roundedButtonRight.Location = new Point(roundedButtonPause.Right + 10, roundedButtonPause.Location.Y + roundedButtonRight.Height / 4 + 2);
            trackBar1.Location = new Point(roundedPictureBox.Location.X - trackBar1.Width/6, roundedPictureBox.Bottom + 16);
            labelCurrentSongTime.Location = new Point(trackBar1.Left - labelCurrentSongTime.Width - 2, trackBar1.Location.Y);
            labelSongTime.Location = new Point(trackBar1.Right, trackBar1.Location.Y);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            PrivateFontCollection fontCollection = new PrivateFontCollection();

            fontCollection.AddFontFile("Fonts/VisbyCF-Bold.ttf");
            fontCollection.AddFontFile("Fonts/VisbyCF-Heavy.ttf");
            fontCollection.AddFontFile("Fonts/VisbyCF-Medium.ttf");

            FontFamily family1 = fontCollection.Families[0]; // VisbyCF-Medium  Тонкий
            FontFamily family2 = fontCollection.Families[1]; // VisbyCF-Heavy   Для заголовков
            FontFamily family3 = fontCollection.Families[2]; // VisbyCF-Bold    Для текста

            labelCurrentSongTime.Font = new Font(family1, 10);
            labelSongTime.Font = new Font(family1, 10);
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            MoveControls();
        }
    }
}
