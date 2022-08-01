using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NLayer.Core.DTOs;

namespace NLayer.API.Filters;

public class ValidateFilterAttribute:ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid)
        {
            var errors = context.ModelState.Values.SelectMany(x => x.Errors).Select(x=>x.ErrorMessage).ToList(); 
            //Dictionary içerisindeki Sadece hataları seçtik
            context.Result = new BadRequestObjectResult(CustomResponseDto<NoContentDto>.Fail(400,errors)); // Yollanan modelin validasyonunda sıkıntı olduğu için bu client hatasıdır bu sebeple badrequest
            //BadRequestResult seçersek body'si boş döner biz bu sebepten object result seçtik ve hataları gönderdik.
        }
        base.OnActionExecuting(context);
    }
}