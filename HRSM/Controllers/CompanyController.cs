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
    public class CompanyController : Controller
    {
        private HRSMDBEntities db = new HRSMDBEntities();

        // GET: Company
        public ActionResult Index()
        {
            var company = db.Company.Include(c => c.Teacher);
            return View(company.ToList());
        }

        // GET: Company/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = db.Company.Find(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        // GET: Company/Create
        public ActionResult Create()
        {
            ViewBag.InterTypes = new SelectList(db.Dict.Where(d => d.TypeName == "面试形式"), "Value", "Name");
            ViewBag.Status = new SelectList(db.Dict.Where(d => d.TypeName == "招聘状态"), "Value", "Name");
            ViewBag.Creator = new SelectList(db.Teacher, "ID", "Name");
            return View();
        }

        // POST: Company/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Title,City,URL,Desc,InterTime,StopTime,InterType,Statu,Creator")] Company company)
        {
            if (ModelState.IsValid)
            {
                db.Company.Add(company);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.InterTypes = new SelectList(db.Dict.Where(d => d.TypeName == "面试形式"), "Value", "Name");
            ViewBag.Status = new SelectList(db.Dict.Where(d => d.TypeName == "招聘状态"), "Value", "Name");
            ViewBag.Creator = new SelectList(db.Teacher, "ID", "Name", company.Creator);
            return View(company);
        }

        // GET: Company/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = db.Company.Find(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            ViewBag.InterTypes = new SelectList(db.Dict.Where(d => d.TypeName == "面试形式"), "Value", "Name");
            ViewBag.Status = new SelectList(db.Dict.Where(d => d.TypeName == "招聘状态"), "Value", "Name");
            ViewBag.Creator = new SelectList(db.Teacher, "ID", "Name", company.Creator);
            return View(company);
        }

        // POST: Company/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Title,City,URL,Desc,InterTime,StopTime,InterType,Statu,Creator")] Company company)
        {
            if (ModelState.IsValid)
            {
                db.Entry(company).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.InterTypes = new SelectList(db.Dict.Where(d => d.TypeName == "面试形式"), "Value", "Name");
            ViewBag.Status = new SelectList(db.Dict.Where(d => d.TypeName == "招聘状态"), "Value", "Name");
            ViewBag.Creator = new SelectList(db.Teacher, "ID", "Name", company.Creator);
            return View(company);
        }

        // GET: Company/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = db.Company.Find(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        // POST: Company/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Company company = db.Company.Find(id);
            db.Company.Remove(company);
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
        public ActionResult Test() {
            return View();
        }
        //按条件查询
        public ActionResult Search(string titleS)
        {
            using (HRSMDBEntities db = new HRSMDBEntities())
            {
                var cs = db.Company.Where(c => c.Title.Contains(titleS)).Include("Teacher").ToList();
                return PartialView("List",cs);//返回分布视图
            }
        }
    }
}
