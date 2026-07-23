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

    #region Guest
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserCredentialsDTO cred)
    {
        Result result = await _uService.Register(cred);

        if (result.IsSuccess) return Created();

        return this.HandleErrorResult(result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserCredentialsDTO cred)
    {
        Result<string> result = await _uService.Login(cred);

        if (result.IsSuccess) return Ok(result.Value);

        return this.HandleErrorResult(result);   
    }
    #endregion

    #region User
    [Authorize]
    [HttpPatch("change-pass")]
    public async Task<IActionResult> ChangePassword([FromQuery] string password)
    {
        Result<int> idResult = _uService.GetSelfId(User);

        if (idResult.IsFailure) return this.HandleErrorResult(idResult);
        int userId = idResult.Value; 

        var result = await _uService.ChangePassword(userId, password);
        if (result.IsSuccess) return NoContent();

        return this.HandleErrorResult(result);
    }

    [Authorize]
    [HttpPatch("change-name")]
    public async Task<IActionResult> ChangeUsername([FromQuery] string username)
    {
        Result<int> idResult = _uService.GetSelfId(User);

        if (idResult.IsFailure) return this.HandleErrorResult(idResult);
        int userId = idResult.Value; 

        var result = await _uService.ChangeUsername(userId, username);
        if (result.IsSuccess) return NoContent();

        return this.HandleErrorResult(result);
    }

    [Authorize]
    [HttpGet("get-self")]
    public async Task<IActionResult> GetSelf()
    {
        var getResult = await _uService.GetSelf(User);
        if (getResult.IsFailure) return this.HandleErrorResult(getResult);
        
        var user = getResult.Value;
        return Ok(user.ToResponse());
    }
    #endregion

    #region Admin
    [Authorize(Roles = nameof(Privilege.Admin))]
    [HttpGet("get-by-username")]
    public async Task<IActionResult> GetByUsername([FromQuery] string username)
    {
        var user = await _uRepo.Get(username);
        if (user == null) return NotFound();

        return Ok(user.ToResponse());
    }

    [Authorize(Roles = nameof(Privilege.Admin))]
    [HttpGet("get-by-id")]
    public async Task<IActionResult> GetById([FromQuery] int userId)
    {
        var user = await _uRepo.Get(userId);
        if (user == null) return NotFound();

        return Ok(user.ToResponse());
    }

    [Authorize(Roles = nameof(Privilege.Admin))]
    [HttpPatch("change-privilege")]
    public async Task<IActionResult> ChangePrivilege([FromQuery] int userId, [FromQuery] Privilege privilege)
    {
        var user = await _uRepo.Get(userId);
        if (user == null) return NotFound();

        user.SetPrivilege(privilege);
        await _uRepo.Update(user);
        return NoContent();
    }

    [Authorize(Roles = nameof(Privilege.Admin))]
    [HttpDelete("delete")]
    public async Task<IActionResult> Delete([FromQuery] int userId)
    {
        var user = await _uRepo.Get(userId);
        if (user == null) return NotFound();

        await _uRepo.Delete(userId);
        return NoContent();
    }

    [Authorize(Roles = nameof(Privilege.Admin))]
    [HttpPatch("admin/change-pass")]
    public async Task<IActionResult> AdminChangePass([FromQuery] int userId, [FromQuery] string password)
    {
        var result = await _uService.ChangePassword(userId, password);
        if (result.IsFailure) return this.HandleErrorResult(result);

        return NoContent();
    }
    #endregion
}