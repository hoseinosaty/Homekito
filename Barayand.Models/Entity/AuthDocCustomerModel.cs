using Barayand.Models.Extra;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Barayand.Models.Entity
{
    public class AuthDocCustomerModel:BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AC_Id { get; set; }
        public int AC_Cid { get; set; }
        public int AC_Aid { get; set; }
        public string AC_Path { get; set; }
        public int AC_Status { get; set; } = 1;///1=>new 2=>accept 3=>fail
        public string AC_FailReason { get; set; }///reason of fail authdocs
    }
}