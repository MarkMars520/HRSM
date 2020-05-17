using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HRSM.Models;

namespace HRSM.Controllers
{
    public class PostController : Controller
    {
        private HRSMDBEntities db = new HRSMDBEntities();

        // GET: Post
        public ActionResult Index()
        {
            var post = db.Post.Include(p => p.Company);
            return View(post.ToList());
        }

        // GET: Post/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Post.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // GET: Post/Create
        public ActionResult Create()
        {
            ViewBag.Types = new SelectList(db.Dict.Where(d => d.TypeName == "岗位类型"), "Value", "Name");
            ViewBag.Company_IDs = new SelectList(db.Company, "ID", "Title");
            return View();
        }

        // POST: Post/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Company_ID,Name,URL,Type,Desc,Num,InterNum")] Post post)
        {
            if (ModelState.IsValid)
            {
                db.Post.Add(post);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Types = new SelectList(db.Dict.Where(d => d.TypeName == "岗位类型"), "Value", "Name");
            ViewBag.Company_IDs = new SelectList(db.Company, "ID", "Title", post.Company_ID);
            return View(post);
        }

        // GET: Post/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Post.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            ViewBag.Types = new SelectList(db.Dict.Where(d => d.TypeName == "岗位类型"), "Value", "Name");
            ViewBag.Company_IDs = new SelectList(db.Company, "ID", "Title", post.Company_ID);
            return View(post);
        }

        // POST: Post/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Company_ID,Name,URL,Type,Desc,Num,InterNum")] Post post)
        {
            if (ModelState.IsValid)
            {
                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Types = new SelectList(db.Dict.Where(d => d.TypeName == "岗位类型"), "Value", "Name");
            ViewBag.Company_IDs = new SelectList(db.Company, "ID", "Title", post.Company_ID);
            return View(post);
        }

        // GET: Post/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Post.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            ViewBag.Types = new SelectList(db.Dict.Where(d => d.TypeName == "岗位类型"), "Value", "Name");
            ViewBag.Company_IDs = new SelectList(db.Company, "ID", "Title", post.Company_ID);
            return View(post);
        }

        // POST: Post/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Post post = db.Post.Find(id);
            db.Post.Remove(post);
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
