using PrivateClinicsWebNet.BusinessLogic.Entities;
using PrivateClinicsWebNet.Infrastructure.AppointmentInfrastructure.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PrivateClinicsWebNet.Infrastructure.AppointmentInfrastructure.Specifications
{
    public class AppointmentFilterSpecification : IAppointmentSpecification
    {
        private readonly string _doctorId;
        private readonly string _patientId;

        public AppointmentFilterSpecification(string doctorId, string patientId)
        {
            _doctorId = doctorId;
            _patientId = patientId;
        } 

        public Expression<Func<Appointment,bool>> IsSatisfiedBy()
        {
            return a => (_doctorId==null || a.DoctorId == _doctorId) && 
                (_patientId == null || a.PatientId == _patientId); 
            
        }
    }
}
