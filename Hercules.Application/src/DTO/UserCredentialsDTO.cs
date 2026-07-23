using System.ComponentModel.DataAnnotations;

public record UserCredentialsDTO
{
    [Required]
    [MinLength(ValidationConstants.MIN_USERNAME_LENGTH)]
    [MaxLength(ValidationConstants.MAX_USERNAME_LENGTH)]
    [RegularExpression(ValidationConstants.USERNAME_REGEX)]
    public string Username { get; init; } = string.Empty;
    [Required]
    [MinLength(ValidationConstants.MIN_PASSWORD_LENGTH)]
    [MaxLength(ValidationConstants.MAX_PASSWORD_LENGTH)]
    public string Password { get; init; } = string.Empty;
}