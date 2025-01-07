using WebMigrationsBack.Interfaces;
using WebMigrationsBack.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add CORS services 
builder.Services.AddCors(options => {
     options.AddPolicy("AllowSpecificOrigins", 
     builder => { 
        builder.WithOrigins("http://localhost:4200").AllowAnyHeader() .AllowAnyMethod(); 
    });
});

builder.Services.AddScoped<IJWTokenService, JWTokenService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// Use the CORS policy 
app.UseCors("AllowSpecificOrigins");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
