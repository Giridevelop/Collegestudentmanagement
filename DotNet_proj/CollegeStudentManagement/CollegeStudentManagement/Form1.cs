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

namespace CollegeStudentManagement
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            load();
        }

        SqlConnection con = new SqlConnection(@"data source=(localdb)\v11.0;initial catalog=College;integrated security=True");
        SqlCommand cmd;
        SqlDataReader read;
        string Admission_no;
        bool mode = true;
        string sql;


        public void load()
        {
            try
            { 
                sql = "select * from std_Records";
                cmd = new SqlCommand(sql, con);
                con.Open();
                read = cmd.ExecuteReader();
                dataGridView1.Rows.Clear();
                while (read.Read())
                {
                    dataGridView1.Rows.Add(read[0], read[1], read[2], read[3], read[4],read[5],read[6]);
                }
                con.Close();

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            } 
        }

        public void getAddno(string Addno)
        {
            sql = "select * from std_Records where Admission_no='" + Admission_no + "'";
            cmd = new SqlCommand(sql, con);
            con.Open();
            read = cmd.ExecuteReader();
            while (read.Read()) 
            {
                txtroll.Text = read[1].ToString();
                txtname.Text = read[2].ToString();
                txtphone.Text = read[3].ToString();
                txtmail.Text = read[4].ToString();
                txtaadhar.Text = read[5].ToString();
                txtnative.Text = read[6].ToString();
            }
            con.Close();



        }


        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int roll = int.Parse(txtroll.Text);
            string name = txtname.Text;
            long phone = long.Parse(txtphone.Text);
            string mail = txtmail.Text;
            long aadhar = long.Parse(txtaadhar.Text);
            string native = txtnative.Text;

            if (mode == true)
            {
                sql = "insert into std_Records(Roll_no,Std_Name,Phone_no,Mail_id,Aadhar_no,Native_Place) values(@Roll_no,@Std_Name,@Phone_no,@Mail_id,@Aadhar_no,@Native_Place)";
                con.Open();
                cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@Roll_no", roll);
                cmd.Parameters.AddWithValue("@Std_Name", name);
                cmd.Parameters.AddWithValue("@Phone_no", phone);
                cmd.Parameters.AddWithValue("@Mail_id", mail);
                cmd.Parameters.AddWithValue("@Aadhar_no", aadhar);
                cmd.Parameters.AddWithValue("@Native_Place", native);
                MessageBox.Show("Record Added");
                cmd.ExecuteNonQuery();

                txtroll.Clear();
                txtname.Clear();
                txtphone.Clear();
                txtmail.Clear();
                txtaadhar.Clear();
                txtnative.Clear();
                txtroll.Focus();
            }
            else
            {
                Admission_no = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                sql = "update std_Records set Roll_no=@Roll_no,Std_Name=@Std_Name,Phone_no=@Phone_no,Mail_id=@Mail_id,Aadhar_no=@Aadhar_no,Native_Place=@Native_Place where Admission_no=@Admission_no";
                con.Open();
                cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@Roll_no", roll);
                cmd.Parameters.AddWithValue("@Std_Name", name);
                cmd.Parameters.AddWithValue("@Phone_no", phone);
                cmd.Parameters.AddWithValue("@Mail_id", mail);
                cmd.Parameters.AddWithValue("@Aadhar_no", aadhar);
                cmd.Parameters.AddWithValue("@Native_Place", native);
                cmd.Parameters.AddWithValue("@Admission_no", Admission_no);

                MessageBox.Show("Record Updated");
                cmd.ExecuteNonQuery();

                txtroll.Clear();
                txtname.Clear();
                txtphone.Clear();
                txtmail.Clear();
                txtaadhar.Clear();
                txtnative.Clear();
                txtroll.Focus();
                button1.Text = "Save";
                mode = true;    

            }

            con.Close();



        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           if(e.ColumnIndex==dataGridView1.Columns["Edit"].Index && e.RowIndex >=0)
           {
               mode = false;
               Admission_no = dataGridView1.CurrentRow.Cells[0].Value.ToString();
               getAddno(Admission_no);
               button1.Text = "Edit";   
           }
           else if (e.ColumnIndex == dataGridView1.Columns["Delete"].Index && e.RowIndex >= 0)
           {
               mode = false;
               Admission_no = dataGridView1.CurrentRow.Cells[0].Value.ToString();
               sql = "delete from std_Records where Admission_no=@Admission_no";
               con.Open();
               cmd = new SqlCommand(sql, con);
               cmd.Parameters.AddWithValue("@Admission_no", Admission_no);
               cmd.ExecuteNonQuery();
               MessageBox.Show("deleted");
               con.Close();
               load();
           }
         
        }

        private void button3_Click(object sender, EventArgs e)
        {
            load();
        }
    }
}
