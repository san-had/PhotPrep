using System;
using System.Configuration;
using System.Windows.Forms;

namespace PhotPrepForm
{
    public partial class Form1 : Form
    {
        private readonly string BLUE_CHANNEL_STARS;

        public Form1()
        {
            InitializeComponent();

            BLUE_CHANNEL_STARS = ConfigurationManager.AppSettings["BlueChannelStars"].ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string SOURCE_START_DIR = ConfigurationManager.AppSettings["SourceStartDir"].ToString();
            string TARGET_DIR = ConfigurationManager.AppSettings["TargetDir"].ToString();

            txtSource.Text = SOURCE_START_DIR;
            txtTarget.Text = TARGET_DIR;
        }

        private void btnSetSource_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.SelectedPath = txtSource.Text;
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txtSource.Text = fbd.SelectedPath;
            }
        }

        private void btnSetTarget_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.SelectedPath = txtTarget.Text;
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txtTarget.Text = fbd.SelectedPath;
            }
        }

        private async void btnCopy_Click(object sender, EventArgs e)
        {
            PhotPrep prep = new PhotPrep();
            txtScript.Text = "Copying files please wait patiently!";
            var scriptLines = await prep.CopyAllFiles(txtSource.Text, txtTarget.Text, BLUE_CHANNEL_STARS);
            txtScript.Text = string.Empty;
            foreach (var line in scriptLines)
            {
                txtScript.Text += line + Environment.NewLine;
            }
            txtScript.Text += "prompt Kalibrálás_kész_szedd_szét_3_színre!" + Environment.NewLine;

            Clipboard.Clear();
            Clipboard.SetText(txtScript.Text);
        }
    }
}