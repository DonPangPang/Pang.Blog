using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Pang.Blog.Server.Controllers.Base;
using Pang.Blog.Server.Entities;
using Pang.Blog.Server.Repertories.Base;
using Pang.Blog.Shared;

namespace Pang.Blog.Server.Controllers
{
    public class BlogController: BaseApi
    {
        private readonly IRepertoryBase<PBlog> _repertory;
        private readonly IMapper _mapper;
        private readonly IRepertoryBase<Comment> _commentRepertory;

        public BlogController(
            IRepertoryBase<PBlog> repertory, 
            IMapper mapper, 
            IRepertoryBase<Comment> commentRepertory)
        {
            _repertory = repertory ?? throw new ArgumentNullException(nameof(repertory));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _commentRepertory = commentRepertory ?? throw new ArgumentNullException(nameof(commentRepertory));
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
    }
}