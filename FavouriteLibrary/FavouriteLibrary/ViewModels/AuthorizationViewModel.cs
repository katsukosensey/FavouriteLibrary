using System.Threading.Tasks;
using AsyncAwaitBestPractices.MVVM;
using CommonServiceLocator;
using FavouriteLibrary.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace FavouriteLibrary.ViewModels
{
    public class AuthorizationViewModel : BaseViewModel
    {

        private readonly IAuthService authService;
        public string Email { get; set; }
        public string Password { get; set; }
        public string Error { get; set; }
        public AsyncCommand LoginCommand { get; }
        public AsyncCommand RegisterCommand { get; }

        public AuthorizationViewModel()
        {
            CheckToken();
            authService = ServiceLocator.Current.GetInstance<IAuthService>();
            LoginCommand = new AsyncCommand(OnLoginClicked);
            RegisterCommand = new AsyncCommand(OnRegisterClicked);
        }

        private async void CheckToken()
        {
            var token = await SecureStorage.GetAsync("token");
            if (token != null)
            {
                await Shell.Current.GoToAsync("//main");
            }
        }

        private async Task OnRegisterClicked()
        {
            await Shell.Current.GoToAsync("//login/registration");
        }

        private async Task OnLoginClicked()
        {
            var result = await authService.Login(Email, Password);
            if (result.IsSuccess)
            {
                await SecureStorage.SetAsync("token", result.Data);
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
