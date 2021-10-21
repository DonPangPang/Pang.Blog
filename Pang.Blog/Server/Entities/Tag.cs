using System;
using System.Collections.Generic;
using Pang.Blog.Server.Entities.Base;

namespace Pang.Blog.Server.Entities
{
    public class Tag: EntityBase
    {
        public string Name { get; set; } = null!;

        public Guid ParentId { get; set; }
    }
}