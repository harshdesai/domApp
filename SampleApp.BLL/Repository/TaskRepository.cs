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
            SampleApp.Entity.Task taskFromDB = new SampleApp.Entity.Task();
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
