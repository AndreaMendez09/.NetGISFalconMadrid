using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Net_Gis_Falcon.Services.Bussines;

namespace Net_Gis_Falcon.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public IndexModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public int Id { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
        public string Language { get; set; }
        public string Direction { get; set; }
        public string Municipality { get; set; }
        public string BirthDay { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Display(Name = "Id")]
            public string Id { get; set; }
            
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

        private async Task LoadAsync(Usuario user)
        {
            IdentityUser u = new IdentityUser();
            var userName = await _userManager.GetUserNameAsync(u);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(u);

            Username = userName;

            Input = new InputModel
            {
                PhoneNumber = phoneNumber
            };
        }

        [Route("{Id}")]
        public async Task<IActionResult> OnGetAsync(int Id)
        {
            SecurityService security = new SecurityService();
            Usuario user = new Usuario();
            if (Id > 0)
            {
                user.IdUsuario = Id;
                Boolean success = security.AuthenticateById(user);

                if (success)
                {
                    Console.WriteLine("Dentro");
                    Name = user.Nombre;
                    Surname = user.Apellido;
                    //Email = user.Email;
                    Gender = user.Genero;
                    Language = user.Idioma;
                    Municipality = user.Municipio;
                    BirthDay = user.FechaNacimiento.ToString();

                    return Page();
                }
                else
                {
                    return Redirect("/Home");
                }
            }
            else
            {
                user.Email = User.Identity.Name;
               
                Boolean success = security.AuthenticateByEmail(user);
                if (success)
                {
                    Console.WriteLine("Dentro");
                    Id = user.IdUsuario;
                    Name = user.Nombre;
                    Surname = user.Apellido;
                    //Email = user.Email;
                    Gender = user.Genero;
                    Language = user.Idioma;
                    Municipality = user.Municipio;
                    BirthDay = user.FechaNacimiento.ToString();

                    return Page();
                }
            }
            

            /*var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }*/

            //await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Usuario user = new Usuario(Input.Name, Input.Surname, User.Identity.Name.ToString(), Input.Gender, Input.Language, "", "", Input.Municipality, DateTime.Parse(Input.BirthDay));
            SecurityService security = new SecurityService();
            Boolean success = security.Update(user);
            
            if (success)
            {
                Console.WriteLine("Actualizado");
                return Page();
            }
            
            return RedirectToPage();
        }
    }
}
