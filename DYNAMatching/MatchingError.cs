using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Media;

namespace DYNAMatching
{
    public partial class MatchingError : Form
    {

        int nTime = 0;
        int TimeToClose = 1;

        //public string MCPartno, MCQTY, MCIssueNO, MCRANNO;
        public string CUSPartno, CUSQTY, CUSIssueNO, CUSRANNO , CUSORDERNO , CUSSERIAL;
        public string DYNAPartno, DYNAQTY, DYNAIssueNO, DYNARANNO , DYNAORDERNO , DYNASERIAL;

        public MatchingError()
        {
            InitializeComponent();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MatchingError_Load(object sender, EventArgs e)
        {

            var tSYS = ConfigurationManager.AppSettings["TimeToHoldErrorScreen"];

            //timer1.Interval = int.Parse(tSYS) ;
            //timer1.Start();
            nTime = 0;

            lbCusPartno.Text = CUSPartno;
            lbCusQTY.Text = CUSQTY;
            lbCusIssueNo.Text = CUSIssueNO;
            lbCusRANNO.Text = CUSRANNO;
            lbCusOrderNo.Text = CUSORDERNO;
            lbCusSerialNo.Text = CUSSERIAL;

            lbdynaPartNo.Text = DYNAPartno;
            lbdynaQTY.Text = DYNAQTY;
            lbdynaIssueNo.Text = DYNAIssueNO;
            lbdynaRan.Text = DYNARANNO;
            lbDynaOrderNo.Text = DYNAORDERNO;
            lbDynaSerialNo.Text = DYNASERIAL;

            playSimpleSound();

            txtBackToMenu.Focus();

        }

        private void txtBackToMenu_KeyPress(object sender, KeyPressEventArgs e)
        {
            CheckExitCode(txtBackToMenu.Text);
        }

        private void playSimpleSound()
        {
            try
            {
                string SoundFilePath = ConfigurationManager.AppSettings["SoundPath"] + ConfigurationManager.AppSettings["ErrorSound"];
                SoundPlayer simpleSound = new SoundPlayer(SoundFilePath);
                simpleSound.Play();
            }
            catch (Exception ex)
            {

            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //nTime++;
            //if (nTime > TimeToClose)
            //{
            //    this.Close();
            //    timer1.Stop();
            //}
        }

        public void CheckExitCode(string value)
        {
            if (value.Trim() == "@BUINNOVATIONEXIT")
            {
                this.Close();
            }
        }

    }
}
