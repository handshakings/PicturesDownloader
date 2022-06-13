using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PicturesDownloader
{
    public delegate void MyDelegate(int downloadCounter);
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void UpdateLabel(int downloadCounter)
        {
            label4.Text = downloadCounter.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            if (DialogResult.OK == folderBrowserDialog.ShowDialog())
            {
                label3.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            int noOfPictures = int.Parse(numericUpDown1.Value.ToString());
            int c = 0;
            if(noOfPictures > 0 && label3.Text != "")
            {
                using (WebClient imgClient = new WebClient())
                {
                    HttpClient client = new HttpClient();
                    while (c < noOfPictures)
                    {
                        try
                        {
                            long now = DateTime.Now.Ticks;
                            string json = await client.GetStringAsync("https://this-person-does-not-exist.com/en?new=" + now);
                            Model model = JsonConvert.DeserializeObject<Model>(json);

                            Uri uri = new Uri("https://this-person-does-not-exist.com/img/" + model.Name);
                            imgClient.DownloadFile(uri, label3.Text + "\\picture" + c + ".jpg");

                            MyDelegate myDelegate = new MyDelegate(UpdateLabel);
                            myDelegate.DynamicInvoke(c + 1);

                        }
                        catch (Exception)
                        {
                            continue;
                        }
                        c++;
                    }
                }
            }
            else
            {
                MessageBox.Show("Provide all inputs");
            }
        }

    }
}
