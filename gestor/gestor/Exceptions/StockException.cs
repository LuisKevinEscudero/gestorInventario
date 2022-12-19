using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gestor.Exceptions
{
    public class StockException : Exception
    {
        public StockException()
        {
        }

        public StockException(string message)
            : base(message)
        {
        }

        public StockException(string message, Exception inner)
            : base(message, inner)
        {
        }

        public StockException(string message, string id)
            : this(message)
        {

        }
    }
}
