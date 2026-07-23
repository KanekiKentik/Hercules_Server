using Npgsql;
using Npgsql.EntityFrameworkCore.PostgreSQL.Query.Expressions;
using System;

internal class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    public ExceptionHandlingMiddleware(RequestDelegate next) => _next = next;
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next.Invoke(context);
        }
        catch (PostgresException)
        {
            //ОШИБКИ САМОЙ БД (НАРУШЕНИЕ ЦЕЛОСТНОСТИ, НАРУШЕНИЙ UNIQUE...)
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        }
        catch (NpgsqlException)
        {
            //ОШИБКИ С ПОДКЛЮЧЕНИЕМ К БД
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        }
        catch (Exception ex)
        {
            System.Console.WriteLine(ex.Message);
            System.Console.WriteLine(ex.StackTrace);

            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsync(ex.Message);
        }
    }
}

internal static class ExceptionHandlingMiddlwareExtension
{
    internal static IApplicationBuilder UseExceptionHandling(this IApplicationBuilder app)
        => app.UseMiddleware<ExceptionHandlingMiddleware>();
}