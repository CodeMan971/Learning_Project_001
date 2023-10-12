using BLL;
using DAL.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PersonelTracking
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnEmployee_Click(object sender, EventArgs e)
        {
            // Hide private data from standard user
            if (!UserStatic.IsAdmin)
            {
                EmployeeDTO dto = EmployeeBLL.GetAll();
                EmployeeDetailDTO detail = dto.Employees.First(x => x.EmployeeID == UserStatic.EmployeeID);
                FrmEmployee frm = new FrmEmployee();
                frm.detail = detail;
                frm.isUpdate = true;
                this.Hide();
                frm.ShowDialog();
                this.Visible = true;
            }
            else
            {
                FrmEmployeeList frm = new FrmEmployeeList();
                this.Hide();
                frm.ShowDialog();
                this.Visible = true;
            }
        }

        private void btnTasks_Click(object sender, EventArgs e)
        {
            FrmTaskList frm = new FrmTaskList();
            this.Hide();
            frm.ShowDialog();
            this.Visible = true;
        }

        private void btnSalary_Click(object sender, EventArgs e)
        {
            FrmSalaryList frm = new FrmSalaryList();
            this.Hide();
            frm.ShowDialog();
            this.Visible = true;
        }

        private void btnPermissions_Click(object sender, EventArgs e)
        {
            FrmPermissionList frm = new FrmPermissionList();
            this.Hide();
            frm.ShowDialog();
            this.Visible = true;
        }

        private void btnDepartment_Click(object sender, EventArgs e)
        {
            FrmDepartmentList frm = new FrmDepartmentList();
            this.Hide();
            frm.ShowDialog();
            this.Visible = true;
        }

        private void btnPosition_Click(object sender, EventArgs e)
        {
            FrmPositionList frm =new FrmPositionList();
            this.Hide();
            frm.ShowDialog();
            this.Visible = true;
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            FrmLogin frm = new FrmLogin();
            this.Hide();
            frm.ShowDialog();
        }

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            // Hide buttons from standard user
            if (!UserStatic.IsAdmin)
            {
                btnDepartment.Visible = false;
                btnPosition.Visible = false;
                btnLogOut.Location = new Point(192, 199);
                btnExit.Location = new Point(386, 199);
            }
        }
    }
    
}
