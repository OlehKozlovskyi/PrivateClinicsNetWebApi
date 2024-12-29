using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateClinicsWebNet.BusinessLogic.Entities
{
    public class Appointment
    {
        public Appointment(string externalId, Guid patientId, Guid doctorId, DateTime date, Patient patient, Doctor doctor)
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
        public Guid PatientId { get; set; }
        public Guid DoctorId { get;set; }
        public DateTime Date {  get; set; }
        public Patient Patient { get; set; }
        public Doctor Doctor { get; set; }
    }
}
