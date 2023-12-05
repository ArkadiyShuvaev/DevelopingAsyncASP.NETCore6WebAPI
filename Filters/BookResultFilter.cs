using AutoMapper;
using Entities;
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

        var resultFromActionValue = resultFromAction.Value;
        if (resultFromActionValue is IEnumerable<Book> books)
        {
            resultFromAction.Value = _mapper.Map<IEnumerable<Models.BookDto>>(books);
            await next();
        }
        else
        {
            resultFromAction.Value = _mapper.Map<Models.BookDto>(resultFromActionValue);
            await next();
        }
    }
}
