using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using ProxiBusNicWeb.Models;

[assembly: OwinStartupAttribute(typeof(ProxiBusNicWeb.Startup))]
namespace ProxiBusNicWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            creacionRoles();
        }
        public void creacionRoles()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var manejadorRol = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
                if (!manejadorRol.RoleExists("Administrador"))
                {
                    // IdentityResult resultado = manejadorRol.Create(new IdentityRole("Administrador"));
                    manejadorRol.Create(new IdentityRole("Administrador"));

                    var manejadorUsuario = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                    var user = new ApplicationUser();
                    user.UserName = "admin@gmail.com";
                    user.Email = "admin@gmail.com";
                    string pass = "12345678";
                    
                    var t=manejadorUsuario.Create(user, pass);
                    if(t.Succeeded)
                    manejadorUsuario.AddToRole(user.Id, "Administrador");

                }
                if (!manejadorRol.RoleExists("Intrama"))
                {
                    // IdentityResult resultado = manejadorRol.Create(new IdentityRole("Administrador"));
                    manejadorRol.Create(new IdentityRole("Intrama"));

                    var manejadorUsuario = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                    var user = new ApplicationUser();
                    user.UserName = "intrama@gmail.com";
                    user.Email = "intrama@gmail.com";
                    string pass = "12345678";
                    manejadorUsuario.Create(user, pass);
                    var t = manejadorUsuario.Create(user, pass);
                    if (t.Succeeded)
                        manejadorUsuario.AddToRole(user.Id, "Intrama");

                }

            }

        }
    }
}
