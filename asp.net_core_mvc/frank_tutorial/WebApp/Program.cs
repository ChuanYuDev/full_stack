using System.Net.Mime;
using System.Text;

// Create ASP.NET CORE Empty project

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Traditional web application framework
//      1. MapGet, MapPost: mapping
//      2. Funtion handle request 
//      3. Return HTML
//
// Problems
//      1. We need get username and password by ourself
//      2. HTML string hard to manipulate
//      3. Password validation (business logic) mixed up with HTML (view)
//
// (HttpContext context): a type of dependency injection
app.MapGet("/", (HttpContext context) =>
{
    // @: Verbatim string literals
    //      Use "" to denote "
    //      Leftmost and right most " is for verbatim string
    //      So @"""" -> "

    // html form
    //      https://www.w3schools.com/html/html_forms.asp
    //      <form> method=""post"": HTTP post transaction
    //      <label> `for` attribute <-> <input> `id` attribute
    WriteHtml(context, @$"
        <!doctype html>
        <html>
            <head><title>miniHTML</title></head>
            <body>
                <h1>Simple Framework</h1>
                <br>
                <form action=""/login"" method=""post"">
                    <label for=""username"">User name: </label>
                    <input type=""text"" id=""username"" name=""username1"" required>
                    <label for=""password"">Password:</label>
                    <input type=""password"" id=""password"" name=""password"" required>
                    <button type=""submit"">Login</button>
                </form>
            </body>
        </html>"
    );
});

app.MapPost("/login", (HttpContext context) =>
{
    // Form index <-> <input> name
    var username = context.Request.Form["username1"];
    Console.WriteLine($"username: {username}");
    var password = context.Request.Form["password"];

    if (username == "frank" && password == "password")
    {
        var html = @$" 
            <!doctype html>
            <html>
                <head><title>miniHTML</title></head>
                <body>
                    <h1>Simple Framework</h1>
                    <br>
                    Welcome to our simple framework!
                </body>
            </html>";

        WriteHtml(context, html);
    }
    else
    {
        var html = @$" 
            <!doctype html>
            <html>
                <head><title>miniHTML</title></head>
                <body>
                    <h1>Simple Framework</h1>
                    <br>
                    <form action=""/login"" method=""post"">
                        <label for=""username"">User name: </label>
                        <input type=""text"" id=""username"" name=""username"" required>
                        <label for=""password"">Password:</label>
                        <input type=""password"" id=""password"" name=""password"" required>
                        <button type=""submit"">Login</button>
                        <br>
                        <label style=""color:red""> Login failed!</label>
                    </form>
                </body>
            </html>";

        WriteHtml(context, html);

    }
});

app.Run();

void WriteHtml(HttpContext context, string html)
{
    context.Response.ContentType = MediaTypeNames.Text.Html;
    context.Response.ContentLength = Encoding.UTF8.GetByteCount(html);
    context.Response.WriteAsync(html);
}