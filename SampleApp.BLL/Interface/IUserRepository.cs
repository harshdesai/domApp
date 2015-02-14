using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bomSampleApp.Entity;
namespace bomSampleApp.BLL.Interface
{
    public interface IUserRepository
    {
        List<User> GetUsers();

        string DeleteUser(int id);

        int SaveUser(User user);

        User GetUserByID(int id);
    }
}
