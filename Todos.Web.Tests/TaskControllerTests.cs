using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Newtonsoft.Json;
using todos.Web.ApiModels;
using Xunit;
using Xunit.Abstractions;

namespace Todos.Web.Tests
{
    public class TaskControllerTests : BaseControllerTests, IClassFixture<TestServerFixture>
    {
        public TaskControllerTests(TestServerFixture fixture, ITestOutputHelper output) : base(fixture, output)
        {
            ControllerUrl = "Tasks/";
        }

        [Fact] public async Task Get_ReturnsAll()
        {
            var allTaskLists = await Get<List<TaskModel>>();
            allTaskLists.Should().NotBeEmpty();
            allTaskLists.ForEach(x => _output.WriteLine(x.ToString()));
        }
        
        [Fact]
        public async Task Post_CreatesTaskForList()
        {
            var taskList = await CreateTaskList();
            var task = new TaskModel
            {
                Name = "Test",
                Completed = false,
                TaskListId = taskList.Id
            };
            
            var expected = await Post(task);
            var actual = await GetById<TaskModel>(expected.Id);
            //actual.Should().Equals(expected, (e1, e2) => e1.Id == e2.Id);
            actual.Should().BeEquivalentTo(expected);
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
        private async Task<TaskModel> CreateTaskForList()
        {
            var task = new TaskModel
            {
                Name = "Test",
                Completed = false
            };
            var savedTask = await Post(task);
            return savedTask;
        }
    }
}