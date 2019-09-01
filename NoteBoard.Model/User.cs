using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteBoard.Model
{
    public class User : BaseEntity
    {
        public User()
        {
            IsActive = false;
        }

        public Guid UserID { get; set; }
        public Guid RoleID { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string EMail { get; set; }
        public string Password { get; set; }
    }
}
