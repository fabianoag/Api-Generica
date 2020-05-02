using APITeste.Repositorio.DAO;
using APITeste.Repositorio.UsuarioDAO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
#region REFERÊNCIAS ADICIONADAS
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using AutoMapper;
using APITeste.WebAPI.Helper;
#endregion

namespace IA.Escala.WebAPI
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
            //-- INICIO - CONFIGURAÇÃO DO 'DATA/DATACONTEXT.CS'
            // Data Source=Fabiano;Initial Catalog=ProAgil;Persist Security Info=True;User ID=sa;Password=teste1239
            services.AddDbContext<EntidadeContext>(
                    x => x.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
            );


            //services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);


            #region SERVIÇOS ADICIONADOS
                #region CONFIGURAÇÃO DE AUTENTICAÇÃO DA WEBAPI
                /* Esta parte permite configurar a autenticação do usuário.
                 * O recurso que ele usa e o JWT do 'NuGet Package Manager'
                 * ==> Microsoft.AspNetCore.Authentication.JWTBearer */
                services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                        .AddJwtBearer(options => {
                            options.TokenValidationParameters = new TokenValidationParameters
                            {
                                ValidateIssuerSigningKey = true,
                                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII
                                    .GetBytes(Configuration.GetSection("AppSettings:TokenKey").Value)),
                                ValidateIssuer = false,
                                ValidateAudience = false
                            };
                        });

                #endregion
                services.AddScoped<IUsuarioDAO, UsuarioDAO>();//Injetando o 'ProAgilRepository' em 'IProAgilRepository'
                //OBS: Ao vc fazer isto toda a vez que alguem usar o 'IProAgilRepository' estara usado os métodos de 'ProAgilRepository'.
                //Ao fazer isto o 'ProAgilContext' não esta mais sendo requisitado diretamente.

                //Para permitir o uso do cors
                services.AddCors();

                #region ESTA LINHA PARA A CONFIGURAÇÃO DA ROTA DA WEBAPI
                /* Aqui configura a rota da aplicação.
                 * Na linha 'AddJsonOptions' ele esta configurada para não repetir registro encontrados dentro
                 * das entidades.*/
                services.AddMvc(options => {
                    /* Aqui ele criar uma politica para toda a ver
                     * que for chamado uma rota dos métodos.
                     * 
                     * OBS = Esta tipo de recurso trabalha muito bem 
                     * com o identity EF.*/
                    var policy = new AuthorizationPolicyBuilder()
                        .RequireAuthenticatedUser()
                        .Build();

                    options.Filters.Add(new AuthorizeFilter(policy));
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions(opt => opt.SerializerSettings.ReferenceLoopHandling =
                                            Newtonsoft.Json.ReferenceLoopHandling.Ignore);
                #endregion

                #region CONFIGURAÇÃO DO AUTOMAPPER
                /* Aqui fica a configuração do 'AutoMapper' usado para
                 * mapear os campos.*/
                var mappingConfig = new MapperConfiguration(mc => {
                    mc.AddProfile(new AutoMapperProfile());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                services.AddSingleton(mapper);
                #endregion
            #endregion
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            #region REFERÊNCIAS ADICIONADAS
                #region HABILITA A AUTENTICAÇÃO
                /* Esta linha e usada para habilitar a autenticação de usuario colocada
                 * no 'ConfigureServices' adiciona no método 'services.AddMVC().*/
                app.UseAuthentication();
                #endregion
                app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            #endregion

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
