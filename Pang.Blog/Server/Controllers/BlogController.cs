using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pang.Blog.Server.Controllers.Base;
using Pang.Blog.Server.Entities;
using Pang.Blog.Server.Repertories.Base;
using Pang.Blog.Shared;

namespace Pang.Blog.Server.Controllers
{
    /// <summary>
    /// 博客API
    /// </summary>
    public class BlogController: BaseApi
    {
        private readonly IRepertoryBase<PBlog> _repertory;
        private readonly IMapper _mapper;
        private readonly IRepertoryBase<Comment> _commentRepertory;
        private readonly IRepertoryBase<Tag> _tagRepertory;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="repertory"></param>
        /// <param name="mapper"></param>
        /// <param name="commentRepertory"></param>
        /// <param name="tagRepertory"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public BlogController(
            IRepertoryBase<PBlog> repertory, 
            IMapper mapper, 
            IRepertoryBase<Comment> commentRepertory,
            IRepertoryBase<Tag> tagRepertory)
        {
            _repertory = repertory ?? throw new ArgumentNullException(nameof(repertory));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _commentRepertory = commentRepertory ?? throw new ArgumentNullException(nameof(commentRepertory));
            _tagRepertory = tagRepertory ?? throw new ArgumentNullException(nameof(tagRepertory));
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult> GetBlog(Guid id)
        {
            if (id == Guid.Empty) throw new ArgumentNullException(nameof(id));

            var blog = await _repertory.GetEntityByIdAsync(id);

            var res = _mapper.Map<BlogDto>(blog);

            res.Comments = _mapper.Map<IEnumerable<CommentDto>>(
                await _commentRepertory
                    .GetEntitiesAsync(x => x.BlogId.Equals(blog.Id) && 
                                           x.DeleteMark.Equals(false)));

            return Success(res);
        }

        [HttpGet("{userId:guid}")]
        public async Task<ActionResult> GetBlogsByBlogger(Guid userId)
        {
            if (userId == Guid.Empty) throw new ArgumentNullException(nameof(userId));

            var blogs = _mapper.Map<IEnumerable<BlogDto>>(
                await _repertory
                    .GetEntitiesAsync(x => x.CreateUserId.Equals(userId) && 
                                           x.DeleteMark.Equals(false)));

            return Success(blogs);
        }

        [HttpGet]
        public async Task<ActionResult> GetBlogs()
        {
            var blogs = _mapper.Map<IEnumerable<BlogDto>>(
                await _repertory
                    .GetEntitiesAsync(x=> x.DeleteMark.Equals(false)));

            return Success(blogs);
        }

        /// <summary>
        /// 添加一篇博客
        /// </summary>
        /// <param name="blog"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        [HttpPost]
        public async Task<ActionResult> AddBlog([FromBody] BlogDto blog)
        {
            if (blog == null) throw new ArgumentNullException(nameof(blog));

            var dto = _mapper.Map<PBlog>(blog);
            dto.Create();

            if (await _repertory.InsertAsync(dto))
            {
                return Success("上传成功");
            }

            return Fail("上传失败");
        }

        /// <summary>
        /// 更新一篇博客
        /// </summary>
        /// <param name="blog"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        [HttpPost]
        public async Task<ActionResult> Update([FromBody] BlogDto blog)
        {
            if (blog == null) throw new ArgumentNullException(nameof(blog));

            var dto = _mapper.Map<PBlog>(blog);

            if (await _repertory.UpdateAsync(dto))
            {
                return Success("修改成功");
            }

            return Fail("修改失败");
        }

        /// <summary>
        /// 删除一篇博客
        /// </summary>
        /// <param name="blogId"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        [HttpGet("{blogId:guid}")]
        public async Task<ActionResult> Delete(Guid blogId)
        {
            if (blogId == Guid.Empty) throw new ArgumentNullException(nameof(blogId));

            var blog = new PBlog()
            {
                Id = blogId,
                DeleteMark = true
            };

            if (await _repertory.UpdateAsync(blog))
            {
                return Success("删除成功");
            }

            return Fail("删除失败");
        }

        /// <summary>
        /// 搜索博客
        /// <para>标题/标签</para>
        /// </summary>
        /// <param name="queryString"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Search(string queryString)
        {
            var blogs = await _repertory.DbSet
                .Where(x => x.Title.Contains(queryString))
                .Select(t => new SearchDto() { Id = t.Id, Title = t.Title })
                .ToListAsync();

            var tags = await _tagRepertory
                .GetEntitiesAsync(x => x.Name.Contains(queryString));

            foreach (var tag in tags)
            {
                blogs.AddRange(
                    await _repertory.DbSet
                    .Where(x => (x.TagIds ?? "").Contains(tag.Id.ToString()))
                    .Select(t => new SearchDto() { Id = t.Id, Title = t.Title })
                    .ToListAsync());
            }

            return Success(blogs);
        }
    }
}