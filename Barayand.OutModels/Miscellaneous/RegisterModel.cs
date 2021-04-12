using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Barayand.OutModels.Miscellaneous
{
    public class RegisterModel
    {
        public string Name { get; set; }
        public string Family { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string CurrentPassword { get; set; }
        public string Avatar { get; set; }
        public string Phone { get; set; }
        public string HomeTel { get; set; }
        public string IdentityCode { get; set; }
        public string CreditCardNum { get; set; }
        public string BirthDate { get; set; }
    }
}
