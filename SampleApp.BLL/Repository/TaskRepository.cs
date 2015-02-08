using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SampleApp.BLL.Interface;
using SampleApp.Entity;
namespace SampleApp.BLL.Repository
{
    public class TaskRepository : ITaskRepository
    {
        SampleAppEntities _entity = null;

        public TaskRepository()
        {
            _entity = new SampleAppEntities();
        }
        public List<SampleApp.Entity.Task> GetTasks()
        {
            throw new NotImplementedException();
        }

        public List<SampleApp.Entity.Task> GetTasksByUserID()
        {
            throw new NotImplementedException();
        }

        public int SaveTasks(SampleApp.Entity.Task task)
        {
            throw new NotImplementedException();
        }

        public List<SendVia> GetSendViaList() {
            List<SendVia> list = _entity.SendVias.ToList();
            return list;
        }

       
    }
}
