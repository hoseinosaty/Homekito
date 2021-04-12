using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Barayand.Models.Extra;
namespace Barayand.Models.Entity
{
    public class TokenExpirationModel:BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int T_Id { get; set; }
        public string T_Token { get; set; }
        public DateTime T_ExpireDate { get; set; }
        public string T_RequestUserAgent { get; set; }
        public string T_RequestIP { get; set; }
    }
}