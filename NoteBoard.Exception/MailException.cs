using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteBoard.CustomException
{
    public class MailException : Exception
    {
        public override string Message
        {
            get
            {
                return "Bu mail adresi zaten sistemde mevcut";
            }
        }
    }
}
