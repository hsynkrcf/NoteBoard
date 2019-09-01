using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteBoard.CustomException
{
    public class ThreeLetterException : Exception
    {
        public override string Message
        {
            get
            {
                return "Şifre en az 3 harf içermelidir";
            }
        }
    }
}
