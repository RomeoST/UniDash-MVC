using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DashBoard.BLL.Interfaces;
using Ninject;

namespace DashBoard.Controllers
{
    public class SubmissionController : Controller
    {
        //TODO: Service in development
       [Inject] public ISubmissionService SubmissionService { get; set; }

        public SubmissionController(ISubmissionService submissionService) => SubmissionService = submissionService;
        // GET: ApplicantDoc
        public ActionResult Index()
        {
            return View();
        }
    }
}