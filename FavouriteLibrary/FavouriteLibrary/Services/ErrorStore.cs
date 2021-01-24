namespace FavouriteLibrary.Services
{
    static class ErrorStore
    {
        public const string PassConfirmationError = "Пароли не совпадают";
        public const string DataLoadingFailure = "Ошибка загрузки";
        public const string DataEditingFailure = "Ошибка обновления";
        public const string DataLoadingFailureMessage = "Произошла ошибка загрузки данных. Пожалуйста попробуйте обновить страницу.";
        public const string DataEditingFailureMessage = "Произошла ошибка при изменении данных. Пожалуйста попробуйте обновить страницу.";
        public const string RegisterError = "Ошибка регистрации. Пожалуйста повторите попытку.";
        public const string LoginError = "Неверные email и/или пароль.";
        public const string EmailInvalid = "Некорректный email.";

    }
}
