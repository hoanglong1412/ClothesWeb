namespace clothesWebSite.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Product")]
    public partial class Product
    {
        //Possible value for state in products table
        static public readonly int STATE_DEATH = 0;
        static public readonly int STATE_INVISIBLE = 1;
        static public readonly int STATE_VISIBLE = 2;

        //Contract for product type gender
        static public readonly String MALE = "M";
        static public readonly String FEMALE = "F";
        static public readonly String KID = "K";

        //Contract for product type
        static public readonly String PANTS = "Q";
        static public readonly String SHIRTS = "A";

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            PaymentDetails = new HashSet<PaymentDetail>();
            ProductDetails = new HashSet<ProductDetail>();
            Discounts = new HashSet<Discount>();
        }

        [Key]
        public int product_id { get; set; }

        [Required]
        [StringLength(10)]
        public string type_id { get; set; }

        [Required]
        [StringLength(200)]
        public string product_name { get; set; }

        public double? sale_price { get; set; }

        public double import_price { get; set; }

        public int? count_views { get; set; }

        public double? total_rating { get; set; }

        public int? count_rating { get; set; }

        [StringLength(500)]
        public string content { get; set; }

        public int state { get; set; }

        public DateTime? last_update { get; set; }

        public DateTime? create_day { get; set; }
        public string image_name { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PaymentDetail> PaymentDetails { get; set; }

        public virtual ProductType ProductType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductDetail> ProductDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Discount> Discounts { get; set; }
    }
}
