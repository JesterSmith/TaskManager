using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ToDo1.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Configuration;

namespace ToDo1.Data
{
    public class TaskManagementContext : DbContext
    {
        public TaskManagementContext() : base("TaskManagementData")
        {

            Database.SetInitializer<TaskManagementContext>(new DropCreateDatabaseIfModelChanges<TaskManagementContext>());
        }

        public DbSet<Task> Tasks { get; set; }
        public DbSet<ToDoList> ToDoLists { get; set; }
    }
}