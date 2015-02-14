using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bomSampleApp.Entity;
namespace bomSampleApp.BLL.Interface
{
    public interface IApplicationStatusRepository
    {
        List<ApplicationStatu> GetApplicationStatus();
    }
}
