using Microsoft.AspNetCore.Mvc;
using NLayer.Core.DTOs;

namespace NLayer.API.Controllers;

[ApiController]
[Route("api/[Controller]")]
public class CustomBaseController : ControllerBase
{
    [NonAction] //Endpoint olmadığını belirttik.
    public IActionResult CreateActionResult<T>(CustomResponseDto<T> response)
    {
        if (response.StatusCode == 204) return new ObjectResult(null) { StatusCode = response.StatusCode };

        return new ObjectResult(response) { StatusCode = response.StatusCode };
    }
}