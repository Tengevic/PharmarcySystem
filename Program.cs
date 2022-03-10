using Example;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PharmarcySystem.Areas.Identity.Data;
using PharmarcySystem.Data;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("PharmarcySystemContextConnection");

builder.Services.AddDbContext<PharmarcySystemContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<PharmarcySystemUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<PharmarcySystemContext>();


// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddMvc();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<PharmarcySystemContext>();
    context.Database.EnsureCreated();
    DbInitializer.Initialize(context);
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
