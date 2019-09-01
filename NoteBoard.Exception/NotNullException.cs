using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteBoard.CustomException
{
    public class NotNullException : Exception
    {
        string _data;

        public NotNullException(string data)
        {
            _data = data;
        }

        public override string Message
        {
            get
            {
                return $"Alan boş geçilemez. Alan : {_data}";
            }
        }
    }
}
