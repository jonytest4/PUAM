using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using PUAM_1.Models;
using System.Data;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace PUAM_1.Controllers
{
    public class AccesoController : Controller
    {
        //string cadena = "Data Source=KALU\\SQLEXPRESS;Initial Catalog=PUAM;Integrated Security=True;Encrypt=false";
        string cadena = "Data Source=KALU;Initial Catalog=PUAM;Integrated Security=True;Encrypt=false";
        public IActionResult Index()
        {
            return View();
        }
        public ActionResult LoginAdulto()
        {
            return View();
        }
        public ActionResult LoginEstudiante()
        {
            return View();
        }

        public ActionResult RegistrarAdulto()
        {
            List<Programa> lst = null;
            using (Models.PuamContext db = new Models.PuamContext())
            {
                lst = (from d in db.Programas
                       select new Programa
                       {
                           IdPrograma = d.IdPrograma,
                           Programa1= d.Programa1
                       }).ToList();
            }

            //Lista para la vista
            List<SelectListItem> items = lst.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.Programa1.ToString(),
                    Value = d.IdPrograma.ToString(),
                    Selected = false
                };
            });
            ViewBag.Items = items;
            return View();
        }
        public ActionResult RegistrarEstudiante()
        {
            List<Carrera> lst = null;
            using (Models.PuamContext db = new Models.PuamContext())
            {
                lst = (from d in db.Carreras
                       select new Carrera
                       {
                           IdCarrera = d.IdCarrera,
                           NombreCarrera  = d.NombreCarrera
                       }).ToList();
            }

            //Lista para la vista
            List<SelectListItem> items = lst.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.NombreCarrera.ToString(),
                    Value = d.IdCarrera.ToString(),
                    Selected = false
                };
            });
            ViewBag.Items = items;
            return View();
        }
        
        public ActionResult ContenidoE()
        {
            return View();
        }
       
      

        [HttpPost]
        public ActionResult RegistrarAdulto(Adulto adulto)
        {
            bool registrado;
            string mensaje;

            if (adulto.Clave == adulto.ConfirmarClave)
            {
                adulto.Clave = ConvertirSha256(adulto.Clave);
            }
            else
            {
                //viewdata envia datos del controlador hacia la vista
                ViewData["Mensaje"] = "Las contraseñas no coinciden";
                return View();
            }

            //Conexion a base de datos
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("sp_RegistrarAdulto", cn);
                cmd.Parameters.AddWithValue("Nombre", adulto.Nombres);
                cmd.Parameters.AddWithValue("Apellido", adulto.Apellidos);
                cmd.Parameters.AddWithValue("Cedula", adulto.Cedula);
                cmd.Parameters.AddWithValue("Edad", adulto.Edad);
                cmd.Parameters.AddWithValue("Celular", adulto.Celular);

                cmd.Parameters.AddWithValue("IdPrograma", adulto.IdPrograma);


                cmd.Parameters.AddWithValue("Clave", adulto.Clave);
                cmd.Parameters.Add("Registrado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;

                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();
                cmd.ExecuteNonQuery();
                registrado = Convert.ToBoolean(cmd.Parameters["Registrado"].Value);
                mensaje = cmd.Parameters["Mensaje"].Value.ToString();
            }
            ViewData["Mensaje"] = mensaje;
            if (registrado)
            {
                //Login= nombre de la vista ,Acceso= nombre del controlador
                return RedirectToAction("LoginAdulto", "Acceso");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult LoginAdulto(Adulto adulto)
        {
            //encripto la clave
            adulto.Clave = ConvertirSha256(adulto.Clave);
            //Conectarse a la base de datos
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("sp_ValidarAdulto", cn);
                cmd.Parameters.AddWithValue("Cedula", adulto.Cedula);
                cmd.Parameters.AddWithValue("Clave", adulto.Clave);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                adulto.IdAdultos = Convert.ToInt32(cmd.ExecuteScalar().ToString());
            }

            if (adulto.IdAdultos != 0)
            {
                //revisar el nombre
                //Session["Adulto"] = adulto;
                //primero el nombre del metodo de la vista  y segundo el del controlador
                //CAMBIAR LA VISTA AQUI

                //return RedirectToAction("Index", "Home");
                return RedirectToAction("ContenidoA", "Adulto");
            }
            else
            {
                ViewData["Mensaje"] = "Usuario no encontrado";
                return View();
            }
        }
        [HttpPost]

        public ActionResult RegistrarEstudiante(Estudiante estudiante)
        {
            bool registrado;
            string mensaje;

            if (estudiante.Clave == estudiante.ConfirmarClave)
            {
                estudiante.Clave = ConvertirSha256(estudiante.Clave);
            }
            else
            {
                //viewdata envia datos del controladorhacia la vista
                ViewData["Mensaje"] = "Las contraseñas no coinciden";
                return View();
            }

            using (SqlConnection cn = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("sp_RegistrarEstudiante",cn);
                cmd.Parameters.AddWithValue("Nombre",estudiante.Nombres);
                cmd.Parameters.AddWithValue("Apellido", estudiante.Apellidos);
                cmd.Parameters.AddWithValue("Cedula", estudiante.Cedula);

                cmd.Parameters.AddWithValue("Carrera", estudiante.Carrera);


                cmd.Parameters.AddWithValue("Semestre", estudiante.Semestre);
                cmd.Parameters.AddWithValue("Coordinador", estudiante.Coordinador);
                cmd.Parameters.AddWithValue("Clave", estudiante.Clave);
                cmd.Parameters.Add("Registrado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;

                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();
                cmd.ExecuteNonQuery();
                registrado = Convert.ToBoolean(cmd.Parameters["Registrado"].Value);
                mensaje = cmd.Parameters["Mensaje"].Value.ToString();
            }
            ViewData["Mensaje"] = mensaje;
            if (registrado)
            {
                //Login=nombre de la vista, Acceso = nombre del controlado
                return RedirectToAction("LoginEstudiante","Acceso");
            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        public ActionResult LoginEstudiante(Estudiante estudiante)
        {
            estudiante.Clave = ConvertirSha256(estudiante.Clave);
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("sp_ValidarEstudiante",cn);
                cmd.Parameters.AddWithValue("Cedula", estudiante.Cedula);
                cmd.Parameters.AddWithValue("Clave",estudiante.Clave);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                estudiante.IdEstudiante = Convert.ToInt32(cmd.ExecuteScalar().ToString());
            }
            if(estudiante.IdEstudiante != 0)
            {
                return RedirectToAction("ContenidoE","Estudiante");
            }
            else
            {
                ViewData["Mensaje"] = "Estudiante no encontrado";
                return View();
            }
        }


        public static string ConvertirSha256(string texto)
        {
            StringBuilder sb = new StringBuilder();
            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;
                byte[] result = hash.ComputeHash(enc.GetBytes(texto));

                foreach (byte b in result)
                    sb.Append(b.ToString("x2"));
            }
            return sb.ToString();
        }

        public ActionResult Salir()
        {
            return RedirectToAction("LoginAdulto", "Acceso");
        }
    }
}
