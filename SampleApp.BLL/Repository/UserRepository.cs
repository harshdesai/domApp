using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bomSampleApp.BLL.Interface;
using bomSampleApp.Entity;
namespace bomSampleApp.BLL.Repository
{
    public class UserRepository : IUserRepository
    {
        SampleAppEntities _entity = null;

        public UserRepository()
        {
            _entity = new SampleAppEntities();
        }

        public int SaveUser(User userFrom)
        {
            User userFromDB = new User();
            if (userFrom.UserID > 0)
            {
                userFromDB = _entity.Users.Find(userFrom.UserID);
            }
            userFromDB.FirstName = userFrom.FirstName;
            userFromDB.LastName = userFrom.LastName;
            userFromDB.Phone = userFrom.Phone;
            userFromDB.Email = userFrom.Email;

            if (userFrom.UserID == 0)
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

        public User GetUserByID(int id)
        {
            User user = new User();
            user = _entity.Users.Find(id);
            return user;
        }
    }
}
