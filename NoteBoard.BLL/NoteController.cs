using NoteBoard.DAL;
using NoteBoard.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteBoard.BLL
{
    public class NoteController
    {
        NoteManagement _noteManagement;
        public NoteController()
        {
            _noteManagement = new NoteManagement();
        }

        public bool Add(Note note)
        {
            note.IsActive = true;
            note.NoteID = Guid.NewGuid();
            int result = _noteManagement.Insert(note);
            return result > 0;
        }

        public bool Update(Note note)
        {
            int result = _noteManagement.Update(note);
            return result > 0;
        }

        public bool Delete(Note note)
        {
            note.IsActive = false;
            int result = _noteManagement.Update(note);
            //int result = _noteManagement.Delete(note.NoteID);
            return result > 0;
        }

        public Note GetNote(Guid noteID)
        {
            return _noteManagement.GetNoteByID(noteID);
        }

        public List<Note> GetListOfNote()
        {
            return _noteManagement.GetAllNotes();
        }

        public List<Note> GetListByUser(Guid userID)
        {
            List<Note> notes = new List<Note>();
            foreach (Note item in _noteManagement.GetAllNotes())
            {
                if (item.UserID == userID && item.IsActive)
                {
                    notes.Add(item);
                }
            }

            return notes;
        }
    }
}
