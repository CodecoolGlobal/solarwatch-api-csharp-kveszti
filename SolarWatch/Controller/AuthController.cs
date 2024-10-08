using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SolarWatch.Contracts;
using SolarWatch.Services.Authentication;


namespace SolarWatch.Controller;

[ApiController]
[Microsoft.AspNetCore.Components.Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authenticationService;

    public AuthController(IAuthService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost("api/Register")]
    public async Task<ActionResult<RegistrationResponse>> Register(RegistrationRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _authenticationService.RegisterAsync(request.Email, request.Username, request.Password, "User");

        if (!result.Success)
        {
            AddErrors(result);
            return BadRequest(ModelState);
        }

        return CreatedAtAction(nameof(Register), new RegistrationResponse(result.Email, result.UserName, true));
    }

    private void AddErrors(AuthResult result)
    {
        foreach (var error in result.ErrorMessages)
        {
            ModelState.AddModelError(error.Key, error.Value);
        }
    }
    
    [HttpPost("api/Login")]
    public async Task<ActionResult<AuthResponse>> Authenticate([FromBody] AuthRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _authenticationService.LoginAsync(request.Email, request.Password);

        if (!result.Success)
        {
            AddErrors(result);
            return BadRequest(ModelState);
        }

        return Ok(new AuthResponse(result.Email, result.UserName, result.Token, true));
    }

    [HttpGet("/api/isadmin"), Authorize(Roles = "User, Admin")]
    public async Task<ActionResult<bool>> SendBackRole()
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var userName = HttpContext.User.Identity.Name;
        var isAdmin = await _authenticationService.isAdmin(userName);

        return Ok(isAdmin);

    }

    [HttpGet("/api/isExpired"), Authorize(Roles = "User, Admin")]
    public ActionResult<bool> IsExpired()
    {
        return Ok(false);
    }
}