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

namespace DB5
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string cnstr = @"Data Source = (LocalDB)\MSSQLLocalDB;" +
                       "AttachDbFilename = |DataDirectory|ch18DB.mdf;" +
                       "Integrated Security = True";
        void ShowData()
        {
            using (SqlConnection cn = new SqlConnection())
            {
                cn.ConnectionString = cnstr;    
                SqlDataAdapter daEmplotee = new SqlDataAdapter("SELECT * FROM 員工 ORDER BY 編號 ASC",cn);
                DataSet ds = new DataSet();
                daEmplotee.Fill(ds,"員工");
                dataGridView1.DataSource = ds.Tables["員工"];
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            labelID.Text = "";
            ShowData();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection())
                {
                    cn.ConnectionString = cnstr;
                    cn.Open();
                    string sqlStr = $"SELECT * FROM 員工 WHERE 姓名 ='{txtName.Text.Replace("'", "''")}'";
                    SqlCommand cmd = new SqlCommand(sqlStr, cn);
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        labelID.Text = $"{dr["編號"].ToString()}";
                        txtPosition.Text = $"{dr["職稱"].ToString()}";
                        txtPhone.Text = $"{dr["電話"].ToString()}";
                        txtSalary.Text = $"{dr["薪資".ToString()]}";
                    }
                    else
                    {
                        MessageBox.Show("找不到該名職員");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message+ ",查詢資料發生錯誤");
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection())
                {
                    cn.ConnectionString = cnstr;
                    cn.Open();
                    string sqlStr = $"INSERT INTO 員工(姓名,職稱,電話,薪資) VALUES('{txtName.Text.Replace("'", "''")}'," +
                        $"'{txtPosition.Text.Replace("'", "''")}','{txtPhone.Text.Replace("'", "''")}',{int.Parse(txtSalary.Text)})";
                    SqlCommand cmd = new SqlCommand(sqlStr, cn);
                    cmd.ExecuteNonQuery();
                }
                ShowData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message+",新增資料發生錯誤");
            }
        }


        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                using(SqlConnection cn = new SqlConnection())
                {
                    cn.ConnectionString= cnstr;
                    cn.Open();
                    string sqlStr = $"UPDATE 員工 SET 職稱='{txtPosition.Text.Replace("'", "''")}'," +
                        $"電話='{txtPhone.Text.Replace("'", "''")}',薪資={int.Parse(txtSalary.Text)} WHERE 姓名='{txtName.Text.Replace("'","''")}'";
                    SqlCommand cmd = new SqlCommand( sqlStr, cn);
                    cmd.ExecuteNonQuery();
                }
                ShowData();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message + ",修改資料發生錯誤");
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            using (SqlConnection cn = new SqlConnection())
            {
                cn.ConnectionString = cnstr;
                cn.Open();
                string sqlStr = $"DELETE FROM 員工 WHERE 姓名='{txtName.Text.Replace("'", "''")}'";
                SqlCommand cmd = new SqlCommand(sqlStr, cn);
                cmd.ExecuteNonQuery();
            }
            ShowData();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            labelID.Text = "";
            txtName.Text = "";
            txtPosition.Text = "";
            txtPhone.Text = "";
            txtSalary.Text = "";
        }
    }
}
