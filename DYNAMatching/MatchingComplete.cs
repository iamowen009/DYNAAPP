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
    public partial class MatchingComplete : Form
    {
        int nTime = 0;
        int TimeToClose = 1;
        public string PartID = "";

        public string MCPartno,MCQTY,MCIssueNO,MCRANNO ;

        private void label5_Click(object sender, EventArgs e)
        {

        }

        public MatchingComplete()
        {
            InitializeComponent();
        }

        private void MatchingComplete_Load(object sender, EventArgs e)
        {

            var tSYS = ConfigurationManager.AppSettings["TimeToHoldCompleteScreen"];

            timer1.Interval = int.Parse(tSYS);
            timer1.Start();
            nTime = 0;

            lbPartno.Text = MCPartno;
            lbQTY.Text = MCQTY;
            lbIssueNo.Text = MCIssueNO;
            lbRANNO.Text = MCRANNO;

            playSimpleSound();

        }

        private void playSimpleSound()
        {
            try
            {
                string SoundFilePath = ConfigurationManager.AppSettings["SoundPath"] + ConfigurationManager.AppSettings["CompleteSound"];
                SoundPlayer simpleSound = new SoundPlayer(SoundFilePath);
                simpleSound.Play();
            }
            catch (Exception ex)
            {
                
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            nTime++;
            if (nTime > TimeToClose)
            {
                this.Close();
                timer1.Stop();
            }
        }
    }
}
