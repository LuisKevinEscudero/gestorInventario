using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gestor.Exceptions
{
    public class InsertException : Exception
    {
        public InsertException()
        {
        }
        public InsertException(string message) 
            : base(message)
        {
        }

        public InsertException(string message, Exception innerException) 
            : base(message, innerException)
        {
        }

        public InsertException(string message, string id)
            : this(message)
        {
        }


    }

}
