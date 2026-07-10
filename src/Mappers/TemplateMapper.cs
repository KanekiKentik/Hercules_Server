using Microsoft.EntityFrameworkCore;

public static class TemplateMapper
{
    public static TemplateResponseDTO ToResponseDTO(this TemplateEntity template)
    {
        var exercises = template.Exercises;

        if(exercises is not { Count: > 0 }) 
            throw new InvalidOperationException($"Exercises not included for TemplateEntity with id: ${template.Id}");

        return new (template.Id, template.Name, exercises.Select(e => e.Id).ToArray());
    }

    public static TemplateRequestDTO ToRequestDTO(this TemplateEntity template)
    {
        var exercises = template.Exercises;

        if(exercises is not { Count: > 0 }) 
            throw new InvalidOperationException($"Exercises not included for TemplateEntity with id: ${template.Id}");

        return new (template.Name, exercises.Select(e => e.Id).ToArray());
    }
}   