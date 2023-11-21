using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace BirthdayApp
{
    internal static class Program
    {
        public static string SavedFilePath =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "BirthdayApp.xml");
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (!File.Exists(SavedFilePath))
            {
                using (var stream = File.OpenWrite(SavedFilePath))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(List<DataType>));
                    serializer.Serialize(stream, new List<DataType>());
                }
            }

            var data = new List<DataType>();
            using (var stream = File.OpenRead(SavedFilePath))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<DataType>));
                data.AddRange(serializer.Deserialize(stream) as List<DataType>);
            }

            var now = DateTime.Now;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (Environment.GetCommandLineArgs().Contains("--config") ||
                Environment.GetCommandLineArgs().Contains("/config"))
            {
                Application.Run(new ConfigForm());
                return;
            }

            foreach (var d in data)
            {
                if (d.Day == now.Day && d.Month == now.Month)
                {
                    var form = new Form1();
                    form.SetUsername(d.Name);
                    Application.Run(form);
                }
            }
        }
    }
}
