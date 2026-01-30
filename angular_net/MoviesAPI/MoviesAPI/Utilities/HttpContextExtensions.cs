namespace MoviesAPI.Utilities;

public static class HttpContextExtensions
{
    public static void InsertPaginationParametersInHeader(this HttpContext httpContext, int count)
    {
        if (httpContext is null)
        {
            throw new ArgumentNullException(nameof(httpContext));
        }

        httpContext.Response.Headers.Append("total-records-count", count.ToString());
    }
}