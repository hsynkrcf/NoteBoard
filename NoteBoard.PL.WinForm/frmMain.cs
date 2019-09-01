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
    public partial class frmMain : Form
    {
        User currentUser;
        UserController _userController;
        NoteController _noteController;
        bool isSecureSignOut = false;
        public frmMain(Guid userID)
        {
            InitializeComponent();
            _userController = new UserController();
            currentUser = _userController.GetUser(userID);
            _noteController = new NoteController();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            lblInfo.Text = $"Hoşgeldin {currentUser.Firstname} {currentUser.Lastname}";
            FillList();
        }

        void FillList()
        {
            lstNotes.DisplayMember = "Title";
            lstNotes.ValueMember = "NoteID";
            lstNotes.DataSource = _noteController.GetListByUser(currentUser.UserID);
        }

        private void lblSignOut_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            isSecureSignOut = true;
            this.Owner.Show();
            this.Close();
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isSecureSignOut)
            {
                return;
            }

            DialogResult result = MessageBox.Show("Çıkış yapmak istediğinize emin misiniz?", "NoteBoard", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void lblInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmProfile profile = new frmProfile(currentUser);
            DialogResult result = profile.ShowDialog();
            if (result == DialogResult.OK)
            {
                isSecureSignOut = true;
                this.Owner.Show();
                this.Close();
            }
        }

        private void btnNewNote_Click(object sender, EventArgs e)
        {
            frmNewNote newNote = new frmNewNote(currentUser, null);
            newNote.ShowDialog();
            FillList();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (lstNotes.SelectedIndex < 0)
            {
                MessageBox.Show("Bir not seçin");
                return;
            }

            Guid noteID = (Guid)lstNotes.SelectedValue;
            frmNewNote frm = new frmNewNote(currentUser, noteID);
            frm.Text = "Güncelle";
            frm.ShowDialog();
            FillList();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lstNotes.SelectedIndex < 0)
            {
                MessageBox.Show("Not seç");
                return;
            }

            Guid noteID = (Guid)lstNotes.SelectedValue;
            Note selectedNote = _noteController.GetNote(noteID);
            _noteController.Delete(selectedNote);
            FillList();
        }
    }
}
