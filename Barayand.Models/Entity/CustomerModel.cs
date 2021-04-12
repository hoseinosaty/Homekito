using Barayand.Models.Extra;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Barayand.Models.Entity
{
    public class CustomerModel: BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int C_Id { get; set; }
        [MaxLength(50)]
        public string C_Name { get; set; }
        [MaxLength(50)]
        public string C_Family { get; set; }
        public bool C_Gender { get; set; }
        [MaxLength(100)]
        public string C_Phone { get; set; }
        public string C_HomeTel { get; set; }
        public string C_Address { get; set; }
        [MaxLength(255)]
        public string C_Email { get; set; }
        public string C_NationalID { get; set; }
        public string C_BirthDay { get; set; }
        public decimal C_Amount { get; set; } = 0;
        public decimal C_TraderAmount { get; set; } = 0;
        public string C_IBAN { get; set; }
        public string C_CreditCardNumber { get; set; }
        [MaxLength(100)]
        public string C_Avatar { get; set; }
        public string C_NationalIdentityCheck { get; set; }//national card scan file ->this field is extra and delete it in after project ended up
        public int C_AuthorizedDocs { get; set; } = 0;//0 = > new 1=> accept 2=>fail ->this field is extra and delete it in after project ended up
        public string C_AdminReason { get; set; }//if C_AuthorizedDocs is 2 then admin write an reson
        public int C_Cal { get; set; }
        [MaxLength(100)]
        public string C_Username { get; set; }
        [MaxLength(255)]
        public string C_Password { get; set; }
        public int C_Post { get; set; } 
        public int C_Status { get; set; } 
        public string C_Token { get; set; }
        
        [MaxLength(12)]
        public string verify_code { get; set; }
        [NotMapped]
        public double C_CalcCustomerDocsPerc { get; set; }


        public int C_PersonalInfoCheck { get; set; } = 0; // 1=>registered 2=>accepted 3=>fail
        public string C_PersonalInfoFailReason { get; set; }//the result of checking customer home tel Or Address.if not accept this field will be fill by admin

        public int C_BankInfoCheck { get; set; } = 0; // 1=>registered 2=>accepted 3=>fail
        public string C_BankInfoFailReason { get; set; }//the result of checking customer home tel.if not accept this field will be fill by admin
        [NotMapped]
        public object AuthDocs { get; set; }

    }
}