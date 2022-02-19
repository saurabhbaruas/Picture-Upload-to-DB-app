using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO; 
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Picture_Upload_to_DB
{
    public partial class Form1 : Form
    {
        string str = @"Data Source=LAPTOP-2OU8NEFO\SQLEXPRESS;Initial Catalog=SMDB;Integrated Security=True;";
        SqlConnection con;
        public Form1()
        {
            InitializeComponent();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "" && textBox5.Text != "" && comboBox1.SelectedIndex != -1)
            {
                if (img_file != null)
                {
                    if (textBox3.Text == textBox4.Text)
                    {
                        FileStream s1 = new FileStream(img_file, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                        byte[] image = new byte[s1.Length];
                        s1.Read(image, 0, Convert.ToInt32(s1.Length));
                        s1.Close();
                        SqlCommand cmd = new SqlCommand("insert into Pic(name,department,password,Question,answer,roll_no,images) values ('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + comboBox1.Text + "','" + textBox5.Text + "','" + label8.Text+ "','@pic' ) ", con);
                        SqlParameter prm = new SqlParameter("@pic", SqlDbType.VarBinary, image.Length, ParameterDirection.Input, false, 0, 0, null, DataRowVersion.Current, image);
                        cmd.Parameters.Add(prm);
                        cmd.ExecuteNonQuery();
                        label10.ForeColor = Color.Green;
                        label10.Text = "User added into Database";

                        // con.Close();


                    }
                    else
                    {
                        label10.ForeColor = Color.Pink;
                        label10.Text = "Please provide Same Password For Confirm Password";

                    }

                }
                else
                {
                    label10.ForeColor = Color.Red;
                    label10.Text = "Pleease Upload Image";
                }
            }
            else
            {
                label10.ForeColor = Color.Red;
                label10.Text = "Pleease Enter All the Detail";

            }
        }

            private void Form1_Load(object sender, EventArgs e)
            {
                con = new SqlConnection(str);
                con.Open();
                label8.Text = getroll();
            }
            string getroll()
            {
                string qry = "Select Count(*) from Pic";
                SqlCommand cmd = new SqlCommand(qry, con);
                int a = 101 + (Int32)cmd.ExecuteScalar();
                string reg = "A" + a.ToString();
                return (reg);
            }

            string img_file = null;
            private void button1_Click(object sender, EventArgs e)
            {
                OpenFileDialog o = new OpenFileDialog();
                if (o.ShowDialog() != DialogResult.Cancel)
                {
                    img_file = o.FileName;
                    pictureBox1.Image = Image.FromFile(o.FileName);
                }
            }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
