namespace FavouriteLibrary.Services
{
    static class ErrorStore
    {
        public const string PassConfirmationError = "Пароли не совпадают";
        public const string DataLoadingFailure = "Ошибка загрузки";
        public const string DataEditingFailure = "Ошибка обновления";
        public const string DataLoadingFailureMessage = "Произошла ошибка загрузки данных. Пожалуйста попробуйте обновить страницу.";
        public const string DataEditingFailureMessage = "Произошла ошибка при изменении данных. Пожалуйста попробуйте обновить страницу.";
        public const string RegisterError = "Произошла ошибка при регистрации пользователя. Возможно пользователь с таким email уже существует.";
        public const string LoginError = "Введены неверные email и/или пароль.";

    }
}
