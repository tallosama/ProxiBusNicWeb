﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using ProxiBusNicWeb.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
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
        public int AgregarParada(ParadasWS paradasWS)
        {
            Parada parada = new Parada();

        parada.Descripcion = paradasWS.Descripcion;
            parada.Alias =paradasWS.Alias;
            if (paradasWS.FotoParada != null)
                parada.FotoParada = paradasWS.FotoParada;
            parada.Estado = paradasWS.Estado;
            parada.Latitud=paradasWS.Latitud;
            parada.Longitud=paradasWS.Longitud;
            parada.FechaCreacion = paradasWS.FechaCreacion;
            parada.UsuarioCreacion = paradasWS.UsuarioCreacion;
            parada.FechaModificacion = paradasWS.FechaModificacion;
            parada.UsuarioModificacion = paradasWS.UsuarioModificacion;

            db.Paradas.Add(parada);
            db.SaveChanges();
            return parada.Id;

        }

        [WebMethod]
        public List<ParadasWS> ListarParada()
        {


            return db.Paradas.Select(p => new ParadasWS()
            {

                Id = p.Id,
                Descripcion = p.Descripcion,
                Alias = p.Alias,
                FotoParada = p.FotoParada,
                Estado = p.Estado,
                Longitud = p.Longitud,
                Latitud = p.Latitud,
                FechaCreacion = p.FechaCreacion,
                UsuarioCreacion = p.UsuarioCreacion,
                FechaModificacion = p.FechaModificacion,
                UsuarioModificacion = p.UsuarioModificacion

            }).ToList();

        }

        [WebMethod]
        public List<ParadasWS> ListarParadaActivas()
        {


            return db.Paradas.Where(p=>p.Estado).Select(p => new ParadasWS()
            {

                Id = p.Id,
                Descripcion = p.Descripcion,
                Alias = p.Alias,
                FotoParada = p.FotoParada,
                Estado = p.Estado,
                Longitud = p.Longitud,
                Latitud = p.Latitud,
                FechaCreacion = p.FechaCreacion,
                UsuarioCreacion = p.UsuarioCreacion,
                FechaModificacion = p.FechaModificacion,
                UsuarioModificacion = p.UsuarioModificacion

            }).ToList();

        }

        [WebMethod]
        public List<BusWS> ListarBus()
        {


            return db.Buses.Select(b => new BusWS()
            {

                Id = b.Id,
                NumeroRuta = b.NumeroRuta,
                Estado = b.Estado,
                FotoBus = b.FotoBus,

                FechaCreacion = b.FechaCreacion,
                UsuarioCreacion = b.UsuarioCreacion,
                FechaModificacion = b.FechaModificacion,
                UsuarioModificacion = b.UsuarioModificacion

            }).ToList();

        }
        [WebMethod]
        public List<BusWS> ListarBusActivos()
        {


            return db.Buses.Where(b=>b.Estado).Select(b => new BusWS()
            {

                Id = b.Id,
                NumeroRuta = b.NumeroRuta,
                Estado = b.Estado,
                FotoBus = b.FotoBus,

                FechaCreacion = b.FechaCreacion,
                UsuarioCreacion = b.UsuarioCreacion,
                FechaModificacion = b.FechaModificacion,
                UsuarioModificacion = b.UsuarioModificacion

            }).ToList();

        }

        [WebMethod]
        public List<BusParadaWS> ListarBusParada()
        {


            return db.BusParadas.Select(b => new BusParadaWS()
            {
                Id = b.Id,
                ParadaId = b.ParadaId,
                BusId = b.BusId
            }).ToList();

        }
        [WebMethod]
        public List<UsuariosWS> ListarUsuarios()
        {

            ApplicationDbContext context = new ApplicationDbContext();
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            return userManager.Users.ToList().Select(b => new UsuariosWS()
            {
                Id = b.Id,
                Correo = b.Email,
                Clave = b.PasswordHash
            }).ToList();

        }

        [WebMethod]
        public int EditarBusParadas(BusParadaWS busParadaWs)
        {


            BusParada busParada = new BusParada();
            busParada.BusId = busParadaWs.BusId;
            busParada.Id = busParadaWs.Id;
            busParada.ParadaId = busParadaWs.ParadaId;

            db.Entry(busParada).State = EntityState.Modified;

            return db.SaveChanges();

        }
        [WebMethod]
        public int EditarBus(BusWS busWs)
        {


            Bus bus = new Bus();
            bus.NumeroRuta = busWs.NumeroRuta;
            bus.Estado = busWs.Estado;
            bus.FotoBus = busWs.FotoBus;
            bus.FechaModificacion = busWs.FechaModificacion;
            bus.UsuarioModificacion = busWs.UsuarioModificacion;
             
            db.Entry(bus).State = EntityState.Modified;

            return db.SaveChanges();

        }
        [WebMethod]
        public int EditarParadas(ParadasWS ParadasWs)
        {
             
            Parada parada = new Parada();
            parada.Id = ParadasWs.Id;
            parada.Descripcion = ParadasWs.Descripcion;
            parada.Alias = ParadasWs.Alias;
            parada.FotoParada = ParadasWs.FotoParada;
            parada.Estado = ParadasWs.Estado;
            parada.Latitud = ParadasWs.Latitud;
            parada.Longitud= ParadasWs.Longitud;
            parada.UsuarioModificacion = ParadasWs.UsuarioModificacion;
            parada.FechaModificacion = ParadasWs.FechaModificacion;
             

        db.Entry(parada).State = EntityState.Modified;

            return db.SaveChanges();

        }

        [WebMethod]
        public int AgregarSugerencia(SugerenciaWS sugerenciaWS)
        {
            Sugerencia sugerencia = new Sugerencia();

            sugerencia.DescripcionSugerencia = sugerenciaWS.DescripcionSugerencia;
            sugerencia.ParadaId = sugerenciaWS.ParadaId;
            sugerencia.FechaCreacion = DateTime.Now;
            sugerencia.UsuarioCreacion = sugerenciaWS.UsuarioCreacion;
        
            db.Sugerencias.Add(sugerencia);
            db.SaveChanges();
            return sugerencia.Id;

        }
        [WebMethod]
        public int AgregarBusParadas(BusParadaWS busParadaWS)
        {
            BusParada busParada = new BusParada();

            busParada.BusId = busParadaWS.BusId;
            busParada.ParadaId = busParadaWS.BusId;

            db.BusParadas.Add(busParada);
            db.SaveChanges();
            

                return busParada.Id;
            
        }
        [WebMethod]
        public int AgregarBus(BusWS busWS)
        {
            Bus bus = new Bus();
            bus.NumeroRuta = busWS.NumeroRuta;
            bus.Estado = busWS.Estado;
            if (busWS.FotoBus != null)
            {
                bus.FotoBus = busWS.FotoBus;
            }
            bus.FechaCreacion = DateTime.Now;
            bus.UsuarioCreacion = busWS.UsuarioCreacion;

            bus.FechaModificacion = DateTime.Now;
            bus.UsuarioModificacion = busWS.UsuarioModificacion;


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
            ResultadoSW resultadoSW = new ResultadoSW();
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
                else
                {
                    resultadoSW.mensaje = "No se ha podido crear el usuario, rectifique los campos";

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
        public ResultadoSW CrearUsuarioRol(string Email, string Pass, string rol)
        {
            ResultadoSW resultadoSW = new ResultadoSW();
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
                    if (rol.Equals("Administrador"))
                    {
                        if (!manejadorRol.RoleExists("Administrador"))
                        {
                            manejadorRol.Create(new IdentityRole("Administrador"));
                        }

                        UserManager.AddToRole(user.Id, "Administrador");

                        resultadoSW.mensaje = "Se ha creado el usuario satisfactoriamente";
                        resultadoSW.respuesta = true;
                    }
                    else if (rol.Equals("Intrama"))
                    {
                        if (!manejadorRol.RoleExists("Intrama"))
                        {
                            manejadorRol.Create(new IdentityRole("Intrama"));
                        }

                        UserManager.AddToRole(user.Id, "Intrama");

                        resultadoSW.mensaje = "Se ha creado el usuario satisfactoriamente";
                        resultadoSW.respuesta = true;
                    }
                   
                }
                else
                {
                    resultadoSW.mensaje = "No se ha podido crear el usuario, rectifique los campos";

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
        public class UsuariosWS
        {
            public string Id { get; set; }
            public string Correo { get; set; }
            public string Clave { get; set; }

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
