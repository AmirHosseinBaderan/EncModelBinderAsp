using EncTest.Annotations;
using Microsoft.AspNetCore.Mvc;

namespace EncTest.Controllers;

[Route("api/[controller]")]
[ApiController, AsyncModelBinder]
public class TestController : ControllerBase
{
    [HttpPost("test-action")]
    public async Task<IActionResult> TestAction(ModelTest model)
    {
        return Ok(model);
    }
}

public record ModelTest(string Name, string LastName);