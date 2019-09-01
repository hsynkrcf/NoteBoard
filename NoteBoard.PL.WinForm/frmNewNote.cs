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
    public partial class frmNewNote : Form
    {
        User currentUser;
        Note currentNote;
        NoteController _noteController;
        Guid? _noteID;
        public frmNewNote(User user, Guid? noteID)
        {
            InitializeComponent();
            currentUser = user;
            _noteController = new NoteController();
            _noteID = noteID;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (currentNote == null)
            {
                Note newNote = new Note();
                newNote.UserID = currentUser.UserID;
                newNote.Title = txtTitle.Text;
                newNote.Content = txtContent.Text;
                _noteController.Add(newNote);
            }
            else
            {
                currentNote.Title = txtTitle.Text;
                currentNote.Content = txtContent.Text;
                _noteController.Update(currentNote);
            }
            
            this.Close();
        }

        private void frmNewNote_Load(object sender, EventArgs e)
        {
            if (_noteID.HasValue)
            {
                currentNote = _noteController.GetNote(_noteID.Value);
                txtTitle.Text = currentNote.Title;
                txtContent.Text = currentNote.Content;
            }
        }
    }
}
