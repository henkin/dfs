using System;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace Todos.Web.Tests
{
    public class PingControllerTests : IClassFixture<TestServerFixture>
    {
        private readonly TestServerFixture _fixture;

        public PingControllerTests(TestServerFixture fixture)
        {
            _fixture = fixture;
        } 

        [Fact]
        public async Task PingControllerHappyPath()
        {
            var response = await _fixture.Client.GetAsync("Lists");

            response.EnsureSuccessStatusCode();

            var responseStrong = await response.Content.ReadAsStringAsync();

            responseStrong.Should().Be("Ping");
        }
    }

    public class TodosAcceptanceTests
    {
        public void GetAll_Success()
        {
            
        }

        public void Create_Success()
        {
            
        }

        public void GetById_Success()
        {
            
        }

        public void Delete_Success()
        {
            
        }
    }
}