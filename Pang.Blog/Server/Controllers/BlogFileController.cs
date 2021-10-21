using System;
using Pang.Blog.Server.Controllers.Base;
using Pang.Blog.Server.Entities;
using Pang.Blog.Server.Repertories.Base;

namespace Pang.Blog.Server.Controllers
{
    public class BlogFileController: BaseApi
    {
        private readonly IRepertoryBase<BlogFile> _repertory;

        public BlogFileController(IRepertoryBase<BlogFile> repertory)
        {
            _repertory = repertory ?? throw new ArgumentNullException(nameof(repertory));
        }
    }
}