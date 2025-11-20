var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

// TODO - need to finish hosting api project in cloud (loan-application-monitor in cloud under GuerraTechNow resource group)
// KV points to API url, but API project isn't currently hosted in the cloud
// after api is being hosted in azure, can switch IDE to single project startup (WebApp project only)
var apiUrl = builder.Configuration["ApiSettings:LoanApplicationsApi"] ?? "https://localhost:7203/";
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