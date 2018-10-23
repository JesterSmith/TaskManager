using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ToDo1.Models
{
    public class ToDoList
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public IList<Task> Tasks { get; set; }

        public void Add(Task task)
        {
            Tasks.Add(task);
        }

        public void AddMany(IList<Task> tasks)
        {

        }
    }
}