using System;
using System.Configuration;
using System.Windows.Forms;

namespace PhotPrepForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
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

        private void btnCopy_Click(object sender, EventArgs e)
        {
            PhotPrep prep = new PhotPrep();
            txtScript.Text = "Copying files please wait patiently!";
            var scriptLines = prep.CopyAllFiles(txtSource.Text, txtTarget.Text);
            txtScript.Text = string.Empty;
            foreach (var line in scriptLines)
            {
                txtScript.Text += line + Environment.NewLine;
            }
            txtScript.Text += "prompt Kalibrálás_kész_szedd_szét_3_színre!" + Environment.NewLine;
        }
    }
}
