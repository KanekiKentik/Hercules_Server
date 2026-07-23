using System.Security.Claims;

public class UsersService
{
    private readonly IUsersRepository _usersRepo;
    private readonly IPasswordHasher _hasher;
    private readonly IJwtProvider _jwtProvider;
    public UsersService(IPasswordHasher hasher, IUsersRepository repo, IJwtProvider jwtProvider)
        => (_hasher, _usersRepo, _jwtProvider) = (hasher, repo, jwtProvider);
    public async Task<Result> Register(UserCredentialsDTO cred)
    {
        var user = await _usersRepo.Get(cred.Username);
        if (user != null) return Result.Failure(
                ErrorType.Conflict,
                "Username is occupied");

        string passwordHash = _hasher.Generate(cred.Password);
        var newUser = new UserEntity(cred.Username, passwordHash, DateTimeOffset.UtcNow);

        await _usersRepo.Post(newUser);
        return Result.Success();
    }
    public async Task<Result<string>> Login(UserCredentialsDTO cred)
    {
        var user = await _usersRepo.Get(cred.Username);
        if (user == null) return Result<string>.Failure(ErrorType.NotFound);

        var verificationResult = _hasher.Verify(user.PasswordHash, cred.Password);
        if (!verificationResult) return Result<string>.Failure(ErrorType.Unauthorized);

        string token = _jwtProvider.GenerateToken(user);
        return Result<string>.Success(token);
    }
    public Result<int> GetSelfId(ClaimsPrincipal user)
    {
        var idClaim = user.FindFirst(ClaimTypes.NameIdentifier);

        if (idClaim == null) return Result<int>.Failure(
                ErrorType.InvalidOperation,
                "Invalid token. No Id claim");

        if (!int.TryParse(idClaim.Value, out int userId)) return Result<int>.Failure(
                ErrorType.InvalidOperation,
                "Invalid token. Cant parse id claim");

        return Result<int>.Success(userId);
    }
    public async Task<Result<UserEntity>> GetSelf(ClaimsPrincipal user)
    {
        var getIdResult = GetSelfId(user);

        if (getIdResult.IsFailure) return Result<UserEntity>.Failure(getIdResult.ErrorType);

        var userEntity = await _usersRepo.Get(getIdResult.Value);
        if (userEntity == null) return Result<UserEntity>.Failure(ErrorType.NotFound);

        return Result<UserEntity>.Success(userEntity);
    }
    public async Task<Result> ChangePassword(int userId, string password)
    {
        var user = await _usersRepo.Get(userId);
        if (user == null) return Result.Failure(ErrorType.NotFound);

        string newHash = _hasher.Generate(password);
        user.SetPasswordHash(newHash);

        var updateResult = await _usersRepo.Update(user);
        if (updateResult.IsFailure) return Result.Failure(updateResult.ErrorType);

        return Result.Success();
    }
    public async Task<Result> ChangeUsername(int userId, string username)
    {
        var user = await _usersRepo.Get(userId);
        if (user == null) return Result.Failure(ErrorType.NotFound);

        user.SetUsername(username);
        
        var updateResult = await _usersRepo.Update(user);
        if (updateResult.IsFailure) return Result.Failure(updateResult.ErrorType);

        return Result.Success();
    }
}