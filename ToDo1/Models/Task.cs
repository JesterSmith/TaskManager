using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ToDo1.Models
{
    public enum Progress { Pending, Started, Done };
    public class Task
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Priority { get; set; }
        public string Status { get; set; }
        public virtual ToDoList SubTasks { get; set; }
        public DateTime? DueDate { get; set; }
        public virtual int ToDoListID { get; set; }

        public Task() { }

        public Task(string title, string description)
        {
            this.Title = title;
            this.Description = description;
            this.Status = Progress.Pending.ToString();
        }
    }
}