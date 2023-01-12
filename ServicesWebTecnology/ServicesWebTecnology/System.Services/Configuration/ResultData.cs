using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Infrastructure.Configuration
{
    public class ResultData<T>: Result where T:class 
    {
        public T Data { get; set; }
    }
}
