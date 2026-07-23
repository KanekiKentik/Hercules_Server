using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

public static class ControllerExtenstions
{
    public static IActionResult HandleErrorResult(this ControllerBase controller, Result result)
    {
        return result.ErrorType switch
        {
            ErrorType.Conflict => controller.Conflict(),
            ErrorType.Forbidden => controller.Forbid(),
            ErrorType.NotFound => controller.NotFound(),
            ErrorType.Unauthorized => controller.Unauthorized(),
            ErrorType.BadRequest => controller.BadRequest(result.Message),
            ErrorType.ValidationError => controller.BadRequest(result.Message),
            ErrorType.InvalidOperation => controller.BadRequest(result.Message),
            _ => controller.StatusCode(500, result.Message)
        };
    }
}