using MVCCURD.Dbcontent;
using MVCCURD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCCURD.Controllers
{
    public class AddEmpController : Controller
    {
        // GET: AddEmp
        [HttpGet]
        public ActionResult AddEmp()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddEmp(empmodel obj)
        {
            RcdbEntities1 db = new RcdbEntities1();
            empdetail tb = new empdetail();
            tb.id = obj.id;
            tb.Name = obj.Name;
            tb.Course = obj.Course;
            tb.gender = obj.gender;
            tb.Mobile = obj.Mobile;
            tb.email = obj.email;
            if (obj.id == 0)
            {
                db.empdetails.Add(tb);
                db.SaveChanges();

            }
            else
            {
                db.Entry(tb).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }

            return RedirectToAction("Indexdasboard", "Home");
        }

        public ActionResult Edit(int id)
        {
            RcdbEntities1 dbobj = new RcdbEntities1();
            empmodel mod = new empmodel();
            var ditem = dbobj.empdetails.Where(m => m.id == id).First();

            mod.id = ditem.id;
            mod.Name = ditem.Name;
            mod.Course = ditem.Course;
            mod.gender = ditem.gender;
            mod.Mobile = ditem.Mobile;
            mod.email = ditem.Mobile;

            return View("AddEmp", mod);

        }

    }
}