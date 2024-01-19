﻿using Customer.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Customer.Web.Shared.ActionFilter
{
    public class AppExceptionFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception is NotFoundEntityAppException)
            {
                context.Result = new NotFoundResult();
                context.ExceptionHandled = true;
            }
        }
    }
}