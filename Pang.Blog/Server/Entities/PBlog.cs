using System;
using System.ComponentModel.DataAnnotations;
using Pang.Blog.Server.Entities.Base;

namespace Pang.Blog.Server.Entities
{
    public class PBlog: EntityBase
    {
        [Display(Name = "标题")]
        public string Title { get; set; } = null!;
        public string? TagIds { get; set; }

        /// <summary>
        /// 浏览数
        /// </summary>
        public int ViewCount { get; set; }

        public string Content { get; set; } = null!;

        //public IEnumerable<Comment>? Comments { get; set; }
    }
}