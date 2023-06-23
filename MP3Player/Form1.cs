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

namespace MP3Player
{
    public partial class Form1 : Form
    {
        private bool isDraggingForm = false; // Для перетаскивания формы
        private Point dragStartPointForm;

        private int cornerRadius = 10; // Радиус закругления

        // ЭЛЕМЕНТЫ
        RoundedPictureBox roundedPictureBox1;

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
            // Controls
            Controls.Add(roundedPictureBox1);

            // other 
            MoveControls();
        }

        private void MoveControls()
        {
            roundedPictureBox1.Location = new Point(Width / 2 - roundedPictureBox1.Width / 2, 50);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //GraphicsPath roundedRect = new GraphicsPath();
            //roundedRect.AddArc(0, 0, 2 * cornerRadius, 2 * cornerRadius, 180, 90);
            //roundedRect.AddArc(Width - 2 * cornerRadius, 0, 2 * cornerRadius, 2 * cornerRadius, 270, 90);
            //roundedRect.AddArc(Width - 2 * cornerRadius, Height - 2 * cornerRadius, 2 * cornerRadius, 2 * cornerRadius, 0, 90);
            //roundedRect.AddArc(0, Height - 2 * cornerRadius, 2 * cornerRadius, 2 * cornerRadius, 90, 90);
            //Region = new Region(roundedRect);
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            MoveControls();
        }
    }

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
                // Создаем закругленную форму с использованием текущего радиуса закругления и размеров элемента PictureBox
                path.AddArc(0, 0, cornerRadius, cornerRadius, 180, 90); // Верхний левый угол
                path.AddArc(Width - cornerRadius, 0, cornerRadius, cornerRadius, 270, 90); // Верхний правый угол
                path.AddArc(Width - cornerRadius, Height - cornerRadius, cornerRadius, cornerRadius, 0, 90); // Нижний правый угол
                path.AddArc(0, Height - cornerRadius, cornerRadius, cornerRadius, 90, 90); // Нижний левый угол

                pe.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                pe.Graphics.FillPath(new SolidBrush(BackColor), path); // Заполняем закругленную форму фоновым цветом

                if (Image != null)
                {
                    pe.Graphics.Clip = new Region(path); // Устанавливаем обрезку для изображения в форме закругленного PictureBox
                    pe.Graphics.DrawImage(Image, ClientRectangle); // Рисуем изображение в обрезанной форме
                }
            }
        }
    }
}
