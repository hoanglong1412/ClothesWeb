namespace clothesWebSite.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Discount")]
    public partial class Discount
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Discount()
        {
            Products = new HashSet<Product>();
        }

        [Key]
        public int discount_id { get; set; }

        public DateTime from_date { get; set; }

        public DateTime to_date { get; set; }

        public int? discount_by_price { get; set; }

        public double? discount_by_ratio { get; set; }

        public string content { get; set; }

        public DateTime? last_update { get; set; }

        public int state { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Product> Products { get; set; }
    }
}
