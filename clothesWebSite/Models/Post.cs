namespace clothesWebSite.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Post")]
    public partial class Post
    {
        [Key]
        public int post_id { get; set; }

        public string content { get; set; }

        public DateTime? last_update { get; set; }

        public int state { get; set; }

        public int post_type { get; set; }
    }
}
