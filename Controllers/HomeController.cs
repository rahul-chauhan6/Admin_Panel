using MVCCURD.Dbcontent;
using MVCCURD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;


namespace MVCCURD.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.Message = "Your index page.";

            return View();
        }



        [HttpPost]
        public ActionResult Index(usermodel obj)
        {
            RcdbEntities1 db = new RcdbEntities1();
           user tb = new user();
            tb.id = obj.id;
            tb.Name = obj.Name;
            tb.Email = obj.Email;
            tb.Password = obj.Password;
            db.users.Add(tb);
            db.SaveChanges();
           
           
            return RedirectToAction("login");
        }
     [Authorize]
        public ActionResult Emptable()
        {
            RcdbEntities1 db = new RcdbEntities1();
            List<empmodel> emp = new List<empmodel>();
            var res = db.empdetails.ToList();
            foreach (var item in res)
            {
                emp.Add(new empmodel
                {
                    id = item.id,
                    Name=item.Name,
                    Course=item.Course,
                    gender=item.gender,
                    Mobile=item.Mobile,
                    email=item.email
                }) ;
            }
            
           
            return View(emp);
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
        [HttpGet]
        public ActionResult login()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        [HttpPost]
        public ActionResult login(user res)
        {
            RcdbEntities1 obj = new RcdbEntities1();
            var UserRes = obj.users.Where(a => a.Email == res.Email).FirstOrDefault();

            if (UserRes == null)
            {
                TempData["Invalid"] = "Email not found or Invalid Username";
            }
            else
            {
                if (UserRes.Email == res.Email && UserRes.Password == res.Password)
                {
                    FormsAuthentication.SetAuthCookie(UserRes.Email, false);

                    Session["username"] = UserRes.Name;
                    Session["useremail"] = UserRes.Email;
                    return RedirectToAction("indexdasboard", "Home");
                }
                else
                {
                    TempData["Wrong"] ="Wrong Password Please Enter Valid Password";
                    return View();
                }
            }
            return View("login");

        }

        public ActionResult Logout()
        {
         

                Session["UserInfo"] = null;
                Session.Abandon();
                return RedirectToAction("login", "Home");
            


           
        }
        [Authorize]
        public ActionResult indexdasboard()
        {
            ViewBag.Message = "Your dasboard  page.";

            return View();
        }

       
        [Authorize]
        public ActionResult Delete(int id)
        {
            RcdbEntities1 dbobj = new RcdbEntities1();
            var ditem = dbobj.empdetails.Where(m => m.id == id).First();
            dbobj.empdetails.Remove(ditem);
            dbobj.SaveChanges();

            return RedirectToAction("Emptable");

        }



    }
}