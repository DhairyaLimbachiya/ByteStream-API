﻿using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace byteStream.Employer.Api.Utility.ApiFilter
{
	public class ValidateModelAttribute:ActionFilterAttribute
	{
		public override void OnActionExecuted(ActionExecutedContext context)
		{
			if (context.ModelState.IsValid == false)
			{
				context.Result = new BadRequestResult();
			}
		}
	}
}
