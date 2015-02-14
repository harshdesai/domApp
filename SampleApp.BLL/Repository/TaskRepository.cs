using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bomSampleApp.BLL.Interface;
using bomSampleApp.Entity;
namespace bomSampleApp.BLL.Repository
{
    public class TaskRepository : ITaskRepository
    {
        SampleAppEntities _entity = null;

        public TaskRepository()
        {
            _entity = new SampleAppEntities();
        }
        public List<bomSampleApp.Entity.Task> GetTasks()
        {
            throw new NotImplementedException();
        }

        public List<bomSampleApp.Entity.Task> GetTasksByUserID()
        {
            throw new NotImplementedException();
        }

        public int SaveTasks(bomSampleApp.Entity.Task task)
        {
            bomSampleApp.Entity.Task taskFromDB = new bomSampleApp.Entity.Task();
            if (task.TaskID > 0)
            {
                taskFromDB = _entity.Tasks.Find(task.TaskID);
            }

            taskFromDB.Type = task.Type;
            taskFromDB.Date = DateTime.Now;
            taskFromDB.FollowupDate = task.FollowupDate;
            taskFromDB.SendViaID = task.SendViaID;
            taskFromDB.UserID = task.UserID;
            taskFromDB.PatientID = task.PatientID;
            if (task.TaskID == 0)
            {

                _entity.Tasks.Add(taskFromDB);
            }
            _entity.SaveChanges();
            return taskFromDB.TaskID;
        }

        public List<SendVia> GetSendViaList()
        {
            List<SendVia> list = _entity.SendVias.ToList();
            return list;
        }


    }
}
