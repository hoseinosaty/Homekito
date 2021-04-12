using Barayand.Models.Extra;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Barayand.Models.Entity
{
    public class AmazingRequestModel:BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int A_Id { get; set; }
        public int A_UserId { get; set; } = 0;
        public int A_ProductId { get; set; } = 0;
        public int A_NotificationType { get; set; } = 1;//1=>sms 2=>email 3=>both
        public int A_Type { get; set; } = 1;//1=>Amazing Request 2=>Available Ntification

    }
}
