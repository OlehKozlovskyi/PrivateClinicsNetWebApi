using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateClinicsWebNet.Application.DTOs;

public record RegisterDto
{
    public string Email { get; set; }
    public string Password { get; set; }
}
