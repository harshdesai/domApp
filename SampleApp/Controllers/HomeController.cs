using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SampleApp.BLL;
using SampleApp.BLL.Interface;
using SampleApp.BLL.Repository;
using SampleApp.Entity;
using System.Collections;
namespace SampleApp.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        private IPatientRepository _pateintRepository;
        private ICategoryRepository _categoryRepository;
        private IApplicationStatusRepository _applicationStatusRepository;
        public HomeController() : this(new PatientRepository(), new CategoryRepository(),new ApplicationStatusRepository()) { }

        public HomeController(IPatientRepository pateintRepository, ICategoryRepository categoryRepository, IApplicationStatusRepository applicationsatusRepository)
        {
            this._pateintRepository = pateintRepository;
            this._categoryRepository = categoryRepository;
            this._applicationStatusRepository = applicationsatusRepository;
        }

        public ActionResult Index()
        {
            return View(_pateintRepository.GetPatient());
        }

        public ActionResult Delete(int id = 0)
        {
            _pateintRepository.DeletePatient(id);
            return RedirectToAction("Index");
        }

        // GET: /Actor/Create
        public ActionResult Create(int id = 0)
        {
            Patient patient = null;
            if (id > 0) { }
            // patient = db.Actors.Find(id);
            if (patient == null)
            {
                patient = new Patient();
            }
            ViewBag.Category = new SelectList(_categoryRepository.GetCategory(), "CatagoryId", "CatagoryName", new  { @class="select2-me input-xlarge" });
            ViewBag.ApplicationStatus = new SelectList(_applicationStatusRepository.GetApplicationStatus(), "ApplicationStatusId", "ApplicationName", new { @class = "select2-me input-xlarge" });
            return View(patient);
        }

        [HttpPost]
        public ActionResult Create(Patient patient, int id = 0)
        {
            if (ModelState.IsValid)
            {
                if (id > 0)
                {
                    _pateintRepository.SavePatients(patient);
                }
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
