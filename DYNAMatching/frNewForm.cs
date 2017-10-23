using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace DYNAMatching
{
    public partial class frNewForm : Form
    {

        //DBTransaction DBT = new DBTransaction();

        public frNewForm()
        {
            InitializeComponent();
        }

        private void frNewForm_Load(object sender, EventArgs e)
        {
            string connetionString = null;
            SqlConnection cnn;
            connetionString = ConfigurationManager.ConnectionStrings["DBDYNA"].ConnectionString;
            //connetionString = "Data Source=192.168.100.212;Initial Catalog=Dyna;User ID=sa;Password=P@ssw0rd";
            //connetionString = "data source=(local);Initial Catalog=DYNA;User ID=sa;Password=123;Integrated Security=SSPI" providerName = "System.Data.SqlClient";
            cnn = new SqlConnection(connetionString);
            try
            {
                cnn.Open();
                MessageBox.Show("Connection Open ! ");
                cnn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Can not open connection ! ");
            }
        }
    }
}
