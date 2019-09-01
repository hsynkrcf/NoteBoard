using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteBoard.CustomException
{
    public class MailFormatException : Exception
    {
        public override string Message
        {
            get
            {
                return "Mail adresi düzgün formatta değil";
            }
        }
    }
}
