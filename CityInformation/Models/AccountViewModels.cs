using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace CityInformation.Models
{
    public class Pretraživanje
    {
        public string tekst { get; set; }

        public string grad { get; set; }

        public string županija { get; set; }

        public string ulica { get; set; }

        public string djelatnost { get; set; }

    }

    public class UploadImagesModels
    {
        public HttpPostedFileBase[] ImageFile { get; set; }

        public string idKorisnika { get; set; }
    }

    public class PromijeniLozinku
    {
        public string ID { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Stara lozinka")]
        [Required(ErrorMessage = "Polje je obavezno.")]
        public string StaraLozinka { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Polje je obavezno.")]
        [Display(Name = "Nova lozinka")]
        [StringLength(100, ErrorMessage = " Lozinka mora biti minimalno {2} znakova dugačka.", MinimumLength = 6)]
        public string NovaLozinka { get; set; }
        [DataType(DataType.Password)]
        [Compare("NovaLozinka", ErrorMessage = "Lozinke se ne poklapaju.")]
        [Required(ErrorMessage = "Polje je obavezno.")]
        [Display(Name = "Ponovite novu lozinku")]
        public string PotvrdiNovuLozinku { get; set; }
    }

    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required(ErrorMessage = "Polje je obavezno!")]
        [Display(Name = "Korisničko ime")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Polje je obavezno!")]
        [DataType(DataType.Password)]
        [Display(Name = "Lozinka")]
        public string Password { get; set; }

        [Display(Name = "Zapamti me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModelPoduzeće
    {
        [Required(ErrorMessage = "Polje je obavezno.")]
        [Display(Name = "Korisničko ime")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Polje je obavezno.")]
        [Display(Name = "Ime poduzeća")]
        public string ImePoduzeća { get; set; }

        [Required(ErrorMessage = "Polje je obavezno.")]
        [StringLength(100, ErrorMessage = "Lozinka mora imati bar {2} slova.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Lozinka")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Potvrdi lozinku")]
        [Compare("Password", ErrorMessage = "Lozinke se ne poklapaju!")]
        [Required(ErrorMessage = "Polje je obavezno.")]
        public string ConfirmPassword { get; set; }

    }
        public class RegisterViewModelAdmin
    {
        [Required(ErrorMessage = "Polje je obavezno.")]
        [Display(Name = "Korisničko ime")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Polje je obavezno.")]
        [StringLength(100, ErrorMessage = "Lozinka mora imati bar {2} slova.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Lozinka")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Potvrdi lozinku")]
        [Compare("Password", ErrorMessage = "Lozinke se ne poklapaju!")]
        [Required(ErrorMessage = "Polje je obavezno.")]
        public string ConfirmPassword { get; set; }
    }

    public class RegisterViewModelKorisnik
    {
        [Required(ErrorMessage = "Polje je obavezno.")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Polje je obavezno.")]
        [Display(Name ="Korisničko ime")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Polje je obavezno.")]
        [StringLength(100, ErrorMessage = "Lozinka mora imati bar {2} slova.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Lozinka")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Potvrdi lozinku")]
        [Compare("Password", ErrorMessage = "Lozinke se ne poklapaju!")]
        [Required(ErrorMessage = "Polje je obavezno.")]
        public string ConfirmPassword { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
