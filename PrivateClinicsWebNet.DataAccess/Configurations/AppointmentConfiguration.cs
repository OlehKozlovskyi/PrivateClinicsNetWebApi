using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Options;
using PrivateClinicsWebNet.BusinessLogic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateClinicsWebNet.DataAccess.Configurations
{
    public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder.ToTable("Appointment").Property(p => p.ExternalId).IsRequired();
            builder.HasIndex(p => p.ExternalId).IsUnique();
            builder.Property(p => p.ExternalId).HasAnnotation("Range", new Range(1, 60));
            builder.Property(p => p.PatientId).IsRequired();
            builder.Property(p => p.PatientId).HasAnnotation("Range", new Range(1, 60));
            builder.Property(p => p.DoctorId).IsRequired();
            builder.Property(p => p.DoctorId).HasAnnotation("Range", new Range(1, 60));
            builder.ToTable("Appointment", table =>
            {
                table.HasCheckConstraint("CHK_Appointment_ValidDate", "Date BETWEEN NOW() AND NOW() + interval '1 year'");
            });
        }
    }
}
