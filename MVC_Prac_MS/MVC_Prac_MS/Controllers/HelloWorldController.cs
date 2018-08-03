using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace MVC_Prac_MS.Controllers
{
    public class HelloWorldController : Controller
    {
        // GET: HelloWorld
        //public string Index()
        //{
        //    return "This is my default action";
        //}

        //GET: /HelloWorld/Welcome/
        //public string Welcome()
        //{
        //    return "This is the Welcome action method...";
        //}

        //public string Welcome(string name,int numTimes = 1)
        //{
        //    return $"Hello {name}, NumTimes is: {numTimes}";
        //}

        public string Welcome(string name, int ID = 1)
        {
            return $"Hello {name}, ID: {ID}";
        }

        public ActionResult Index(string id)
        {
            ViewBag.abc= string.Concat("Your id is ",id);
            return View();
        }
    }
}