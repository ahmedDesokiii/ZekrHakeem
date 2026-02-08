using ZekrHakeem_v11.IRepository.ServicesRepository;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting; // For IWebHostEnvironment if needed
namespace ZekrHakeem_v11
{
    public class Program
    {
        public async static Task Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews(); // Adds MVC services

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
                throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(connectionString));

            var Configuration = builder.Configuration;

            builder.Services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
            builder.Services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();
            //builder.Services.AddDbContext<ApplicationDbContext>(ServiceLifetime.Scoped);

            // External Login Services
            builder.Services.AddAuthentication()
                // GoogleAccount ExternalLogin
                .AddGoogle(options =>
                {
                    IConfigurationSection googleAuthSection = Configuration.GetSection("Authentication:Google");

                    options.ClientId = googleAuthSection["ClientId"];
                    options.ClientSecret = googleAuthSection["ClientSecret"];
                })
                //FaceBookAccount ExternalLogin
                .AddFacebook(facebookOptions =>
                {
                    facebookOptions.AppId = Configuration["Authentication:Facebook:AppId"];
                    facebookOptions.AppSecret = Configuration["Authentication:Facebook:AppSecret"];
                });
            //.AddMicrosoftAccount(microsoftOptions =>
            //{
            //    microsoftOptions.ClientId = Configuration["Authentication:Microsoft:ClientId"];
            //    microsoftOptions.ClientSecret = Configuration["Authentication:Microsoft:ClientSecret"];
            //});



            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(
                options => options.SignIn.RequireConfirmedAccount = true)
                            .AddEntityFrameworkStores<ApplicationDbContext>()
                            .AddDefaultUI()
                            .AddDefaultTokenProviders();

            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            //Services To ReloadPage after update any process 
            builder.Services.Configure<SecurityStampValidatorOptions>(options =>
            {
                options.ValidationInterval = TimeSpan.Zero;
            });

            // Centrers Services 
            builder.Services.AddScoped<IServicesRepository<Center>, ServicesCenter>();
            builder.Services.AddScoped<IServicesCenter, ServicesCenter>();
            builder.Services.AddScoped<IServicesRepositoryLog<LogCenter>, ServicesLogCenter>();
            // Progs Services 
            builder.Services.AddScoped<IServicesRepository<Prog>, ServicesProg>();
            builder.Services.AddScoped<IServicesProg, ServicesProg>();
            builder.Services.AddScoped<IServicesRepositoryLog<LogProg>, ServicesLogProg>();
            //Districts Services
            builder.Services.AddScoped<IServicesRepository<District>, ServicesDistrict>();
            builder.Services.AddScoped<IServicesDistrict, ServicesDistrict>();
            //Location Services
            builder.Services.AddScoped<IServicesRepository<Location>, ServicesLocation>();
            builder.Services.AddScoped<IServicesLocation, ServicesLocation>();
            builder.Services.AddScoped<IServicesRepositoryLog<LogLocation>, ServicesLogLocation>();
            //Episodes Services
            builder.Services.AddScoped<IServicesRepository<Episode>, ServicesEpisode>();
            builder.Services.AddScoped<IServicesEpisode, ServicesEpisode>();
            builder.Services.AddScoped<IServicesRepositoryLog<LogEpisode>, ServicesLogEpisode>();
            //EpisodeProgs Services
            builder.Services.AddScoped<IServicesRepository<EpisodeProg>, ServicesEpisodeProg>();
            builder.Services.AddScoped<IServicesEpisodeProg, ServicesEpisodeProg>();
            builder.Services.AddScoped<IServicesRepositoryLog<LogEpisodeProg>, ServicesLogEpisodeProg>();
            //Students Services
            builder.Services.AddScoped<IServicesRepository<Student>, ServicesStudent>();
            builder.Services.AddScoped<IServicesRepository<StudentProg>, ServicesStudentProg>();
            builder.Services.AddScoped<IServicesStudentProg, ServicesStudentProg>();
            builder.Services.AddScoped<IServicesStudent, ServicesStudent>();
            builder.Services.AddScoped<IServicesRepositoryLog<LogStudent>, ServicesLogStudent>();
            builder.Services.AddScoped<IServicesRepository<Demand>, ServicesDemand>();
            builder.Services.AddScoped<IServicesDemand, ServicesDemand>();
            builder.Services.AddScoped<IServicesRepositoryLog<LogDemand>, ServicesLogDemand>();


            //Levels Services
            builder.Services.AddScoped<IServicesRepository<LevelData>, ServicesLevelData>();
            builder.Services.AddScoped<IServicesRepositoryLog<LogLevelData>, ServicesLogLevelData>();
            builder.Services.AddScoped<IServicesLevelData, ServicesLevelData>();
            //Plans Services

            builder.Services.AddScoped<IServicesPlanData, ServicesPlanData>();
            builder.Services.AddScoped<IServicesRepository<PlanData>, ServicesPlanData>();
            builder.Services.AddScoped<IServicesRepositoryLog<LogPlanData>, ServicesLogPlanDataData>();
            //Teacher Services
            builder.Services.AddScoped<IServicesTeacher, ServicesTeacher>();
            //Report Services
            builder.Services.AddScoped<IServicesReport, ServicesReport>();

            //LessonData
            builder.Services.AddScoped<IServicesRepository<LessonData>, ServicesLessonData>();
            builder.Services.AddScoped<IServicesLessonData, ServicesLessonData>();

            //Email Services
            builder.Services.AddTransient<IEmailSender, EmailSender>();
            builder.Services.Configure<AuthMessageSenderOptions>(builder.Configuration);



            builder.Services.AddSession();
            builder.Services.AddControllersWithViews();

            var app = builder.Build();
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var loggerFactory = services.GetRequiredService<ILoggerFactory>();
                var logger = loggerFactory.CreateLogger("app");
                try
                {
                    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                    await Seeds.DefaultRoles.SeedAsync(userManager, roleManager);
                    await Seeds.DefaultUsers.SeedParentUserAsync(userManager, roleManager);
                    await Seeds.DefaultUsers.SeedTeacherUserAsync(userManager, roleManager);
                    await Seeds.DefaultUsers.SeedSuervisorUserAsync(userManager, roleManager);
                    await Seeds.DefaultUsers.SeedAdminAsync(userManager, roleManager);

                    logger.LogInformation("Finished Seeding Default Data");
                    logger.LogInformation("Application Starting");
                }
                catch (Exception ex)
                {
                    logger.LogWarning(ex, "An error occurred seeding the DB");
                }
            }
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            //app.UseMvc();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseSession();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();
        }
    }
}




