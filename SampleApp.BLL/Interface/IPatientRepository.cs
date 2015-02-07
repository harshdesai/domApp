using Newtonsoft.Json.Linq;
using SampleApp.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleApp.BLL.Interface
{
    public interface IPatientRepository
    {
        int SavePatients(Patient patient);

        List<Patient> GetPatient();
        
        string DeletePatient(int id);

    }
}
