using System;

namespace FavouriteLibrary.Services
{
    public interface IDialogService
    {
        void ShowError(string message, string title, string buttonText, Action afterHideCallback);
        void ShowError(Exception error, string title, string buttonText, Action afterHideCallback);
        void ShowMessage(string message, string title);
        void ShowMessage(string message, string title, string buttonText, Action afterHideCallback);
        void ShowMessage(string message, string title, string buttonConfirmText, string buttonCancelText, Action<bool> afterHideCallback);

        void ShowMessage(string message, string title, string buttonPositiveText, string buttonNegativeText,
            string buttonCancelText, Action<bool> afterHideCallback);
        void ShowMessageBox(string message, string title);
        void ShowInfoMessage(string message, string title);
        void CloseMessage();
    }
}
