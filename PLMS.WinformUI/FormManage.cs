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
    public partial class FormManage : Form
    {
        public bool isRoot;

        public FormManage()
        {
            InitializeComponent();
        }

        public FormManage(bool isRoot)
        {
            InitializeComponent();
            this.isRoot = isRoot;
        }

        private void FormManage_Load(object sender, EventArgs e)
        {
            if (!isRoot)  tabPage5.Parent = null;           
            Location = new Point(100,100);
            dataGridView1.AutoGenerateColumns = false;            
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView2.AutoGenerateColumns = false;            
            dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView3.AutoGenerateColumns = false;
            dataGridView3.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView4.AutoGenerateColumns = false;
            dataGridView4.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView5.AutoGenerateColumns = false;
            dataGridView5.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            tabControl1_SelectedIndexChanged(null, null);

            FormSimulate form = new FormSimulate();
            form.Show(this);
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tabControl1.SelectedIndex)
            {
                case 0:
                    dataGridView1.DataSource = AdminBLL.GetFormalUsers();
                    buttonAdd.ForeColor = buttonEdit.ForeColor = Color.White;
                    buttonAdd.Cursor = buttonEdit.Cursor = Cursors.Default;
                    break;
                case 1:
                    dataGridView2.DataSource = AdminBLL.GetCasualUsers();                    
                    buttonAdd.ForeColor = buttonEdit.ForeColor = Color.CadetBlue;
                    buttonAdd.Cursor = buttonEdit.Cursor = Cursors.No;
                    break;
                case 2:
                    dataGridView3.DataSource = AdminBLL.GetParks();
                    buttonAdd.ForeColor = buttonEdit.ForeColor = Color.CadetBlue;
                    buttonAdd.Cursor = buttonEdit.Cursor = Cursors.No;
                    break;
                case 3:
                    dataGridView4.DataSource = AdminBLL.GetOrders();
                    buttonAdd.ForeColor = buttonEdit.ForeColor = Color.CadetBlue;
                    buttonAdd.Cursor = buttonEdit.Cursor = Cursors.No;
                    break;
                case 4:
                    dataGridView5.DataSource = AdminBLL.GetAdmins();
                    buttonAdd.ForeColor = buttonEdit.ForeColor = Color.White;
                    buttonAdd.Cursor = buttonEdit.Cursor = Cursors.Default;
                    break;
            }
        }

        public new void Refresh()
        {
            BaseBLL.SaveALL();
            BaseBLL.InitAll();
            dataGridView1.DataSource = AdminBLL.GetFormalUsers();
            dataGridView2.DataSource = AdminBLL.GetCasualUsers();
            dataGridView3.DataSource = AdminBLL.GetParks();
            dataGridView4.DataSource = AdminBLL.GetOrders();
            dataGridView5.DataSource = AdminBLL.GetAdmins();
        }

        private void FormManage_FormClosing(object sender, FormClosingEventArgs e)
        {
            MessageBoxButtons messButton = MessageBoxButtons.OKCancel;
            DialogResult dr = MessageBox.Show("确认登出，取消直接退出", "您要登出吗", messButton);
            if (dr == DialogResult.OK)
            {
                Owner.Show();
            }
            else
            {
                Dispose();
                Application.Exit();
            }            
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            switch (tabControl1.SelectedIndex) {
                case 0:
                    FormFormalUser formFormalUser = new FormFormalUser(FormFormalUser.Permissions.Add);
                    formFormalUser.ShowDialog(this);
                    break;
                case 4:
                    FormAdmin formAdmin = new FormAdmin(FormAdmin.Permissions.Add);
                    formAdmin.ShowDialog(this);
                    break;
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show(this, "确定要删除选中行数据码？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialog == DialogResult.Yes)
            {
                switch (tabControl1.SelectedIndex) {
                    case 0:
                        DeleteFormalUser(dataGridView1);
                        break;
                    case 1:
                        DeleteCasualUser(dataGridView2);
                        break;
                    case 2:
                        DeletePark(dataGridView3);
                        break;
                    case 3:
                        DeleteOrder(dataGridView4);
                        break;
                    case 4:
                        DeleteAdmin(dataGridView5);
                        break;
                }
            }                            
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            switch (tabControl1.SelectedIndex)
            {
                case 0:
                    string userId = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                    FormFormalUser form1 = new FormFormalUser(FormFormalUser.Permissions.Edit, userId);
                    form1.ShowDialog(this);
                    break;
                case 4:
                    string adminId = dataGridView5.SelectedRows[0].Cells[0].Value.ToString();
                    FormAdmin form2 = new FormAdmin(FormAdmin.Permissions.Edit, adminId);
                    form2.ShowDialog(this);
                    break;
            }
        }

        private void buttonFind_Click(object sender, EventArgs e)
        {
            string findStr = textBoxFindStr.Text;
            if (findStr == "")
            {
                tabControl1_SelectedIndexChanged(null, null);
                return;
            }
            switch (tabControl1.SelectedIndex)
            {
                case 0:
                    dataGridView1.DataSource = AdminBLL.GetFormalUsers().Where(f => f.LicensePlateNum == findStr || f.Phone == findStr).ToList();
                    break;
                case 1:
                    dataGridView2.DataSource = AdminBLL.GetCasualUsers().Where(c => c.LicensePlateNum == findStr).ToList();
                    break;
                case 2:
                    dataGridView3.DataSource = AdminBLL.GetParks().Where(p => p.LicensePlateNum == findStr).ToList();
                    break;
                case 3:
                    User user = VehicleBLL.GetUserByLicensePlateNum(findStr);
                    if(user != null)
                        dataGridView4.DataSource = AdminBLL.GetOrders().Where(o => o.UserId == user.UserId).ToList();
                    break;
                case 4:
                    dataGridView5.DataSource = AdminBLL.GetAdmins().Where(a => a.Id == findStr).ToList();
                    break;
            }
        }

        #region delete operations
        private void DeleteFormalUser(DataGridView dataGridView)
        {
            for (int i = dataGridView.SelectedRows.Count - 1; i >= 0; i--)
            {
                string userId = dataGridView.SelectedRows[0].Cells[0].Value.ToString();
                if (AdminBLL.RemoveFormalUserById(userId))
                    Refresh();
                else
                    MessageBox.Show("删除失败");
            }
        }

        private void DeleteCasualUser(DataGridView dataGridView)
        {
            for (int i = dataGridView.SelectedRows.Count - 1; i >= 0; i--)
            {
                string userId = dataGridView.SelectedRows[0].Cells[0].Value.ToString();
                if (AdminBLL.RemoveCasualUserById(userId))
                    Refresh();
                else
                    MessageBox.Show("删除失败");
            }
        }

        private void DeletePark(DataGridView dataGridView)
        {
            for (int i = dataGridView.SelectedRows.Count - 1; i >= 0; i--)
            {
                string parkId = dataGridView.SelectedRows[0].Cells[0].Value.ToString();
                if (AdminBLL.RemoveParkById(parkId))
                    Refresh();
                else
                    MessageBox.Show("删除失败");
            }
        }

        private void DeleteOrder(DataGridView dataGridView)
        {
            for (int i = dataGridView.SelectedRows.Count - 1; i >= 0; i--)
            {
                string orderId = dataGridView.SelectedRows[0].Cells[0].Value.ToString();
                if (AdminBLL.RemoveOrderById(orderId))
                    Refresh();
                else
                    MessageBox.Show("删除失败");
            }
        }

        private void DeleteAdmin(DataGridView dataGridView)
        {
            for (int i = dataGridView.SelectedRows.Count - 1; i >= 0; i--)
            {
                string adminId = dataGridView.SelectedRows[0].Cells[0].Value.ToString();
                if (AdminBLL.RemoveAdminById(adminId))
                    Refresh();
                else
                    MessageBox.Show("删除失败");
            }
        }
        #endregion

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "ColumnButtonDelay")
            {
                string userId = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                FormFormalUser form1 = new FormFormalUser(FormFormalUser.Permissions.Delay, userId);
                form1.ShowDialog(this);
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView2.Columns[e.ColumnIndex].Name == "ColumnButtonUp")
            {
                string userId = dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString();
                FormFormalUser form1 = new FormFormalUser(FormFormalUser.Permissions.Up, userId);
                form1.ShowDialog(this);
            }
        }

        private void dataGridView5_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView5.Columns[e.ColumnIndex].Name == "ColumnButtonReset")
            {
                string adminId = dataGridView5.Rows[e.RowIndex].Cells[0].Value.ToString();
                if (AdminBLL.ResetErrorNum(adminId))
                    Refresh();
                else
                    MessageBox.Show("重置失败请重试！");
            }
        }
    }
}
