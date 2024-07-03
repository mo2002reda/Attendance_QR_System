using AutoMapper;
using Bll;
using Bll.Interfaces_Repository;
using Bll.Repository;
using Dll.DataContext;
using Dll.Entity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PllDoctor.DTO;
using System.Security.Principal;

namespace PllDoctor
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);
			builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
					.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
					{
						//when user login he Execute path /Account/Login
						options.LoginPath = new PathString("/Account/Login");
						//if user try to do anything that he don't have permission the path /Home/Error will execute (there were Default Error Page in Home Page )
						options.LogoutPath = new PathString("/Account/SignOut");
						options.AccessDeniedPath = new PathString("/Account/AccessDenied");

					});

			// Add services to the container.
			builder.Services.AddControllersWithViews();
			builder.Services.AddDbContext<SystemDbContext>(options =>
			{
				options.UseSqlServer(builder.Configuration.GetConnectionString("DataBaseConnection"));
			}
		  );
			builder.Services.AddScoped<IdoctorRepository, DoctorRepository>();
			builder.Services.AddScoped<Isubjects, SubjectsRepository>();
			builder.Services.AddScoped<IstudentRepository, StudentRepository>();
			builder.Services.AddScoped<IAttendance, AttendanceRepository>();
			builder.Services.AddScoped<IUniteOfWork, UniteOfWork>();

			builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
			builder.Services.AddTransient<IPrincipal>(
			provider => provider.GetService<IHttpContextAccessor>().HttpContext.User);

			builder.Services.AddAutoMapper(o => o.AddProfiles(new List<Profile>() { new SubjectProfile(), new UserProfile(), new RoleProfile(), new AttendanceProfile() }));
			builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
						.AddEntityFrameworkStores<SystemDbContext>();

			builder.Services.AddIdentityCore<ApplicationUser>(options =>
			{
				#region policy for Password
				options.Password.RequireDigit = true;
				options.Password.RequireUppercase = true;
				options.Password.RequireLowercase = true;
				//options.Password.RequireNonAlphanumeric = true;
				options.Password.RequiredLength = 6;
				options.SignIn.RequireConfirmedAccount = false;
				//to confirm with another Account
				//options.Lockout.MaxFailedAccessAttempts = 5;
				//can try to signin faild at 5 time
				#endregion

			}).AddEntityFrameworkStores<SystemDbContext>()
			   .AddTokenProvider<DataProtectorTokenProvider<ApplicationUser>>(TokenOptions.DefaultProvider);

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

			app.UseRouting();
			app.UseAuthentication();

			app.UseAuthorization();

			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Account}/{action=SignUp}/{id?}");

			app.Run();
		}
	}
}