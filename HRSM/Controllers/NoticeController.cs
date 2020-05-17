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
    public class NoticeController : Controller
    {
        private HRSMDBEntities db = new HRSMDBEntities();

        // GET: Notice
        public ActionResult Index()
        {
            var notice = db.Notice.Include(n => n.Teacher);
            return View(notice.ToList());
        }

        // GET: Notice/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Notice notice = db.Notice.Find(id);
            if (notice == null)
            {
                return HttpNotFound();
            }
            return View(notice);
        }

        // GET: Notice/Create
        public ActionResult Create()
        {
            ViewBag.Creator = new SelectList(db.Teacher, "ID", "Name");
            return View();
        }

        // POST: Notice/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Title,Content,Attachment,CreateTime,Creator")] Notice notice, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                //上传文件代码
                if (file != null && file.ContentLength != 0)
                {
                    notice.Attachment = "~/Upload/Notice/" + file.FileName;
                    file.SaveAs(Server.MapPath(notice.Attachment));//保存到服务器(上传该附件)
                }
                db.Notice.Add(notice);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Creator = new SelectList(db.Teacher, "ID", "Name", notice.Creator);
            return View(notice);
        }

        // GET: Notice/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Notice notice = db.Notice.Find(id);
            if (notice == null)
            {
                return HttpNotFound();
            }
            ViewBag.Creator = new SelectList(db.Teacher, "ID", "Name", notice.Creator);
            return View(notice);
        }

        // POST: Notice/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Title,Content,Attachment,CreateTime,Creator")] Notice notice,HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null && file.ContentLength != 0)
                {
                    notice.Attachment = "~/Upload/Notice/" + file.FileName;
                    file.SaveAs(Server.MapPath(notice.Attachment));//保存到服务器(上传该附件)
                }
                db.Entry(notice).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Creator = new SelectList(db.Teacher, "ID", "Name", notice.Creator);
            return View(notice);
        }

        // GET: Notice/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Notice notice = db.Notice.Find(id);
            if (notice == null)
            {
                return HttpNotFound();
            }
            return View(notice);
        }

        // POST: Notice/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Notice notice = db.Notice.Find(id);
            db.Notice.Remove(notice);
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
