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
    public class ToDoListController : Controller
    {
        private TaskManagementContext db = new TaskManagementContext();

        // GET: ToDoList
        public ActionResult Index()
        {
            return View(db.ToDoLists.ToList());
        }

        // GET: ToDoList/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ToDoList toDoList = db.ToDoLists.Find(id);
            if (toDoList == null)
            {
                return HttpNotFound();
            }
            toDoList.Tasks = new List<Task>();

            toDoList.Tasks = (from task in db.Tasks where task.ToDoListID == id select task).OrderBy(x => x.Priority).ToList();

            return View(toDoList);
        }

        // GET: ToDoList/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ToDoList/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Title")] ToDoList toDoList)
        {
            if (ModelState.IsValid)
            {
                db.ToDoLists.Add(toDoList);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(toDoList);
        }

        // GET: ToDoList/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ToDoList toDoList = db.ToDoLists.Find(id);
            if (toDoList == null)
            {
                return HttpNotFound();
            }
            return View(toDoList);
        }

        // POST: ToDoList/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Title")] ToDoList toDoList)
        {
            if (ModelState.IsValid)
            {
                db.Entry(toDoList).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(toDoList);
        }

        // GET: ToDoList/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ToDoList toDoList = db.ToDoLists.Find(id);
            if (toDoList == null)
            {
                return HttpNotFound();
            }
            return View(toDoList);
        }

        // POST: ToDoList/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ToDoList toDoList = db.ToDoLists.Find(id);
            db.ToDoLists.Remove(toDoList);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: ToDoList/Priority/5
        public ActionResult Priority(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ToDoList toDoList = db.ToDoLists.Find(id);
            if (toDoList == null)
            {
                return HttpNotFound();
            }

            toDoList.Tasks = new List<Task>();

            toDoList.Tasks = (from task in db.Tasks where task.ToDoListID == id select task).OrderBy(x => x.Priority).ToList();
            return View(toDoList);
        }

        // POST: ToDoList/Priority/5
        [HttpPost, ActionName("Priority")]
        [ValidateAntiForgeryToken]
        public ActionResult Priority(int id)
        {
            ToDoList toDoList = db.ToDoLists.Find(id);

            toDoList.Tasks = new List<Task>();

            toDoList.Tasks = (from task in db.Tasks where task.ToDoListID == id select task).ToList();
            List<int> ids = Request.Form["curItem.ID"].Split(',').Select(int.Parse).ToList();
            List<int> priorities = Request.Form["curItem.Priority"].Split(',').Select(int.Parse).ToList();

            for (int i = 0; i < ids.Count(); i++)
            {
                Task task = toDoList.Tasks.Where(x => x.ID == ids[i]).FirstOrDefault();

                task.Priority = priorities[i];
                db.Entry(task).State = EntityState.Modified;
            }

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
