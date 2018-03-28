using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using AutoMapper;
using DashBoard.Attributes;
using DashBoard.BLL.Infrastructure;
using DashBoard.BLL.Interfaces;
using DashBoard.BLL.Services;
using DashBoard.Model.Models;
using DashBoard.Models;

namespace DashBoard.Controllers
{
    //[OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
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

        [HttpGet]
        public ActionResult GetBlSearch()
        {
            return PartialView("_blockSearch");
        }

        [HttpGet]
        public ActionResult LoadApplicants(int count)
        {
            // Используем Task.Run, а не await потому что PartialView не умеет работать с Async/Await
            var applicants = Task.Run(() => ApplicantService.GetApplicants()).Result;

            ViewBag.IdName = "list-applicant";
            ViewBag.SizeList = "18";

            if (count < 1)
                return PartialView("_PartialDefaultList", new BaseFormModel[]{});

            if (count > applicants.Count())
                count = applicants.Count();

            applicants = applicants.Skip(applicants.Count() - count);
            var result = Mapper.Map<IEnumerable<Applicant>, IEnumerable<BaseFormModel>>(applicants);

            return PartialView("_PartialDefaultList", result);
        }

        public ActionResult GetApplicantDetail(int id)
        {
            var applicant =  ApplicantService.GetApplicantById(id);
            if (applicant == null)
                return Json(new {model = "failed", modelList = new[] {"Детальної інформації не знайдено!"}});

            var result = Mapper.Map<Applicant, ApplicantFormModel>(applicant);

            return Json(new {model = "confirmed", applicant = result}, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAjax]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SaveApplicant(ApplicantFormModel applicant)
        {
            var data = Mapper.Map<ApplicantFormModel, Applicant>(applicant);
            OperationDetails result;

            // Create or Edit applicant
            if (data.ApplicantId == 0)
            {
                result = await ApplicantService.CreateApplicant(data);
                return result.Successed ?
                    Json(new { model = "confirmed", modelList = result.Message, applicantId = result.Property }, JsonRequestBehavior.AllowGet) :
                    Json(new { model = "failed", modelList = result.Message }, JsonRequestBehavior.AllowGet);
            }

            result = await ApplicantService.EditApplicant(data);

            return Json(new { model = result.Successed ? "confirmed" : "failed", modelList = result.Message, type="save" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteApplicant(BaseFormModel model)
        {
            var applicant = ApplicantService.GetApplicantById(int.Parse(model.Id));
            if(applicant == null)
                return Json(new { model = "failed", modelList = "Абітурієнта не знайдено!" }, JsonRequestBehavior.AllowGet);

            var result = await ApplicantService.DeleteApplicant(applicant);

            return Json(new { model = result.Successed ? "confirmed" : "failed", modelList = result.Message, type="remove" }, JsonRequestBehavior.AllowGet);
        }

        [ChildActionOnly]
        [OutputCache(Duration = 120, Location = OutputCacheLocation.Downstream)]
        public async Task<ActionResult> GetDepartments()
        {
            var list = await UStructService.GetAdmissionDepartments();
            var tmp = Mapper.Map<IEnumerable<Department>, IEnumerable<BaseFormModel>>(list);
            ViewBag.IdName = "listDepart";
            return PartialView("_PartialDefaultComboBox", tmp);
        }

        [HttpGet]
        public async Task<ActionResult> LoadTable()
        {
            var applicant = await ApplicantService.GetApplicants();
            var res = Mapper.Map<IEnumerable<Applicant>, IEnumerable<ApplicantFormModel>>(applicant);
            return View(res);
        }
    }
}