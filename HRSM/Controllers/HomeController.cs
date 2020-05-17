using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using HRSM.Models;//引入实体模型相关类命名空间
namespace HRSM.Controllers
{
    public class HomeController : Controller
    {
        //管理员/顾问首页
       [Authorize]//只有登录验证后才可访问
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        //登录页
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        //候选人首页
        [Authorize]//只有登录验证后才可访问
        public ActionResult StuIndex()
        {
            return View();
        }
        //登录验证
        [AllowAnonymous]//允许匿名访问（即不登录验证也可访问）
        [HttpPost]
        public ActionResult Login(string urole)
        {
            using (HRSMDBEntities db = new Models.HRSMDBEntities())
            {
                string uname=Request["uname"];//传入文本框name名
                string upwd = Request["upwd"];//传入文本框name名
                if (urole == "0")//老师
                {
                    Teacher teacher = db.Teacher.FirstOrDefault(t => t.LoginName == uname && t.LoginPwd == upwd);
                    if (teacher != null)
                    {
                        Session["teacher"] = teacher;//存储登录顾问信息
                        Session["role"] = "teacher";//存储角色
                        FormsAuthentication.SetAuthCookie(teacher.Name, false);//添加通行证：说明通过了认证
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["msg"] = "老师帐号或密码错误!";
                        ModelState.AddModelError("", "老师帐号或密码错误!");
                        return View();
                    }
                }
                else
                {
                    //学生
                    Student student = db.Student.FirstOrDefault(t => t.LoginName == uname && t.LoginPwd == upwd);
                    if (student != null)//成功
                    {
                        Session["student"] = student;//存储登录候选人信息
                        Session["role"] = "student";//存储角色
                        FormsAuthentication.SetAuthCookie(student.Name, false);//添加通行证：说明通过了认证
                        return RedirectToAction("StuIndex");
                    }
                    else//失败
                    {
                        TempData["msg"] = "学生帐号或密码错误!";
                        ModelState.AddModelError("", "学生帐号或密码错误!");
                        return View();
                    }
                }
            }
        }

        //管理员/顾问退出
        [Authorize]
        public ActionResult TLogout()
        {
            Session["teacher"] = null;//清空session
            FormsAuthentication.SignOut(); //撤销通行证
            return RedirectToAction("Login");
        }
        //候选人退出
        [Authorize]
        public ActionResult SLogout()
        {
            Session["student"] = null;//清空session
            FormsAuthentication.SignOut();//撤销通行证
            return RedirectToAction("Login"); ;
        }
		 //下载
        public ActionResult Down(string file)
        {
            var path = Server.MapPath(file);//获取下载文件路径
            var name = System.IO.Path.GetFileName(path);//获取下载文件名
            return File(path, "application/x-zip-compressed", name);//下载文件
        }
    }
}