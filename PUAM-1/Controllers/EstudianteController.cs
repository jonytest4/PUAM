using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using PUAM_1.Models;
using System.Data;
using System.Security.Cryptography.Pkcs;

namespace PUAM_1.Controllers
{
    public class EstudianteController : Controller
    {
        string cadena = "Data Source=KALU;Initial Catalog=PUAM;Integrated Security=True;Encrypt=false";
        public IActionResult ContenidoE()
        {
            return View();
        }
        public ActionResult Salir()
        {
            return RedirectToAction("LoginEstudiante", "Acceso");
        }
        public ActionResult Registro()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Registro(Registro registro)
        {
            bool registrado;
            string mensaje;

            using (SqlConnection cn = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("sp_IngresarRegistro", cn);
                cmd.Parameters.AddWithValue("CedulaEstudiante",registro.CedulaEstudiante);
                cmd.Parameters.AddWithValue("Fecha",registro.Fecha);
                cmd.Parameters.AddWithValue("NumeroHoras",registro.NumeroHoras);
                cmd.Parameters.AddWithValue("CedulaAdulto",registro.CedulaAdulto);
                cmd.Parameters.AddWithValue("Evidencia",registro.Evidencia);
                cmd.Parameters.AddWithValue("Observaciones",registro.Observaciones);
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
                return RedirectToAction("Registro", "Estudiante");
            }
            else
            {
                return View();
            }
        }
    }
}
