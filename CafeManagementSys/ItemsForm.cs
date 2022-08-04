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
    public partial class ItemsForm : Form
    {
        public ItemsForm()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\SAINATH\Documents\Cafedb.mdf;Integrated Security=True;Connect Timeout=30");
        void populate()
        {
            con.Open();
            string query = "select * from ItemTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            ItemGV.DataSource = ds.Tables[0];
            con.Close();
        }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 login = new Form1();
            login.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            UserOrder uorder = new UserOrder();
            uorder.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            UserForm uf = new UserForm();
            uf.Show();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (ItemNameTb.Text == "" || ItemNumTb.Text == "" || ItemPriceTb.Text == "")
            {
                MessageBox.Show("Fill All The Data...");
            }
            else
            {
                con.Open();
                string query = "insert into ItemTbl values(" + ItemNumTb.Text + ",'" + ItemNameTb.Text + "','" + CatCb.SelectedItem.ToString()+ "', "+ItemPriceTb.Text+")";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Item Successfully Added...");
                con.Close();
                populate();
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void ItemsForm_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (ItemNumTb.Text == "")
            {
                MessageBox.Show("Select The Item to be Deleted...");
            }
            else
            {
                con.Open();
                string query = "delete from ItemTbl where ItemNum = '" + ItemNumTb.Text + "' ";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Item Successfully Deleted...");
                con.Close();
                populate();
            }
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            if (ItemNumTb.Text == "" || ItemNameTb.Text == "" || ItemPriceTb.Text == "")
            {
                MessageBox.Show("Fill All The Fields...");
            }
            else
            {
                con.Open();
                string query = "update ItemTbl set ItemName = '" + ItemNameTb.Text + "', ItemCat ='" + CatCb.SelectedItem.ToString() + "' where ItemPrice = '" + ItemPriceTb.Text + "'";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Item Successfully Updated...");
                con.Close();
                populate();
            }
        }
        private void ItemGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            ItemNumTb.Text = ItemGV.SelectedRows[0].Cells[0].Value.ToString();
            ItemNameTb.Text = ItemGV.SelectedRows[0].Cells[1].Value.ToString();
            CatCb.SelectedItem = ItemGV.SelectedRows[0].Cells[2].Value.ToString();
            ItemPriceTb.Text = ItemGV.SelectedRows[0].Cells[3].Value.ToString();
        }
    }
}