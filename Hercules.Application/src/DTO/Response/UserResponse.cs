using System.ComponentModel.DataAnnotations;

public record UserResponse
{
    [Range(0, int.MaxValue)]
    public int UserId { get; init; }

    [Required]
    [MinLength(ValidationConstants.MIN_USERNAME_LENGTH)]
    [MaxLength(ValidationConstants.MAX_USERNAME_LENGTH)]
    [RegularExpression(ValidationConstants.USERNAME_REGEX)]
    public string Username { get; init; } = string.Empty;
    public string Privilege { get; init; }
    public DateTimeOffset RegistrationDate { get; init; } = default;

    public UserResponse(int userId, string username, string privilege, DateTimeOffset registrationDate)
        => (UserId, Username, Privilege, RegistrationDate) = (userId, username, privilege, registrationDate);
}