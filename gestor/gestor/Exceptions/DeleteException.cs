using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gestor.Exceptions
{
    public class DeleteException : Exception
    {
        public DeleteException()
        {
        }
        public DeleteException(string message)
            : base(message)
        {
        }

        public DeleteException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public DeleteException(string message, string id)
            : this(message)
        {
        }
    }
}
