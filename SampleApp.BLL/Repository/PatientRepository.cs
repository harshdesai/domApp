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

        public int SavePatients(Patient patientFrom)
        {
            Patient patientFromDB = new Patient();
            if (patientFrom.PatientID > 0)
            {
                patientFromDB = _entity.Patients.Find(patientFrom.PatientID);
            }

            patientFromDB.FirstName = patientFrom.FirstName;
            patientFromDB.LastName = patientFrom.LastName;
            patientFromDB.WifeFirstName = patientFrom.WifeFirstName;
            patientFromDB.MaidenName = patientFrom.MaidenName;
            patientFromDB.PhoneNumber = patientFrom.PhoneNumber;
            patientFromDB.Email = patientFrom.Email;
            patientFromDB.CatagoryId = patientFrom.CatagoryId;
            patientFromDB.ApplicationStatusID = patientFrom.ApplicationStatusID;
            patientFromDB.Active = true;//patientFromDB.Active;
            patientFromDB.Denied = false;//patientFromDB.Denied;
            if (patientFrom.PatientID == 0)
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

        public Patient GetPatientByID(int id)
        {
            Patient patient = new Patient();
            patient = _entity.Patients.Find(id);
            return patient;
        }
    }
}