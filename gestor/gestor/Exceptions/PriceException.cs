using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gestor.Exceptions
{
    public class PriceException : Exception
    {
        public PriceException()
        {
        }

        public PriceException(string message)
            : base(message)
        {
        }

        public PriceException(string message, Exception inner)
            : base(message, inner)
        {
        }

        public PriceException(string message, string id)
            : this(message)
        {

        }
    }
}
