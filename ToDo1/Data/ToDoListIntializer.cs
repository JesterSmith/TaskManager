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
                new Task("Get Food", "Need to got to the store to get some food and water"),
                new Task("Clean up yard", "Rake leaves, clean up dog stuff, clean up kids stuff"),
                new Task("Mow Lawn", "Bag lawn clippings for mulch"),
                new Task("Clean up shed", "Reorganize shed and get summer stuff in storage"),
                new Task("Change Oil", "Need to change oil on Jeep")
            };

            tasks.ForEach(x => data.Tasks.Add(x));

            var todoList = new ToDoList();
            todoList.Title = "Around the House";
            todoList.AddMany(tasks);

            data.ToDoLists.Add(todoList);
            data.SaveChanges();
        }
    }
}