using System;
using System.Security.Cryptography;
using Xunit;
using Xunit.Abstractions;

namespace Pang.Blog.Test
{
    public class UnitTest1
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public UnitTest1(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void Test1()
        {
            //var s = MD5.Create("123456")?.ToString();

            _testOutputHelper.WriteLine("");
        }
    }
}
