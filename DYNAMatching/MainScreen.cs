using System;
using System.Windows.Forms;

namespace DYNAMatching
{
    public partial class MainScreen : Form
    {

        public string UserKey;
        public string UserName;
        public string UNIQUEID;
        public string FirstName;

        public MainScreen()
        {
            InitializeComponent();
        }

        private void btNissanNMT_Click(object sender, EventArgs e)
        {
            OpenComparePart("NISSANNMT");
        }

        private void btNissanNPT_Click(object sender, EventArgs e)
        {
            OpenComparePart("NISSANNPT");
        }

        public void OpenComparePart(string FormName)
        {

            txtCustomer.Text = "";
            txtCustomer.Focus();

            switch (FormName)
            {
                case "NISSANNPT":
                    ComparePartNPT CPNPT = new ComparePartNPT();
                    CPNPT.userkey = UserKey;
                    CPNPT.UNIQUEID = UNIQUEID;
                    CPNPT.ShowDialog();
                    break;
                case "NISSANNMT":
                    ComparePartNMT CPNMT = new ComparePartNMT();
                    CPNMT.userkey = UserKey;
                    CPNMT.UNIQUEID = UNIQUEID;
                    CPNMT.ShowDialog();
                    break;
                case "TOYOTA":
                    ComparePartToyota CPTYT = new ComparePartToyota();
                    CPTYT.userkey = UserKey;
                    CPTYT.UNIQUEID = UNIQUEID;
                    CPTYT.ShowDialog();
                    break;
            }

            //ComparePart CP = new ComparePart();
            //CP.Customer = FormName;
            //CP.ShowDialog();
        }

        private void btToyota_Click(object sender, EventArgs e)
        {
            OpenComparePart("TOYOTA");

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            OpenComparePart("NISSANNMT");
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            OpenComparePart("NISSANNPT");
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            OpenComparePart("TOYOTA");
        }

        private void MainScreen_Load(object sender, EventArgs e)
        {
            //tLBUsername.Text = UserName;
            tLBUsername.Text = FirstName;
            tslbUniqueID.Text = UNIQUEID;
            txtCustomer.Text = "";
            txtCustomer.Focus();
        }

        private void MainScreen_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void MainScreen_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                MessageBox.Show(e.ToString());
            }
        }

        private void txtCustomer_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                OpenComparePart(txtCustomer.Text.Trim());
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            ScanMatchingList SML = new ScanMatchingList();
            SML.userkey = UserKey;
            SML.UNIQUEID = UNIQUEID;
            SML.ShowDialog();
        }
    }
}
