using System.Collections.Generic;//Lista de colecciones genericas
using System.Data;//Identificar el tipo de objeto a manipular en base de datos
using Microsoft.Data.SqlClient;//Controlador de acceso a datos
using System.Linq;//Para hacer macth entre lista del dataReader y los antributos del modelo (usuario y contrasena)
using WebApplication1.Models;//Para instanciar los atributos de la entidad usuario
using Microsoft.Extensions.Configuration;//Para acceder al archivo de configuración appsettings.json
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    
    public class LoginController : Controller
    {
        const string SessionUser = "_User";
        private readonly ILogger<LoginController> _logger;

        public IConfiguration Configuration { get; }
        public LoginController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        [HttpGet]
        [Route("Login/Login")]
        public IActionResult Login()

        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(Registro model)

        {
            string connectionString = Configuration["ConnectionStrings:DefaultConnection"];
            using (SqlConnection connection = new SqlConnection(connectionString))

            {
                var list_users = new List<Registro>();
                if (model.email == null || model.email.Equals("") ||
                    model.contrasena == null || model.contrasena.Equals(""))
                {
                    ModelState.AddModelError("", "Ingresar los datos solictiados");
                }//@1Final
                else
                {
                    connection.Open();//Abrir la conexión a la base de datos
                    SqlCommand com = new SqlCommand("GET_SEG_USUARIO", connection);//Referencia al procedimiento almacenad
                    com.CommandType = CommandType.StoredProcedure;//Se define el tipo de comando a utilizar

                    //Paso los parámetros de acuerdo a los datos cargados segun el modelo usario
                    com.Parameters.AddWithValue("@email", model.email);
                    com.Parameters.AddWithValue("@contrasena", model.contrasena);
                    SqlDataReader dr = com.ExecuteReader();//Ejecuto el comando a través de un DataReader

                    //@2Inicio: Recorro los datos y adiciono en la lista list_users los valores usuario y contrasena
                    while (dr.Read())
                    {
                        Registro clsUsuario = new Registro();
                        clsUsuario.email = Convert.ToString(dr["email"]);
                        clsUsuario.contrasena = Convert.ToString(dr["contrasena"]);

                        list_users.Add(clsUsuario);//Adicionar en la lista

                    }

                    if (list_users.Any(p => p.email == model.email && p.contrasena == model.contrasena))
                    {
                        //HttpContext.Session.SetString(SessionUser, model.nombre);//Iniciamos la sesión pasando el valor (nombre del usuario)

                        return RedirectToAction("Datos", "Home");//Redireccionar a la vista usario (Lista de Usuarios)
                    }
                    else
                    {
                        ModelState.AddModelError("", "Datos ingresado no válido.");//Error personalizado
                    }
                }
                return View(model);
            }
        }
        [HttpPost]

        public ActionResult LogOff()
        {
            HttpContext.Session.Clear();//Limpiar la sesión
            return RedirectToAction("Login", "Login");//Redireccionar a la vista login
        }


    }
}
