using HRSM.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace HRSM.Controllers
{
    public class GradeController : Controller
    {
        private HRSMDBEntities db = new HRSMDBEntities();
        // GET: Grade查询
        public ActionResult Index()
        {
            ViewData["grades"] = db.Grade.ToList();//将专业班数据存放在ViewData中
            return View();
        }
        //GET:详情查询
        public ActionResult Details(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Grade grade = db.Grade.Find(id);
            if (grade == null)
                return HttpNotFound();
            ViewBag.Grade = grade;//将查询的数据返回视图
            return View();
        }
        //GET:删除前详情查询
        public ActionResult Delete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Grade grade = db.Grade.Find(id);
            if (grade == null)
                return HttpNotFound();
            ViewBag.Grade = grade;//将查询的数据返回视图
            return View();
        }
        //POST:删除字典
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int? id)
        {
            Grade grade = db.Grade.Find(id);//根据ID查询出要删除的数据
            db.Grade.Remove(grade);
            db.SaveChanges();//从数据库中删除该条数据
            return RedirectToAction("Index");//跳转至查询列表页面
        }
        //POST:新增专业班
        public ActionResult Create()
        {
            ViewBag.Professions = new SelectList(db.Dict.Where(d => d.TypeName == "专业"), "Value", "Name");
            ViewBag.MProfessions = new SelectList(db.Dict.Where(d => d.TypeName == "MHYS共建专业"), "Value", "Name");
            ViewBag.Counsellors = new SelectList(db.Teacher.Where(t => t.Roles == "辅导员"), "ID", "Name");//辅导员列表
            ViewBag.Lecturers = new SelectList(db.Teacher.Where(t => t.Roles == "技术顾问"), "ID", "Name");//技术顾问列表
            return View();
        }
        //POST:新增专业班
        [HttpPost]
        public ActionResult Create(Grade grade)
        {
            if (ModelState.IsValid)
            {
                db.Grade.Add(grade);
                db.SaveChanges();
                return RedirectToAction("Index");//新增成功，跳转到查询页面
            }
            ViewBag.Grade = grade;
            ViewBag.Professions = new SelectList(db.Dict.Where(d => d.TypeName == "专业"), "Value", "Name");
            ViewBag.MProfessions = new SelectList(db.Dict.Where(d => d.TypeName == "MHYS共建专业"), "Value", "Name");
            ViewBag.Counsellors = new SelectList(db.Teacher.Where(t => t.Roles == "辅导员"), "ID", "Name");//辅导员列表
            ViewBag.Lecturers = new SelectList(db.Teacher.Where(t => t.Roles == "技术顾问"), "ID", "Name");//技术顾问列表
            return View();
        }
        //GET:编辑专业班
        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Grade grade = db.Grade.Find(id);
            if (grade == null)
                return HttpNotFound();
            ViewBag.Grade = grade;//将查询的专业班数据返回视图
            ViewBag.Professions = new SelectList(db.Dict.Where(d => d.TypeName == "专业"), "Value", "Name", grade.Profession);
            ViewBag.MProfessions = new SelectList(db.Dict.Where(d => d.TypeName == "MHYS共建专业"), "Value", "Name", grade.MProfession);
            ViewBag.Counsellors = new SelectList(db.Teacher.Where(t => t.Roles == "辅导员"), "ID", "Name", grade.Counsellor);//辅导员列表
            ViewBag.Lecturers = new SelectList(db.Teacher.Where(t => t.Roles == "技术顾问"), "ID", "Name", grade.Lecturer);//技术顾问列表
            return View();
        }
        //POST:编辑专业班
        [HttpPost]
        public ActionResult Edit(Grade grade)
        {
            if (ModelState.IsValid)
            {
                db.Entry(grade).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");//编辑成功，跳转到查询页面
            }
            ViewBag.Grade = grade;
            ViewBag.Professions = new SelectList(db.Dict.Where(d => d.TypeName == "专业"), "Value", "Name", grade.Profession);
            ViewBag.MProfessions = new SelectList(db.Dict.Where(d => d.TypeName == "MHYS共建专业"), "Value", "Name", grade.MProfession);
            ViewBag.Counsellors = new SelectList(db.Teacher.Where(t => t.Roles == "辅导员"), "ID", "Name", grade.Counsellor);//辅导员列表
            ViewBag.Lecturers = new SelectList(db.Teacher.Where(t => t.Roles == "技术顾问"), "ID", "Name", grade.Lecturer);//技术顾问列表
            return View();
        }
    }
}