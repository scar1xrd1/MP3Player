using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MP3Player
{
    public partial class GetTextDialog : Form
    {
        public GetTextDialog()
        {
            InitializeComponent();
            //Location = GetText.Location;
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                if(textBox1.Text != string.Empty)
                {
                    GetText.Text = textBox1.Text;
                    Close();
                }
            }
            else if(e.KeyCode == Keys.Escape) { GetText.Text = string.Empty; Close(); }
        }
    }
}
