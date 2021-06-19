namespace clothesWebSite.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Contact")]
    public partial class Contact
    {
        [Key]
        public int contact_id { get; set; }

        [Required]
        [StringLength(250)]
        public string sender_full_name { get; set; }

        [Required]
        [StringLength(15)]
        public string sender_phone { get; set; }

        [Required]
        [StringLength(100)]
        public string sender_email { get; set; }

        [Required]
        public string content { get; set; }

        public int contact_type { get; set; }

        public int state { get; set; }

        public DateTime? create_date { get; set; }
    }
}
