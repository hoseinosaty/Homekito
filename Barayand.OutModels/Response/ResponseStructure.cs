using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Barayand.OutModels.Response
{
    public class ResponseStructure
    {
        public bool Status { get; set; } = true;
        public string Msg { get; set; } = null;
        public object Data { get; set; } = null;
    }
}
