using Microsoft.AspNetCore.HttpOverrides;
using NLog;
using PuebaNET6.Extensions;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.

LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

builder.Services.ConfigureCors();

builder.Services.ConfigureIISIntegration();

builder.Services.ConfigureLoggerService();

builder.Services.ConfigureRepositoryManager();

var configuration = builder.Configuration;

builder.Services.ConfigureSqlContext(configuration);

builder.Services.AddControllers();

var app = builder.Build();




// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.All
});
app.UseCors("CorsPolicy");
app.UseAuthorization();
app.MapControllers();
app.Run();