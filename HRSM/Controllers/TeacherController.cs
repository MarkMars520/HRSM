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
    public class TeacherController : Controller
    {
     private HRSMDBEntities db = new HRSMDBEntities();
        // GET: 查询
        public ActionResult Index()
        {
            ViewData["teachers"] = db.Teacher.ToList();//将数据存放在ViewData中
            return View();
        }
        //GET:详情查询
        public ActionResult Details(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Teacher teacher= db.Teacher.Find(id);
            if (teacher == null)
                return HttpNotFound();
            ViewBag.Teacher = teacher;//将查询的数据返回视图
            return View();
        }
        //GET:删除前详情查询
        public ActionResult Delete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Teacher teacher = db.Teacher.Find(id);
            if (teacher == null)
                return HttpNotFound();
            ViewBag.Teacher = teacher;//将查询的数据返回视图
            return View();
        }
        //POST:删除
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int? id)
        {
            Teacher teacher = db.Teacher.Find(id);//根据ID查询出要删除的数据
            db.Teacher.Remove(teacher);
            db.SaveChanges();//从数据库中删除该条数据
            return RedirectToAction("Index");//跳转至查询列表页面
        }
        //GET:新增
        public ActionResult Create()
        {
            return View();
        }
        //POST:新增
        [HttpPost]
        public ActionResult Create(Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                db.Teacher.Add(teacher);
                db.SaveChanges();
                return RedirectToAction("Index");//新增成功，跳转到查询页面
            }
            ViewBag.Teacher = teacher;
            return View();
        }
        //GET:编辑
        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Teacher teacher = db.Teacher.Find(id);
            if (teacher == null)
                return HttpNotFound();
            ViewBag.Teacher = teacher;//将查询的数据返回视图
            return View();
        }
        //POST:编辑
        [HttpPost]
        public ActionResult Edit(Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                db.Entry(teacher).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");//编辑成功，跳转到查询页面
            }
            ViewBag.Teacher = teacher;//将查询的数据返回视图
            return View();
        }
        //JQuery-validate的remote远程验证用户名是否已存在
        [HttpPost]
        public string UserHased(string LoginName)
        {
            var rs = db.Teacher.Where(t => t.LoginName == LoginName).ToList();
            if (rs != null && rs.Count > 0)
                return "false";//登录名已被使用，不能通过验证
            return "true";//登录名可用，通过验证
        }
        //JQuery-validate的remote远程验证用户名是否已存在
        [HttpPost]
        public string UserHasedForEdit(string LoginName,string OldLoginName)
        {
            if (LoginName == OldLoginName)
                return "true";//说明编辑时，并未修改该登录用户名，验证通过
            var rs = db.Teacher.Where(t => t.LoginName == LoginName).ToList();
            if (rs != null && rs.Count > 0)
                return "false";//登录名已被使用，不能通过验证
            return "true";//登录名可用，通过验证
        }
    }
}