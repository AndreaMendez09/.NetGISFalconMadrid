using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Npgsql;
using System.Data;
using System.Security.Claims;
using Net_Gis_Falcon.Controllers;
using System.Security.Cryptography;
using System.Text;
using Net_Gis_Falcon.Data;
using Net_Gis_Falcon.Services.Bussines;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Net_Gis_Falcon.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<LoginModel> _logger;

        public LoginModel(SignInManager<IdentityUser> signInManager, 
            ILogger<LoginModel> logger,
            UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

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

            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl ??= Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }

        /*[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Usuario userModel)
        {
            if (!ModelState.IsValid)
            {
                Console.WriteLine("isValid");
                //return View(userModel);
            }
            var user = await _userManager.FindByEmailAsync(userModel.Email);
            if (user != null &&
                await _userManager.CheckPasswordAsync(user, userModel.Contraseña))
            {
                var identity = new ClaimsIdentity(IdentityConstants.ApplicationScheme);
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id));
                identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
                await HttpContext.SignInAsync(IdentityConstants.ApplicationScheme,
                    new ClaimsPrincipal(identity));
                //return RedirectToAction(nameof(HomeController.Index), "Home");
                return RedirectToPage("./Lockout");
            }
            else
            {
                ModelState.AddModelError("", "Invalid UserName or Password");
                return Page();
            }
        }*/

        [HttpPost]
        public async Task<IActionResult> OnPostAsync(Usuario userModel)
        {
            SecurityService security = new SecurityService();
            userModel.Contraseña = Input.Password;
            Boolean success = security.Authenticate(userModel);


            if (success)
            {
                var rol = "";
                if (userModel.Rol == 1)
                    rol = "operador";
                else if (userModel.Rol == 2)
                    rol = "admin";
                else
                    rol = "usuario";

                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, userModel.Email),
                    new Claim(ClaimTypes.NameIdentifier, userModel.IdUsuario.ToString()),
                    new Claim(ClaimTypes.Role, rol)
                };

                var claimsIdentity = new ClaimsIdentity(claims, "Login");
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);
                return Redirect("/Home");
                
                //This return redirect to Profile
                //return Redirect("./Manage/Index?Id=" + userModel.IdPersona);
            }
            else
            {
                Console.WriteLine("No");
                return Page();
            }
        }

       /* public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            try
            {
                returnUrl ??= Url.Content("~/");

                *//*var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");
                    Console.WriteLine("User logged in.");
                    return LocalRedirect(returnUrl);
                }*//*
        // Insertion After Validations
        using (NpgsqlConnection connection = new NpgsqlConnection())
        {
            connection.ConnectionString = BdConnection.connectionString;
            connection.Open();
            NpgsqlCommand cmd = new NpgsqlCommand();
            cmd.Connection = connection;
                    MD5 md5 = new MD5CryptoServiceProvider();

                    //compute hash from the bytes of text  
                    md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(Input.Password));

                    //get hash result after compute it  
                    byte[] result = md5.Hash;

                    StringBuilder strBuilder = new StringBuilder();
                    for (int i = 0; i < result.Length; i++)
                    {
                        //change it into 2 hexadecimal digits  
                        //for each byte  
                        strBuilder.Append(result[i].ToString("x2"));
                    }

                    Input.Password = strBuilder.ToString();
                    cmd.CommandText = "Select * from personas where email= '" + Input.Email.ToString() + "' and contraseña='" + Input.Password.ToString() + "'";
            cmd.CommandType = CommandType.Text;

            Console.WriteLine("XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX");
            Console.WriteLine(cmd.ExecuteNonQuery());
            Console.WriteLine(cmd.CommandText.ToString());
            Console.WriteLine("XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX");

            NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            //NpgsqlDataReader rd = cmd.ExecuteReader();
            da.Fill(dt);
            if(dt.Rows.Count > 0)
            {
                Console.WriteLine("Entre");
                cmd.Dispose();
                connection.Close();
                return LocalRedirect(returnUrl + "Home/Privacy");
            }


            cmd.Dispose();
            connection.Close();
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine("XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX");
        Console.WriteLine(ex.Message.ToString());
        Console.WriteLine("XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX");
    }

    *//*returnUrl ??= Url.Content("~/");

    ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

    if (ModelState.IsValid)
    {
        // This doesn't count login failures towards account lockout
        // To enable password failures to trigger account lockout, set lockoutOnFailure: true
        var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: false);
        if (result.Succeeded)
        {
            _logger.LogInformation("User logged in.");
            return LocalRedirect(returnUrl);
        }
        if (result.RequiresTwoFactor)
        {
            return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
        }
        if (result.IsLockedOut)
        {
            _logger.LogWarning("User account locked out.");
            return RedirectToPage("./Lockout");
        }
        else
        {
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return Page();
        }
    }
    *//*
    // If we got this far, something failed, redisplay form
    return Page();
}*/
    }
}
