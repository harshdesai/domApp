using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using bomSampleApp.Entity;
namespace bomSampleApp.BLL.Interface
{
    public interface ITaskRepository
    {
        List<Task> GetTasks();
        
        List<Task> GetTasksByUserID();

        int SaveTasks(Task task);

        List<SendVia> GetSendViaList();
    }
}
