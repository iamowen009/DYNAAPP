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
    public partial class ComparePart : Form
    {

        public string Customer;
        public string username;
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
        public string sysOrderNo = "";
        DBTransaction DBT = new DBTransaction();

        public ComparePart()
        {
            InitializeComponent();
        }

        private void ComparePart_Load(object sender, EventArgs e)
        {
            lbCustomer.Text = Customer;

            if (Customer.Contains("Nissan"))
                pbCustomerPart.Image = Properties.Resources.Nissan;
            else
                pbCustomerPart.Image = Properties.Resources.toyota;

            pbCustomerPart.Refresh();
            pbCustomerPart.Visible = true;

            ClearScreen(); 

        }

        public void MatchingResult(string Results)
        {
            if (Results == "Complete")
            {

                decimal QTY = decimal.Parse(sysQTY);

                DBT.InsertTScanLabel(sysCUSTOMER,sysPartID,sysPARTNO,sysPARTNAME, QTY, sysRANKNO , txtCusIssueNo.Text , "", "", username, UNIQUEID);
                MatchingComplete MC = new MatchingComplete();
                MC.PartID = sysPartID;
                MC.MCPartno = sysPARTNO;
                MC.MCIssueNO = txtCusIssueNo.Text;
                MC.MCQTY = sysQTY;
                MC.MCRANNO = sysRANKNO;
                MC.ShowDialog();
            }
            else
            {
                MatchingError MC = new MatchingError();
                MC.DYNAPartno = sysPARTNO;
                MC.DYNAIssueNO = txtCusIssueNo.Text;
                MC.DYNAQTY = sysQTY;
                MC.DYNARANNO = sysRANKNO;

                MC.ShowDialog();
            }

            ClearScreen();

        }

        public void ClearScreen()
        {
            txtCusPartNo.Text = "";
            txtCusQTY.Text = "";
            txtCusIssueNo.Text = "";
            txtCusRAN.Text = "";
            txtSerialNo.Text = "" ;
            txtOrderNo.Text = "";
            txtDYNAPartNo.Text = "";
            lbDYNALot.Text = "";
            lbDYNAQTY.Text = "";

            txtCusPartNo.Focus();

        }

        public void CheckMatching()
        {
            if (txtCusPartNo.Text.Trim() == txtDYNAPartNo.Text.Trim())
            {
                MatchingResult("Complete");
            }
            else
            {
                MatchingResult("Error");
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

            //try
            //{
            //    MemoryStream ms = new MemoryStream(byteArrayIn, 0, byteArrayIn.Length);
            //    ms.Write(byteArrayIn, 0, byteArrayIn.Length);
            //    Image returnImage = Image.FromStream(ms, true);
            //}
            //catch { }
            //return returnImage;

        }

        private void txtCusPartNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                CheckToyotaPartNo();

                txtCusPartNo.Text = txtCusPartNo.Text.Trim();
                txtCusPartNo.Text = txtCusPartNo.Text.Substring(1, txtCusPartNo.Text.Length - 1);

                DataTable DTS = new DataTable();
                string condition = " and Part_no = '" + txtCusPartNo.Text.Trim() + "'" ;
                DTS = DBT.GetExcuteDataTableOneCon("spGetMPartByConditionForApp", condition);

                if (DTS.Rows.Count > 0)
                {
                    if ((DTS.Rows[0]["Picture"] != null) || (DTS.Rows[0]["Picture"].ToString().Trim() != ""))
                    {
                        pbCusImage.Image = byteArrayToImage(DTS.Rows[0]["Picture"] as byte[]);
                    }
                    txtCusRAN.Text = DTS.Rows[0]["Part_Name"].ToString();
                }

                txtCusQTY.Focus();
            }
        }

        private string GetPartID(string PartNo,string DRAWINGNO , string RANKNO)
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

        private void txtCusQTY_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                txtCusQTY.Text = txtCusQTY.Text.Trim();
                txtCusQTY.Text = txtCusQTY.Text.Substring(1, txtCusQTY.Text.Length - 1);
                txtCusQTY.Text = txtCusQTY.Text.Trim();
                txtCusIssueNo.Focus();
            }
        }

        private void txtCusIssueNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                txtCusRAN.Focus();
                //txtSerialNo.Focus();
                //txtOrderNo.Text = "";
            }
        }

        private void txtCusRAN_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                //txtDYNAPartNo.Focus();
                txtSerialNo.Focus();
            }
        }

        public void CheckDYNAPartNo()
        {
            //if (txtDYNAPartNo.Text.Trim() == "")
            //{
            // MatchingResult("Complete");
            //string value = "CUSTOMER^LABLENO^PARTNO^PARTNAME^DRAWINGNO^RANKNO^QTY^LOT^PRODUCTDATE" ;
                string value = txtDYNAPartNo.Text.Trim() ;
                SpilitString(value);
                txtDYNAPartNo.Text = sysPARTNO;
                lbDYNAQTY.Text = sysQTY;
                lbDYNALot.Text = sysLOT;
                sysPartID = GetPartID(sysPARTNO,sysDRAWINGNO,sysRANKNO);
                CheckMatching();
            //}
            //else
            //{
            //    // MatchingResult("Error");
            //}
        }

        public void SpilitString(string value)
        {
            int n = 0;
            string[] separators = { "^" };
            string[] words = value.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            foreach (var word in words)
            {
                n++;
                //listBox1.Items.Add(n.ToString() + " - " + word);
                switch (n)
                {
                    case 1:
                        sysCUSTOMER = word ;
                        break;
                    case 2:
                        sysLABLENO = word ;
                        break;
                    case 3:
                        sysPARTNO = word ;
                        break;
                    case 4:
                        sysPARTNAME = word ;
                        break;
                    case 5:
                        sysDRAWINGNO = word ;
                        break;
                    case 6:
                        sysRANKNO = word ;
                        break;
                    case 7:
                        sysQTY = word ;
                        break;
                    case 8:
                        sysLOT = word ;
                        break;
                    case 9:
                        sysPRODUCTDATE = word ;
                        break;
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
                listBox1.Items.Add(n.ToString() + " - " + word);

                switch (n)
                {
                    case 2:
                        sysOrderNo = word;
                        break;
                    case 3:
                        sysPARTNO = word;
                        break;
                    case 4:
                        sysQTY = word;
                        break;
                }

                //switch (n)
                //{
                //    case 1:
                //        sysCUSTOMER = word;
                //        break;
                //    case 2:
                //        sysLABLENO = word;
                //        break;
                //    case 3:
                //        sysPARTNO = word;
                //        break;
                //    case 4:
                //        sysPARTNAME = word;
                //        break;
                //    case 5:
                //        sysDRAWINGNO = word;
                //        break;
                //    case 6:
                //        sysRANKNO = word;
                //        break;
                //    case 7:
                //        sysQTY = word;
                //        break;
                //    case 8:
                //        sysLOT = word;
                //        break;
                //    case 9:
                //        sysPRODUCTDATE = word;
                //        break;
                //}

            }

        }

        public void CheckToyotaPartNo()
        {
            string value = txtCusPartNo.Text.Trim();
            SpilitStringBlank(value);
            txtCusPartNo.Text = sysPARTNO;
            txtCusQTY.Text = sysQTY;
            txtOrderNo.Text = sysOrderNo;

            sysPartID = GetPartID(sysPARTNO, sysDRAWINGNO, sysRANKNO);
            // CheckMatching();
        }

        private void txtDYNAPartNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                CheckDYNAPartNo();
            }  
        }

        private void txtSerialNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                txtOrderNo.Focus();
            }
        }

        private void txtOrderNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                txtDYNAPartNo.Focus();
            }
        }
    }
}
