using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Net_Gis_Falcon.Data;
using Net_Gis_Falcon.Models;
using System.Configuration;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System;
using Npgsql;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace Net_Gis_Falcon.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private testContext Context { get; }


        public HomeController(testContext _context)
        {
            this.Context = _context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            List<Persona> personas = (from Personas in this.Context.Personas select Personas).ToList();
            /*String connectionString = "Data Source=localhost;" +
               "Initial Catalog=test;" +
               "User id=postgres;" +
               "Password=ADMIN;";
             String sql = "SELECT * FROM Usuarios";


             var model = new List<Usuario>();
             using (SqlConnection conn = new SqlConnection(connectionString))
             {
                 SqlCommand cmd = new SqlCommand(sql, conn);
                 conn.Open();
                 SqlDataReader rdr = cmd.ExecuteReader();
                 while (rdr.Read())
                 {
                     var usuario = new Usuario();
                     Console.WriteLine(rdr["id_usuario"] + ",  " + rdr["nombre"]);

                     //usuario.Id = rdr[""];
                     //usuario.Name = rdr["nombre"];

                     //model.Add(usuario);
                 }
                 rdr.Close();

             }*/

            // return View(model);
            return View(personas);

        }

        public IActionResult Register()
        {


            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [Authorize]
        public IActionResult Secured()
        {
            return View();
        }


        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Usuario userModel, string returnUrl)
        {
            if (userModel.Email == "Andrea@gmail.com" && userModel.Contraseña == "A1234a@")
            {
                var claims = new List<Claim>();
                claims.Add(new Claim("username", userModel.Email));
                claims.Add(new Claim(ClaimTypes.NameIdentifier, userModel.Email));
                var CLAIMSiDENTITY = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(CLAIMSiDENTITY);
                await HttpContext.SignInAsync(claimsPrincipal);
                Console.WriteLine("XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX");
                Console.WriteLine(returnUrl);
                Console.WriteLine("XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX");
                return Redirect(returnUrl);
            }

            return BadRequest();
        }


    }
}
