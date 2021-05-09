using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Barayand.OutModels.Models
{
    public class PromotionBoxProducts
    {
        public int X_Id { get; set; }
        public int X_SectionId { get; set; } = 0;
        public int X_ProdId { get; set; } = 0;
        public int X_ColorId { get; set; } = 0;
        public int X_WarrantyId { get; set; } = 0;
        public decimal X_DiscountedPrice { get; set; } = 0;
        public bool X_DiscountType { get; set; } = false; // false=>percentage true=>price after reduce discount


        public string X_SD { get; set; }
        public string X_ED { get; set; }
        public DateTime X_StartDate { get { return ConvertDatetime(X_SD); } set { X_StartDate = value; } }//استفاده فقط در فروش ویژه
        public DateTime X_EndDate { get { return ConvertDatetime(X_ED); } set { X_StartDate = value; } }//استفاده فقط در فروش ویژه
        public bool X_Status { get; set; } = true;//استفاده فقط در فروش ویژه
        public bool X_ShowInIndex { get; set; } = false;//استفاده فقط در فروش ویژه
        public bool showDateDialog { get; set; } = false;
        public bool showTimeDialog { get; set; } = false;
        public DateTime ConvertDatetime(string date)
        {
            try
            {
                Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
                var d = DateTime.Parse(date);
                return d;
            }
            catch (Exception ex)
            {
                return DateTime.Now;
            }
        }
    }
}
