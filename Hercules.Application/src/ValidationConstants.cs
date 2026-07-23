public static class ValidationConstants
{
    public const int MAX_USERNAME_LENGTH = 30;
    public const int MIN_USERNAME_LENGTH = 3;
    public const int MAX_PASSWORD_LENGTH = 40;
    public const int MIN_PASSWORD_LENGTH = 6;
    public const int MAX_TEMPLATE_NAME_LENGTH = 50;
    public const int MAX_EXERCISE_NAME_LENGTH = 75;
    public const int MAX_MUSCLE_NAME_LENGTH = 75;
    public const string USERNAME_REGEX = @"[a-zA-Z\d$#]{3,30}";
}