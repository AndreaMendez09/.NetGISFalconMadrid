using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Net_Gis_Falcon.Services.Bussines;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Net_Gis_Falcon.Views.Home
{
    public class LoginADMIN_Model : PageModel
    {
        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

        }

        [HttpPost]
        public async Task<IActionResult> OnPostAsync(Personasistema p_sistemaModel)
        {
            SecurityService security = new SecurityService();
            //p_sistemaModel.Contraseña = Input.Password;
            Boolean success = security.Authenticate(p_sistemaModel);

            if (success)
            {
                string rol;
                if (p_sistemaModel.Rol == 1)
                {
                    rol = "Admin";
                }else
                {
                    rol = "operador";
                }
                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, p_sistemaModel.Email),
                    new Claim(ClaimTypes.NameIdentifier, p_sistemaModel.IdPersonasistema.ToString()),
                    new Claim(ClaimTypes.Role, rol)
                };

                var claimsIdentity = new ClaimsIdentity(claims, "Login");
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);
                return Redirect("/Home");

                //This return redirect to Profile
                //return Redirect("./Manage/Index?Id=" + p_sistemaModel.IdPersona);
            }
            else
            {
                Console.WriteLine("No");
                return Page();
            }
        }

    }
}
