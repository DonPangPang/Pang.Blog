using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Pang.Blog.Shared
{
    public class BlogDto: BaseDto
    {
        public string Title { get; set; } = null!;
        public IEnumerable<TagDto> Tags { get; set; }
        public DateTime? CreateDate { get; set; }

        /// <summary>
        /// 浏览数
        /// </summary>
        public int ViewCount { get; set; }

        public string Content { get; set; } = null!;

        public IEnumerable<CommentDto> Comments { get; set; }

        public Guid CreateUserId { get; set; }
        public string CreateUserName { get; set; }
    }
}