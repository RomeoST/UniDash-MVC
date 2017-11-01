using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DashBoard.BLL.Interfaces;

namespace DashBoard.Controllers
{
    public class SubmissionController : Controller
    {
        //TODO: Service in development
        //private ISubmissionService SubmissionService;

        //public SubmissionController(ISubmissionService submissionService) => SubmissionService = submissionService;
        // GET: ApplicantDoc
        public ActionResult Index()
        {
            return View();
        }
    }
}