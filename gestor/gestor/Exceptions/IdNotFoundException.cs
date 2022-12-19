using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gestor.Exceptions
{
    [Serializable]
    public class IdNotFoundException : Exception
    {
        public string Id { get; }
        public IdNotFoundException()
        {
        }

        public IdNotFoundException(string message)
            : base(message)
        {
        }

        public IdNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }

        public IdNotFoundException(string message, string id)
            : this(message)
        {
            Id = id;
            
        }
    }
}
