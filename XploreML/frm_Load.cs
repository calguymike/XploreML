using System;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using System.Timers;


namespace XploreML
{
   

    public partial class frm_FileSelection : Form
    {
        [DllImport("user32.dll")]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vlc);
        [DllImport("user32.dll")]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);


        public static string ds1 = "";
        public static string ds2 = "";
        public static string ds3 = "";
        bool preventSleep = false;

        public frm_FileSelection()
        {
            InitializeComponent();
            var HotKeyManager = new HotkeyManager();
            RegisterHotKey(HotKeyManager.Handle, 123, Constants.CTRL + Constants.SHIFT, (int)Keys.S);
        }


        #region Buttons_frmSearch

        private void Btn_LiveXML_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "XML Files(*.xml)| *.xml";
            openFileDialog1.FilterIndex = 0;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string selectedFileName = openFileDialog1.FileName;
                txtbx_LiveXML.Text = selectedFileName;
            }

        }

        private void Btn_LiveTAB_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "TAB Files(*.tab)| *.tab";
            openFileDialog1.FilterIndex = 0;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string selectedFileName = openFileDialog1.FileName;
                txtbx_LiveTAB.Text = selectedFileName;
            }
        }

        private void Btn_XML1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "XML Files(*.xml)| *.xml";
            openFileDialog1.FilterIndex = 0;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string selectedFileName = openFileDialog1.FileName;
                txtbx_XML1.Text = selectedFileName;
            }
        }

        private void Btn_TAB1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "TAB Files(*.tab)| *.tab";
            openFileDialog1.FilterIndex = 0;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string selectedFileName = openFileDialog1.FileName;
                txtbx_TAB1.Text = selectedFileName;
            }
        }

        private void Btn_XML2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "XML Files(*.xml)| *.xml";
            openFileDialog1.FilterIndex = 0;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string selectedFileName = openFileDialog1.FileName;
                txtbx_XML2.Text = selectedFileName;
            }
        }

        private void Btn_TAB2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "TAB Files(*.tab)| *.tab";
            openFileDialog1.FilterIndex = 0;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string selectedFileName = openFileDialog1.FileName;
                txtbx_TAB2.Text = selectedFileName;
            }
        }

        private void Btn_XML3_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "XML Files(*.xml)| *.xml";
            openFileDialog1.FilterIndex = 0;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string selectedFileName = openFileDialog1.FileName;
                txtbx_XML3.Text = selectedFileName;
            }
        }

        private void Btn_TAB3_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "TAB Files(*.tab)| *.tab";
            openFileDialog1.FilterIndex = 0;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string selectedFileName = openFileDialog1.FileName;
                txtbx_TAB3.Text = selectedFileName;
            }
        }

        private void Frm_FileSelection_Resize(object sender, EventArgs e)
        {
            //if the form is minimized   
            //hide it from the task bar   
            //and show the system tray icon (represented by the NotifyIcon control)   
            if (this.WindowState == FormWindowState.Minimized)
            {
                Hide();
                notifyIcon1.Visible = true;
            }
        }

        private void NotifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            Show();
            this.WindowState = FormWindowState.Normal;
            notifyIcon1.Visible = false;
        }

        private void BlaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (System.Windows.Forms.Application.MessageLoop)
            {
                // WinForms app
                System.Windows.Forms.Application.Exit();
            }
            else
            {
                // Console app
                System.Environment.Exit(1);
            }
        }

        private void BlaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clear_form();
        }

        private void Btn_LoadData_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(txtbx_LiveXML.Text) || (string.IsNullOrEmpty(txtbx_LiveTAB.Text)))
            {
                MessageBox.Show("You must enter an XML and TAB");
            }
            else
                copy_user_files();
            ds1 = txtbx_Name1.Text;
            ds2 = txtbx_Name2.Text;
            ds3 = txtbx_Name3.Text; 
            Save_User_Settings();
            MessageBox.Show("Files Loaded!");

        }

        #endregion

        private void copy_user_files()
        {
            //Define variables
            string sourceLiveXML = txtbx_LiveXML.Text;
            string sourceLiveTAB = txtbx_LiveTAB.Text;
            string sourceXML1 = txtbx_XML1.Text;
            string sourceTAB1 = txtbx_TAB1.Text;
            string sourceXML2 = txtbx_XML2.Text;
            string sourceTAB2 = txtbx_TAB2.Text;
            string sourceXML3 = txtbx_XML3.Text;
            string sourceTAB3 = txtbx_TAB3.Text;
            string LiveXML = "LiveXML.xml";
            string LiveTAB = "LiveTAB.tab";
            string XML1 = "XML1.xml";
            string XML2 = "XML2.xml";
            string XML3 = "XML3.xml";
            string TAB1 = "TAB1.TAB";
            string TAB2 = "TAB2.TAB";
            string TAB3 = "TAB3.TAB";
            string targetPath = @"C:\XploreML";

            //Set destination filepaths
            string destLiveXML = System.IO.Path.Combine(targetPath, LiveXML);
            string destLiveTAB = System.IO.Path.Combine(targetPath, LiveTAB);
            string destXML1 = System.IO.Path.Combine(targetPath, XML1);
            string destTAB1 = System.IO.Path.Combine(targetPath, TAB1);
            string destXML2 = System.IO.Path.Combine(targetPath, XML2);
            string destTAB2 = System.IO.Path.Combine(targetPath, TAB2);
            string destXML3 = System.IO.Path.Combine(targetPath, XML3);
            string destTAB3 = System.IO.Path.Combine(targetPath, TAB3);

            // Create destination directory, if it doesn't already exist
            // If the directory already exists, this method does not create a new directory.
            System.IO.Directory.CreateDirectory(targetPath);

            //Delete files from directory if it has files inside
            Array.ForEach(Directory.GetFiles(@"C:\XploreML"),
              delegate (string path) { File.Delete(path); });

            // Copy files to desination
            // overwrite the destination file if it already exists.
            // Check for empty is done in button press loop
            System.IO.File.Copy(sourceLiveXML, destLiveXML, true);
            System.IO.File.Copy(sourceLiveTAB, destLiveTAB, true);
            //txtbx_LiveXML.ReadOnly = true;
            //txtbx_LiveTAB.ReadOnly = true;


            //Check which datasets are selected, copy them to program directory and make textbox read only
            if (!string.IsNullOrEmpty(txtbx_TAB1.Text) || (!string.IsNullOrEmpty(txtbx_XML1.Text)))
            {
                System.IO.File.Copy(sourceXML1, destXML1, true);
                System.IO.File.Copy(sourceTAB1, destTAB1, true);
                //txtbx_XML1.ReadOnly = true;
                //txtbx_TAB1.ReadOnly = true;
            }


            if (!string.IsNullOrEmpty(txtbx_TAB2.Text) || (!string.IsNullOrEmpty(txtbx_XML2.Text)))
            {
                System.IO.File.Copy(sourceXML2, destXML2, true);
                System.IO.File.Copy(sourceTAB2, destTAB2, true);
                //txtbx_XML2.ReadOnly = true;
                //txtbx_TAB2.ReadOnly = true;
            }


            if (!string.IsNullOrEmpty(txtbx_TAB3.Text) || (!string.IsNullOrEmpty(txtbx_XML3.Text)))
            {
                System.IO.File.Copy(sourceXML3, destXML3, true);
                System.IO.File.Copy(sourceTAB3, destTAB3, true);
                //txtbx_XML3.ReadOnly = true;
                //txtbx_TAB3.ReadOnly = true;
            }

        }


        private void clear_form()
        {
            txtbx_TAB1.Text = "";
            txtbx_TAB2.Text = "";
            txtbx_TAB3.Text = "";
            txtbx_XML1.Text = "";
            txtbx_XML2.Text = "";
            txtbx_XML3.Text = "";
            txtbx_LiveXML.Text = "";
            txtbx_LiveTAB.Text = "";
            txtbx_Name1.Text = "";
            txtbx_Name2.Text = "";
            txtbx_Name3.Text = "";
            txtbx_LiveXML.ReadOnly = false;
            txtbx_LiveTAB.ReadOnly = false;
            txtbx_XML1.ReadOnly = false;
            txtbx_TAB1.ReadOnly = false;
            txtbx_XML2.ReadOnly = false;
            txtbx_TAB2.ReadOnly = false;
            txtbx_XML3.ReadOnly = false;
            txtbx_TAB3.ReadOnly = false;
        }

        private void LoadDatasetsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Show();
            this.WindowState = FormWindowState.Normal;
            notifyIcon1.Visible = false;
        }

        private void SearchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_search searchForm = new frm_search();
            searchForm.Show();
        }

        private void Frm_FileSelection_Load(object sender, EventArgs e)
        {
            ds1 = txtbx_Name1.Text;
            ds2 = txtbx_Name2.Text;
            ds3 = txtbx_Name3.Text;
        }

        private void Frm_FileSelection_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.Shift && e.KeyCode == Keys.F12)
            //{
            //    MessageBox.Show("It worked");
            //}
        }

        private void SearchToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frm_search searchForm = new frm_search();
            searchForm.Show();
            this.WindowState = FormWindowState.Minimized;
        }

        private void Save_User_Settings()
        {
            global::XploreML.Properties.Settings.Default.LiveTABsetting = txtbx_LiveTAB.Text;
            global::XploreML.Properties.Settings.Default.TAB1setting = txtbx_TAB1.Text;
            global::XploreML.Properties.Settings.Default.TAB2setting = txtbx_TAB2.Text;
            global::XploreML.Properties.Settings.Default.TAB3setting = txtbx_TAB3.Text;
            global::XploreML.Properties.Settings.Default.LiveXMLsetting = txtbx_LiveXML.Text;
            global::XploreML.Properties.Settings.Default.XML1setting = txtbx_XML1.Text;
            global::XploreML.Properties.Settings.Default.XML2setting = txtbx_XML2.Text;
            global::XploreML.Properties.Settings.Default.XML3setting = txtbx_XML3.Text;
            global::XploreML.Properties.Settings.Default.ds1Namesetting = txtbx_Name1.Text;
            global::XploreML.Properties.Settings.Default.ds2Namesetting = txtbx_Name2.Text;
            global::XploreML.Properties.Settings.Default.ds3Namesetting = txtbx_Name3.Text;
            global::XploreML.Properties.Settings.Default.settingPrevSleep = true;

            Properties.Settings.Default.Save();
        }

        private void NotifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (System.Windows.Forms.Application.MessageLoop)
            {
                // WinForms app
                System.Windows.Forms.Application.Exit();
            }
            else
            {
                // Console app
                System.Environment.Exit(1);
            }
        }

        private void minimiseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
       
        private void preventSleepToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(preventSleepToolStripMenuItem.Checked == true)
            {
                preventSleepToolStripMenuItem.Checked = false;
                preventSleep = false;
            }
            else
            {
                preventSleepToolStripMenuItem.Checked = true;
                preventSleep = true;
            }
        }

        private void preventPCSleep()
        {
            while (preventSleep == true)
            {


            }
        }

        private void convertTabToHEXToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //TODO
            //  1. Read TAB into array (copy code from search function)
            //  2. Convert each element of array to HEX
            //  3. Write array back to file in correct format (what is the correct format?)


            //Read TAB to array



        }
    }

    public static class Constants
    {
        public const int NOMOD = 0x0000;
        public const int ALT = 0x0001;
        public const int CTRL = 0x0002;
        public const int SHIFT = 0x0004;
        public const int WIN = 0x0008;

        public const int WM_HOTKEY_MSG_ID = 0x0312;
    }

}

