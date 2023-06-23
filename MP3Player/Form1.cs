using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static MP3Player.Classes;

namespace MP3Player
{
    public partial class Form1 : Form
    {
        private bool isDraggingForm = false; // Для перетаскивания формы
        private Point dragStartPointForm;

        private int cornerRadius = 10; // Радиус закругления

        // ЭЛЕМЕНТЫ
        RoundedPictureBox roundedPictureBox1;
        RoundedButton roundedButton1;

        public Form1()
        {
            InitializeComponent();
            MyInitialize();
        }

        
        private void MyInitialize()
        {
            // roundedPictureBox1
            roundedPictureBox1 = new RoundedPictureBox();
            roundedPictureBox1.Size = new Size(200, 200);            
            roundedPictureBox1.Image = Image.FromFile("Images/NoSong.png");
            roundedPictureBox1.BackColor = Color.Transparent; //Color.FromArgb(16, 255,255,255);
            roundedPictureBox1.CornerRadius = 40;
            // roundedButton1
            roundedButton1 = new RoundedButton();
            roundedButton1.BackColor = Color.Transparent;
            roundedButton1.BackgroundImage = Image.FromFile("Images/ButtonImages/ButtonStatePause.png");
            roundedButton1.BackgroundImageLayout = ImageLayout.Zoom;
            roundedButton1.Size = new Size(75, 75);
            roundedButton1.FlatStyle = FlatStyle.Flat;
            roundedButton1.FlatAppearance.BorderSize = 0;
            roundedButton1.CornerRadius = 16;
            roundedButton1.MouseEnter += RoundedButton1_MouseEnter;
            roundedButton1.MouseUp += RoundedButton1_MouseEnter;
            roundedButton1.MouseLeave += RoundedButton1_MouseLeave;
            roundedButton1.MouseClick += RoundedButton1_MouseClick;
            roundedButton1.MouseDown += RoundedButton1_MouseDown;
            // Controls
            Controls.Add(roundedPictureBox1);
            Controls.Add(roundedButton1);
            // other 
            MoveControls();
        }

        private void SetButtonImage(Button button, ImageState image)
        {
            if(image == ImageState.Pause) button.BackgroundImage = Image.FromFile("Images/ButtonImages/ButtonStatePause.png");
            else if(image == ImageState.PauseFocus) button.BackgroundImage = Image.FromFile("Images/ButtonImages/ButtonStatePauseFocusEnter.png");
            else if(image == ImageState.PauseClick) button.BackgroundImage = Image.FromFile("Images/ButtonImages/ButtonStatePauseClick.png");
            else if(image == ImageState.Play) button.BackgroundImage = Image.FromFile("Images/ButtonImages/ButtonStatePlay.png");
            else if (image == ImageState.PlayFocus) button.BackgroundImage = Image.FromFile("Images/ButtonImages/ButtonStatePlayFocusEnter.png");
            else if (image == ImageState.PlayClick) button.BackgroundImage = Image.FromFile("Images/ButtonImages/ButtonStatePlayClick.png");
        }

        private void RoundedButton1_MouseDown(object sender, MouseEventArgs e)
        {
            if (roundedButton1.ButtonState == ButtonState.Pause) SetButtonImage(roundedButton1, ImageState.PauseClick);
            else SetButtonImage(roundedButton1, ImageState.PlayClick);
        }
        private void RoundedButton1_MouseLeave(object sender, EventArgs e)
        {
            if (roundedButton1.ButtonState == ButtonState.Pause) SetButtonImage(roundedButton1, ImageState.Pause);
            else SetButtonImage(roundedButton1, ImageState.Play);
        }

        private void RoundedButton1_MouseEnter(object sender, EventArgs e)
        {
            if(roundedButton1.ButtonState == ButtonState.Pause) SetButtonImage(roundedButton1, ImageState.PauseFocus);
            else SetButtonImage(roundedButton1, ImageState.PlayFocus);
        }
       
        private void RoundedButton1_MouseClick(object sender, MouseEventArgs e)
        {
            if (roundedButton1.ButtonState == ButtonState.Pause)
            {
                SetButtonImage(roundedButton1, ImageState.Play);
                roundedButton1.ButtonState = ButtonState.Play;

                // ПЕСНЯ НАЧИНАЕТ ИГРАТЬ
            }
            else
            {
                SetButtonImage(roundedButton1, ImageState.Pause);
                roundedButton1.ButtonState = ButtonState.Pause;
            }
        }

        private void MoveControls()
        {
            roundedPictureBox1.Location = new Point(Width / 2 - roundedPictureBox1.Width / 2, 70);
            roundedButton1.Location = new Point(roundedPictureBox1.Location.X + roundedButton1.Width*2 - 83, roundedPictureBox1.Height + 85);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Тут будет загрузка шрифтов
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            MoveControls();
        }
    }    
}
