using NoteBoard.BLL;
using NoteBoard.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NoteBoard.PL.WinForm
{
    public partial class frmLogin : Form
    {
        UserController _userController;
        LoginDTO login;
        public frmLogin()
        {
            InitializeComponent();
            _userController = new UserController();
        }

        private void lblRegister_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmRegister register = new frmRegister();
            register.Show();
            register.Owner = this;
            this.Hide();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            login = new LoginDTO();
            login.Mail = txtMail.Text;
            login.Password = txtPassword.Text;

            string result = _userController.Login(login);
            Guid userID;
            if (Guid.TryParse(result, out userID))
            {
                frmMain main = new frmMain(userID);
                main.Owner = this;
                main.Show();
                this.Hide();
                if (!chkRememberMe.Checked)
                {
                    txtMail.Clear();
                    txtPassword.Clear();
                }
            }
            else
            {
                MessageBox.Show(result);
            }

            RememberMe();
        }

        void RememberMe()
        {
            if (chkRememberMe.Checked)
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream fs = new FileStream("NoteBoard.psg", FileMode.Create, FileAccess.Write);
                bf.Serialize(fs,login);
                fs.Close();
            }
            else
            {
                if (File.Exists("NoteBoard.psg"))
                {
                    File.Delete("NoteBoard.psg");
                }
            }
        }

        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (chkShowPassword.Checked)
            {
                txtPassword.UseSystemPasswordChar = false;
            }
            else
            {
                txtPassword.UseSystemPasswordChar = true;
            }
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            if (File.Exists("NoteBoard.psg"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream fs = new FileStream("NoteBoard.psg", FileMode.Open, FileAccess.Read);
                login = bf.Deserialize(fs) as LoginDTO;
                fs.Close();

                txtMail.Text = login.Mail;
                txtPassword.Text = login.Password;
                chkRememberMe.Checked = true;
            }
        }

        private void frmLogin_VisibleChanged(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.IsPasswordUpdate)
            {
                if (File.Exists("NoteBoard.psg"))
                {
                    File.Delete("NoteBoard.psg");
                    txtMail.Clear();
                    txtPassword.Clear();
                    chkRememberMe.Checked = false;
                }
            }
        }
    }
}
