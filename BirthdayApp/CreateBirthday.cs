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
    public partial class CreateBirthday : Form
    {
        public event EventHandler OnFilledOut;
        public string userName;
        public DateTime date;

        public CreateBirthday()
        {
            InitializeComponent();
            button1.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            userName = textBox1.Text;
            date = dateTimePicker1.Value;
            OnFilledOut?.Invoke(this, null);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
            Dispose();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            button1.Enabled = textBox1.Text.Length > 2 && textBox1.Text.Length < 16;
        }
    }
}
