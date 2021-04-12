using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Barayand.OutModels.Models
{
    public class Coppon
    {
        public int CP_Id { get; set; }
        public string CP_Title { get; set; }
        public string CP_Code { get; set; }
        public DateTime CP_StartDate()
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en");
            return DateTime.Parse(CP_SD);
        }
        public DateTime CP_EndDate()
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en");
            return DateTime.Parse(CP_ED);
        }

        public decimal CP_Discount { get; set; } = 0;
        public int CP_UsageCount { get; set; } = 0;
        public bool CP_Status { get; set; } = true;
        public int CP_Type { get; set; } = 1;
        public bool CP_IsDeleted { get; set; } = false;
        public string CP_SD { get; set; }
        public string CP_ED { get; set; }
    }
}
