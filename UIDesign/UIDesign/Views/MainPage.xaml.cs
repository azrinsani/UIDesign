using ContactManager.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using ContactManager.Models;
using ContactManager.ViewModels;
using RandomNameGeneratorLibrary;
namespace UIDesign
{
    public partial class MainPage : TabbedPage
    {
        public MainPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this.ContentPage, false);

        }

        private async void TabbedPage_Appearing(object sender, EventArgs e)
        {
            AppCore.VM.ShowLoading = true; //This shows the loading spinner
            AppCore.VM.LoadingProgress = 0;
            while (!this.IsVisible) await Task.Delay(50);
            var contacts = new List<Contact>();
            var g = new RandomNameGeneratorLibrary.PersonNameGenerator();

            for (int n=0; n<3000;n++)
            {
                contacts.Add(new Contact()
                {
                    Name = g.GenerateRandomMaleFirstAndLastName(),
                    Company = g.GenerateRandomLastName(),
                    Job = "Worker",
                });
            }
            AppCore.Model = new AppModel()
            {
                Contacts = contacts
            };
            AppCore.Settings = new Settings();
            AppCore.VM.LoadViewModel();
            AppCore.VM.ShowLoading = false;
        }
    }
}
