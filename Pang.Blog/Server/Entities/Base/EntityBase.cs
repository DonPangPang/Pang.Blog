using System;
using System.ComponentModel.DataAnnotations.Schema;
using Pang.Blog.Server.Helpers;

namespace Pang.Blog.Server.Entities.Base
{
    public abstract class EntityBase
    {
        public Guid Id { get; set; }


        public Guid CreateUserId { get; set; }
        public string? CreateUserName { get; set; }
        public DateTime? CreateDate { get; set; }

        public Guid ModifyUserId { get; set; }
        public string? ModifyUserName { get; set; }
        public DateTime? ModifyDate { get; set; }

        public bool? EnableMark { get; set; } = true;
        public bool? DeleteMark { get; set; } = false;

        public virtual void Create()
        {
            Id = Guid.NewGuid();
            var user = LoginUserInfo.Get();
            CreateUserId = user.Id;
            CreateUserName = user.Name;
            CreateDate = DateTime.Now;
        }

        public virtual void Modify()
        {
            var user = LoginUserInfo.Get();
            ModifyUserId = user.Id;
            ModifyUserName = user.Name;
            ModifyDate = DateTime.Now;
        }
    }
}