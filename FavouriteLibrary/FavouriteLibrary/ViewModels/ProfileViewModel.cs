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
        public Command LogoutCommand { get; set; }

        public Command LoadProfileCommand { get; set; }
        public ProfileViewModel()
        {
            authService = ServiceLocator.Current.GetInstance<IAuthService>();
            dialogService = DependencyService.Get<IDialogService>();
            LogoutCommand = new Command(Logout);
            LoadProfileCommand = new Command(InitProfile);
            IsBusy = true;
            InitProfile();
        }

        private async void Logout()
        {
            var token = await SecureStorage.GetAsync("token");
            var result = await authService.Logout(token);
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

        private async void InitProfile()
        {
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
