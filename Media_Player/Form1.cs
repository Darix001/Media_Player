using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Media_Player
{
    /*"https://www.youtube.com/watch?v=OmY3XNnqT0E"*/
    public partial class Form1 : Form
    {
        public string[] formats = { "mp3", "wmv", "wav" };
        public Form1()
        {
            InitializeComponent();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }


        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string filters = "";
            foreach (string format in formats)
            {
                filters+=format.ToUpper()+"|*."+format+"|";
            }
            filters=filters.Substring(0, filters.Length-1);
            using (OpenFileDialog openFile = new OpenFileDialog()
            {
                Multiselect = true,
                ValidateNames = true,
                Filter = filters,
            })
            {
                if (openFile.ShowDialog() == DialogResult.OK)
                {
                    List<Media_File> files = new List<Media_File>();
                    foreach (string filename in openFile.FileNames)
                    {
                        FileInfo fi = new FileInfo(filename);
                        files.Add(new Media_File() { Filename = Path.GetFileNameWithoutExtension(fi.FullName), Path = fi.FullName });
                    }
                    File_List.DataSource = files;
                    File_List.ValueMember = "Path";
                    File_List.DisplayMember = "Filename";
                }
            }
        }

        private void File_List_SelectedIndexChanged(object sender, EventArgs e)
        {
            Media_File file = File_List.SelectedItem as Media_File;
            if (file != null)
            {
                axWindowsMediaPlayer1.URL = file.Path;
                axWindowsMediaPlayer1.Ctlcontrols.play();
            }
        }
    }
}
