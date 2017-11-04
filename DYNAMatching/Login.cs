using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DYNAMatching
{
    public partial class Login : Form
    {

        DBTransaction DBT = new DBTransaction();

        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            try
            {
                //txtUsername.Text = "admin";
                //txtPassword.Text = "1234";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            

        }

        private void btOk_Click(object sender, EventArgs e)
        {
            Login_Click();
        }

        private void Login_Click() {
            DataTable DTS = new DataTable();
            DTS = DBT.GetUserInfoLogin(txtUsername.Text, txtPassword.Text);

            if (DTS.Rows.Count > 0)
            {
                if (DTS.Rows[0]["resultcode"].ToString() == "1")
                {
                    this.Visible = false;
                    MainScreen MS = new MainScreen();
                    MS.UserKey = DTS.Rows[0]["key1"].ToString();
                    MS.UNIQUEID = DTS.Rows[0]["UNIQUEID"].ToString();
                    MS.UserName = txtUsername.Text.Trim();

                    DataTable DTUSER = new DataTable();
                    string Condition = " AND U_Id='" + DTS.Rows[0]["key1"].ToString() + "'";
                    DTUSER = DBT.GetMUserByCondition(Condition);
                    MS.FirstName = DTUSER.Rows[0]["FirstName"].ToString(); 

                    MS.ShowDialog();
                }
                else
                {
                    //MessageBox("Incorrect Username or Password. Please try again.");
                    MessageBox.Show("Incorrect Username or Password. Please try again.", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtUsername_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                Login_Click();
            }
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                Login_Click();
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
