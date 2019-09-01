using NoteBoard.BLL;
using NoteBoard.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NoteBoard.PL.WinForm
{
    public partial class frmRegister : Form
    {
        UserController _userController;

        public frmRegister()
        {
            InitializeComponent();
            _userController = new UserController();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (txtPassword.Text != txtConfirmPassword.Text)
            {
                MessageBox.Show("Şifreler uyuşmuyor");
                txtConfirmPassword.BackColor = Color.Red;
                txtConfirmPassword.SelectAll();
                return;
            }

            User newUser = new User();
            newUser.Firstname = txtName.Text;
            newUser.Lastname = txtSurname.Text;
            newUser.EMail = txtMail.Text;
            newUser.Password = txtPassword.Text;

            try
            {
                bool result = _userController.Add(newUser);
                if (result)
                {
                    MessageBox.Show("Kayıt başarılı");
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void chkConfirm_CheckedChanged(object sender, EventArgs e)
        {
            if (chkConfirm.Checked)
            {
                btnRegister.Enabled = true;
            }
            else
            {
                btnRegister.Enabled = false;
            }
        }

        private void frmRegister_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Owner.Show();
        }
    }
}
