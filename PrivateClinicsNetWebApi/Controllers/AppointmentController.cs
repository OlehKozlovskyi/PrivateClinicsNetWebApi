using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrivateClinicsNetWebApi.Extensions;
using PrivateClinicsWebNet.Application.Abstractions;
using PrivateClinicsWebNet.Application.DTOs.AppointmentDTOs;
using System.Security.Claims;

namespace PrivateClinicsNetWebApi.Controllers
{
    [ApiController]
    [Route("api/v1/appointments")]
    public class AppointmentController(IAppointmentService appointmentService) : ControllerBase
    {
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetAppointment([FromRoute] string id)
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

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetUserAppointments([FromQuery] AppointmentsRequestDto requestDto)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userType = User.FindFirstValue(ClaimTypes.Role);
            var result = await appointmentService.GetUserAppointmentsAsync(userId, userType, requestDto);
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
