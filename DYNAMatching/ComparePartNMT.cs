﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Configuration;
using System.Media;

namespace DYNAMatching
{
    public partial class ComparePartNMT : Form
    {

        public string Customer;
        public string username;
        public string userkey;
        public string UNIQUEID;

        public string sysPartID = "";
        public string sysCUSTOMER = "";
        public string sysLABLENO = "";
        public string sysPARTNO = "";
        public string sysPARTNAME = "";
        public string sysDRAWINGNO = "";
        public string sysRANNO = "";
        public string sysQTY = "";
        public string sysLOT = "";
        public string sysPRODUCTDATE = "";
        public string sysIssueNo = "";
        public string sysSerialNo = "";
        public string sysOrderNo = "";
        public string sysLable_Id = "";
        DBTransaction DBT = new DBTransaction();

        public ComparePartNMT()
        {
            InitializeComponent();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void CheckExitCode(string value)
        {
            if (value.Trim() == "@BUINNOVATIONEXIT")
            {
                this.Close();
            }

            //if (value.Trim() == "@SCANMATCHINGLIST")
            //{
            //    CallScanMatchingList();
            //}

        }

        private Image byteArrayToImage(byte[] byteArrayIn)
        {
            if (byteArrayIn == null)
            {
                return null;
            }
            else
            {
                MemoryStream ms = new MemoryStream(byteArrayIn);
                ms.Position = 0;
                Image returnImage = Image.FromStream(ms);
                return returnImage;
            }

        }

        public void ClearText()
        {
            sysPartID = "";
            sysCUSTOMER = "";
            sysLABLENO = "";
            sysPARTNO = "";
            sysPARTNAME = "";
            sysDRAWINGNO = "";
            sysRANNO = "";
            sysQTY = "";
            sysLOT = "";
            sysPRODUCTDATE = "";
            sysOrderNo = "";
            sysIssueNo = "";
            sysSerialNo = "";
        }

        private string GetPartID(string PartNo, string DRAWINGNO, string RANKNO)
        {
            string xResult = "";

            DataTable DTS = new DataTable();
            string condition = " and Part_no = '" + PartNo + "' and Drawing_No = '" + DRAWINGNO + "' and Rank_No = '" + RANKNO + "' ";
            DTS = DBT.GetExcuteDataTableOneCon("spGetMPartByConditionForApp", condition);

            if (DTS.Rows.Count > 0)
            {
                xResult = DTS.Rows[0]["Part_Id"].ToString();
            }

            return xResult;
        }

        public void ClearScreen()
        {
            txtCusPartNo.Text = "";
            txtCusQTY.Text = "";
            txtIssueNo.Text = "";
            txtRANNO.Text = "";
            txtDYNAPartNo.Text = "";
            lbDYNAQTY.Text = "";
            sysLable_Id = "";

            txtCusPartNo.Focus();

        }

        private void ComparePartNMT_Load(object sender, EventArgs e)
        {
            ClearScreen();
        }

        private void txtCusPartNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter) && (txtCusPartNo.Text.Trim() != ""))
            {
                CheckExitCode(txtCusPartNo.Text);
                //CheckNissanPartNo();


                if (txtCusPartNo.Text != "")
                {
                    if (txtCusPartNo.Text == "@BUINNOVATIONTAB")
                    {
                        txtCusPartNo.Text = "";
                        MatchingResult("Error");
                        txtCusQTY.Focus();
                    }
                    else
                    {
                        txtCusPartNo.Text = CheckFormatPartNo(txtCusPartNo.Text.Trim());

                        DataTable DTS = new DataTable();
                        string condition = " and Part_no = '" + txtCusPartNo.Text.Trim() + "'";
                        DTS = DBT.GetExcuteDataTableOneCon("spGetMPartByConditionForApp", condition);

                        if (DTS.Rows.Count > 0)
                        {
                            //if ((DTS.Rows[0]["Picture"] != null) || (DTS.Rows[0]["Picture"].ToString().Trim() != ""))
                            //{
                            //    pbCusImage.Image = byteArrayToImage(DTS.Rows[0]["Picture"] as byte[]);
                            //}

                            if (DTS.Rows[0]["Picture"].ToString().Trim() != "")
                            {
                                byte[] temp = (byte[])DTS.Rows[0]["Picture"];
                                if (Convert.ToBase64String(temp) != "")
                                    pbCusImage.Image = byteArrayToImage(DTS.Rows[0]["Picture"] as byte[]);
                            }

                        }

                        txtCusQTY.Focus();
                    }
                }

            }
        }

        public void SpilitStringBlank(string value)
        {
            int n = 0;
            string[] separators = { " " };
            string[] words = value.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            foreach (var word in words)
            {
                n++;
                //listBox1.Items.Add(n.ToString() + " - " + word);

                switch (n)
                {
                    case 2:
                        //sysOrderNo = CheckFormatOrderNo(word);
                        break;
                    case 3:
                        sysPARTNO = CheckFormatPartNo(word);
                        break;
                    case 4:
                        sysQTY = word;
                        break;
                }

            }

            if (n == 1)
            {
                ClearText();
                sysPARTNO = value.ToString().Trim();
                txtCusQTY.Focus();
            }
            else
            {
                txtDYNAPartNo.Focus();
            }

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

        private void MSGError(string Values)
        {
            if (Values.Trim() != "@BUINNOVATIONEXIT")
            {
                int timeAutoClose = int.Parse(ConfigurationManager.AppSettings["TimeAutoClose"]);
                playSimpleSound();
                AutoClosingMessageBox.Show("The format is " + SmartCheckFormat(Values) + " !!! , Please try again for correct format.", "BU System", timeAutoClose);
            }
        }

        private string CheckFormatPartNo(string Values)
        {
            string xReturn = Values;
            if (Values.Substring(0, 1) == "P")
            {
                xReturn = Values.Substring(1, Values.Length - 1);
                xReturn = xReturn.Replace(" ", "");
            }
            else
            {
                MSGError(Values);
                xReturn = "";
            }

            return xReturn.Trim();
        }

        private string CheckFormatSNPQTY(string Values)
        {
            string xReturn = Values;
            if (Values.Substring(0, 1) == "Q")
            {
                xReturn = Values.Substring(1, Values.Length - 1);
            }
            else
            {
                MSGError(Values);
                xReturn = "";
            }

            return xReturn.Trim();
        }

        private string CheckFormatSerialNo(string Values)
        {
            string xReturn = Values;
            if (Values.Substring(0, 1) == "S")
            {
                xReturn = Values.Substring(1, Values.Length - 1);
            }
            else
            {
                MSGError(Values);
                xReturn = "";
            }

            return xReturn.Trim();
        }

        private string CheckFormatRANNo(string Values)
        {
            string xReturn = Values;
            if (Values.Substring(0, 1) == "A")
            {
                xReturn = Values.Substring(1, Values.Length - 1);
            }
            else
            {
                MSGError(Values);
                xReturn = "";
            }

            return xReturn.Trim();
        }

        private string SmartCheckFormat(string Values)
        {
            string xReturn = "";
            switch (Values.Substring(0, 1))
            {
                case "P":
                    xReturn = "Part No";
                    break;
                case "Q":
                    xReturn = "SNP/QUANTITY";
                    break;
                case "S":
                    xReturn = "Issue No";
                    break;
                case "V":
                    xReturn = "Supplier";
                    break;
                case "A":
                    xReturn = "RAN No";
                    break;
            }

            return xReturn;
        }

        private void txtCusQTY_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (txtCusQTY.Text == "@BUINNOVATIONTAB")
                {
                    txtCusQTY.Text = "";
                    txtIssueNo.Focus();
                }
                else
                {
                    CheckExitCode(txtCusQTY.Text);
                    txtCusQTY.Text = CheckFormatSNPQTY(txtCusQTY.Text.Trim());

                    if (txtCusQTY.Text != "")
                    {
                        txtIssueNo.Focus();
                    }
                }
            }
        }

        private void txtIssueNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (txtIssueNo.Text == "@BUINNOVATIONTAB")
                {
                    txtIssueNo.Text = "";
                    txtRANNO.Focus();
                }
                else
                {
                    CheckExitCode(txtIssueNo.Text);
                    txtIssueNo.Text = CheckFormatSerialNo(txtIssueNo.Text.Trim());

                    if (txtIssueNo.Text != "")
                    {
                        txtRANNO.Focus();
                    }
                }

            }
        }

        private void txtRANNO_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (txtRANNO.Text == "@BUINNOVATIONTAB")
                {
                    txtRANNO.Text = "";
                    txtDYNAPartNo.Focus();
                }
                else
                {
                    CheckExitCode(txtRANNO.Text);
                    txtRANNO.Text = CheckFormatRANNo(txtRANNO.Text.Trim());

                    if (txtRANNO.Text != "")
                    {
                        txtDYNAPartNo.Focus();
                    }
                }
            }
        }

        public void SpilitStringBUQRCode(string value)
        {
            ClearText();
            int n = 0;
            string[] separators = { "^" };
            //string[] words = value.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            string[] words = value.Split('^');

            foreach (var word in words)
            {
                n++;
                //listBox1.Items.Add(n.ToString() + " - " + word);
                switch (n)
                {
                    case 1:
                        sysCUSTOMER = word;
                        break;
                    case 2:
                        sysLABLENO = word;
                        break;
                    case 3:
                        sysPARTNO = word;
                        break;
                    case 4:
                        sysPARTNAME = word;
                        break;
                    case 5:
                        sysDRAWINGNO = word;
                        break;
                    case 6:
                        sysRANNO = word;
                        break;
                    case 7:
                        sysQTY = word;
                        break;
                    case 8:
                        sysLOT = word;
                        break;
                    case 9:
                        sysPRODUCTDATE = word;
                        break;
                    case 10:
                        sysPartID = word;
                        break;
                    case 11:
                        sysLable_Id = word;
                        break;

                }
            }

            //if (n == 1)
            //{
            //    ClearText();
            //    sysPARTNO = CheckFormatPartNo(value);
            //}

        }

        private void txtDYNAPartNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter) && (txtDYNAPartNo.Text != ""))
            {
                if (txtDYNAPartNo.Text == "@BUINNOVATIONTAB")
                {
                    txtDYNAPartNo.Text = "";
                    txtCusPartNo.Focus();
                }
                else
                {
                    CheckExitCode(txtDYNAPartNo.Text);
                    CheckDYNAPartNo(txtDYNAPartNo.Text);
                }
            }
        }

        private void CheckDYNAPartNo(string Values)
        {
            string value = txtDYNAPartNo.Text.Trim();
            SpilitStringBUQRCode(value);
            txtDYNAPartNo.Text = sysPARTNO;
            lbDYNAQTY.Text = sysQTY;

            //sysPartID = GetPartID(sysPARTNO, sysDRAWINGNO, sysRANNO);
            //sysPartID = "ABFDBA66-CED4-4084-8799-D6F19DF1FAE8";

            if (txtDYNAPartNo.Text.Trim() != "")
            {
                if (CheckDuplicate())
                {
                    CheckMatching();
                }
                else
                {
                    int timeAutoClose = int.Parse(ConfigurationManager.AppSettings["TimeAutoClose"]);
                    AutoClosingMessageBox.Show("Issure No: " + txtIssueNo.Text + " มีการ Scan ไปแล้ว", "BU System", timeAutoClose * 9999);
                    txtRANNO.Text = "";
                    txtDYNAPartNo.Text = "";
                    txtIssueNo.Focus();
                }
            }
        }

        private bool CheckDuplicate()
        {
            bool isCheck = true;
            string UNIQUECondition = " TimeToScan = '" + UNIQUEID + "' ";
            string condition = " and " + UNIQUECondition + getCustomerCondition() + getIssueCondition() ;
            DataTable DTS = new DataTable();
            
            DTS = DBT.GetExcuteDataTableOneCon("spGetTScanLabelByCondition", condition);

            if (DTS.Rows.Count > 0)
                isCheck = false;

            return isCheck;
        }

        private string getCustomerCondition()
        {
            string Condition = "'NISSAN NMT'";
            Condition = " And (TScan.Customer in (" + Condition + ")) ";
            return Condition;
        }

        private string getIssueCondition()
        {
            string Condition = " And TScan.Issue_No = '" + txtIssueNo.Text.Trim() +"'";
         
            return Condition;
        }



        public void CheckMatching()
        {
            if ((txtCusPartNo.Text.Trim() == txtDYNAPartNo.Text.Trim()) && (txtCusQTY.Text.Trim() == lbDYNAQTY.Text))
            //if (txtCusPartNo.Text.Trim() == txtDYNAPartNo.Text.Trim())
            {
                MatchingResult("Complete");
            }
            else
            {
                MatchingResult("Error");
            }
        }

        public void MatchingResult(string Results)
        {
            if (Results == "Complete")
            {

                if (sysQTY.Trim() == "")
                    sysQTY = "0";

                decimal QTY = decimal.Parse(sysQTY);

                sysCUSTOMER = "NISSAN NMT";

                sysIssueNo = txtIssueNo.Text.Trim();
                sysRANNO = txtRANNO.Text.Trim();

                DBT.InsertTScanLabel(sysCUSTOMER, sysPartID, sysPARTNO, sysPARTNAME, QTY, sysRANNO, sysIssueNo, sysSerialNo, sysOrderNo, userkey , UNIQUEID, sysRANNO, sysLable_Id);
                MatchingComplete MC = new MatchingComplete();
                MC.PartID = sysPartID;
                MC.MCPartno = sysPARTNO;
                MC.MCQTY = sysQTY;
                MC.MCRANNO = sysRANNO;
                MC.ShowDialog();
            }
            else
            {
                MatchingError MC = new MatchingError();
                //MC.PartID = sysPartID;

                MC.DYNAPartno = sysPARTNO;
                MC.DYNAQTY = sysQTY;
                MC.DYNAIssueNO = "";
                MC.DYNARANNO = "";

                MC.CUSPartno = txtCusPartNo.Text.Trim();
                MC.CUSQTY = txtCusQTY.Text.Trim();
                MC.CUSIssueNO = txtIssueNo.Text.Trim();
                MC.CUSRANNO = txtRANNO.Text.Trim();

                MC.ShowDialog();
            }

            ClearScreen();

        }

        public class AutoClosingMessageBox
        {
            System.Threading.Timer _timeoutTimer;
            string _caption;
            AutoClosingMessageBox(string text, string caption, int timeout)
            {
                _caption = caption;
                _timeoutTimer = new System.Threading.Timer(OnTimerElapsed,
                    null, timeout, System.Threading.Timeout.Infinite);
                MessageBox.Show(text, caption);
            }

            public static void Show(string text, string caption, int timeout)
            {
                new AutoClosingMessageBox(text, caption, timeout);
            }

            void OnTimerElapsed(object state)
            {
                IntPtr mbWnd = FindWindow(null, _caption);
                if (mbWnd != IntPtr.Zero)
                    SendMessage(mbWnd, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
                _timeoutTimer.Dispose();
            }
            const int WM_CLOSE = 0x0010;
            [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
            static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
            [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
            static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);
        }

        private void pbScanMatchingList_Click(object sender, EventArgs e)
        {
            CallScanMatchingList();
        }

        public void CallScanMatchingList()
        {
            //ClearScreen();
            ScanMatchingList SML = new ScanMatchingList();
            SML.Customer = "NISSAN NMT";
            SML.UNIQUEID = UNIQUEID;
            SML.ShowDialog();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            SendKeys.Send("{TAB}");
        }
    }
}
