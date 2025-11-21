var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

string apiUrl = builder.Configuration["ApiSettings:BaseUrl"] ?? string.Empty;

builder.Services.AddHttpClient("BackendApi", client =>
{
    client.BaseAddress = new Uri(apiUrl);
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("ApiPolicy", policy =>
        policy.WithOrigins
        (
            "https://localhost:7203",
            "https://loanapplicationmonitorapi-e0f6gcajhxcjc8bt.centralus-01.azurewebsites.net"
        )
        .AllowAnyHeader()
        .AllowAnyMethod());
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
    context.Response.Redirect("HealthMonitoringMessages");
    return Task.CompletedTask;
});

app.UseCors("ApiPolicy");
app.MapRazorPages();

app.Run();