using Newtonsoft.Json.Linq;
using bomSampleApp.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bomSampleApp.BLL.Interface
{
    public interface IPatientRepository
    {
        int SavePatients(Patient patient);

        List<Patient> GetPatient();

        string GetPatientJson();
        
        string DeletePatient(int id);

        Patient GetPatientByID(int id);

        string GetPatientByCategory();
    }
}
