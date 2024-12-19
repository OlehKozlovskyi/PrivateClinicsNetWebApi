using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrivateClinicsWebNet.Application.DTOs;
using PrivateClinicsWebNet.Application.Services;

namespace PrivateClinicsNetWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleManagmentConroller : ControllerBase
    {
        private readonly RoleService _roleService;

        public RoleManagmentConroller(RoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpPost("role-create")]
        public async Task<IActionResult> CreateRoleAsync([FromBody] RoleCreateDto model)
        {
            await _roleService.CreateRoleAsync(model.RoleName);
            return Ok("Role created successfully");
        }
    }
}
