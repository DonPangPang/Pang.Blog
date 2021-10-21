using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Pang.Blog.Server.Entities;
using Pang.Blog.Server.Entities.Base;
using Pang.Blog.Server.Extensions;
using Xunit;
using Xunit.Abstractions;

namespace Pang.Blog.Test
{
    public class ExtensionsTest
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public ExtensionsTest(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void EntityExtensionsTest()
        {
            var list = new List<EntityBase>()
            {
                new User()
                {
                    Id = Guid.Parse("A5E65CB1-CE32-1220-F100-87EAA7A23F85")
                },
                new User()
                {
                    Id = Guid.Parse("2757C971-7AFA-2F1A-4897-844009C8AC73")
                }
            };

            var s = list.ToLineString();
            var q = "A5E65CB1-CE32-1220-F100-87EAA7A23F85,2757C971-7AFA-2F1A-4897-844009C8AC73".ToLower();

            Assert.Equal(s, q);

            //Assert.True(q.Contains(Guid.Parse("A5E65CB1-CE32-1220-F100-87EAA7A23F85").ToString()));

            _testOutputHelper.WriteLine(s);
        }
    }
}