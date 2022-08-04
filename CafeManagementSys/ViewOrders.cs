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
    public partial class ViewOrders : Form
    {
        public ViewOrders()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\SAINATH\Documents\Cafedb.mdf;Integrated Security=True;Connect Timeout=30");

        void populate()
        {
            con.Open();
            string query = "select * from OrdersTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            OrdersGV.DataSource = ds.Tables[0];
            con.Close();
        }

        private void ViewOrders_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void OrdersGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString("- - - - - MyCafe - - - - -", new Font("Century", 25, FontStyle.Bold), Brushes.Red, new Point(260, 40));
            e.Graphics.DrawString("------ Order Summary ------", new Font("Century", 25, FontStyle.Bold), Brushes.Blue, new Point(220, 100));
            e.Graphics.DrawString("Order Number:" + OrdersGV.SelectedRows[0].Cells[0].Value.ToString(), new Font("Century", 15, FontStyle.Bold), Brushes.Black, new Point(210, 240));
            e.Graphics.DrawString("Order Date:" + OrdersGV.SelectedRows[0].Cells[1].Value.ToString(), new Font("Century", 15, FontStyle.Bold), Brushes.Black, new Point(210, 290));
            e.Graphics.DrawString("Seller:" + OrdersGV.SelectedRows[0].Cells[2].Value.ToString(), new Font("Century", 15, FontStyle.Bold), Brushes.Black, new Point(210, 340));
            e.Graphics.DrawString("Amount:" + OrdersGV.SelectedRows[0].Cells[3].Value.ToString(), new Font("Century", 15, FontStyle.Bold), Brushes.Black, new Point(210, 390));
            e.Graphics.DrawString("-*-*-* THANK YOU *-*-*-", new Font("Century", 25, FontStyle.Bold), Brushes.Blue, new Point(200, 500));
        }
    }
}
