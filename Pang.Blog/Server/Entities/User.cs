using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Cors;
using Pang.Blog.Server.Entities.Base;

namespace Pang.Blog.Server.Entities
{
    public class User: EntityBase
    {
        [Display(Name = "账号")] 
        public string Account { get; set; } = null!;

        [Display(Name = "密码")]
        public string Password { get; set; } = null!;

        [Display(Name = "是否为超级管理员")] 
        public bool IsSuper { get; set; } = false;

        /// <summary>
        /// 是否有编写博客的权限
        /// </summary>
        public bool IsAuthor { get; set; } = false;

        public string Name { get; set; } = null!;

        public override void Create()
        {
            Id = Guid.NewGuid();
            CreateDate = DateTime.Now;
        }
    }
}