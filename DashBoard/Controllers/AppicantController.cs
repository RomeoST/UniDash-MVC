using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using DashBoard.BLL.Interfaces;
using DashBoard.BLL.Services;

namespace DashBoard.Controllers
{
    public class ApplicantController : Controller
    {
        private IApplicantService ApplicantService;

        public ApplicantController(IApplicantService applicantService)
        {
            ApplicantService = applicantService;
        }

        // GET: Appicant
        public ActionResult Index()
        {
            return View();
        }

        [ChildActionOnly]
        [OutputCache(Duration = 120, Location = OutputCacheLocation.Downstream)]
        public ActionResult GetDepartments()
        {
            return PartialView("_PartialDefaultComboBox");
        } 
    }
}