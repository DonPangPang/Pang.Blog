using System;
using Pang.Blog.Server.Entities.Base;

namespace Pang.Blog.Server.Entities
{
    public class BlogFile: EntityBase
    {
        public string? Name { get; set; }
        public string? FileType { get; set; }
        public string? FileAddress { get; set; }
    }
}