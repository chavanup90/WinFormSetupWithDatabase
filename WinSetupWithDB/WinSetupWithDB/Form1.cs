using System.Data;
using System.Data.SqlClient;
using System.Net;

namespace WinSetupWithDB
{
    public partial class Form1 : Form
    {
        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Projects\WinSetupWithDB\WinSetupWithDB\Database1.mdf;Integrated Security=True";
        string conString = @"Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True;User Instance=True";
        public Form1()
        {
            InitializeComponent();
            dataLoad();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //SqlConnection con = new SqlConnection("Data Source=NiluNilesh;Integrated Security=True");  
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("sp_insert", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@name", textBox1.Text);
            cmd.Parameters.AddWithValue("@email", textBox2.Text);
            cmd.Parameters.AddWithValue("@phone", textBox3.Text);
            cmd.Parameters.AddWithValue("@address", textBox4.Text);
            con.Open();
            int i = cmd.ExecuteNonQuery();

            con.Close();
            dataLoad();
            if (i != 0)
            {
                MessageBox.Show(i + "Data Saved");
            }
        }

        private void dataLoad()
        {
            {
                DataTable dt = new DataTable();
                SqlConnection con = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand("sp_getAll", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                SqlDataAdapter adapt = new SqlDataAdapter(cmd);
                con.Open();
                adapt.Fill(dt);
                con.Close();
                if (dt.Rows.Count > 0)
                {
                    dataGridView1.DataSource = dt;                    
                }
            }
        }
    }
}