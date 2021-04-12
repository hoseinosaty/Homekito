using Barayand.Models.Extra;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Barayand.Models.Entity
{
    public class NewsletterModel :BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NL_Id { get; set; }
        [MaxLength(500)]
        public string NL_Entity { get; set; }
        public int NL_Type { get; set; } = 0;//0 => phone number 1=>Email ...

    }
}