namespace clothesWebSite.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("User")]
    public partial class User
    {

        //Possible value for gender
        static public readonly int FEMALE = 0;
        static public readonly int MALE = 1;

        //Possible value for user role
        static public readonly int CUSTOMER = 0;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            Payments = new HashSet<Payment>();
        }

        [Key]
        public int user_id { get; set; }

        public int user_role { get; set; }

        [StringLength(100)]
        public string full_name { get; set; }

        [Required(ErrorMessage = "Please input your phone in this field")]
        [StringLength(15)]
        public string phone { get; set; }

        [StringLength(150)]
        public string email { get; set; }

        [StringLength(40)]
        public string password { get; set; }

        public int? gender { get; set; }

        public DateTime? date_birth { get; set; }

        public DateTime? create_date { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Payment> Payments { get; set; }
    }
}
