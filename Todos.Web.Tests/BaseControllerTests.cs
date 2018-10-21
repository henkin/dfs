using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using todos.Web.ApiModels;
using Todos.Models;
using Xunit.Abstractions;

namespace Todos.Web.Tests
{
    public class BaseControllerTests
    {
        protected TestServerFixture _fixture;
        protected ITestOutputHelper _output;
        protected string ControllerUrl;

        protected BaseControllerTests(TestServerFixture fixture, ITestOutputHelper output)
        {
            _fixture = fixture;
            _output = output;
        }
        
        protected async Task Delete(Guid id)
        {
            var response = await _fixture.Client.DeleteAsync(ControllerUrl + id);
            await GetSuccessfulResponseData<TodoTaskList>(response);
        }

        protected async Task<T> Post<T>(T item)
        {
            var stringContent = StringContent(item);
            var response = await _fixture.Client.PostAsync(ControllerUrl, stringContent);
            return await GetSuccessfulResponseData<T>(response);
        }

        protected async Task<T> Put<T>(T item, Guid id)
        {
            var stringContent = StringContent(item);
            var response = await _fixture.Client.PutAsync(ControllerUrl + id, stringContent);
            return await GetSuccessfulResponseData<T>(response);
        }

        protected async Task<T> Get<T>()
        {
            var response = await _fixture.Client.GetAsync(ControllerUrl);
            return await GetSuccessfulResponseData<T>(response);
        }

        protected async Task<T> GetById<T>(Guid id)
        {
            var response = await _fixture.Client.GetAsync(ControllerUrl + id);
            return await GetSuccessfulResponseData<T>(response);
        }
        
        protected static async Task<T> GetSuccessfulResponseData<T>(HttpResponseMessage response)
        {
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var entity = JsonConvert.DeserializeObject<T>(responseString);
            return entity;
        }
        
        protected static StringContent StringContent<T>(T item)
        {
            var stringContent = new StringContent(
                JsonConvert.SerializeObject(item),
                Encoding.UTF8,
                "application/json");
            return stringContent;
        }

        /// <summary>
        /// This will be refactored into a separate Test Workflow component
        /// </summary>
        /// <returns></returns>
        protected async Task<TaskListModel> CreateTaskList()
        {
            var taskList = new TaskListModel()
            {
                Name = "TestTaskList",
                Description = "TestDescription",
                Tasks = new List<TaskModel>()
                {
                    new TaskModel()
                    {
                        Name = "TestTaskName",
                        Completed = false
                    }
                }
            };
            var stringContent = StringContent(taskList);
            var response = await _fixture.Client.PostAsync("Lists/", stringContent);
            var savedTask = await GetSuccessfulResponseData<TaskListModel>(response);
            return savedTask;
        }
    }
}