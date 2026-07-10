using System.ComponentModel.DataAnnotations;

public record UserDTO
{
    [Range(0, int.MaxValue)]
    public int UserId { get; init; }

    [Required]
    [MinLength(ValidationConstants.MIN_USERNAME_LENGTH)]
    [MaxLength(ValidationConstants.MAX_USERNAME_LENGTH)]
    [RegularExpression(ValidationConstants.USERNAME_REGEX)]
    public string Username { get; init; } = string.Empty;
    public Privilege Privilege { get; init; }
    public DateTimeOffset RegistrationDate { get; init; } = default;

    public UserDTO(int userId, string username, Privilege privilege, DateTimeOffset registrationDate)
        => (UserId, Username, Privilege, RegistrationDate) = (userId, username, privilege, registrationDate);
}