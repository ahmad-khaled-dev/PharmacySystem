using Pharmacy.Core.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Core.Domain.Entities
{
   public class Notification
    { 
        
        [Key]
        public int NotificationID { set; get; }

        public string ?Message { set; get; }

        public DateTime ?Create_at { set; get; }

        bool IsRead_By_Admin { set; get; }

        public DateTime? Read_at { set; get; }

         public NotificationType Type { get; set; }

        [ForeignKey("Product")]
        public int ?ProductID { get; set; }

        public Product ?Product { get; set; }


        [ForeignKey("Batch")]
        public int ?BatchID { set; get; }

        public Batch ?Batch { set; get; }

    }

}
