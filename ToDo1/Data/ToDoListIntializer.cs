using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using ToDo1.Models;


namespace ToDo1.Data
{
    public class ToDoListIntializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<TaskManagementContext>
    {
        protected override void Seed(TaskManagementContext data)
        {
            var tasks = new List<Task>
            {
                new Task("Get Food", "Need to go to the store to get some food and water"),
                new Task("Clean up yard", "Rake leaves, clean up dog stuff, clean up kids stuff"),
                new Task("Mow Lawn", "Bag lawn clippings for mulch"),
                new Task("Clean up shed", "Reorganize shed and get summer stuff in storage"),
                new Task("Change Oil", "Need to change oil on Jeep")
            };

            tasks.ForEach(x => data.Tasks.Add(x));

            var todoList = new ToDoList();
            todoList.Title = "Around the House";
            todoList.AddMany(tasks);

            var tasks1 = new List<Task>
            {
                new Task("Get Out Shovels", "Need to get shovels from shed"),
                new Task("Get ice melt", "Need to get ice melt from store"),
                new Task("Get mower cleaned", "get mower prep done for winter/run dry")
            };

            tasks1[0].DueDate = new DateTime(2018, 10, 25);
            tasks1[1].DueDate = new DateTime(2018, 10, 26);
            tasks1[2].DueDate = new DateTime(2018, 10, 23);

            tasks1.ForEach(x => data.Tasks.Add(x));

            var todoList1 = new ToDoList();
            todoList1.Title = "Winter Prep";
            todoList1.AddMany(tasks1);



            data.ToDoLists.Add(todoList);
            data.ToDoLists.Add(todoList1);
            data.SaveChanges();
        }
    }
}