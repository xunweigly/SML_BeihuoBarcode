using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using LKU8.shoukuan;

namespace fuzhu
{
    public partial class FormSetUp : Form
    {
        public FormSetUp()
        {
            InitializeComponent();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            string sql = string.Format("update zdy_pagesetup set gao ={0},kuan = {1},shang = {2},xia = {3},zuo ={4},you ={5}",
                txtgao.Text,txtkuan.Text,txtshang.Text,txtxia.Text,txtzuo.Text,txtyou.Text );
                         
                int iResult = DbHelper.ExecuteNonQuery(sql);
                if (iResult > 0)
                {
                    MessageBox.Show("添加成功", "提示");
                    canshu.igao = Convert.ToDecimal(txtgao.Text);
                    canshu.ikuan = Convert.ToDecimal(txtkuan.Text);
                    canshu.ishang = Convert.ToDecimal(txtshang.Text);
                    canshu.ixia = Convert.ToDecimal(txtxia.Text);
                    canshu.izuo = Convert.ToDecimal(txtzuo.Text);
                    canshu.iyou = Convert.ToDecimal(txtyou.Text);


                }
                else
                {
                 MessageBox.Show("数据库连接失败", "提示");
                }

        
        }

        private void FormSetUp_Load(object sender, EventArgs e)
        {
            txtgao.Text = canshu.igao.ToString();
            txtkuan.Text = canshu.ikuan.ToString();
            txtshang.Text = canshu.ishang.ToString();
            txtxia.Text = canshu.ixia.ToString();
            txtzuo.Text = canshu.izuo.ToString();
            txtyou.Text = canshu.iyou.ToString();

        }
    }
}
