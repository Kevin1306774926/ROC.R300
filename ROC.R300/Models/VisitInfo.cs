using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ROC.R300.Models
{
    public class VisitInfo
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string UserId { get; set; }
        [MaxLength(20)]
        public string Ip { get; set; }
        [MaxLength(2000)]
        public string Browser { get; set; }
        [MaxLength(200)]
        public string UrlReferrer { get; set; }
        /// <summary>
        /// 访问次数
        /// </summary>
        public int VisitTimes { get; set; }
        /// <summary>
        /// 第一次访问时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 最后一次访问时间
        /// </summary>
        public DateTime LastTime { get; set; }
        /// <summary>
        /// 访问时长（秒)
        /// </summary>
        public int TimeSpan { get; set; }

    }
}