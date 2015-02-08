using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SampleApp.BLL.Interface;
using SampleApp.BLL.Repository;
using SampleApp.Entity;
using System.Collections;
namespace SampleApp.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /User/
        private IUserRepository _userRepository;
        public UserController() : this(new UserRepository()) { }

        public UserController(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }

        public ActionResult Index()
        {
            return View(_userRepository.GetUsers());
        }

        public ActionResult Delete(int id = 0)
        {
            _userRepository.DeleteUser(id);
            return RedirectToAction("Index");
        }

        // GET: /Actor/Create
        public ActionResult Create(int id = 0)
        {
            User user = null;
            if (id > 0)
            {
                user = _userRepository.GetUserByID(id);
            }
            if (user == null)
            {
                user = new User();
            }
            return View(user);
        }

        [HttpPost]
        public ActionResult Create(User user, int id = 0)
        {
            if (ModelState.IsValid)
            {
                _userRepository.SaveUser(user);
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
