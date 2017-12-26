using System;
using System.Data;
using System.Windows.Forms;
using System.Globalization;

namespace DYNAMatching
{
    public partial class ScanMatchingList : Form
    {

        public string Customer;
        public string username;
        public string userkey;
        public string UNIQUEID;
        public int SumLabel_Qty = 0;

        DBTransaction DBT = new DBTransaction();

        public ScanMatchingList()
        {
            InitializeComponent();
        }

        private void btSearch_Click(object sender, EventArgs e)
        {
            getDate();
        }

        private void getDate()
        {

            CultureInfo _cultureEngInfo = new CultureInfo("en-US");

            //var StartDate = dtStartDate.Value.ToString("yyyy-MM-dd", _cultureEngInfo);
            //var EndDate = dtEndDate.Value.ToString("yyyy-MM-dd", _cultureEngInfo);

            //string DateCondition = " (Convert(Date,TScan.CreateDate) >='" + StartDate + "' AND Convert(Date,TScan.CreateDate) <= '" + EndDate + "' ) ";

            string UNIQUECondition = " TimeToScan = '" + UNIQUEID + "' ";

            DataTable DTS = new DataTable();
            //string condition = getCustomerCondition();
            string condition = " and " + UNIQUECondition + getCustomerCondition();


            DTS = DBT.GetExcuteDataTableOneCon("spGetTScanLabelByCondition", condition);

            DTS.Columns.Remove("Part_Id");
            DTS.Columns.Remove("CreateBy");
            DTS.Columns.Remove("Log_Id");
            DTS.Columns.Remove("Part_Name");
            DTS.Columns.Remove("TimeToScan");
            



            foreach (DataRow row in DTS.Rows) {
                SumLabel_Qty += Convert.ToInt32(row["Label_Qty"]);
            }
            


            lbSumLabel_Qty.Text = SumLabel_Qty.ToString();

            if (DTS.Rows.Count > 0)
            {
               
                dgView.DataSource = DTS;
            }
            else
            {
                dgView.DataSource = null;
            }


        }

        private string getCustomerCondition()
        {
            var Condition = "";
            if (ckNissanNMT.Checked)
            {
                Condition = Condition + "'NISSAN NMT'";
            }
            if (ckNissanNPT.Checked)
            {

                if (Condition != "")
                {
                    Condition = Condition + " , ";
                }

                Condition = Condition + "'NISSAN NPT'";
            }
            if (ckToyota.Checked)
            {

                if (Condition != "")
                {
                    Condition = Condition + " , ";
                }

                Condition = Condition + "'TOYOTA'";
            }

            if (Condition != "")
            {
                Condition = " And (TScan.Customer in (" + Condition + ")) ";
            }

            return Condition;

        }

        private void txtCustomer_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                this.Close();
            }
        }

        private void ScanMatchingList_Load(object sender, EventArgs e)
        {

            lbUniqueID.Text = UNIQUEID;
            ckNissanNMT.Checked = false;
            ckNissanNPT.Checked = false;
            ckToyota.Checked = false;

            if (Customer == "NISSAN NMT")
            {
                ckNissanNMT.Checked = true;
            }

            if (Customer == "NISSAN NPT")
            {
                ckNissanNPT.Checked = true;
            }

            if (Customer == "TOYOTA")
            {
                ckToyota.Checked = true;
            }

            if (Customer != "")
            {
                getDate();
            }

            txtCustomer.Focus();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtCustomer_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
