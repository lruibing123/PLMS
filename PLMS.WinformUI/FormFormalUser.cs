using PLMS.BLL;
using PLMS.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PLMS.WinformUI
{
    public partial class FormFormalUser : Form
    {
        FormManage form;
        string userId;
        Permissions permissions;

        public FormFormalUser()
        {
            InitializeComponent();
        }

        public FormFormalUser(Permissions permissions, string userId = null)
        {
            InitializeComponent();
            this.userId = userId;
            this.permissions = permissions;
        }

        private void AddFormalUser_Load(object sender, EventArgs e)
        {
            form = (FormManage)Owner;
            FormalUser formalUser = (FormalUser)AdminBLL.GetFormalUserById(userId);
            switch (permissions) {
                case Permissions.Add:
                    Text = "正式车办理";
                    break;
                case Permissions.Edit:
                    textBox1.Text = formalUser.LicensePlateNum;
                    textBox2.Text = formalUser.Phone;
                    buttonOK.Hide();
                    Text = "修改信息";
                    radioButton1.Enabled = radioButton2.Enabled = false;
                    break;
                case Permissions.Delay:
                    textBox1.Text = formalUser.LicensePlateNum;
                    textBox2.Text = formalUser.Phone;
                    buttonOK.Hide();
                    Text = "正式车续期";
                    textBox1.Enabled = textBox2.Enabled = false;
                    break;
                case Permissions.Up:
                    Text = "临时车升级";
                    CasualUser casualUser = (CasualUser)AdminBLL.GetCasualUsersById(userId);
                    textBox1.Text = casualUser.LicensePlateNum;
                    textBox1.Enabled = false;
                    break;
            }
        }

        private void buttonYes_Click(object sender, EventArgs e)
        {
            try
            {
                switch (permissions)
                {
                    case Permissions.Add:
                        if (Input())
                            Close();
                        break;
                    case Permissions.Edit:
                        if (!CheckInoput())
                            break;
                        FormalUser formalUser = (FormalUser)AdminBLL.GetFormalUserById(userId);
                        if (formalUser != null)
                        {
                            formalUser.LicensePlateNum = textBox1.Text;
                            formalUser.Phone = textBox2.Text;
                            form.Refresh();
                            Close();
                        }
                        break;
                    case Permissions.Delay:
                        int months = radioButton1.Checked ? 3 : 12;
                        string err;
                        if (AdminBLL.DelayExpirationTime(userId, months, out err))
                        {
                            form.Refresh();
                            MessageBox.Show(err);
                            Close();
                        }
                        break;
                    case Permissions.Up:
                        if (Input())
                        {
                            if (AdminBLL.RemoveCasualUserById(userId))
                            {
                                form.Refresh();
                                Close();
                            }
                        }                                                            
                        break;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("操作错误");
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            Input();
            textBox1.Text = textBox2.Text = "";
        }

        private void buttonNo_Click(object sender, EventArgs e)
        {
            Close();
        }

        private bool Input()
        {
            if (!CheckInoput()) return false;
            FormalUser formalUser = new FormalUser(textBox1.Text, textBox2.Text);
            int months = radioButton1.Checked ? 3 : 12;
            string err;
            if (AdminBLL.AddFormalUser(formalUser, months, out err))
            {
                form.Refresh();
                MessageBox.Show(err);
                return true;
            }
            MessageBox.Show(err);
            return false;
        }

        private bool CheckInoput()
        {
            label4.Text =
                textBox1.Text == "" ? "请输入车牌号" :
                !new Regex(@"^[(\u4e00-\u9fa5)]{1}[A-Z]{1}[a-zA-Z_0-9]{4,6}[a-zA-Z_0-9_\u4e00-\u9fa5]$").IsMatch(textBox1.Text) ? "车牌号格式不对" :
                textBox2.Text == "" ? "请输入手机号" :
                !new Regex(@"^[1]+\d{10}").IsMatch(textBox2.Text) ? "手机号格式不对" : "";
            if (label4.Text != "") return false;
            return true;
        }

        public enum Permissions {
            Add = 1,
            Edit = 2,
            Delay = 3,
            Up = 4
        }
    }
}
