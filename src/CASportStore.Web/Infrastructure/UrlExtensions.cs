using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CASportStore.Web.Infrastructure
{
    public static class UrlExtensions
    {
        // Generates a URL that browser will be returning to after the cart being updated
        // Taking into account the query string if there is one
        public static string PathAndQuery(this HttpRequest request) =>
            request.QueryString.HasValue
            ? $"{request.Path}{request.QueryString}"
            : request.Path.ToString();

    }
}
