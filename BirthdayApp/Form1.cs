using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BirthdayApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public void SetUsername(string userName)
        {
            label3.Text = userName;
            label4.Text = $"Type \"Happy birthday {userName}\" below to continue using this computer.";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button1.Enabled = false;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            button1.Enabled = textBox1.Text == $"Happy birthday {label3.Text}";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (textBox1.Text != $"Happy birthday {label3.Text}")
            {
                e.Cancel = true;
                MessageBox.Show("You have to wish a happy birthday before you can continue using your computer!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
