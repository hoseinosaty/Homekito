using Barayand.Models.Extra;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Barayand.Models.Entity
{
    public class CustomerAccountLevel: BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CAL_Id { get; set; }
        [MaxLength(500)]
        public string CAL_Title { get; set; }
        [MaxLength(2000)]
        public string CAL_Image { get; set; }
        public bool CAL_Status { get; set; }
        public decimal CAL_MinTrade { get; set; }//minimum trade price
        public decimal CAL_Percentage { get; set; }
        public string CAL_Description { get; set; }

    }
}