using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pang.Blog.Server.Controllers.Base;
using Pang.Blog.Server.Entities;
using Pang.Blog.Server.Helpers;
using Pang.Blog.Server.Repertories.Base;
using System;
using System.Security.Cryptography;
using System.Threading.Tasks;
using AutoMapper;
using Pang.Blog.Server.Extensions;
using Pang.Blog.Shared;

namespace Pang.Blog.Server.Controllers
{
    public class UserController: BaseApi
    {
        private readonly IRepertoryBase<User> _repertory;
        private readonly IMapper _mapper;
        private readonly DbSet<User> _user;

        public UserController(IRepertoryBase<User> repertory, IMapper mapper)
        {
            _repertory = repertory ?? throw new ArgumentNullException(nameof(repertory));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _user = _repertory.DbSet;
        }

        [HttpPost]
        public async Task<ActionResult> Login([FromBody] UserDto user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            var loginInfo = await _repertory.GetEntityAsync(x => x.Account.Equals(user.Account));

            if (loginInfo is null)
            {
                return Fail("账号不存在");
            }

            if (loginInfo.Password.Equals(user.Password.ToMd5()))
            {
                LoginUserInfo.Set(loginInfo);
                return Success("登录成功");
            }

            return Fail("登陆失败");
        }

        [HttpGet("{userId:guid}")]
        public async Task<ActionResult> GetUserById(Guid userId)
        {
            var user = await _repertory.GetEntityByIdAsync(userId);

            return Success(user);
        }

        [HttpGet]
        public async Task<ActionResult> GetUsers()
        {
            var users = await _repertory.GetEntitiesAsync();

            return Success(users);
        }

        [HttpPost]
        public async Task<ActionResult> Update([FromBody]UserDto user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            var userInfo = await _repertory.GetEntityAsync(x => x.Account.Equals(user.Account));

            if (userInfo.Password.Equals(user.OldPassword.ToMd5()))
            {
                userInfo.Password = user.Password.ToMd5();

                if (await _repertory.UpdateAsync(userInfo))
                {
                    return Success("修改密码成功");
                }
                else
                {
                    return Fail("修改失败");
                }
            }
            else
            {
                return Fail("密码错误");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Register([FromBody] UserDto user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            var dto = _mapper.Map<User>(user);

            dto.Create();

            if (await _repertory.InsertAsync(dto))
            {
                return Success("注册成功");
            }
            else
            {
                return Fail("注册失败");
            }
        }

        [HttpGet("{id:guid}")]
        public ActionResult Test(Guid id)
        {
            return Success("测试接口", new{Data = id});
        }
    }
}
