using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using nkatman.Core.DTOs;
using nkatman.Core.Models;
using nkatman.Core.Services;

namespace Nkatman.API.Filters
{
    public class NotFoundFilter<T> : IAsyncActionFilter where T : BaseEntity
    {

        private readonly IService<T> _service;

        public NotFoundFilter(IService<T> service)
        {
            _service = service;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var ıdValue = context.ActionArguments.Values.FirstOrDefault();

            if (ıdValue == null)
            {
                await next.Invoke();
                return;
            }
            var ıd = (int)ıdValue;
            var anyEntity = await _service.AnyAsync(x => x.Id == ıd);

            if (!anyEntity)
            {
                await next.Invoke();
                return;
            }
            var model = new CustomResponseDto<NoContentDto>();

            context.Result =new NotFoundObjectResult(model.Fail(404,
                $"{typeof(T).Name} not found"));
        }
    }
}
