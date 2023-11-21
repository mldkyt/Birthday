using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace BirthdayApp
{
    public partial class ConfigForm : Form
    {
        private List<DataType> data = new List<DataType>();
        public ConfigForm()
        {
            InitializeComponent();

            LoadData(true);

            removeTheSelectedItemToolStripMenuItem.Enabled = false;
        }

        private void LoadData(bool reloadFromDisk)
        {
            if (reloadFromDisk)
            {
                data.Clear();
                using (var stream = File.OpenRead(Program.SavedFilePath))
                {
                    var serializer = new XmlSerializer(typeof(List<DataType>));
                    data.AddRange(serializer.Deserialize(stream) as List<DataType>);
                }
            }

            listBox1.Items.Clear();
            data.ForEach(x => listBox1.Items.Add(x));
            toolStripStatusLabel1.Text = data.Count + " items";
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            removeTheSelectedItemToolStripMenuItem.Enabled = listBox1.SelectedIndex != -1 && listBox1.SelectedIndex < listBox1.Items.Count;
        }

        private void saveChangesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var stream = File.OpenWrite(Program.SavedFilePath))
            {
                var serializer = new XmlSerializer(typeof(List<DataType>));
                serializer.Serialize(stream, data);
            }
        }

        private void reloadFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadData(true);
        }

        private void addANewBirthdayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new CreateBirthday();
            form.OnFilledOut += (o2, e2) =>
            {
                data.Add(new DataType
                {
                    Day = form.date.Day,
                    Month = form.date.Month,
                    Name = form.userName
                });
                LoadData(false);
                form.Close();
                form.Dispose();
            };
            form.FormClosed += (o2, e2) =>
            {
                Enabled = true;
            };
            Enabled = false;
            form.Show();
        }

        private void removeTheSelectedItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1 && listBox1.SelectedIndex >= listBox1.Items.Count)
            {
                MessageBox.Show("Select an element", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            data.RemoveAt(listBox1.SelectedIndex);
            LoadData(false);
        }

        private void aboutBirthdayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new About();
            form.Show();
        }

        private void websiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("https://mldkyt.com");
        }
    }
}
