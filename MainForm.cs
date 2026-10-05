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
using System.Web;



namespace Budgettracker
{
    public partial class MainForm : Form
    {

        string stringConnection = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\User\Documents\expense.mdf;Integrated Security=True;Connect Timeout=30";
        public MainForm()
        {
            

            InitializeComponent();

        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void close_click_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {



            try
            {
                SqlConnection con =
                new SqlConnection(stringConnection);

                con.Open();

                string query =
                @"INSERT INTO expenses (expenseid, expensename, amount, category, expensedate, paymentmethod) VALUES (@expenseid, @expensename, @amount, @category, @date, @paymentmethod)";

                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@expenseid",
                int.Parse(txtID.Text));

                cmd.Parameters.AddWithValue("@expensename",
                txtName.Text);

                decimal amount;

                if (!decimal.TryParse(txtAmount.Text.Trim(), out amount))
                {
                    MessageBox.Show("Invalid amount");
                    return;
                }

                cmd.Parameters.AddWithValue("@amount", amount);

                cmd.Parameters.AddWithValue("@category",
                cmbCategory.Text);

                cmd.Parameters.AddWithValue("@date",
                dateTimePicker1.Value.Date);

                cmd.Parameters.AddWithValue("@paymentmethod",
                txtPayment.Text);

                cmd.ExecuteNonQuery();

                con.Close();

                MessageBox.Show("Expense Added");
                btnView_Click(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnView_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(stringConnection);

            con.Open();

            SqlDataAdapter da =
            new SqlDataAdapter(
            "SELECT * FROM expenses",
            con);

            DataTable dt = new DataTable();

            da.Fill(dt);

            dataGridView1.AutoGenerateColumns = true;

            dataGridView1.DataSource = null;
            dataGridView1.DataSource = dt;

            con.Close();

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(stringConnection);

            con.Open();

            string query =
            @"UPDATE expenses SET expensename=@expensename, amount=@amount, category=@category, expensedate=@date, paymentmethod=@paymentmethod WHERE expenseid=@expenseid";

            SqlCommand cmd =
            new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@expenseid",
            txtID.Text);

            cmd.Parameters.AddWithValue("@expensename",
            txtName.Text);

            cmd.Parameters.AddWithValue("@amount",
            txtAmount.Text);

            cmd.Parameters.AddWithValue("@category",
            cmbCategory.Text);

            cmd.Parameters.AddWithValue("@date",
            dateTimePicker1.Value);

            cmd.Parameters.AddWithValue("@paymentmethod",
            txtPayment.Text);

            cmd.ExecuteNonQuery();

            con.Close();

            MessageBox.Show("Updated");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(stringConnection);

            con.Open();

            SqlCommand cmd =
            new SqlCommand(
            "DELETE FROM expenses WHERE expenseid=@id",
            con);

            cmd.Parameters.AddWithValue("@id",
            txtID.Text);

            cmd.ExecuteNonQuery();

            con.Close();

            MessageBox.Show("Deleted");
        }

        private void btnTotal_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(stringConnection);

            con.Open();

            SqlCommand cmd =
            new SqlCommand(
            "SELECT SUM(amount) FROM expenses",
            con);

            object result = cmd.ExecuteScalar();

            lblTotal.Text =
            "Total Expenses: $" + result;

            con.Close();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            Form1 login = new Form1();

            login.Show();

            this.Hide();
        }

        private void txtAmount_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
