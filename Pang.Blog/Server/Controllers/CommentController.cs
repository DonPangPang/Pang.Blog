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

        /// <summary>
        /// 获取子评论
        /// </summary>
        /// <param name="commentId"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        [HttpGet("{commentId:guid}")]
        public async Task<ActionResult> GetChildrenComments(Guid commentId)
        {
            if (commentId == Guid.Empty) throw new ArgumentNullException(nameof(commentId));

            var comments = await _repertory
                .GetEntitiesAsync(x => x.ParentId.Equals(commentId));

            var res = _mapper.Map<IEnumerable<CommentDto>>(comments);

            return Success(res);
        }

        /// <summary>
        /// 评论
        /// </summary>
        /// <param name="comment"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        [HttpPost]
        public async Task<ActionResult> AddComment([FromBody] CommentDto comment)
        {
            if (comment == null) throw new ArgumentNullException(nameof(comment));

            var dto = _mapper.Map<Comment>(comment);
            dto.Create();

            if (await _repertory.InsertAsync(dto))
            {
                return Success("评论成功");
            }

            return Fail("评论失败");
        }

        /// <summary>
        /// 删除评论
        /// </summary>
        /// <param name="commentId">删除评论的Id</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        [HttpGet("{commentId:guid}")]
        public async Task<ActionResult> Delete(Guid commentId)
        {
            if (commentId == Guid.Empty) throw new ArgumentNullException(nameof(commentId));

            var comment = new Comment()
            {
                Id = commentId,
                DeleteMark = true
            };
            comment.Modify();

            if (await _repertory.UpdateAsync(comment))
            {
                return Success("删除成功");
            }

            return Fail("删除失败");
        }
    }
}