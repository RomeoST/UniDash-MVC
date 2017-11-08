using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using AutoMapper;
using DashBoard.BLL.Interfaces;
using DashBoard.BLL.Services;
using DashBoard.Model.Models;
using DashBoard.Models;

namespace DashBoard.Controllers
{
    public class ApplicantController : Controller
    {
        private IApplicantService ApplicantService;
        private IUStructService UStructService;

        public ApplicantController(IApplicantService applicantService, IUStructService uStructService)
        {
            ApplicantService = applicantService;
            UStructService = uStructService;
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
            var list = UStructService.FindDepartments(p => p.isAdmission == true).ToList();
            var tmp = Mapper.Map<IEnumerable<Department>, IEnumerable<BaseFormModel>>(list);
            ViewBag.IdName = "listDepart";
            return PartialView("_PartialDefaultComboBox", tmp);
        } 
    }
}