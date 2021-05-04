using System;
using System.Threading;
using System.Threading.Tasks;
using ContactManager.Core.Properties;
using Xamarin.Forms;
using Xamarin.Essentials;
using ContactManager;

[assembly: Dependency(typeof(UIDesign.AppService))]
namespace UIDesign
{
    public class AppService : IAppService
    {
        public async Task HandleError(Exception ex)
        {
            await Device.InvokeOnMainThreadAsync(async () =>
            {
                bool res = await App.Current.MainPage.DisplayAlert("Error occured!", ex.ToString(), "Yes", "No");
                if (res)
                {
                    //Send email
                }
                Environment.Exit(0);
            });
        }

        public async Task<string> MessagePromptInput(string title, string content)
        {
            string res = null;
            await Device.InvokeOnMainThreadAsync(async () => res = await App.Current.MainPage.DisplayPromptAsync(title, content));
            return res;
        }

        public async Task<bool> MessagePromptYesNo(string title, string content, string yes, string no)
        {
            bool res = false;
            await Device.InvokeOnMainThreadAsync(async ()=> res = await App.Current.MainPage.DisplayAlert(title, content, yes, no));            
            return res;
        }


    }

}
