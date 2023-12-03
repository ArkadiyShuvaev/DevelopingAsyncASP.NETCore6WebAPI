using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Filters;

public class BookResultFilterAttribute : ResultFilterAttribute
{
    public async override Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        var resultFromAction = context.Result as ObjectResult;
        if (resultFromAction?.Value == null || resultFromAction.StatusCode < 200 || resultFromAction.StatusCode >= 300)
        {
            await next();
            return;
        }

        resultFromAction.Value = ((Book)resultFromAction.Value).Title.ToUpperInvariant();

        await next();
    }

    public override void OnResultExecuting(ResultExecutingContext context)
    {
        var result = context.Result as ObjectResult;
        if (result?.Value is Book book)
        {
            book.Title = book.Title.ToUpperInvariant();
        }
    }
}
