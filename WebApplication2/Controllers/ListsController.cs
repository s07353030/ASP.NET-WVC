using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication2;

namespace WebApplication2.Controllers
{
    public class ListsController : Controller
    {
        private ToDoEntities db = new ToDoEntities();

        // GET: Lists
        public ActionResult Index()
        {
            return View(db.List.ToList());
        }

        // GET: Lists/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List list = db.List.Find(id);
            if (list == null)
            {
                return HttpNotFound();
            }
            return View(list);
        }

        // GET: Lists/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Lists/Create
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SN,Title,State,CreateTime,ModifyTime")] List list)
        {
            if (ModelState.IsValid)
            {
                list.ModifyTime = DateTime.Now;
                list.CreateTime = DateTime.Now;
                db.List.Add(list);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(list);
        }

        // GET: Lists/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List list = db.List.Find(id);
            if (list == null)
            {
                return HttpNotFound();
            }
            return View(list);
        }

        // POST: Lists/Edit/5
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SN,Title,State,CreateTime,ModifyTime")] List list)
        {
            if (ModelState.IsValid)
            {
                //List old_list = db.List.Find(list.SN);
                //list.CreateTime = old_list.CreateTime;

                list.ModifyTime = DateTime.Now;
                db.Entry(list).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(list);
        }

        // GET: Lists/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List list = db.List.Find(id);
            if (list == null)
            {
                return HttpNotFound();
            }
            return View(list);
        }

        // POST: Lists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            List list = db.List.Find(id);
            db.List.Remove(list);
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
