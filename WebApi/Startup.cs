using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Core.Base_Classes;
using Core.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Authorization;
using DAL;
using DAL.UnitOfWork;
using Business.ServiceLayers;

namespace WebApi
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


            string Authentication = Configuration.GetSection("MyCustomSettings").GetSection("Authentication").Value;

            switch (Authentication)
            {
                case "Basic":
                    services.AddAuthentication("BasicAuthentication");//.
                                                                      // AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);
                    break;
                case "Azure"://not tested yet
                             // services.AddAuthentication(AzureADDefaults.BearerAuthenticationScheme)
                             //.AddAzureADBearer(options => Configuration.Bind("AzureAd", options));
                    break;
                case "JWT":
                    //string jwtSecretKey = Configuration.GetSection("JWT_Settings").GetSection("Secret_Key").Value;
                    //var key = Encoding.ASCII.GetBytes(jwtSecretKey);

                    //services.AddAuthentication(x => {
                    //    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    //    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    //}).AddJwtBearer(x =>
                    //{
                    //    x.RequireHttpsMetadata = true;
                    //    x.SaveToken = true;
                    //    x.TokenValidationParameters = new TokenValidationParameters
                    //    {
                    //        ValidateIssuerSigningKey = true,
                    //        IssuerSigningKey = new SymmetricSecurityKey(key),
                    //        ValidateIssuer = false,
                    //        ValidateAudience = false

                    //    };

                    //});
                    break;
            }


            services.AddAuthorization(options =>
            {
                //options.AddPolicy("SuperAdmin",
                //  policy => policy
                //    .Requirements
                //    .Add(new SuperAdminOrNotRequirement(true)));

                //options.AddPolicy("JuniorAdmin",
                //  policy => policy
                //    .Requirements
                //    .Add(new SuperAdminOrNotRequirement(false)));
            });

            string Database = Configuration.GetSection("MyCustomSettings").GetSection("Database").Value;
            HygieneContext .DatabaseServer = Database;


            //the change in AddCors statment fixed the error when connecting using signalR
            //so now api requests and signalR requests both work without CORS problem
            services.AddCors(options => {

                options.AddPolicy("CorsPolicy", builder =>
                {
                    builder.SetIsOriginAllowed(origin => true).AllowAnyMethod().AllowAnyHeader().AllowCredentials();
                });
            });

            services.AddDbContext<HygieneContext>(options =>
            {
                //options.EnableSensitiveDataLogging();

                switch (Database)
                {
                    case "SqlServer":
                        options.UseSqlServer(Configuration.GetConnectionString("sqlserverConn"));
                        break;
                    case "MySql":
                        // options.UseMySql(Configuration.GetConnectionString("mySqlConn"));
                        break;
                }

            }
            );


            //services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            //    .AddEntityFrameworkStores<DataContext>();


            //services.AddIdentityCore<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            //    .AddRoles<IdentityRole>()////this is the change for RoleManager service id you need it (i did not use it)
            //    .AddEntityFrameworkStores<EcommerceContext>();





            services.AddControllers().AddNewtonsoftJson(o =>   // needs Microsoft.AspNetCore.Mvc.NewtonsoftJson from nugget
            {
                o.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;//fix refernce looping
                o.SerializerSettings.ContractResolver = new DefaultContractResolver();//to return the json as it is without converting the keys to lowercase
            }).ConfigureApiBehaviorOptions(options =>
            {
                options.SuppressModelStateInvalidFilter = true;// so i can return custom format of bad request if action's model's anotation errors exist
                //if i did not set this option , then if anotation error exist then the action will not be fired and i will get built in bad request message
            });
            //services.Configure<FormOptions>(o => {//for max uploaad
            //    o.ValueLengthLimit = int.MaxValue;
            //    o.MultipartBodyLengthLimit = int.MaxValue;
            //    o.MemoryBufferThreshold = int.MaxValue;
            //});


            //services.AddSignalR();
            //services.AddSignalR().AddNewtonsoftJsonProtocol(opt =>
            //{
            //    opt.PayloadSerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;//to fix recusrion problem in signalR
            //    opt.PayloadSerializerSettings.ContractResolver = new DefaultContractResolver();//to fix camel case problem

            //});

            //services.AddSingleton<IAuthorizationHandler, SuperAdminOrNotHandler>();

            services.AddScoped<IUnitOfWork<uowBasic>, uowBasic>();
            services.AddScoped<IServiceLayer<slBasic>, slBasic>();

            //services.AddScoped<IUnitOfWork<uowCustomer>, uowCustomer>();
            //services.AddScoped<IServiceLayer<slCustomer>, slCustomer>();


            //the next 2 lines work , they help me to inject ProductUOW directly to the controller not injecting the interface ,
            //but i am not sure is "context" is the same DataContext created or not
            //so i will still injecting the interrface as the previous line

            // var context = services.BuildServiceProvider().GetService<EcommerceContext>();
            //services.AddScoped<IUnitOfWork<ProductUOW>>(sp => new ProductUOW(context));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //app.Use(async (context, next) =>
            //{ //this middleware handlees the requests ( api requests , signalR requests)
            //    //the problem is that web socket requests (for signalR) not always have header for the token , sometimes the token is sent as query string
            //    //here we check both for signalR requests
            //    // i.e /hub/negotiate have the token in the header    while  /hub  have the token in the query string
            //    //i need to handle both cases so the token is always catched and converted to the aproperate user

            //    var path = context.Request.Path;
            //    if (path.ToString().Contains("/hub_"))// if signalR requests (i always use hub_xxx for paths of the hubs)
            //    {
            //        if (!context.Request.Headers.ContainsKey("Authorization") && !context.Request.QueryString.Value.Contains("access_token"))
            //        {
            //            await next();
            //            return;
            //        }
            //        var token = "";

            //        if (!context.Request.Headers.ContainsKey("Authorization"))
            //        {
            //            //  /hub_user request will enter here as it contains the token as query string not in the header
            //            string qs = context.Request.QueryString.Value;
            //            token = HttpUtility.ParseQueryString(qs)["access_token"];
            //        }
            //        else
            //        {
            //            //   /hub_user/negotiate  request will enter here , as it has the token in the header
            //            token = context.Request.Headers["Authorization"][0];
            //        }
            //        if (token.StartsWith("Bearer Basic "))
            //        {
            //            token = token.Replace("Bearer Basic ", "Basic ");//remove extra "Bearer"' keyword added by front end statment

            //        }
            //        if (token.StartsWith("Bearer Bearer "))
            //        {
            //            token = token.Replace("Bearer Bearer ", "Bearer ");//remove extra "Bearer"' keyword added by front end statment
            //        }
            //        context.Request.Headers.Add("Authorization", token);//add the correct toekn to the header
            //                                                            //, now the used authentication system ( Basic Authentication or  jwt authentication) can understand the token and reconize the 
            //                                                            //logged user


            //    }
            //    await next();
            //});

            app.UseStaticFiles();//


            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseRouting();
            app.UseCors("CorsPolicy");//must be between app.UseRouting(); , app.UseAuthorization();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapHub<UserHub>("/hub_user", options =>
                //{

                //});
                //endpoints.MapHub<GeneralHub>("/hub_general", options =>
                //{

                //});
                endpoints.MapControllers();


            });


        }
    }
}
