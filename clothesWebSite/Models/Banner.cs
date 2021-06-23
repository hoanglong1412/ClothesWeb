namespace clothesWebSite.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Banner")]
    public partial class Banner
    {
        static public readonly int VISIBLE_TOP = 1;
        static public readonly int VISIBLE_BOTTOM = 2;
        static public readonly int INVISIBLE = 0;

        [Key]
        public int banner_id { get; set; }

        public int state { get; set; }

        public DateTime? from_date { get; set; }

        public DateTime? to_date { get; set; }

        public DateTime? create_date { get; set; }

        [StringLength(300)]
        public string url { get; set; }
    }
}
