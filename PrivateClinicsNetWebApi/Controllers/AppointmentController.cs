using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrivateClinicsNetWebApi.Extensions;
using PrivateClinicsWebNet.Application.Abstractions;
using PrivateClinicsWebNet.Application.DTOs.AppointmentDTOs;
using System.Security.Claims;

namespace PrivateClinicsNetWebApi.Controllers
{
    [ApiController]
    [Route("api/appointments")]
    public class AppointmentController(IAppointmentService appointmentService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAppointment([FromQuery] string id)
        {
            var result = await appointmentService.GetAppointmentAsync(id);
            return result.ToResponse();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAppointment(CreateAppointmentDto appointmentDto)
        {
            var result = await appointmentService.CreateAppointmentAsync(appointmentDto);
            return result.ToResponse();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAppointment(UpdateAppointmentDto appointmentDto)
        {
            var result = await appointmentService.UpdateAppointmentAsync(appointmentDto);
            return result.ToResponse();
        }

        [Authorize(Roles = "Doctor")]
        [HttpGet]
        [Route("/doctor/appointments")]
        public async Task<IActionResult> GetDoctorAppointments([FromQuery] AppointmentsRequestDto requestDto)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await appointmentService.GetDoctorAppointmentsAsync(userId, requestDto);
            return result.ToResponse();
        }

        [Authorize(Roles = "Patient")]
        [HttpGet]
        [Route("/patient/appointments")]
        public async Task<IActionResult> GetPatientAppointments([FromQuery] AppointmentsRequestDto requestDto)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await appointmentService.GetPatientAppointmentsAsync(userId, requestDto);
            return result.ToResponse();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAppointment([FromQuery] string id)
        {
            var result = await appointmentService.DeleteAppointmentAsync(id);
            return result.ToResponse();
        }
    }
}
