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
    public partial class UserOrder : Form
    {
        public UserOrder()
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
        void filterbycategory()
        {
            con.Open();
            string query = "select * from ItemTbl where ItemCat = '" + categorycb.SelectedItem.ToString() + "' ";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            ItemGV.DataSource = ds.Tables[0];
            con.Close();
        }

        int num=0;
        int price, total;
        string item,cat;

        DataTable table = new DataTable();
        int flag = 0;
        int sum = 0;
        private void UserOrder_Load(object sender, EventArgs e)
        {
            populate();
            table.Columns.Add("Num",typeof(int));
            table.Columns.Add("Item",typeof(string));
            table.Columns.Add("Category",typeof(string));
            table.Columns.Add("UnitPrice",typeof(int));
            table.Columns.Add("Total",typeof(int));
            OrdersGV.DataSource = table;    
            Datelbl.Text = DateTime.Today.Day.ToString()+"/"+ DateTime.Today.Month.ToString() + "/" + DateTime.Today.Year.ToString();
            SellerName.Text = Form1.user;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (QtyTb.Text == "")
            {
                MessageBox.Show("What is the Quantity of an item?");
            }
            else if (flag==0)
            {
                MessageBox.Show("Select The Product To Be Ordered...");
            }
            else
            {
                num = num + 1;
                total = price * Convert.ToInt32(QtyTb.Text);
                table.Rows.Add(num,item,cat,price,total);
                OrdersGV.DataSource = table;
                flag = 0;
            }
            sum = sum + total;
            labelAmnt.Text = ""+sum;
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
            ItemsForm Item = new ItemsForm();
            Item.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            UserForm user = new UserForm();
            user.Show();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            filterbycategory();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            populate();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ItemGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            item = ItemGV.SelectedRows[0].Cells[1].Value.ToString();
            cat = ItemGV.SelectedRows[0].Cells[2].Value.ToString();
            price = Convert.ToInt32(ItemGV.SelectedRows[0].Cells[3].Value.ToString());
            flag = 1;
        }

        private void labelAmnt_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            ViewOrders view = new ViewOrders();
            view.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            con.Open();
            string query = "insert into OrdersTbl values("+OrderName.Text+",'" + Datelbl.Text + "','" + SellerName.Text + "', '" + labelAmnt.Text + "')";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Order Successfully Created...");
            con.Close();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

    }
}