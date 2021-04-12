using Barayand.Models.Extra;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Barayand.Models.Entity
{
    public class AdsModel :BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int A_Id { get; set; }
        public int A_Type { get; set; }//1=main page ads 2=shop page ads 3=academy ads
        [MaxLength(500)]
        public string A_Title { get; set; }
        [MaxLength(1000)]
        public string A_Link { get; set; }
        public string A_Banner { get; set; }
        public int A_SortField { get; set; }
        public bool A_Status { get; set; }

    }
}