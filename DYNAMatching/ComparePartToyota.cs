using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Data.SqlClient;
using System.IO;

namespace DYNAMatching
{
    public partial class ComparePartToyota : Form
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
        public string sysRANKNO = "";
        public string sysQTY = "";
        public string sysLOT = "";
        public string sysPRODUCTDATE = "";
        public string sysIssueNo = "";
        public string sysSerialNo = "";
        public string sysOrderNo = "";
        public string sysLable_Id = "";

        public string gPARTNO = "";
        DBTransaction DBT = new DBTransaction();

        public ComparePartToyota()
        {
            InitializeComponent();
        }

        private void pbCustomerPart_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtCusPartNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter) && (txtCusPartNo.Text.Trim() != ""))
            {
                gPARTNO = txtCusPartNo.Text;

                Control control = (Control)sender;
                if (control.Text == "@BUINNOVATIONTAB")
                {
                    txtCusPartNo.Text = "";
                    MatchingResult("Error");
                    txtCusPartNo.Focus();
                }
                else
                {
                    CheckExitCode(txtCusPartNo.Text.Trim());
                    CheckToyotaPartNo();
                    txtCusPartNo.Text = txtCusPartNo.Text.Trim();
                    //txtCusPartNo.Text = txtCusPartNo.Text.Substring(1, txtCusPartNo.Text.Length - 1); เอาลบตัวหน้าออก

                    DataTable DTS = new DataTable();
                    string condition = " and Part_no = '" + txtCusPartNo.Text.Trim() + "'";
                    DTS = DBT.GetExcuteDataTableOneCon("spGetMPartByConditionForApp", condition);

                    if (DTS.Rows.Count > 0)
                    {
                        //if ((DTS.Rows[0]["Picture"] != null) || (DTS.Rows[0]["Picture"].ToString().Trim() != ""))
                        if (DTS.Rows[0]["Picture"].ToString().Trim() != "")
                        {
                            byte[] temp = (byte[])DTS.Rows[0]["Picture"];
                            if (Convert.ToBase64String(temp) != "")
                                pbCusImage.Image = byteArrayToImage(DTS.Rows[0]["Picture"] as byte[]);
                        }

                        if(gPARTNO.Length < 70)
                        txtCusQTY.Text = DTS.Rows[0]["Qty"].ToString();
                    }

                    if ((txtCusQTY.Text != "") && (txtOrderNo.Text != ""))
                    {
                        txtDYNAPartNo.Focus();
                    }
                    else
                    {
                        //txtCusQTY.Focus(); load by Base
                        txtOrderNo.Focus();
                    }
                }

            }
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

        public void CheckToyotaPartNo()
        {
            string value = txtCusPartNo.Text.Trim();
            SpilitStringBlank(value);
            txtCusPartNo.Text = sysPARTNO;
            txtCusQTY.Text = sysQTY;
            txtOrderNo.Text = sysOrderNo;
            sysPartID = GetPartID(sysPARTNO, sysDRAWINGNO, sysRANKNO);
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

        public void ClearText()
        {
            sysPartID = "";
            sysCUSTOMER = "";
            sysLABLENO = "";
            sysPARTNO = "";
            sysPARTNAME = "";
            sysDRAWINGNO = "";
            sysRANKNO = "";
            sysQTY = "";
            sysLOT = "";
            sysPRODUCTDATE = "";
            sysOrderNo = "";
            sysIssueNo = "";
            sysSerialNo = "";
            sysLable_Id = "";
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
                        sysOrderNo = CheckFormatOrderNo(word);
                        break;
                    case 3:
                        sysPARTNO = CheckFormatPartNo(word);
                        break;
                    case 4:
                        sysQTY = word;
                        txtCusQTY.Text = word;
                        break;
                }

            }

            //if ((n > 1) && (n < 4))
            //{
            //    MessageBox.Show("Format not found . Please recheck your Barcode/QRCode and re-Scan again.");
            //    ClearText();
            //}

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

        private string CheckFormatPartNo(string Values)
        {
            string xReturn = "";
            char ch = Values[3];

            //xReturn = Values.Replace("888X","");
            if (Char.IsDigit(ch)) {
            }
            else {
                xReturn = Values.Substring(4);
            }

            xReturn = xReturn.Substring(0,12);
            return xReturn;
        }

        private string CheckFormatOrderNo(string Values)
        {
            string xReturn = "";
            xReturn = Values.Replace("FFA", "");
            return xReturn;
        }

        private void txtCusQTY_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (txtCusQTY.Text == "@BUINNOVATIONTAB")
                {
                    txtCusQTY.Text = "";
                    txtOrderNo.Focus();
                }
                else
                {
                    CheckExitCode(txtCusQTY.Text.Trim());
                    txtOrderNo.Focus();
                }
            }
        }

        private void txtOrderNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (txtOrderNo.Text.Trim() == "@BUINNOVATIONTAB")
                    txtOrderNo.Text = "";
                CheckExitCode(txtOrderNo.Text.Trim());
                txtDYNAPartNo.Focus();
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
                        sysPARTNO = word.Replace("OEM","");
                        break;
                    case 4:
                        sysPARTNAME = word;
                        break;
                    case 5:
                        sysDRAWINGNO = word;
                        break;
                    case 6:
                        sysRANKNO = word;
                        break;
                    case 7:
                        sysQTY = word;
                        txtCusQTY.Text = word;
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
            //    sysPARTNO = value.ToString().Trim();
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
                    CheckExitCode(txtDYNAPartNo.Text.Trim());
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
            lbDYNALot.Text = sysLOT;
            //sysPartID = GetPartID(sysPARTNO, sysDRAWINGNO, sysRANKNO);
            //sysPartID = "ABFDBA66-CED4-4084-8799-D6F19DF1FAE8";

            if (txtDYNAPartNo.Text.Trim() != "")
            {
                CheckMatching();
            }

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

                sysCUSTOMER = "TOYOTA";
                sysOrderNo = txtOrderNo.Text.Trim();

                //DBT.InsertTScanLabel(sysCUSTOMER, sysPartID, sysPARTNO, sysPARTNAME, QTY, sysRANKNO, "", "", "", userkey);
                DBT.InsertTScanLabel(sysCUSTOMER, sysPartID, sysPARTNO, sysPARTNAME, QTY, sysRANKNO, sysIssueNo, sysSerialNo, sysOrderNo, userkey, UNIQUEID, "", sysLable_Id);
                MatchingComplete MC = new MatchingComplete();
                MC.PartID = sysPartID;
                MC.MCPartno = sysPARTNO;
                MC.MCQTY = sysQTY;
                MC.MCRANNO = sysRANKNO;
                MC.ShowDialog();
            }
            else
            {
                MatchingError MC = new MatchingError();
                MC.DYNAPartno = sysPARTNO;
                MC.DYNAQTY = sysQTY;
                MC.DYNAORDERNO  = "" ;

                MC.CUSPartno = txtCusPartNo.Text.Trim();
                MC.CUSQTY = txtCusQTY.Text.Trim();
                MC.CUSORDERNO = txtOrderNo.Text.Trim();

                MC.ShowDialog();
            }

            ClearScreen();

        }

        public void ClearScreen()
        {
            txtCusPartNo.Text = "";
            txtCusQTY.Text = "";
            txtOrderNo.Text = "";
            txtDYNAPartNo.Text = "";
            lbDYNALot.Text = "";
            lbDYNAQTY.Text = "";

            txtCusPartNo.Focus();

        }

        private void ComparePartToyota_Load(object sender, EventArgs e)
        {
            ClearScreen();
        }

        private void pbScanMatchingList_Click(object sender, EventArgs e)
        {
            CallScanMatchingList();
        }

        public void CallScanMatchingList()
        {
            ScanMatchingList SML = new ScanMatchingList();
            SML.Customer = "TOYOTA";
            SML.UNIQUEID = UNIQUEID;
            SML.ShowDialog();
            //ClearScreen();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
            
        private void pictureBox4_Click(object sender, EventArgs e)
        {
            SendKeys.Send("{TAB}");
        }

        private void ComparePartToyota_KeyPress(object sender, KeyPressEventArgs e)
        {
            SendKeys.Send("{TAB}");
        }
    }
}