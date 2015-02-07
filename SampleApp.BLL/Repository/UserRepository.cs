using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SampleApp.BLL.Interface;
using SampleApp.Entity;
namespace SampleApp.BLL.Repository
{
    public class UserRepository : IUserRepository
    {
        SampleAppEntities _entity = null;

        public UserRepository()
        {
            _entity = new SampleAppEntities();
        }

        public int SaveUser(User userFromDB)
        {
            if (userFromDB.UserID > 0)
            {
                userFromDB = _entity.Users.Find(userFromDB.UserID);
            }
            userFromDB.FirstName = userFromDB.FirstName;
            userFromDB.LastName = userFromDB.LastName;
            userFromDB.Phone = userFromDB.Phone;
            userFromDB.Email = userFromDB.Email;

            if (userFromDB.UserID == 0)
            {
                _entity.Users.Add(userFromDB);
            }
            _entity.SaveChanges();
            return userFromDB.UserID;
        }


        public List<User> GetUsers()
        {
            List<User> list = _entity.Users.ToList();
            return list;
        }

        public string DeleteUser(int userID)
        {
            User user = _entity.Users.Find(userID);
            _entity.Users.Remove(user);
            _entity.SaveChanges();
            return "Delete Successfully!!";
        }
    }
}
