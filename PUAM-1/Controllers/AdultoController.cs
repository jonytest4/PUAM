using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using PUAM_1.Models;
using System.Data;

namespace PUAM_1.Controllers
{
    public class AdultoController : Controller
    {
        string cadena = "Data Source=KALU;Initial Catalog=PUAM;Integrated Security=True;Encrypt=false";
        public ActionResult ContenidoA()
        {
            return View();
        }
        public ActionResult Clases()
        {
            List<Clase> lst = null;
            using (Models.PuamContext db = new Models.PuamContext())
            {
                lst =(from d in db.Clases
                     select new Clase
                     {
                         IdClase = d.IdClase,
                         NombreClase=d.NombreClase
                     }).ToList();
            }

            //Lista para la vista
            List<SelectListItem> items = lst.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.NombreClase.ToString(),
                    Value = d.IdClase.ToString(),
                    Selected = false
                };
            });
            ViewBag.Items = items;
                return View();
        }
        public ActionResult Nosotros()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Inscripcion(Inscripcion inscripcion)
        {
            bool inscrito;
            string mensaje;

            using (SqlConnection cn = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("sp_IngresarInscripcion", cn);

                cmd.Parameters.AddWithValue("CedulaAdulto",inscripcion.CedulaAdulto);
                
                cmd.Parameters.AddWithValue("idClase",inscripcion.IdClase);

                cmd.Parameters.Add("Registrado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;

                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();
                cmd.ExecuteNonQuery();
                inscrito = Convert.ToBoolean(cmd.Parameters["Registrado"].Value);
                mensaje = cmd.Parameters["Mensaje"].Value.ToString();
            }
            ViewData["Mensaje"] = mensaje;
            if (inscrito)
            {
                //Login= nombre de la vista ,Acceso= nombre del controlador
                return RedirectToAction("Clases", "Adulto");
            }
            else
            {
                return RedirectToAction("Clases","Adulto");
            }
        }
    }
}
