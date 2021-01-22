using FavouriteLibrary.Views;
using FavouriteLibrary.Services;
using Xamarin.Forms;

namespace FavouriteLibrary.ViewModels
{
    public class AuthorizationViewModel : BaseViewModel
    {

        private readonly IAuthService authService;
        public string Email { get; set; }
        public string Password { get; set; }
        public string Error { get; set; }
        public Command LoginCommand { get; }
        public Command RegisterCommand { get; }

        public AuthorizationViewModel()
        {
            authService = new AuthService();
            LoginCommand = new Command(OnLoginClicked);
            RegisterCommand = new Command(OnRegisterClicked);
        }

        private async void OnRegisterClicked()
        {
            await Shell.Current.GoToAsync("//login/registration");
        }

        private async void OnLoginClicked()
        {
            var result = await authService.Login(Email, Password);
            if (result.IsSuccess)
            {
                await Shell.Current.GoToAsync("//main");
            }
            else
            {
                Error = result.Error;
                OnPropertyChanged(nameof(Error));
            }
        }
    }
}
