using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeKito.Models
{
    public class FilterSerch
    {
        /// <summary>
        /// (See=1)-(sell=2)-(fav=3)-(new=4)-(cheap=5)-(expenc=6)-(fast=7)-(bestoffer=8)
        /// </summary>
        public int Order { get; set; } = 0;
        public int[] Brand { get; set; } = new int[] { };
        public string TitleSerch { get; set; }
        public bool Count { get; set; } = false;

    }
}
