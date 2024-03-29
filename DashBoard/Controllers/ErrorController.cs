﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DashBoard.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult NotFound()
        {
            Response.StatusCode = 404;
            return View();
        }

        public ActionResult TimeOut()
        {
            Response.StatusCode = 504;
            return View();
        }

        public ActionResult InternalError()
        {
            Response.StatusCode = 500;
            return View();
        }
    }
}