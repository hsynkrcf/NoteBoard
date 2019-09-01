using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteBoard.Model
{
    public class Note : BaseEntity
    {
        public Guid NoteID { get; set; }
        public Guid UserID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public bool IsConfirmed { get; set; }
    }
}
