public static class TemplateMapper
{
    public static Result<TemplateResponse> ToResponse(this TemplateEntity template)
    {
        var exercises = template.Exercises;
        if(exercises == null) 
            return Result<TemplateResponse>.Failure(
                    ErrorType.InvalidOperation,
                    $"Exercises not included for TemplateEntity with id: ${template.Id}");

        var response = new TemplateResponse(template.Id, template.Name, template.Exercises.Select(e => e.Id).ToArray());
        return Result<TemplateResponse>.Success(response);
    }

    public static Result<TemplateRequest> ToRequest(this TemplateEntity template)
    {
        var exercises = template.Exercises;
        if(exercises == null) 
            return Result<TemplateRequest>.Failure(
                    ErrorType.InvalidOperation,
                    $"Exercises not included for TemplateEntity with id: ${template.Id}");

        var response = new TemplateRequest(template.Name, template.Exercises.Select(e => e.Id).ToArray());
        return Result<TemplateRequest>.Success(response);
    }
}   