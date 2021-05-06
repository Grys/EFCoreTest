using System;
using Microsoft.AspNetCore.Mvc;



namespace RuCitizens
{
    public static class ExceptionExtension
    {
        public static ContentResult ToContentResult(this Exception ex)
        {
            return new ContentResult()
            {
                StatusCode = 400,
                Content = ex.Message
            };
        }
    }
}
