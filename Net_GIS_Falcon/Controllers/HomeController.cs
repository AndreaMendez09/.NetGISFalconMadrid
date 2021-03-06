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
            //List<Persona> personas = (from Personas in this.Context.Personas select Personas).ToList();
            //return View(personas);
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return Redirect("/Home");
        }

        [Authorize]
        //[Route("secured")]
        public IActionResult Secured()
        {
            return View();
        }

        [HttpGet("login")]
        public IActionResult Login(string ReturnUrl)
        {
            ViewData["ReturnUrl"] = ReturnUrl;
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/Home");
        }

        //GET: Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        //GET: LoginADMIN
        public IActionResult LoginADMIN() {
            return View();
        }

        //GET: /ListaEstados
        public IActionResult ListaEstados()
        {
            return View();
        }


    }
}
