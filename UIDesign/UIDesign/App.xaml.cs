using ContactManager.Core;
using ContactManager.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UIDesign
{
    public partial class App : Application
    {
        public static string NewLine = Environment.NewLine;

        public App()
        {
            InitializeComponent();
            AppCore.VM = (AppViewModel)this.BindingContext;
            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
