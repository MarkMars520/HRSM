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
    public class DictController : Controller
    {
        private HRSMDBEntities db = new HRSMDBEntities();
        // GET: Dict查询
        public ActionResult Index()
        {
            ViewData["dicts"]=db.Dict.ToList();//将字典数据存放在ViewData中
            return View();
        }
        //GET:详情查询
        public ActionResult Details(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Dict dict = db.Dict.Find(id);
            if (dict == null)
                return HttpNotFound();
            ViewBag.Dict = dict;//将查询的字典数据返回视图
            return View();
        }
        //GET:删除前详情查询
        public ActionResult Delete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Dict dict = db.Dict.Find(id);
            if (dict == null)
                return HttpNotFound();
            ViewBag.Dict = dict;//将查询的字典数据返回视图
            return View();
        }
        //POST:删除字典
        [HttpPost,ActionName("Delete")]
        public ActionResult DeleteConfirmed(int? id)
        {
            Dict dict = db.Dict.Find(id);//根据ID查询出要删除的数据
            db.Dict.Remove(dict);
            db.SaveChanges();//从数据库中删除该条数据
            return RedirectToAction("Index");//跳转至查询列表页面
        }
        //GET:新增字典
        public ActionResult Create()
        {
            return View();
        }
        //POST:新增字典
        [HttpPost]
        public ActionResult Create(Dict dict)
        {
            if (ModelState.IsValid)
            {
                db.Dict.Add(dict);
                db.SaveChanges();
                return RedirectToAction("Index");//新增成功，跳转到查询页面
            }
            ViewBag.Dict = dict;
            return View();
        }
        //GET:编辑字典
        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Dict dict = db.Dict.Find(id);
            if (dict == null)
                return HttpNotFound();
            ViewBag.Dict = dict;//将查询的字典数据返回视图
            return View();
        }
        //POST:编辑字典
        [HttpPost]
        public ActionResult Edit(Dict dict)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dict).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");//编辑成功，跳转到查询页面
            }
            ViewBag.Dict = dict;
            return View();
        }
    }
}