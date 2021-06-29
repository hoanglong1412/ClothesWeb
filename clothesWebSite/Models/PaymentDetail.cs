namespace clothesWebSite.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PaymentDetail")]
    public partial class PaymentDetail
    {
        [Key]
        public int ordinal_number { get; set; }

        public int payment_id { get; set; }

        public int product_id { get; set; }

        [StringLength(10)]
        public string size { get; set; }

        public int? color { get; set; }

        public int? quantity { get; set; }

        public float? price { get; set; }

        public virtual Payment Payment { get; set; }

        public virtual Product Product { get; set; }
    }
}
