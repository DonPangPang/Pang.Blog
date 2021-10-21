using System;
using System.Collections.Generic;
using System.Security.Permissions;
using Pang.Blog.Server.Entities.Base;

namespace Pang.Blog.Server.Entities
{
    public class Comment: EntityBase
    {
        public Guid BlogId { get; set; }

        public bool IsShow { get; set; } = false;

        public string Content { get; set; } = null!;

        public Guid ParentId { get; set; }

        public Guid AnswerToUserId { get; set; }
        public string AnswerToUserName
        {
            get => $"@{AnswerToUserName}";
            set => AnswerToUserName = value;
        }

        public bool IsRead { get; set; }
    }
}