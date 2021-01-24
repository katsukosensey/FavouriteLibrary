using System.Threading.Tasks;
using AsyncAwaitBestPractices.MVVM;
using CommonServiceLocator;
using FavouriteLibrary.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace FavouriteLibrary.ViewModels
{
    class ProfileViewModel : BaseViewModel
    {
        private IAuthService authService;
        private IDialogService dialogService;
        public string Name { get; set; }
        public string Email { get; set; }
        public AsyncCommand LogoutCommand { get; set; }
        public ProfileViewModel()
        {
            authService = ServiceLocator.Current.GetInstance<IAuthService>();
            dialogService = DependencyService.Get<IDialogService>();
            LogoutCommand = new AsyncCommand(Logout);
            _ = InitProfile();
        }

        private async Task Logout()
        {
            var result = await authService.Logout();
            if (result.IsSuccess)
            {
                SecureStorage.Remove("token");
                await Shell.Current.GoToAsync("//login");
            }
            else
            {
                dialogService.ShowError(
                    result.Error,
                    ErrorStore.DataLoadingFailure,
                    "Ok",
                    () => dialogService.CloseMessage());
            }
        }

        private async Task InitProfile()
        {
            IsBusy = true;
            var result = await authService.GetMe();
            if (result.IsSuccess)
            {
                Name = result.Data.Name;
                Email = result.Data.Email;
                OnPropertyChanged(nameof(Name));
                OnPropertyChanged(nameof(Email));
                IsBusy = false;
            }
            else
            {
                IsBusy = false;
                dialogService.ShowError(
                    result.Error,
                    ErrorStore.DataLoadingFailure,
                    "Ok",
                    () => dialogService.CloseMessage());
            }
        }
    }
}
