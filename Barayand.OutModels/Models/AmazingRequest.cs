using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Barayand.OutModels.Models
{
    public class AmazingRequest
    {
        public int A_Id { get; set; }
        public int A_UserId { get; set; } = 0;
        public string A_UserName { get; set; }
        public int A_ProductId { get; set; } = 0;
        public string A_ProductTitle { get; set; }
        public int A_NotificationType { get; set; } = 1;//1=>sms 2=>email 3=>both
        public string A_NotificationTypeStr
        {
            get
            {
                switch (A_NotificationType)
                {
                    case 1:
                        return "پیامک";
                    case 2:
                        return "ایمیل";
                    case 3:
                        return "پیامک / ایمیل";
                    default:
                        return "پیامک";
                }
            }
        }
        public int A_Type { get; set; } = 1;//1=>Amazing Request 2=>Available Ntification
    }
}
