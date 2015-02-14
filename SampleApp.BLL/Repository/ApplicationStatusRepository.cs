using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bomSampleApp.BLL.Interface;
using bomSampleApp.Entity;
namespace bomSampleApp.BLL.Repository
{
    public class ApplicationStatusRepository : IApplicationStatusRepository
    {
        SampleAppEntities _entity = null;
        public ApplicationStatusRepository() {
            _entity = new SampleAppEntities();
        }

        public List<ApplicationStatu> GetApplicationStatus()
        {
            List<ApplicationStatu> list = _entity.ApplicationStatus.ToList();
            return list;
        }
    }
}
