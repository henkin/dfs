using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
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

        [Fact] public async Task Get_ReturnsAll()
        {
            var taskList = await Get<List<TaskList>>();
            //taskList.Should()(new TaskList());
        }
        
        [Fact]
        public async Task Post_CreatesTaskList()
        {
            var taskList = new TaskList()
            {
                Name = "TestTaskList",
                Description = "TestDescription"
            };
            var savedTask = await Post(taskList);

            var actual = await GetById<TaskList>(savedTask.Id);

            actual.Should().Be(savedTask);
            //taskList.Should()(new TaskList());
        }

        private async Task<TaskList> Post(TaskList item)
        {
            var stringContent = new StringContent(
                JsonConvert.SerializeObject(item), 
                Encoding.UTF8, 
                "application/json");
            var response = await _fixture.Client.PostAsync("Lists", stringContent);
            return await GetSuccessfulResponseData<TaskList>(response);
        }
        
        private async Task<T> Get<T>()
        {
            var response = await _fixture.Client.GetAsync("Lists");
            return await GetSuccessfulResponseData<T>(response);
        }
        
        private async Task<T> GetById<T>(Guid id)
        {
            var response = await _fixture.Client.GetAsync("Lists/" + id);
            return await GetSuccessfulResponseData<T>(response);
        }

        private static async Task<T> GetSuccessfulResponseData<T>(HttpResponseMessage response)
        {
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