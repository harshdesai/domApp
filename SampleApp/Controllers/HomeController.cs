using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SampleApp.BLL;
using SampleApp.BLL.Interface;
using SampleApp.BLL.Repository;
using SampleApp.Entity;
namespace SampleApp.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        private IPatientRepository _pateintRepository;
        
        public HomeController() : this(new PatientRepository()) { }

        public HomeController(IPatientRepository pateintRepository)
        {
            this._pateintRepository = pateintRepository;
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
            //Actor actor = null;
            //if (id > 0)
            //    actor = db.Actors.Find(id);
            //if (actor == null)
            //{
            //    actor = new Actor();
            //}
            return View();
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
