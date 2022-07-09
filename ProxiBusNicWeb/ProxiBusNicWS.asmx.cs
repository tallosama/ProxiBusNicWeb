using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using ProxiBusNicWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Services;

namespace ProxiBusNicWeb
{
    /// <summary>
    /// Descripción breve de ProxiBusNicWS
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class ProxiBusNicWS : System.Web.Services.WebService
    {
        ProxiBusNicEntityContainer db = new ProxiBusNicEntityContainer();

        [WebMethod]
        public int AgregarParada(string descripcion, string alias, byte[] foto, bool estado, string usuario)
        {
            Parada parada = new Parada();

            parada.Descripcion = descripcion;
            parada.Alias = alias;
            if (foto != null)
                parada.FotoParada = foto;
            parada.Estado = estado;

            parada.FechaCreacion = DateTime.Now;
            parada.UsuarioCreacion = usuario;

            parada.FechaModificacion = DateTime.Now;
            parada.UsuarioModificacion = usuario;

            db.Paradas.Add(parada);
            db.SaveChanges();
            return parada.Id;

        }

        [WebMethod]
        public int BuscarParada(string descripcion, string alias, byte[] foto, bool estado, string usuario)
        {
            Parada parada = new Parada();

            parada.Descripcion = descripcion;
            parada.Alias = alias;
            if (foto != null)
                parada.FotoParada = foto;
            parada.Estado = estado;

            parada.FechaCreacion = DateTime.Now;
            parada.UsuarioCreacion = usuario;

            parada.FechaModificacion = DateTime.Now;
            parada.UsuarioModificacion = usuario;

            db.Paradas.Add(parada);
            db.SaveChanges();
            return parada.Id;

        }


        [WebMethod]
        public int AgregarSugerencia(string descripcion, string usuario, int idParada)
        {
            Sugerencia sugerencia = new Sugerencia();

            sugerencia.DescripcionSugerencia = descripcion;
            sugerencia.ParadaId = idParada;


            sugerencia.FechaCreacion = DateTime.Now;
            sugerencia.UsuarioCreacion = usuario;


            db.Sugerencias.Add(sugerencia);
            db.SaveChanges();
            return sugerencia.Id;

        }
        [WebMethod]
        public bool AgregarBusParadas(int idBus, int idParada)
        {
            BusParada busParada = new BusParada();

            busParada.BusId = idBus;
            busParada.ParadaId = idParada;

            db.BusParadas.Add(busParada);
            if (db.SaveChanges() > 0)
            {

                return true;
            }
            else
                return false;
        }
        [WebMethod]
        public int AgregarBus(string numRuta, bool estado, string usuario)
        {
            Bus bus = new Bus();
            bus.NumeroRuta = numRuta;
            bus.Estado = estado;

            bus.FechaCreacion = DateTime.Now;
            bus.UsuarioCreacion = usuario;

            bus.FechaModificacion = DateTime.Now;
            bus.UsuarioModificacion = usuario;

            db.Buses.Add(bus);
            db.SaveChanges();
            return bus.Id;

        }
  
        [WebMethod]
        public bool CambiarClave(string correo, string Pass)
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            var resultado = userManager.Users.Where(u => u.Email.Equals(correo)).FirstOrDefault();
            if (resultado == null)
            {
                return false;
            }
            else
            {
                userManager.RemovePassword(resultado.Id);
                userManager.AddPassword(resultado.Id, Pass);
                    return true;
            }
            
        }


        [WebMethod]
        public ResultadoSW CrearUsuario(string Email, string Pass)
        {
            ResultadoSW resultadoSW= new ResultadoSW();
            ApplicationDbContext context = new ApplicationDbContext();
            //string resultado = "";
        
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var validarUsuario = UserManager.Users.Where(u => u.Email.Equals(Email)).FirstOrDefault();

            if (validarUsuario == null)
            {

                var user = new ApplicationUser();
                user.UserName = Email;
                user.Email = Email;
                string userPWD = Pass;

                var chkUser = UserManager.Create(user, userPWD);

                if (chkUser.Succeeded)
                {
                    var manejadorRol = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
                    if (!manejadorRol.RoleExists("UsuarioAnonimo"))
                    {
                        manejadorRol.Create(new IdentityRole("UsuarioAnonimo"));
                    }

                     UserManager.AddToRole(user.Id, "UsuarioAnonimo");

                    resultadoSW.mensaje = "Se ha creado el usuario satisfactoriamente";
                    resultadoSW.respuesta = true;
                }
                else{
                    resultadoSW.mensaje = "No se ha podido crear el usuario";
                  
                    resultadoSW.respuesta = false;
                }

            }
            else
            {
                resultadoSW.mensaje = "El correo ingresado ya existe";
                resultadoSW.respuesta = false;
            }
             return resultadoSW;
         
        }

        [WebMethod]
        public bool Login(string cuenta, string Pass)
        {
            return Validar(cuenta, Pass);
        }

        private bool Validar(string cuenta, string Pass)
        {

            var result = HttpContext.Current.GetOwinContext().Get<ApplicationSignInManager>().PasswordSignIn(cuenta, Pass, false, false);
           
            if (result == SignInStatus.Success)
            {
                return true;
            }
            else
                return false;
        }
        public class ResultadoSW
        {
            public string mensaje;
            public bool respuesta;
        }
        public class BusParadaWS
        {
            public int Id { get; set; }
            public int ParadaId { get; set; }
            public int BusId { get; set; }
        }
            public class SugerenciaWS{
            public int Id { get; set; }
            public string DescripcionSugerencia { get; set; }
            public string UsuarioCreacion { get; set; }
            public System.DateTime FechaCreacion { get; set; }
            public int ParadaId { get; set; }
        }

        public class BusWS{
            public int Id { get; set; }
            public string NumeroRuta { get; set; }
            public bool Estado { get; set; }
            public byte[] FotoBus { get; set; }
            public System.DateTime FechaCreacion { get; set; }
            public string UsuarioCreacion { get; set; }
            public System.DateTime FechaModificacion { get; set; }
            public string UsuarioModificacion { get; set; }
        }

        public class ParadasWS
        {
            public int Id { get; set; }
            public string Descripcion { get; set; }
            public string Alias { get; set; }
            public byte[] FotoParada { get; set; }
            public bool Estado { get; set; }
            public string Longitud { get; set; }
            public string Latitud { get; set; }
            public System.DateTime FechaCreacion { get; set; }
            public string UsuarioCreacion { get; set; }
            public System.DateTime FechaModificacion { get; set; }
            public string UsuarioModificacion { get; set; }
        }
    }
}
