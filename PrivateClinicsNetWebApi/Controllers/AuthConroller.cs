using Microsoft.AspNetCore.Mvc;
using PrivateClinicsWebNet.DataAccess;

namespace PrivateClinicsNetWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthConroller : ControllerBase
    {
        private readonly AuthService _authService;
    }
}
