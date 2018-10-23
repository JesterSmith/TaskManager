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
    public class TaskController : Controller
    {
        private TaskManagementContext db = new TaskManagementContext();

        // GET: Task
        public ActionResult Index()
        {
            var tasks = db.Tasks;
            tasks.OrderBy(x => x.ID).OrderBy(x => x.ParentTaskID);
            return View(tasks.ToList());
        }

        // GET: Task/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Task task = db.Tasks.Find(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(task);
        }

        // GET: Task/Create
        public ActionResult Create()
        {
            ViewBag.ToDoListID = new SelectList(db.ToDoLists, "ID", "Title");
            return View();
        }

        // POST: Task/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Title,Description,Priority,Status,DueDate,ToDoListID")] Task task)
        {
            if (ModelState.IsValid)
            {
                db.Tasks.Add(task);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ToDoListID = new SelectList(db.ToDoLists, "ID", "Title", task.ToDoListID);
            return View(task);
        }

        // GET: Task/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Task task = db.Tasks.Find(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            ViewBag.ToDoListID = new SelectList(db.ToDoLists, "ID", "Title", task.ToDoListID);
            return View(task);
        }

        // POST: Task/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Title,Description,Priority,Status,DueDate,ToDoListID")] Task task)
        {
            if (ModelState.IsValid)
            {
                db.Entry(task).State = EntityState.Modified;
                
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ToDoListID = new SelectList(db.ToDoLists, "ID", "Title", task.ToDoListID);
            return View(task);
        }

        // GET: Task/SubTask/5
        public ActionResult SubTask(int id)
        {
            ToDo1.Models.Task sTask = new Task();

            sTask.ParentTaskID = id;
            sTask.ToDoListID = db.Tasks.Where(x => x.ID == id).Select(x => x.ToDoListID).FirstOrDefault();
            


            return View(sTask);
        }

        // POST: Task/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SubTask([Bind(Include = "ID,Title,Description,ToDoListID,ParentTaskID")] Task task)
        {
            if (ModelState.IsValid)
            {
                db.Tasks.Add(task);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //ViewBag.ToDoListID = new SelectList(db.ToDoLists, "ID", "Title", task.ToDoListID);
            return View(task);
        }

        // GET: Task/SubTasks/5
        public ActionResult SubTasks(int id)
        {
            return View(db.Tasks.Where(x => x.ParentTaskID == id).ToList());
        }

        // GET: Task/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Task task = db.Tasks.Find(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(task);
        }

        // POST: Task/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Task task = db.Tasks.Find(id);
            db.Tasks.Remove(task);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
