using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentArchiver.Wrapper
{
    public class ResponseWrapper
    {
        public bool Valid { get; set; }
        public string Message { get; set; }
        public dynamic Data { get; set; }
    }
}
