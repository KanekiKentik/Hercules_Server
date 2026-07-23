public enum ErrorType
{
    ValidationError,
    InvalidOperation,
    None = 200,
    BadRequest = 400,
    Unauthorized = 401,
    Forbidden = 403,
    NotFound = 404,
    Conflict = 409,
    TooManyRequests = 429,
    InternalServerError = 500
}