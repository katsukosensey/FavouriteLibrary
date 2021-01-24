using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AsyncAwaitBestPractices.MVVM;
using CommonServiceLocator;
using FavouriteLibrary.Services;
using Xamarin.Forms;

namespace FavouriteLibrary.ViewModels
{
    class RegistrationViewModel : BaseViewModel
    {
        private readonly IAuthService authService;
        private string confirmationPassword;
        private bool isPasswordsEqual;
        private string email;
        private string userName;
        private string password;

        public string UserName
        {
            get => userName;
            set => SetProperty(ref userName, value);
        }

        public string Password
        {
            get => password;
            set
            {
                password = value;
                OnPropertyChanged(nameof(CanApply));
            }
        }

        public string ConfirmationPassword
        {
            get => confirmationPassword;
            set
            {
                confirmationPassword = value;
                isPasswordsEqual = Password == confirmationPassword;
                if (!string.IsNullOrEmpty(confirmationPassword) && !isPasswordsEqual)
                {
                    Error = ErrorStore.PassConfirmationError;
                }
                else
                {
                    Error = string.Empty;
                }
                OnPropertyChanged(nameof(Error));
                OnPropertyChanged(nameof(CanApply));
            }
        }

        public string Email
        {
            get => email;
            set
            {
                email = value;
                Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                Match match = regex.Match(email);
                Error = !match.Success ? ErrorStore.EmailInvalid : string.Empty;
                OnPropertyChanged(nameof(Error));
                OnPropertyChanged(nameof(CanApply));
            }
        }

        public string Error { get; set; }
        public AsyncCommand ApplyCommand { get; }

        public bool CanApply => !string.IsNullOrEmpty(UserName) &&
                                !string.IsNullOrEmpty(Email) &&
                                !string.IsNullOrEmpty(Password) &&
                                !string.IsNullOrEmpty(ConfirmationPassword)
                                && isPasswordsEqual;

        public RegistrationViewModel()
        {
            authService = ServiceLocator.Current.GetInstance<IAuthService>();
            ApplyCommand = new AsyncCommand(Apply);
        }

        private async Task Apply()
        {
            var result = await authService.Register(UserName, Email, Password, ConfirmationPassword);
            if (result.IsSuccess)
            {
                await Shell.Current.GoToAsync("..");
            }
            else
            {
                Error = result.Error;
                OnPropertyChanged(nameof(Error));
            }
        }
    }
}
