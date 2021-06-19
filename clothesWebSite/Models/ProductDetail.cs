namespace clothesWebSite.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ProductDetail")]
    public partial class ProductDetail
    {
        [Key]
        public int product_detail_id { get; set; }

        public int product_id { get; set; }

        [Required]
        [StringLength(10)]
        public string size { get; set; }

        public int color { get; set; }

        public int quantity { get; set; }

        public virtual Product Product { get; set; }
    }
}
