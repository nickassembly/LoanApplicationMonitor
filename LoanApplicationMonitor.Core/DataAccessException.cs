using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanApplicationMonitor.Core
{
    public class DataAccessException : Exception
    {
        public DataAccessException(string message, Exception? innerException = null)
            : base(message, innerException)
        {
        }
    }

}
