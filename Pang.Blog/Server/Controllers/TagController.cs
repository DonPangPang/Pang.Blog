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
    /// 标签API
    /// </summary>
    public class TagController: BaseApi
    {
        private readonly IRepertoryBase<Tag> _repertory;
        private readonly IMapper _mapper;
        private readonly IRepertoryBase<PBlog> _blogRepertory;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="repertory"></param>
        /// <param name="mapper"></param>
        /// <param name="blogRepertory"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public TagController(
            IRepertoryBase<Tag> repertory,
            IMapper mapper,
            IRepertoryBase<PBlog> blogRepertory)
        {
            _repertory = repertory ?? throw new ArgumentNullException(nameof(repertory));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _blogRepertory = blogRepertory ?? throw new ArgumentNullException(nameof(blogRepertory));
        }

        /// <summary>
        /// 获取所有标签
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> GetTags()
        {
            var tags = await _repertory
                .GetEntitiesAsync(x=>x.DeleteMark.Equals(false));

            var res = _mapper.Map<IEnumerable<TagDto>>(tags);

            return Success(res);
        }

        /// <summary>
        /// 获取博客的标签
        /// </summary>
        /// <param name="blogId">博客Id</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        [HttpGet("{blogId:guid}")]
        public async Task<ActionResult> GetTagsByBlog(Guid blogId)
        {
            if (blogId == Guid.Empty) throw new ArgumentNullException(nameof(blogId));

            var blog = await _blogRepertory.GetEntityByIdAsync(blogId);

            var tags = _repertory
                .GetEntityAsync(x => (blog.TagIds ?? "")
                    .Contains(x.Id.ToString()) &&  
                    x.DeleteMark.Equals(false));

            var res = _mapper.Map<IEnumerable<TagDto>>(tags);

            return Success(res);
        }
    }
}