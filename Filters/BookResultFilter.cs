using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Filters;

public class BookResultFilter : IAsyncResultFilter
{
    private IMapper _mapper;

    public BookResultFilter(IMapper mapper)
    {
        _mapper = mapper;
    }

    public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        var resultFromAction = context.Result as ObjectResult;
        if (resultFromAction?.Value == null || resultFromAction.StatusCode < 200 || resultFromAction.StatusCode >= 300)
        {
            await next();
            return;
        }

        resultFromAction.Value = _mapper.Map<Models.BookDto>(resultFromAction.Value);

        await next();
    }

    // public override void OnResultExecuting(ResultExecutingContext context)
    // {
    //     var result = context.Result as ObjectResult;
    //     if (result?.Value is Book book)
    //     {
    //         book.Title = book.Title.ToUpperInvariant();
    //     }
    // }
}
