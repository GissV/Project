using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using System.Data;//Identificar el tipo de objeto a manipular en base de datos
using Microsoft.Data.SqlClient;//Controlador de acceso a datos

namespace WebApplication1.Controllers
{
    public class DatosController : Controller
    {
        public IConfiguration Configuration { get; }

        public DatosController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }
        // GET: DatosGenerales
        [HttpGet]
        [Route("Home/Datos")]
        public IActionResult Datos()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Datos(Data clsData)
        {
            if (ModelState.IsValid)
            {
                string connectionString = Configuration["ConnectionStrings:DefaultConnection"];
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string sql = $"Insert Into DATOS_GENERALES(direccion,edad,telefono, id_usuario) Values('{clsData.direccion}', '{clsData.edad}', '{clsData.telefono}', '{clsData.id_usuario}')";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.CommandType = CommandType.Text;

                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                    return RedirectToAction("Index");
                }
            }
            else
                return View("Views/Home/Index.cshtml");
        }

        // GET: DatosGenerales/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DatosGenerales/Create
       

        // POST: DatosGenerales/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: DatosGenerales/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DatosGenerales/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View("Index");
            }
        }

        // GET: DatosGenerales/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DatosGenerales/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
