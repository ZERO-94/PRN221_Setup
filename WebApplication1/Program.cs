using Microsoft.EntityFrameworkCore;
using Repository.Models;
using Repository.Repositories.CandidateProfileRepository;
using Repository.Repositories.HraccountRepository;
using Repository.Repositories.JobPostingRepository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<JobManagementContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultDb")
));

builder.Services.AddScoped<ICandidateProfileRepository, CandidateProfileRepository>();
builder.Services.AddScoped<IJobPostingRepository, JobPostingRepository>();
builder.Services.AddScoped<IHraccountRepository, HraccountRepository>();

builder.Services.AddSession();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseSession();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
