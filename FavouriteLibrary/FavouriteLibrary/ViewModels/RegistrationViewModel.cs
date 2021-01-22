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
        public string UserName { get; set; }
        public string Password { get; set; }

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
            }
        }

        public string Email { get; set; }
        public string Error { get; set; }
        public Command ApplyCommand { get; }

        public bool CanApply => !string.IsNullOrEmpty(UserName) &&
                                !string.IsNullOrEmpty(Email) &&
                                !string.IsNullOrEmpty(Password) &&
                                !string.IsNullOrEmpty(ConfirmationPassword)
                                && isPasswordsEqual;

        public RegistrationViewModel()
        {
            authService = ServiceLocator.Current.GetInstance<IAuthService>();
            ApplyCommand = new Command(Apply);
        }

        private async void Apply()
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
