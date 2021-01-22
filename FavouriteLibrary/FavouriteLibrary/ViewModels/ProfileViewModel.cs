using FavouriteLibrary.Services;
using Xamarin.Forms;

namespace FavouriteLibrary.ViewModels
{
    class ProfileViewModel : BaseViewModel
    {
        private IAuthService authService;
        private IDialogService dialogService;
        public string Name { get; set; }
        public string Email { get; set; }
        public Command LogoutCommand { get; set; }

        public ProfileViewModel()
        {
            authService = new AuthService();
            dialogService = DependencyService.Get<IDialogService>();
            LogoutCommand = new Command(Logout);
            InitProfile();
        }

        private async void Logout()
        {
            var result = await authService.Logout();
            if (result.IsSuccess)
            {
                await Shell.Current.GoToAsync("//login");
            }
            else
            {
                dialogService.ShowError(
                    ErrorStore.DataLoadingFailureMessage,
                    ErrorStore.DataLoadingFailure,
                    "Ok",
                    () => dialogService.CloseMessage());
            }
        }

        private async void InitProfile()
        {
            var result = await authService.GetMe();
            if (result.IsSuccess)
            {
                Name = result.Data.Name;
                Email = result.Data.Email;
                OnPropertyChanged(nameof(Name));
                OnPropertyChanged(nameof(Email));
            }
            else
            {
                dialogService.ShowError(
                    ErrorStore.DataLoadingFailureMessage,
                    ErrorStore.DataLoadingFailure,
                    "Ok",
                    () => dialogService.CloseMessage());
            }
        }
    }
}
