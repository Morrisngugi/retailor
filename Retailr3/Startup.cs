using System;
using System.IdentityModel.Tokens.Jwt;
using Core.Models;
using Core.Models.Configurations;
using Core.Models.IdentityModels;
using Core.Repositories;
using Core.Repositories.EntityRepositories;
using Core.Services;
using Hangfire;
using Hangfire.SqlServer;
using Infrastructure.Services;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Infrastructure.Repositories.EntityRepositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Retailr3.Helpers;
using Core.Models.EntityModel;
using Core.Services.EntityServices;
using Infrastructure.Services.EntityService;

namespace Notifyr
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //add http redirection configs
            services.AddHttpsRedirection(options =>
            {
                options.RedirectStatusCode = StatusCodes.Status308PermanentRedirect;
                options.HttpsPort = 443;
            });
            //configure hsts expiry age to one year
            services.AddHsts(options =>
            {
                options.IncludeSubDomains = true;
                options.MaxAge = TimeSpan.FromDays(366); //one 
                options.Preload = true;
            });
            services.AddIdentityServer();

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = "Cookies";
                options.DefaultChallengeScheme = "oidc";
            })
            .AddCookie("Cookies", options =>
            {
                
                options.SlidingExpiration = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(10);

                //added secure attribute to cookie
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                //options.Cookie.SecurePolicy = env.IsDevelopment() ? CookieSecurePolicy.None : CookieSecurePolicy.Always;

                //set httponlyflag for cookie to true to prevent client script from accessing the contents of the cookie over insecure channels
                options.Cookie.HttpOnly = true;

            })
            .AddOpenIdConnect("oidc", options =>
            {
                options.SignInScheme = "Cookies";
                options.Authority = Configuration["AppConfigs:Authority"];
                options.RequireHttpsMetadata = false;
                options.ClientId = Configuration["AppConfigs:ClientId"];
                options.ClientSecret = Configuration["AppConfigs:ClientSecret"];
                options.ResponseType = "code id_token";
                options.SaveTokens = true;
                options.GetClaimsFromUserInfoEndpoint = true;
                options.SignedOutRedirectUri = Configuration["AppConfigs:PayrBaseUrl"] + "account/landing";
                options.Scope.Add("roles");
                options.Scope.Add("api1");
                options.Scope.Add("profile");
                options.Scope.Add("custom.profile");
                //options.Scope.Add("offline_access");
                options.ClaimActions.MapJsonKey("website", "website");
                options.ClaimActions.MapJsonKey("role", "role", "role");
                options.ClaimActions.MapJsonKey("DisplayName", "DisplayName");
                options.TokenValidationParameters.NameClaimType = "DisplayName";
                options.TokenValidationParameters.RoleClaimType = "role";
                options.Events.OnTicketReceived = async (context) =>
                {
                   context.Properties.ExpiresUtc = DateTime.UtcNow.AddMinutes(10);
                };
            });

            services.AddHangfireServer(options =>
            {
                options.Queues = new[] { "queryqueue", "subunsubqueue", "sendingsubunsubsms", "sendingqueuedsms", "savingqueuedsms", "sendingsms" };
            });


            services.AddHangfire(options =>
                options.UseSqlServerStorage(Configuration["ConnectionStrings:HangfireConnection"], new SqlServerStorageOptions { QueuePollInterval = TimeSpan.FromSeconds(1) })
                );

            services.AddDbContext<IdentityDataContext>(options =>
            options.UseSqlServer(Configuration["ConnectionStrings:IdentityDataContext"],
                providerOptions => providerOptions.EnableRetryOnFailure()));


            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration["ConnectionStrings:DbConnection"],
                providerOptions => providerOptions.EnableRetryOnFailure()));

            services.AddDbContext<LogDbContext>(options =>
                options.UseSqlServer(Configuration["ConnectionStrings:LogDbConnection"],
                providerOptions => providerOptions.EnableRetryOnFailure()));

            services.AddDbContext<EntityDbContext>(options =>
                options.UseSqlServer(Configuration["ConnectionStrings:EntityDbConnection"],
                providerOptions => providerOptions.EnableRetryOnFailure()));

            services.AddTransient<ILogRepository<EwsLog>, LogRepository>();
            services.AddTransient<ITierRepository, TierRepository>();
            services.AddTransient<ICatalogRepository, CatalogRepository>();
            services.AddTransient<IEntityTypeRepository, EntityTypeRepository>();
            services.AddTransient<IBrandRepository, BrandRepository>();
            services.AddTransient<IVatCategoryRepository, VatCategoryRepository>();
            services.AddTransient<IPackagingRepository, PackagingRepository>();
            services.AddTransient<IPackagingTypeRepository, PackagingTypeRepository>();
            services.AddTransient<ISubCategoryRepository, SubCategoryRepository>();
            services.AddTransient<ISubBrandRepository, SubBrandRepository>();
            services.AddTransient<IVatRepository, VatRepository>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IUnitOfMeasureTypeRepository, UnitOfMeasureTypeRepository>();
            services.AddTransient<IUnitOfMeasureRepository, UnitOfMeasureRepository>();
            services.AddTransient<IBaseRepository<UnitOfMeasureType>, BaseRepository<UnitOfMeasureType>>();
            services.AddTransient<IBaseRepository<UnitOfMeasure>, BaseRepository<UnitOfMeasure>>();
            services.AddTransient<IBaseRepository<Tier>, BaseRepository<Tier>>();
            services.AddTransient<IBaseRepository<Catalog>, BaseRepository<Catalog>>();
            services.AddTransient<IBaseRepository<Brand>, BaseRepository<Brand>>();
            services.AddTransient<IBaseRepository<VatCategory>, BaseRepository<VatCategory>>();
            services.AddTransient<IBaseRepository<Packaging>, BaseRepository<Packaging>>();
            services.AddTransient<IBaseRepository<Pricing>, BaseRepository<Pricing>>();
            services.AddTransient<IBaseRepository<PackagingType>, BaseRepository<PackagingType>>();
            services.AddTransient<IBaseRepository<SubBrand>, BaseRepository<SubBrand>>();
            services.AddTransient<IBaseRepository<SubCategory>, BaseRepository<SubCategory>>();
            services.AddTransient<IBaseRepository<Vat>, BaseRepository<Vat>>();
            services.AddTransient<IBaseRepository<Category>, BaseRepository<Category>>();


            services.AddTransient<IBaseEntityRepository<Anchor>, BaseEntityRepository<Anchor>>();
            services.AddTransient<IBaseEntityRepository<Merchant>, BaseEntityRepository<Merchant>>();
            services.AddTransient<IBaseEntityRepository<Consumer>, BaseEntityRepository<Consumer>>();
            services.AddTransient<IBaseEntityRepository<Supplier>, BaseEntityRepository<Supplier>>();
            services.AddTransient<IBaseEntityRepository<Country>, BaseEntityRepository<Country>>();
            services.AddTransient<IBaseEntityRepository<Region>, BaseEntityRepository<Region>>();
            services.AddTransient<IBaseEntityRepository<ContactPerson>, BaseEntityRepository<ContactPerson>>();
            services.AddTransient<IBaseEntityRepository<Setting>, BaseEntityRepository<Setting>>();
            services.AddTransient<IBaseEntityRepository<Address>, BaseEntityRepository<Address>>();
            services.AddTransient<IBaseEntityRepository<EntityType>, BaseEntityRepository<EntityType>>();



            services.AddTransient<ICountryRepository, CountryRepository>();
            services.AddTransient<IRegionRepository, RegionRepository>();
            services.AddTransient<IAddressRepository, AddressRepository>();
            services.AddTransient<IContactPersonRepository, ContactPersonRepository>();

            //Entity
            services.AddTransient<IAnchorRepository, AnchorRepository>();
            services.AddTransient<ISupplierRepository, SupplierRepository>();
            services.AddTransient<IMerchantRepository, MerchantRepository>();
            services.AddTransient<IConsumerRepository, ConsumerRepository>();

            services.AddTransient<ISmsSenderJob, SmsSenderJob>();
            services.AddTransient<IPasswordGeneratorService, PasswordGeneratorService>();
            services.AddTransient<ICodeGeneratorService, CodeGeneratorService>();
            services.AddTransient<ILogService, LogService>();
            services.AddTransient<ITierService, TierService>();
            services.AddTransient<ICatalogService, CatalogService>();
            services.AddTransient<IEntityTypeService, EntityTypeService>();
            services.AddTransient<IBrandService, BrandService>();
            services.AddTransient<IPricingService, PricingService>();
            services.AddTransient<IVatCategoryService, VatCategoryService>();
            services.AddTransient<IPackagingService, PackagingService>();
            services.AddTransient<IPackagingTypeService, PackagingTypeService>();
            services.AddTransient<ISubBrandService, SubBrandService>();
            services.AddTransient<ISubCategoryService, SubCategoryService>();
            services.AddTransient<IVatService, VatService>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<IUnitOfMeasureTypeService, UnitOfMeasureTypeService>();
            services.AddTransient<IUnitOfMeasureService, UnitOfMeasureService>();
            services.AddTransient<IAnchorService, AnchorService>();
            services.AddTransient<ISupplierService, SupplierService>();
            services.AddTransient<IMerchantService, MerchantService>();
            services.AddTransient<IConsumerService, ConsumerService>();

            services.AddTransient<ICountryService, CountryService>();
            //services.AddTransient<IContactPersonService, ContactPersonService>();
            services.AddTransient<IRegionService, RegionService>();
            //services.AddTransient<ISettingService, SettingSe>();
            //services.AddTransient<IAddressService, AddressService>();

            //Entity
            services.AddTransient<ISessionUserService, SessionUserService>();

            services.AddIdentityCore<ApplicationUser>()
            .AddRoles<IdentityRole>()
            .AddClaimsPrincipalFactory<UserClaimsPrincipalFactory<ApplicationUser, IdentityRole>>()
            .AddEntityFrameworkStores<IdentityDataContext>()
            .AddDefaultTokenProviders()
            .AddDefaultUI();

            // Add functionality to inject IOptions<T>
            services.AddOptions();

            // Add our Config object so it can be injected
            services.Configure<AppConfig>(Configuration.GetSection("AppConfigs"));
            services.Configure<SmsProvider>(Configuration.GetSection("SmsProviders"));

            services.AddMvc(options =>
            {
                options.CacheProfiles.Add("Default30",
                    new CacheProfile()
                    {
                        Duration = 30
                    });
                options.CacheProfiles.Add("NoCache",
                    new CacheProfile()
                    {
                        Duration = 0,
                        NoStore = true,
                        Location = ResponseCacheLocation.None
                    });
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2).AddJsonOptions(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHangfireServer();
            app.UseHangfireDashboard();

            //===================
            //app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            //api authentication
            app.UseWhen(x => (x.Request.Path.StartsWithSegments("/api", StringComparison.OrdinalIgnoreCase)),
            builder =>
            {
                builder.UseMiddleware<ApiAuthenticationMiddleware>();
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    // template: "{controller=Home}/{action=Index}/{id?}");
					template: "{controller=Account}/{action=Landing}/{id?}");
            });
        }
    }
}
