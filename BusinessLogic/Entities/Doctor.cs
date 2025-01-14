using Microsoft.AspNetCore.Identity;
using PrivateClinicsWebNet.BusinessLogic.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PrivateClinicsWebNet.BusinessLogic.Entities
{
    public class Doctor : IdentityUser, IHasAppointments
    {
        public string DoctorType { get; set; }
        public List<Appointment> Appointments { get; set; }
    }
}
