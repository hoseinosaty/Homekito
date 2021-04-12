using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Barayand.OutModels.Models
{
    public class User
    {
        public int U_Id { get; set; }
        public string U_Name { get; set; }
        public string U_Family { get; set; }

        public string U_Email { get; set; }
        public string U_Avatar { get; set; } = "noimage.png";
        public string U_Phone { get; set; }
        public string U_HomeTel { get; set; }
        public string U_IdentityCode { get; set; }
        public string U_CreditCardNum { get; set; }
        public string U_BirthDate { get; set; }
        public string U_Password { get; set; }
        public decimal U_Wallet { get; set; } = 0;
        public int U_Status { get; set; }
        public int U_Role { get; set; } = 1;//1=>user admin 2=>Customer
        public int U_Coupon { get; set; } = 0;
    }
}
