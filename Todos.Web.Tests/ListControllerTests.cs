using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Newtonsoft.Json;
using todos.Web.ApiModels;
using Todos.Models;
using Xunit;
using Xunit.Abstractions;

namespace Todos.Web.Tests
{
    [Collection("Controller Tests")]
    public class ListControllerTests : BaseControllerTests, IClassFixture<TestServerFixture>
    {
        public ListControllerTests(TestServerFixture fixture, ITestOutputHelper output) : base(fixture, output)
        {
            ControllerUrl = "lists/";
        }

        [Fact] public async Task Get_ReturnsAll()
        {
            await CreateTaskList();
            var allTaskLists = await Get<List<TaskListModel>>();
            allTaskLists.Should().NotBeEmpty();
            allTaskLists.ForEach(x => _output.WriteLine(x.ToString()));
        }
        
        [Fact]
        public async Task Post_CreatesTaskList()
        {
            var savedTask = await CreateTaskList();
            var actual = await GetById<TaskListModel>(savedTask.Id);
            actual.Should().BeEquivalentTo(savedTask);
        }
        
//        [Fact]
//        public async Task Put_UpdatesTaskList()
//        {
//            var taskList = await CreateTaskList();
//            var updatedDescription = "Updated Description";
//            taskList.Description = updatedDescription;
//
//            await Put(taskList);
//            
//            var actual = await GetById<TodoTaskList>(taskList.Id);
//            actual.Description.Should().Be(updatedDescription);
//        }
//        
//        [Fact]
//        public async Task Delete_DeletesTaskList()
//        {
//            var savedTask = await CreateTaskList();
//           
//            await Delete(savedTask.Id);
//            
//            var actual = await GetById<TodoTaskList>(savedTask.Id);
//            actual.Should().BeNull();
//        }
//        
    }
}