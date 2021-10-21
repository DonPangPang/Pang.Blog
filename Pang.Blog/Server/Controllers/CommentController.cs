using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Pang.Blog.Server.Controllers.Base;
using Pang.Blog.Server.Entities;
using Pang.Blog.Server.Repertories.Base;
using Pang.Blog.Shared;

namespace Pang.Blog.Server.Controllers
{
    /// <summary>
    /// 评论API
    /// </summary>
    public class CommentController: BaseApi
    {
        private readonly IRepertoryBase<Comment> _repertory;
        private readonly IMapper _mapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="repertory"></param>
        /// <param name="mapper"></param>
        public CommentController(
            IRepertoryBase<Comment> repertory,
            IMapper mapper)
        {
            _repertory = repertory ?? throw new ArgumentNullException(nameof(repertory));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// 获取所有评论
        /// </summary>
        /// <param name="blogId"></param>
        /// <returns></returns>
        [HttpGet("{blogId:guid}")]
        public async Task<ActionResult> GetCommentsByBlog(Guid blogId)
        {
            if (blogId == Guid.Empty) throw new ArgumentNullException(nameof(blogId));
            var comments = await _repertory
                .GetEntitiesAsync(x => x.BlogId.Equals(blogId));

            var res = _mapper.Map<IEnumerable<CommentDto>>(comments);

            return Success(res);
        }
    }
}