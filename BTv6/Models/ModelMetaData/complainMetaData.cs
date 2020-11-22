using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BTv6.Models.ModelMetaData
{
    public class complainMetaData
    {
        [Key]
        public int cID { get; set; }
        [Required]
        public string sub { get; set; }
        public string OwnerID { get; set; }
        [Required]
        public string Text { get; set; }

        public virtual customer customer { get; set; }
    }
}