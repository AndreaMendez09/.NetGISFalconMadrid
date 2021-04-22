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
            List<Usuario> usuarios = (from Usuarios in this.Context.Usuarios select Usuarios).ToList();
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
            return View(usuarios);

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


    }
}
