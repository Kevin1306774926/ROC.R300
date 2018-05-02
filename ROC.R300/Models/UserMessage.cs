using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ROC.R300.Models
{
    public class UserMessage
    {
        public int Id { get; set; }
        [MaxLength(20)]
        public string Name { get; set; }
        [MaxLength(20)]
        public string Tel { get; set; }
        [MaxLength(2000)]
        public string Message { get; set; }
        public DateTime CreateTime { get; set; }
    }
}