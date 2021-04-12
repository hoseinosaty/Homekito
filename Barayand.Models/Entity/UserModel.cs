using Barayand.Models.Extra;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Barayand.Models.Entity
{
    public class UserModel : BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int U_Id { get; set; }
        [MaxLength(50)]
        public string U_Name { get; set; }
        [MaxLength(50)]
        public string U_Family { get; set; }

        [MaxLength(255)]
        public string U_Email { get; set; }
        [MaxLength(100)]
        public string U_Avatar { get; set; } = "noimage.png";
        [MaxLength(100)]
        public string U_Phone { get; set; }
        public string U_HomeTel { get; set; }
        public string U_IdentityCode { get; set; }
        public string U_CreditCardNum { get; set; }
        public string U_BirthDate { get; set; }
        public string U_Password { get; set; }
        public decimal U_Wallet { get; set; } = 0;
        public int U_Coupon { get; set; } = 0;
        public int U_Status { get; set; } = 1;//1=>active 2=>suspended
        public string U_SuspendReason { get; set; }
        public int U_Role { get; set; } = 1;//1=>user admin 2=>Customer
        public int U_RoleId { get; set; } = 0;//if U_Role == 1 -> use this field to set permissions
        [NotMapped]
        public string Token { get; set; }
        [NotMapped]
        public List<int> Permissions { get; set; } = new List<int>();
        [NotMapped]
        public string surename { get { return this.U_Name + " " + this.U_Family; } }
        [NotMapped]
        public List<AddressModel> UserAdresses { get; set; } = new List<AddressModel>();
    }
}
