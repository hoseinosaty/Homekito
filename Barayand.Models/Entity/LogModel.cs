using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Barayand.Models.Entity
{
    public class LogModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int L_Id { get; set; }
        public string L_Title { get; set; }
        public string L_Body { get; set; }
        public string L_FileName { get; set; }
        public string L_Controller { get; set; }
        public string L_Action { get; set; }
        public string L_Class { get; set; }
        public string L_Function { get; set; }
    }
}
