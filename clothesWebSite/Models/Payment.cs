namespace clothesWebSite.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Payment")]
    public partial class Payment
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Payment()
        {
            PaymentDetails = new HashSet<PaymentDetail>();
        }

        [Key]
        public int payment_id { get; set; }

        public int user_id { get; set; }

        public DateTime? create_date { get; set; }

        public DateTime? deliver_date { get; set; }

        [StringLength(200)]
        public string deliver_name { get; set; }

        public int state { get; set; }

        public string payway { get; set; }

        public string deliver_type { get; set; }

        public string address_deliver { get; set; }

        [StringLength(15)]
        public string phone_receiver { get; set; }

        public string name_receiver { get; set; }


        public virtual User User { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PaymentDetail> PaymentDetails { get; set; }
    }
}
