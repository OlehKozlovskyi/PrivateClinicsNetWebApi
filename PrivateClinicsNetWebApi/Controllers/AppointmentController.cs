using Microsoft.AspNetCore.Mvc;
using PrivateClinicsNetWebApi.Extensions;
using PrivateClinicsWebNet.Application.Abstractions;
using PrivateClinicsWebNet.Application.DTOs.AppointmentsDTOs;

namespace PrivateClinicsNetWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentController(IAppointmentService appointmentService) 
        {
            _appointmentService = appointmentService;
        }

        [HttpGet]
        [Route("/{id}")]
        public async Task<IActionResult> GetAppointment([FromBody] string id)
        {
            var result = await _appointmentService.GetAppointmentAsync(id);
            return result.ToResponse();
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> CreateAppointment([FromBody] CreateAppointmentDto appointmentDto)
        {
            var result = await _appointmentService.CreateAppointmentAsync(appointmentDto);
            return result.ToResponse();
        }

        [HttpPut]
        [Route("/{id}")]
        public async Task<IActionResult> UpdateAppointment([FromBody] UpdateAppointmentDto appointmentDto)
        {
            var result = await _appointmentService.UpdateAppointmentAsync(appointmentDto);
            return result.ToResponse();
        }

        [HttpGet]
        [Route("/users/{requestDto.DoctorId}/appointments")]
        public async Task<IActionResult> GetDoctorAppointments([FromRoute] DoctorAppointmentsRequestDto requestDto)
        {
            var result = await _appointmentService.GetDoctorAppointmentsAsync(requestDto);
            return result.ToResponse();
        }

        [HttpGet]
        [Route("users/{requestDto.PatientId}/appointments")]
        public async Task<IActionResult> GetPatientAppointments([FromRoute] PatientAppointmentsRequestDto requestDto)
        {
            var result = await _appointmentService.GetPatientAppointmentsAsync(requestDto);
            return result.ToResponse();
        }

        [HttpDelete]
        [Route("/{id}")]
        public async Task<IActionResult> DeleteAppointment([FromRoute] string id)
        {
            var result = await _appointmentService.DeleteAppointmentAsync(id);
            return result.ToResponse();
        }
    }
}
