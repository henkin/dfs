using System;
using System.Threading.Tasks;
using FluentAssertions;
using Newtonsoft.Json;
using Todos.Models;
using Xunit;

namespace Todos.Web.Tests
{
    public class ListControllerTests : IClassFixture<TestServerFixture>
    {
        private readonly TestServerFixture _fixture;

        public ListControllerTests(TestServerFixture fixture)
        {
            _fixture = fixture;
        } 

        [Fact]
        public async Task ListController_ReturnList()
        {
            var responseStrong = await Get<TaskList>("Lists");

            responseStrong.Should().Be("Ping");
        }

        private async Task<T> Get<T>(string url) 
        {
            var response = await _fixture.Client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var entity = JsonConvert.DeserializeObject<T>(responseString);
            return entity;
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