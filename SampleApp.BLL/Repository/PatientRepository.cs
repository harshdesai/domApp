using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SampleApp.BLL.Interface;
using SampleApp.Entity;
using Newtonsoft.Json.Linq;
namespace SampleApp.BLL.Repository
{
    public class PatientRepository : IPatientRepository
    {
        SampleAppEntities _entity = null;
        public PatientRepository()
        {
            _entity = new SampleAppEntities();
        }

        public int SavePatients(Patient patientFromDB)
        {
            if (patientFromDB.PatientID > 0)
            {
                patientFromDB = _entity.Patients.Find(patientFromDB.PatientID);
            }
            patientFromDB.FirstName = patientFromDB.FirstName;
            patientFromDB.LastName = patientFromDB.LastName;
            patientFromDB.WifeFirstName = patientFromDB.WifeFirstName;
            patientFromDB.MaidenName = patientFromDB.MaidenName;
            patientFromDB.PhoneNumber = patientFromDB.PhoneNumber;
            patientFromDB.Email = patientFromDB.Email;
            patientFromDB.CatagoryId = patientFromDB.CatagoryId;
            patientFromDB.ApplicationStatusID = patientFromDB.ApplicationStatusID;
            patientFromDB.Active = true;//patientFromDB.Active;
            patientFromDB.Denied = false;//patientFromDB.Denied;
            if (patientFromDB.PatientID == 0)
            {
                _entity.Patients.Add(patientFromDB);
            }
            _entity.SaveChanges();
            return patientFromDB.PatientID;
        }

        public List<Patient> GetPatient()
        {
            List<Patient> list = new List<Patient>();
            list = _entity.Patients.ToList();
            return list;
        }

        public string DeletePatient(int pateintID) {
            Patient patient = _entity.Patients.Find(pateintID);
            _entity.Patients.Remove(patient);
            _entity.SaveChanges();
            return "Delete Successfully!!";
        }
    }
}