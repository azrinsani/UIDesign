using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ContactManager.Models;
using ContactManager.ViewModels;
using Xamarin.Forms;

namespace ContactManager.Core
{
    public static class AppCore
    {
        public static AppModel Model { get; set; }
        public static Settings Settings { get; set; }
        public static AppViewModel VM;
        public static CancellationTokenSource CancellationTokenSource;
        public static string ModelFile;
        public static string SettingsFile;        
        public static IDeviceService DeviceService { get; set; }
        public static IAppService AppService { get; set; }      
        public static async Task RefreshModel(CancellationToken cancellationToken)
        {
            try
            {
                AppCore.VM.ShowLoadingProgress = true;
                AppCore.VM.LoadingProgress = 0;
                await Task.WhenAll(
                    Task.Run(() =>
                    {
                        var appModel = AppCore.DeviceService.GetAppModel((msg, progress) => AppCore.VM.LoadingProgress = progress, cancellationToken, null);
                        AppCore.Model = appModel;
                        AppCore.Model.Save();
                        if (cancellationToken.IsCancellationRequested) return;
                    }, cancellationToken),
                    Task.Delay(1000, cancellationToken)
                );
            }
            catch (Exception ex)
            {
                await AppCore.AppService.HandleError(ex);  //This terminates the Program               
            }
        }



    }
}
