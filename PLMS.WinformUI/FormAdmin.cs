using PLMS.BLL;
using PLMS.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PLMS.WinformUI
{
    public partial class FormAdmin : Form
    {
        FormManage form;
        string adminId;
        Permissions permissions;
        public FormAdmin()
        {
            InitializeComponent();
        }

        public FormAdmin(Permissions permissions, string adminId = null)
        {
            InitializeComponent();
            this.permissions = permissions;
            this.adminId = adminId;
        }

        private void AddAdmin_Load(object sender, EventArgs e)
        {
            form = (FormManage)Owner;
            switch (permissions) {
                case Permissions.Add:
                    Text = "增加管理员";
                    break;
                case Permissions.Edit:
                    Text = "修改管理员密码";
                    textBox1.Enabled = false;
                    buttonOK.Hide();
                    Admin admin = AdminBLL.GetAdminById(adminId);
                    textBox1.Text = admin.Id;
                    textBox2.Text = admin.Password;
                    break;
            }
        }

        private void buttonYes_Click(object sender, EventArgs e)
        {
            switch (permissions)
            {
                case Permissions.Add:
                    label3.Text = "";
                    if (Input())
                        Close();
                    break;
                case Permissions.Edit:
                    AdminBLL.GetAdminById(adminId).Password = textBox2.Text;
                    form.Refresh();
                    MessageBox.Show("修改成功");
                    Close();
                    break;
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
            Admin admin = new Admin(textBox1.Text, textBox2.Text);
            if (AdminBLL.AddAdmin(admin))
            {
                form.Refresh();
                MessageBox.Show("增加成功");
                return true;
            }
            label3.Text = "Id已存在";
            return false;
        }

        public enum Permissions
        {
            Add = 1,
            Edit = 2
        }
    }
}
