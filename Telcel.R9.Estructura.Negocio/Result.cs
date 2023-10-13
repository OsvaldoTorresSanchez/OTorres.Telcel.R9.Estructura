using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Telcel.R9.Estructura.Negocio
{
    public class Result
    {
        public bool Correct { get; set; }

        public string ErrorMessage { get; set; }

        public object Object { get; set; }

        public Exception Ex { get; set; }

        public List<object> Objects { get; set; }
    }
}
