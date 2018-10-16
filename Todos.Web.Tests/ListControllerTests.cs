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
            await CreateTaskList();
            var taskList = await Get<List<TaskList>>();
            taskList.Should().NotBeEmpty();
        }
        
        [Fact]
        public async Task Post_CreatesTaskList()
        {
            var savedTask = await CreateTaskList();
            var actual = await GetById<TaskList>(savedTask.Id);
            actual.Should().Be(savedTask);
        }
        
        [Fact]
        public async Task Put_UpdatesTaskList()
        {
            var taskList = await CreateTaskList();
            var updatedDescription = "Updated Description";
            taskList.Description = updatedDescription;

            await Put(taskList);
            
            var actual = await GetById<TaskList>(taskList.Id);
            actual.Description.Should().Be(updatedDescription);
        }
        
        [Fact]
        public async Task Delete_DeletesTaskList()
        {
            var savedTask = await CreateTaskList();
           
            await Delete(savedTask.Id);
            
            var actual = await GetById<TaskList>(savedTask.Id);
            actual.Should().BeNull();
        }
        
        private async Task<TaskList> CreateTaskList()
        {
            var taskList = new TaskList()
            {
                Name = "TestTaskList",
                Description = "TestDescription"
            };
            var savedTask = await Post(taskList);
            return savedTask;
        }

        private async Task Delete(Guid id)
        {
            var response = await _fixture.Client.DeleteAsync("Lists/" + id);
            await GetSuccessfulResponseData<TaskList>(response);
        }

        private async Task<T> Post<T>(T item)
        {
            var stringContent = StringContent(item);
            var response = await _fixture.Client.PostAsync("Lists", stringContent);
            return await GetSuccessfulResponseData<T>(response);
        }

        private async Task<T> Put<T>(T item) where T : Entity
        {
            var stringContent = StringContent(item);
            var response = await _fixture.Client.PutAsync("Lists/" + item.Id, stringContent);
            return await GetSuccessfulResponseData<T>(response);
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

        private static StringContent StringContent<T>(T item)
        {
            var stringContent = new StringContent(
                JsonConvert.SerializeObject(item),
                Encoding.UTF8,
                "application/json");
            return stringContent;
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