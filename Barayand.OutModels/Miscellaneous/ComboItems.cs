using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Barayand.OutModels.Miscellaneous
{
    public class ComboItems
    {
        public class Brand
        {
            public int B_Id { get; set; }
            public string B_Title { get; set; }
        }
        public class Label
        {
            public int L_Id { get; set; }
            public string L_Title { get; set; }
        }
        public class Attribute
        {
            public int A_Id { get; set; }
            public string A_Title { get; set; }
            public int A_Type { get; set; } = 1;//1=>ComboBox 2=>TextBox
        }
        public class IGCategory
        {
            public int GC_Id { get; set; }
            public string GC_Titles { get; set; }
        }
        public class NoticesCategory
        {
            public int NC_Id { get; set; }
            public string NC_Title { get; set; }
        }
        public class Formula
        {
            public int F_Id { get; set; }
            public string F_Title { get; set; }
        }
        public class MCountry
        {
            public int M_Id { get; set; }
            public string M_Title { get; set; }
        }
        public class EnergyGiftWrapper
        {
            public int E_Id { get; set; }
            public string E_Title { get; set; }
        }
        public class Warranty
        {
            public int W_Id { get; set; }
            public string W_Title { get; set; }
        }
        public class Color
        {
            public int C_Id { get; set; }
            public string C_Title { get; set; }
        }
        public class FaqCategory
        {
            public int F_Id { get; set; }
            public string F_Title { get; set; }
        }
    }
    
}
