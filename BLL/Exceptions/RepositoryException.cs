using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Exceptions
{
    public class RepositoryException : Exception
    {
        public int Numero { get; set; }
        public RepositoryException() : base()
        {
        }

        public RepositoryException(string message) : base(message)
        {
        }

        public RepositoryException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public RepositoryException(string message, Exception innerException, int numero) : base(message, innerException)
        {
            Numero = numero;
        }
    }
}
