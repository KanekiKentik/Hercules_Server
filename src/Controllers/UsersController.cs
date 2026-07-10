using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("/users")]
public class UsersController : ControllerBase
{
    private readonly IUsersRepository _uRepo;
    private readonly UsersService _uService;
    public UsersController(IUsersRepository uRep, UsersService usersService)
        => (_uRepo, _uService) = (uRep, usersService);
    private IActionResult HandleErrorResult(Result result)
    {
        return result.ErrorType switch
        {
            ErrorType.Conflict => Conflict(),
            ErrorType.Forbidden => Forbid(),
            ErrorType.NotFound => NotFound(),
            ErrorType.Unauthorized => Unauthorized(),
            ErrorType.BadRequest => BadRequest(),
            ErrorType.ValidationError => BadRequest(result.ErrorMessage),
            _ => StatusCode(500)
        };
    }
    #region Guest
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserCredentialsDTO cred)
    {
        Result result = await _uService.Register(cred);

        if (result.IsSuccessful) return Created();

        return HandleErrorResult(result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserCredentialsDTO cred)
    {
        Result<string> result = await _uService.Login(cred);

        if (result.IsSuccessful) return Ok(result.Value);

        return HandleErrorResult(result);   
    }
    #endregion

    #region User
    [Authorize]
    [HttpPatch("change-pass")]
    public async Task<IActionResult> ChangePassword([FromQuery] string password)
    {
        var userId = _uService.GetSelfId(User);

        var result = await _uService.ChangePassword(userId, password);
        if (result.IsSuccessful) return NoContent();

        return HandleErrorResult(result);
    }

    [Authorize]
    [HttpPatch("change-name")]
    public async Task<IActionResult> ChangeUsername([FromQuery] string username)
    {
        int userId = _uService.GetSelfId(User);

        var result = await _uService.ChangeUsername(userId, username);
        if (result.IsSuccessful) return NoContent();

        return HandleErrorResult(result);
    }

    [Authorize]
    [HttpGet("get-self")]
    public async Task<IActionResult> GetSelf()
    {
        var user = await _uService.GetSelf(User);
        if (user == null) return NotFound();

        return Ok(user.ToDTO());
    }
    #endregion

    #region Admin
    [Authorize(Roles = nameof(Privilege.Admin))]
    [HttpGet("get-by-username")]
    public async Task<IActionResult> GetByUsername([FromQuery] string username)
    {
        var user = await _uRepo.Get(username);
        if (user == null) return NotFound();

        return Ok(user.ToDTO());
    }

    [Authorize(Roles = nameof(Privilege.Admin))]
    [HttpGet("get-by-id")]
    public async Task<IActionResult> GetById([FromQuery] int id)
    {
        var user = await _uRepo.Get(id);
        if (user == null) return NotFound();

        return Ok(user.ToDTO());
    }

    [Authorize(Roles = nameof(Privilege.Admin))]
    [HttpPatch("change-privilege")]
    public async Task<IActionResult> ChangePrivilege([FromQuery] int id, [FromQuery] Privilege privilege)
    {
        var user = await _uRepo.Get(id);
        if (user == null) return NotFound();

        user.SetPrivilege(privilege);
        await _uRepo.Update(user);
        return NoContent();
    }

    [Authorize(Roles = nameof(Privilege.Admin))]
    [HttpDelete("delete")]
    public async Task<IActionResult> Delete([FromQuery] int id)
    {
        bool exists = await _uRepo.Exists(id);
        if (!exists) return NotFound();

        await _uRepo.Delete(id);
        return NoContent();
    }

    [Authorize(Roles = nameof(Privilege.Admin))]
    [HttpPatch("admin/change-pass")]
    public async Task<IActionResult> AdminChangePass([FromQuery] int id, [FromQuery] string password)
    {
        var result = await _uService.ChangePassword(id, password);
        if (result.IsSuccessful) return NoContent();

        return HandleErrorResult(result);
    }
    #endregion
}