using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Pang.Blog.Shared;
using Pang.Blog.Server.Entities;

namespace Pang.Blog.Server.Controllers.Base
{
    [ApiController]
    [Route("api/[Controller]/[Action]")]
    public class BaseApi : ControllerBase
    {
        [NonAction]
        public virtual OkObjectResult Success(string message)
            => new OkObjectResult(new ReturnDto()
            {
                Code = 200,
                Message = message,
            });

        [NonAction]
        public virtual OkObjectResult Success([ActionResultObjectValue] object value)
            => new OkObjectResult(new ReturnDto()
            {
                Code = 200,
                Data = value
            });

        [NonAction]
        public virtual OkObjectResult Success(string message, [ActionResultObjectValue] object value)
            => new OkObjectResult(new ReturnDto()
            {
                Code = 200,
                Message = message,
                Data = value
            });

        [NonAction]
        public virtual OkObjectResult Fail(string message)
            => new OkObjectResult(new ReturnDto()
            {
                Code = 400,
                Message = message,
            });

        [NonAction]
        public virtual OkObjectResult Fail([ActionResultObjectValue] object value)
            => new OkObjectResult(new ReturnDto()
            {
                Code = 400,
                Message = "",
                Data = value
            });

        [NonAction]
        public virtual OkObjectResult Fail(string message, [ActionResultObjectValue] object value)
            => new OkObjectResult(new ReturnDto()
            {
                Code = 400,
                Message = message,
                Data = value
            });
    }
}