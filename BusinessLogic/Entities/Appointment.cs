using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateClinicsWebNet.BusinessLogic.Entities
{
    public class Appointment
    {
        public Appointment() { }

        public Appointment(string externalId, string patientId, string doctorId, DateTime date, Patient patient, Doctor doctor)
        {
            Id = Guid.NewGuid();
            ExternalId = externalId;
            PatientId = patientId;
            DoctorId = doctorId;
            Date = date;
            Patient = patient;
            Doctor = doctor;
        }

        public Guid Id { get; set; }
        public string ExternalId { get; set; }
        public string PatientId { get; set; }
        public string DoctorId { get;set; }
        public DateTime Date {  get; set; }
        public Patient Patient { get; set; }
        public Doctor Doctor { get; set; }
    }
}
