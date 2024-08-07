﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HLKDotNetCore.RestApiWithNLayer.Models
{
    [Table("Tbl_Blog")]
    public class BlogModel
    {
        [Key]
        public int BlogID { get; set; }
        public string? BlogTitle { get; set; }
        public string? BlogAuthor { get; set; }
        public string? BlogContent { get; set; }
    }
}
