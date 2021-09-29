using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MarksMovies.WebServices;

namespace MarksMovies
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            //services.AddDbContext<MarksMoviesContext>(options =>
            //        options.UseSqlServer(Configuration.GetConnectionString("MarksMoviesContext")));
            //services.AddScoped<IMarksMoviesContext>(provider => provider.GetService<MarksMoviesContext>());

            //services.AddScoped<IMovieDBAccess, MovieDBAccess>();
            //services.AddHttpClient<ITMDBapi, TMDBapi>();
            services.AddHttpClient<WebMovieIndexService>();
            services.AddHttpClient<WebCreateService>();
            services.AddHttpClient<WebDetailsService>();
            services.AddHttpClient<WebEditService>();
            services.AddHttpClient<WebDeleteService>();
            services.AddHttpClient<WebRankMovieService>();
            services.AddHttpClient<WebExportService>();



            //services.AddTransient<CreateService>();
            //services.AddTransient<EditService>();
            //services.AddTransient<DetailsService>();
            //services.AddTransient<DeleteService>();
            //services.AddTransient<MovieIndexService>();
            //services.AddTransient<RankMoviesService>();
            //services.AddTransient<ExportService>();

            //services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("v1", new OpenApiInfo { Title = "MarksMovies API", Version = "v1",
            //                    Description = "API for the MarksMovies .Net Core Web project"});

            //    // Set the comments path for the Swagger JSON and UI.
            //    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            //    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            //    c.IncludeXmlComments(xmlPath);
                
            //});

            

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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            //app.UseSwagger();

            //// Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            //// specifying the Swagger JSON endpoint.
            //app.UseSwaggerUI(c =>
            //{
            //    c.SwaggerEndpoint("/swagger/v1/swagger.json", "MarksMovies API V1");
            //});

            app.UseMvc();
        }
    }
}
