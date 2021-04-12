using Barayand.Models.Extra;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Barayand.Models.Entity
{
    public class CopponModel : BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CP_Id { get; set; }
        public string CP_Title { get; set; }
        public string CP_Code { get; set; }
        public DateTime CP_StartDate { get; set; } = DateTime.Now;
        public DateTime CP_EndDate { get; set; } = DateTime.Now;
        public decimal CP_Discount { get; set; } = 0;
        public int CP_Type { get; set; } = 1;
        public bool CP_Status { get; set; } = true;
        public bool CP_IsDeleted { get; set; } = false;
        [NotMapped]
        public int CP_UsageCount { get; set; } = 0;
        public string CP_SD()
        {
            return CP_StartDate.ToString("yyyy-MM-dd");
        }
        public string CP_ED()
        {
            return CP_EndDate.ToString("yyyy-MM-dd");
        }
    }
}