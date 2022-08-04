using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace CafeManagementSys
{
    public partial class UserForm : Form
    {
        public UserForm()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\SAINATH\Documents\Cafedb.mdf;Integrated Security=True;Connect Timeout=30");
        void populate()
        {
            con.Open();
            string query = "select * from UsersTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query,con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            UsersGV.DataSource = ds.Tables[0];
            con.Close();
        }
        private void sample_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            UserOrder uorder =new UserOrder();
            uorder.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ItemsForm item = new ItemsForm();
            item.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 login = new Form1();
            login.Show();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            con.Open();
            string query = "insert into UsersTbl values('"+UnameTb.Text+"','"+UphoneTb.Text+"','"+UpassTb.Text+"')";
            SqlCommand cmd = new SqlCommand(query,con);
            cmd.ExecuteNonQuery();
            MessageBox.Show("User Successfully Created...");
            con.Close();
            populate();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void UnameTb_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(UphoneTb.Text == "")
            {
                MessageBox.Show("Select The User to be Deleted...");
            }
            else
            {
                con.Open();
                string query = "delete from UsersTbl where Uphone = '"+UphoneTb.Text+"' ";
                SqlCommand cmd = new SqlCommand(query,con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Item Successfully Deleted...");
                con.Close();
                populate();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (UphoneTb.Text =="" || UpassTb.Text =="" || UnameTb.Text =="")
            {
                MessageBox.Show("Fill All The Fields...");
            }
            else
            {
                con.Open();
                string query = "update UsersTbl set Uname = '"+UnameTb.Text+"', Upassword ='"+UpassTb.Text+"' where Uphone = '"+UphoneTb.Text+"'";
                SqlCommand cmd = new SqlCommand(query,con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("User Successfully Updated...");
                con.Close();
                populate();
            }
        }

        private void UsersGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            UnameTb.Text = UsersGV.SelectedRows[0].Cells[0].Value.ToString();
            UphoneTb.Text = UsersGV.SelectedRows[0].Cells[1].Value.ToString();
            UpassTb.Text = UsersGV.SelectedRows[0].Cells[2].Value.ToString();
        }
    }
}
