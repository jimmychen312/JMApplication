using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using JMWebApplication.Filters;
 

namespace JMWebApplication.Controllers
{
    public class SeedDataController : Controller
    {
        
        /// <summary>
        /// Seed Data
        /// </summary>
        /// <returns></returns>
       // [AuthenicationAction]
        public ActionResult SeedData()
        {         
            return View("SeedData");
        }
	}
}