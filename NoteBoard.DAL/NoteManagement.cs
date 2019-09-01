using NoteBoard.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteBoard.DAL
{
    public class NoteManagement
    {
        Helper h;
        public NoteManagement()
        {
            h = new Helper();
        }

        public int Insert(Note note)
        {
            string query = "INSERT INTO Note(NoteID,UserID,Title,Content,IsConfirmed,IsActive) VALUES(@noteID,@userID,@title,@content,@confirm,@active)";
            List<SqlParameter> parameters = GetParameters(note, true);

            h.AddParametersToCommand(parameters);
            return h.MyExecuteQuery(query);
        }

        public int Update(Note note)
        {
            string query = "UPDATE Note SET Title = @title, Content = @content, IsConfirmed = @confirm, IsActive = @active WHERE NoteID = @noteID";
            List<SqlParameter> parameters = GetParameters(note, false);

            h.AddParametersToCommand(parameters);
            return h.MyExecuteQuery(query);
        }

        List<SqlParameter> GetParameters(Note note, bool isInsert)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@noteID", note.NoteID));
            if (isInsert)
            {
                parameters.Add(new SqlParameter("@userID", note.UserID));
            }
            parameters.Add(new SqlParameter("@title", note.Title));
            parameters.Add(new SqlParameter("@content", note.Content));
            parameters.Add(new SqlParameter("@confirm", note.IsConfirmed));
            parameters.Add(new SqlParameter("@active", note.IsActive));
            return parameters;
        }

        public int Delete(Guid noteID)
        {
            string query = "DELETE FROM Note WHERE NoteID = @noteID";
            h.AddParametersToCommand(new List<SqlParameter>()
            {
                new SqlParameter()
                {
                    ParameterName = "@noteID",
                    Value = noteID

                }
            });

            return h.MyExecuteQuery(query);
        }

        public Note GetNoteByID(Guid noteID)
        {
            Note currentNote = new Note();
            string query = "SELECT * FROM Note WHERE NoteID = @noteID";
            h.AddParametersToCommand(new List<SqlParameter>()
            {
                new SqlParameter()
                {
                    ParameterName = "@noteID",
                    Value = noteID

                }
            });

            SqlDataReader reader = h.MyExecuteReader(query);
            reader.Read();
            currentNote.NoteID = noteID;
            currentNote.UserID = (Guid)reader["UserID"];
            currentNote.Title = reader["Title"].ToString();
            currentNote.Content = reader["Content"].ToString();
            currentNote.IsConfirmed = (bool)reader["IsConfirmed"];
            currentNote.IsActive = (bool)reader["IsActive"];
            currentNote.CreatedDate = (DateTime)reader["CreatedDate"];

            reader.Close();

            return currentNote;
        }

        public List<Note> GetAllNotes()
        {
            List<Note> notes = new List<Note>();
            Note currentNote = null;
            string query = "SELECT * FROM Note";

            SqlDataReader reader = h.MyExecuteReader(query);
            while (reader.Read())
            {
                currentNote = new Note();
                currentNote.NoteID = (Guid)reader["NoteID"];
                currentNote.UserID = (Guid)reader["UserID"];
                currentNote.Title = reader["Title"].ToString();
                currentNote.Content = reader["Content"].ToString();
                currentNote.IsConfirmed = (bool)reader["IsConfirmed"];
                currentNote.IsActive = (bool)reader["IsActive"];
                currentNote.CreatedDate = (DateTime)reader["CreatedDate"];
                notes.Add(currentNote);
            }
            reader.Close();
            
            return notes;
        }
    }
}
