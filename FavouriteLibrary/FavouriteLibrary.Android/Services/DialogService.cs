using System;
using Android.Support.V7.App;
using FavouriteLibrary.Droid.Services;
using FavouriteLibrary.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(DialogService))]
namespace FavouriteLibrary.Droid.Services
{
    public class DialogService : IDialogService
    {
        private AlertDialog alert;
        public void ShowError(string message,
            string title,
            string buttonText,
            Action afterHideCallback)
        {
            var builder = new AlertDialog.Builder(Forms.Context);
            builder.SetTitle(title);
            builder.SetMessage(message);
            builder.SetNeutralButton(buttonText, (sender, args) => afterHideCallback());
            alert = builder.Show();
        }

        public void ShowError(
            Exception error,
            string title,
            string buttonText,
            Action afterHideCallback)
        {
            var builder = new AlertDialog.Builder(Forms.Context);
            builder.SetTitle(title);
            builder.SetMessage(error.Message);
            builder.SetNeutralButton(buttonText, (sender, args) => afterHideCallback());
            alert = builder.Show();
        }

        public void ShowMessage(
            string message,
            string title)
        {
            var builder = new AlertDialog.Builder(Forms.Context);
            builder.SetTitle(title);
            builder.SetMessage(message);
            builder.SetNeutralButton("OK", (sender, args) => builder.Dispose());
            alert = builder.Show();
        }

        public void ShowMessage(
            string message,
            string title,
            string buttonText,
            Action afterHideCallback)
        {

            var builder = new AlertDialog.Builder(Forms.Context);
            builder.SetTitle(title);
            builder.SetMessage(message);
            builder.SetNeutralButton(buttonText, (sender, args) => afterHideCallback());
            alert = builder.Show();
        }

        public void ShowMessage(
            string message,
            string title,
            string buttonConfirmText,
            string buttonCancelText,
            Action<bool> afterHideCallback)
        {
            var builder = new AlertDialog.Builder(Forms.Context);
            builder.SetTitle(title);
            builder.SetMessage(message);
            builder.SetPositiveButton(buttonConfirmText, (sender, args) => afterHideCallback(true));
            builder.SetNegativeButton(buttonCancelText, (sender, args) => afterHideCallback(false));
            alert = builder.Show();
        }
        public void ShowMessage(
            string message,
            string title,
            string buttonPositiveText,
            string buttonNegativeText,
            string buttonCancelText,
            Action<bool> afterHideCallback)
        {
            var builder = new AlertDialog.Builder(Forms.Context);
            builder.SetTitle(title);
            builder.SetMessage(message);
            builder.SetPositiveButton(buttonPositiveText, (sender, args) => afterHideCallback(true));
            builder.SetNegativeButton(buttonNegativeText, (sender, args) => afterHideCallback(false));
            builder.SetNeutralButton(buttonCancelText, (sender, args) => builder.Dispose());
            alert = builder.Show();
        }

        public void ShowMessageBox(
            string message,
            string title)
        {
            var builder = new AlertDialog.Builder(Forms.Context);
            builder.SetTitle(title);
            builder.SetMessage(message);
            builder.SetNeutralButton("OK", (sender, args) => builder.Dispose());
            alert = builder.Show();
        }
        public void ShowInfoMessage(
            string message,
            string title)
        {
            var builder = new AlertDialog.Builder(Forms.Context);
            builder.SetTitle(title);
            builder.SetMessage(message);
            alert = builder.Show();
        }

        public void CloseMessage()
        {
            alert.Cancel();
        }
       
    }
}
