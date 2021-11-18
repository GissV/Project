using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Diagnostics;
using WebApplication1.Models;
using SelectPdf;
using PdfDocument = SelectPdf.PdfDocument;
using System;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {

        const string SessionUser = "_User";
        private readonly ILogger<HomeController> _logger;

        public IConfiguration Configuration { get; }
        public HomeController(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        /*public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }*/

        public IActionResult Index()
        {
            return View();
        }

        


        public IActionResult Lista()
        {
            List<Registro> UsuariosList = new List<Registro>();

            string connectionString = Configuration["ConnectionStrings:DefaultConnection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                connection.Open();

                string sql = "select * from USUARIO";
                SqlCommand command = new SqlCommand(sql, connection);

                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        Registro clsUsuario = new Registro();
                        clsUsuario.id = Convert.ToInt32(dataReader["Id"]);
                        clsUsuario.nombre = Convert.ToString(dataReader["nombre"]);
                        clsUsuario.apellido = Convert.ToString(dataReader["apellido"]);
                        clsUsuario.email = Convert.ToString(dataReader["email"]);
                        clsUsuario.contrasena = Convert.ToString(dataReader["contrasena"]);

                        UsuariosList.Add(clsUsuario);
                    }
                }

                connection.Close();
            }
            return View(UsuariosList);
        }

        public IActionResult GeneratePdf(string html)
        {
            html = html.Replace("StrTag", "<").Replace("EndTag", ">");
            

            HtmlToPdf oHtmlToPdf = new HtmlToPdf();
            PdfDocument oPdfDocument = oHtmlToPdf.ConvertHtmlString(html);
            //PdfDocument doc = converter.ConvertUrl("https://es.stackoverflow.com/questions/243536/c%C3%B3mo-solucionar-error-uncaught-referenceerror-is-not-defined-en-html");
            byte[] pdfFile = oPdfDocument.Save();
            oPdfDocument.Close();

            return File(
                pdfFile,
                "aplication/pdf", "Lista.pdf");
        }
        


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Registro clsUsuario)
        {
            if (ModelState.IsValid)
            {
                string connectionString = Configuration["ConnectionStrings:DefaultConnection"];
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string sql = $"Insert Into USUARIO(nombre,apellido,email, contrasena) Values('{clsUsuario.nombre}', '{clsUsuario.apellido}', '{clsUsuario.email}', '{clsUsuario.contrasena}')";

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
                return View();
        }
        
        //[HttpPost]
        /*public async Task<ActionResult> Datos(Datos_ViewModel viewModel)
        {
            try
            {

            }
            catch (Exception ex)
            {
                return Json(new { success = false, ErrorMessage = ex.Message, JsonRequestBehavior.DenyGet });
            }
        }*/

    }
}