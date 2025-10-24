var builder = WebApplication.CreateBuilder(args);

// Add Razor Pages
builder.Services.AddRazorPages();

var apiUrl = builder.Configuration["ApiSettings:BaseUrl"] ?? "https://localhost:7203/";
builder.Services.AddHttpClient("BackendApi", client =>
{
    client.BaseAddress = new Uri(apiUrl);
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapGet("/", context =>
{
    context.Response.Redirect("/HealthMonitoringMessages");
    return Task.CompletedTask;
});

app.MapRazorPages();

app.Run();
