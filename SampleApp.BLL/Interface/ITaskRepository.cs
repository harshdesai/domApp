using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SampleApp.Entity;
namespace SampleApp.BLL.Interface
{
    public interface ITaskRepository
    {
        List<Task> GetTasks();
        
        List<Task> GetTasksByUserID();

        int SaveTasks(Task task);

        List<SendVia> GetSendViaList();
    }
}
