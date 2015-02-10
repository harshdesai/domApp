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
using SampleApp.BLL.Common;
using System.IO;
using Newtonsoft.Json.Linq;
namespace SampleApp.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        private IPatientRepository _pateintRepository;
        private ICategoryRepository _categoryRepository;
        private IApplicationStatusRepository _applicationStatusRepository;
        private ITaskRepository _taskRepository;
        private IUserRepository _userRepository;
        public HomeController() : this(new PatientRepository(), new CategoryRepository(), new ApplicationStatusRepository(), new TaskRepository(), new UserRepository()) { }

        public HomeController(IPatientRepository pateintRepository, ICategoryRepository categoryRepository,
            IApplicationStatusRepository applicationsatusRepository, ITaskRepository taskRepository, IUserRepository userRepository)
        {
            this._pateintRepository = pateintRepository;
            this._categoryRepository = categoryRepository;
            this._applicationStatusRepository = applicationsatusRepository;
            this._taskRepository = taskRepository;
            this._userRepository = userRepository;
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
            if (id > 0)
            {
                patient = _pateintRepository.GetPatientByID(id);
            }
            if (patient == null)
            {
                patient = new Patient();
            }
            ViewBag.Category = new SelectList(_categoryRepository.GetCategory(), "CatagoryId", "CatagoryName", patient.CatagoryId);
            ViewBag.ApplicationStatus = new SelectList(_applicationStatusRepository.GetApplicationStatus(), "ApplicationStatusId", "ApplicationName", patient.ApplicationStatusID);
            return View(patient);
        }

        [HttpPost]
        public JsonResult Create(Patient patient, int id = 0)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Category = new SelectList(_categoryRepository.GetCategory(), "CatagoryId", "CatagoryName", patient.CatagoryId);
                ViewBag.ApplicationStatus = new SelectList(_applicationStatusRepository.GetApplicationStatus(), "ApplicationStatusId", "ApplicationName", patient.ApplicationStatusID);
                //_pateintRepository.SavePatients(patient);
                //string html = Helper.getRegistrationHtml(patient);

                TempData["Patient"] = patient;
                return Json(patient, JsonRequestBehavior.AllowGet);
                //new JsonResult(n ) { Data = Newtonsoft.Json.JsonConvert.SerializeObject(patient)};
            }
            return new JsonResult() { Data = "s" };
        }

        public ActionResult CreateTask(int id = 0)
        {
            Task task = null;
            if (id > 0)
            {
                //  task = _taskRepository.GetTasksByUserID(id);
            }
            if (task == null)
            {
                task = new Task();
            }
            ViewBag.Users = new SelectList(_userRepository.GetUsers(), "UserID", "FirstName", task.UserID);
            ViewBag.SendVia = new SelectList(_taskRepository.GetSendViaList(), "SendViaID", "SendViaName", task.SendViaID);
            return View(task);
        }

        [HttpPost]
        public ActionResult CreateTask(Task task)
        {
            Patient patient = TempData["Patient"] as Patient;
            if (task.TaskID == 0)
            {
                //task = _taskRepository.GetTasks();
            }
            if (task == null)
            {
                task = new Task();
            }
            int PateintID = _pateintRepository.SavePatients(patient);
            task.PatientID = PateintID;
            ViewBag.Users = new SelectList(_userRepository.GetUsers(), "UserID", "FirstName", task.UserID);
            ViewBag.SendVia = new SelectList(_taskRepository.GetSendViaList(), "SendViaID", "SendViaName", task.SendViaID);
            _taskRepository.SaveTasks(task);
            MemoryStream _contentStream = Helper.getPdf(patient);
            string fileName = Helper.getRegistrationFileName();
            Helper.sendMail("Application Complete", "Please find the attachment", patient.Email, _contentStream, fileName);
            return RedirectToAction("Create/" + PateintID);
        }

        [HttpGet]
        public JsonResult GetCategoryByPateintGroup()
        {
            string data = _pateintRepository.GetPatientJson();
            return Json(data ,JsonRequestBehavior.AllowGet);
        }
    }
}
