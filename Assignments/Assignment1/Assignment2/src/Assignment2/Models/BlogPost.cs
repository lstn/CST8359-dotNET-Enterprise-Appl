using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment2.Models
{
    public class BlogPost
    {
        [Key]
        public int BlogPostId
        {
            get;
            set;
        }

        [ForeignKey("UserId")]
        public int UserId
        {
            get;
            set;
        }

        [StringLength(200)]
        public string Title
        {
            get;
            set;
        }

        [StringLength(4000)]
        public string Content
        {
            get;
            set;
        }

        [DataType(DataType.DateTime)]
        public DateTime Posted
        {
            get;
            set;
        }
    }
}
