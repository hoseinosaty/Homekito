using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Barayand.OutModels.Response
{
    public static class ResponseModel
    {
        public static ResponseStructure Success(string msg = "عملیات با موفقیت انجام شد",object data = null,bool state = true)
        {
            return new ResponseStructure() {Msg = msg,Data = data,Status = state };
        }
        public static ResponseStructure Error(string msg = null, object data = null, bool state = false)
        {
            return new ResponseStructure() { Msg = msg, Data = data, Status = state };
        }
        public static ResponseStructure ServerInternalError(string msg = "در حال حاضر سیستم قادر به پردازش درخواست شما نمیباشد.لطفا با پشتیبان سیستم تماس بگیرید", object data = null, bool state = false)
        {
            return new ResponseStructure() { Msg = msg, Data = data, Status = state };
        }
    }
}
