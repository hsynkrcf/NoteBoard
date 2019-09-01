using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteBoard.Model
{
    public abstract class BaseEntity
    {
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
