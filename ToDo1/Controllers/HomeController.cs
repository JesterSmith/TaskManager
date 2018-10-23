using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ToDo1.Data;
using ToDo1.Models;

namespace ToDo1.Controllers
{
    public class HomeController : Controller
    {
        private TaskManagementContext db = new TaskManagementContext();
        public ActionResult Index()
        {
            List<Task> tasks = new List<Task>();
            foreach (var x in db.Tasks.Where(x => x.DueDate != null).ToList())
            {
                if(x.DueDate.Value.Day == DateTime.Now.Day && x.DueDate.Value.Month == DateTime.Now.Month && x.DueDate.Value.Year == DateTime.Now.Year)
                {
                    tasks.Add(x);
                }
            }

            return View(tasks);
        }

    }
}