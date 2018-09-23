using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SITQnAAPIServiceADFS.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return Content("SIT QnA Web API Running");
        }
    }
}
