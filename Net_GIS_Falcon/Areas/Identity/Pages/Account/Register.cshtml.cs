using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Npgsql;
using Microsoft.IdentityModel.Protocols;
using System.Security.Cryptography;
using Net_Gis_Falcon.Data;
using Net_Gis_Falcon.Services.Bussines;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Net_Gis_Falcon.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;

        public RegisterModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

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

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            [Required]
            [Display(Name = "Nombre")]
            public string Name { get; set; }

            [Required]
            [Display(Name = "Apellido")]
            public string Surname { get; set; }

            [Required]
            [DataType(DataType.PhoneNumber)]
            [Display(Name = "NumeroDeTLF")]
            public string PhoneNumber { get; set; }

            [Required]
            [Display(Name = "Genero")]
            public string Gender { get; set; }

            [Required]
            [Display(Name = "Idioma")]
            public string Language { get; set; }

            [Required]
            [Display(Name = "Direccion")]
            public string Direction { get; set; }

            [Required]
            [Display(Name = "Municipio")]
            public string Municipality { get; set; }

            [Required]
            [DataType(DataType.Date)]
            [Display(Name = "FechaDeNacimiento")]
            public string BirthDay { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            Boolean success = false;
            SecurityService security = new SecurityService();
            Usuario user = new Usuario(Input.Name, Input.Surname, Input.Email, Input.Gender, Input.Language, Input.Password, null, Input.Municipality, DateTime.Parse(Input.BirthDay), 3);
            success = security.AuthenticateAndCompareByEmail(user);
            if (!success)
            {
                success = security.Create(user);
                Console.WriteLine(Input.BirthDay);
                Console.WriteLine(DateTime.Parse(Input.BirthDay));
                var idUsuario = 0;
                using (NpgsqlConnection connection = new NpgsqlConnection())
                {
                    connection.ConnectionString = BdConnection.connectionString;
                    connection.Open();
                    NpgsqlCommand cmd = new NpgsqlCommand();
                    cmd.Connection = connection;

                    cmd.CommandText = "Select id_usuario from usuarios where email= '" + user.Email.ToString() + "'";
                    cmd.CommandType = CommandType.Text;

                    /*Console.WriteLine("XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX");
                    Console.WriteLine(cmd.ExecuteNonQuery());
                    Console.WriteLine(cmd.CommandText.ToString());
                    Console.WriteLine("XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX");*/
                    try
                    {
                        NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        idUsuario = int.Parse(dt.Rows[0][0].ToString());
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                    //NpgsqlDataReader rd = cmd.ExecuteReader();
                    /*da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        Console.WriteLine("Entre");
                        cmd.Dispose();
                        connection.Close();
                        return LocalRedirect(returnUrl + "Home/Privacy");
                    }*/


                    cmd.Dispose();
                    connection.Close();
                }


                if (success)
                {
                    var rol = "";
                    if (user.Rol == 1)
                        rol = "operador";
                    else if (user.Rol == 2)
                        rol = "admin";
                    else
                        rol = "usuario";
                    var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, user.Email),
                    new Claim(ClaimTypes.NameIdentifier, idUsuario.ToString()),
                    new Claim(ClaimTypes.Role, rol)
                };

                    var claimsIdentity = new ClaimsIdentity(claims, "Login");
                    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);
                    Console.WriteLine("Si");
                    return Redirect("/Home");
                }
                else
                {
                    Console.WriteLine("No");
                    return Page();
                }
            }
            else
            {
                ViewData["Error"] = "Error al registrarse. El correo eléctrónico ya está en uso.";
            }

            /*returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = Input.Email, Email = Input.Email };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }*/

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}