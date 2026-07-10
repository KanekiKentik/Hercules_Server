using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

public class UsersService
{
    private readonly IPasswordHasher _hasher;
    private readonly IUsersRepository _usersRepo;
    private readonly IJwtProvider _jwtProvider;
    public UsersService(IPasswordHasher hasher, IUsersRepository repo, IJwtProvider jwtProvider)
        => (_hasher, _usersRepo, _jwtProvider) = (hasher, repo, jwtProvider);
    public async Task<Result> Register(UserCredentialsDTO cred)
    {
        var user = await _usersRepo.Get(cred.Username);

        if (user != null) return Result.Failure(ErrorType.Conflict);

        string passwordHash = _hasher.Generate(cred.Password);
        var newUser = new UserEntity(cred.Username, passwordHash, DateTimeOffset.UtcNow);

        await _usersRepo.Post(newUser);
        return Result.Success();
    }
    public async Task<Result<string>> Login(UserCredentialsDTO cred)
    {
        var user = await _usersRepo.Get(cred.Username);
        if (user == null) return Result<string>.Failure(string.Empty, ErrorType.NotFound);

        var verificationResult = _hasher.Verify(user.PasswordHash, cred.Password);
        if (!verificationResult) return Result<string>.Failure(string.Empty, ErrorType.Unauthorized);

        string token = _jwtProvider.GenerateToken(user);
        return Result<string>.Success(token);
    }
    public int GetSelfId(ClaimsPrincipal user)
    {
        var idClaim = user.FindFirst(ClaimTypes.NameIdentifier);

        if (idClaim == null) throw new TokenClaimException("Cannot find id claim in token");
        if (!int.TryParse(idClaim.Value, out int userId)) throw new TokenClaimException("Cannot parse id value from token`s claim");

        return userId;
    }
    public async Task<UserEntity?> GetSelf(ClaimsPrincipal user)
    {
        int userId = GetSelfId(user);

        var userEntity = await _usersRepo.Get(userId);
        return userEntity;
    }
    public async Task<Result> ChangePassword(int userId, string password)
    {
        if (password.Length < ValidationConstants.MIN_PASSWORD_LENGTH) 
            return Result.Failure($"Password must be at least {ValidationConstants.MIN_PASSWORD_LENGTH} characters long" ,ErrorType.ValidationError);

        var user = await _usersRepo.Get(userId);
        if (user == null) return Result.Failure(ErrorType.NotFound);

        string newHash = _hasher.Generate(password);
        user.SetPasswordHash(newHash);
        await _usersRepo.Update(user);
        return Result.Success();
    }
    public async Task<Result> ChangeUsername(int userId, string username)
    {
        var user = await _usersRepo.Get(userId);
        if (user == null) return Result.Failure(ErrorType.NotFound);

        user.SetUsername(username);
        await _usersRepo.Update(user);
        return Result.Success();
    }
}