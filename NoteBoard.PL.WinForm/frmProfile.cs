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
    public partial class frmProfile : Form
    {
        User currentUser;
        UserController _userController;
        public frmProfile(User user)
        {
            InitializeComponent();
            currentUser = user;
            _userController = new UserController();
        }

        private void frmProfile_Load(object sender, EventArgs e)
        {
            txtMail.Text = currentUser.EMail;
            txtName.Text = currentUser.Firstname;
            txtSurname.Text = currentUser.Lastname;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (currentUser.Password == txtPassword.Text)
            {
                currentUser.Firstname = txtName.Text;
                currentUser.Lastname = txtSurname.Text;

                if (!string.IsNullOrWhiteSpace(txtNewPassword.Text))
                {
                    if (txtNewPassword.Text == txtNewPasswordConfirm.Text)
                    {
                        currentUser.Password = txtNewPassword.Text;
                        Properties.Settings.Default.IsPasswordUpdate = true;
                    }
                    else
                    {
                        MessageBox.Show("Şifreler uyuşmuyor");
                        return;
                    }
                }

                try
                {
                    bool result = _userController.Update(currentUser);
                    if (result)
                    {
                        MessageBox.Show("Güncelleme başarılı");
                        this.DialogResult = DialogResult.OK;
                    }
                    else
                    {
                        MessageBox.Show("Güncelleme esnasında hata oluştu");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Şifre yanlış");
            }
        }
    }
}
