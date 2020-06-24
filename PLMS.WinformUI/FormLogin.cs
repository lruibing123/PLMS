using PLMS.BLL;
using System;
using System.Configuration;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PLMS.Model;

namespace PLMS.WinformUI
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }        

        private void FormLogin_Load(object sender, EventArgs e)
        {
            BaseBLL.InitAll();
            radioButton1_CheckedChanged(null, null);
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            label3.Text = radioButton1.Checked && label3.Text.Contains("锁定") ? "错误 3 次已锁定" : "";            
        }

        private void buttonSignin_Click(object sender, EventArgs e)
        {
            label3.Text = textBox1.Text == "" ? "请输入Id" : textBox2.Text == "" ? "请输入密码" : "";
            if (label3.Text != "") return;
            string errorMsg;
            if (LoginBLL.Login(textBox1.Text, textBox2.Text, !radioButton1.Checked, out errorMsg))
            {
                textBox2.Text = "";
                FormManage form = new FormManage(radioButton2.Checked);
                form.Show(this);
                Hide();
            }
            label3.Text = errorMsg;
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                textBox2.Select();
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)            
                buttonSignin_Click(null, null);           
        }        
    }
}
