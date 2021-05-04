using ContactManager;
using ContactManager.Core;
using ContactManager.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
using Contact = ContactManager.Models.Contact;

namespace ContactManager.ViewModels
{

    public class AppViewModel : BaseViewModel, INotifyPropertyChanged
    {
        public AppViewModel()
        {
            ContactsFullRefreshCommand = new Command(async (_) => await OnContactsFullRefreshCommand());
            ShowDetailsTapCommand = new Command(async (_) => await OnShowDetailsTapCommand());
            DeleteGroupCommand = new Command(async (obj) => await OnDeleteGroupCommand(obj));
        }
     


        #region MAIN PAGE UI RELATED
        public ObservableRangeCollection<GroupMembershipViewModel> GroupsInAddGroupPopup { get; set; } = new ObservableRangeCollection<GroupMembershipViewModel>();
        public ContactViewModel ContactFocused; //A temporary placeholder
        public bool ShowAddMorePopup { get => showAddMorePopup; set => SetProperty(ref showAddMorePopup, value); }
        private bool showAddMorePopup = false;
        public bool ShowLoading { get => showLoading; set => SetProperty(ref showLoading, value); }
        private bool showLoading = false;
        public bool ShowLoadingProgress { get => showLoadingProgress; set => SetProperty(ref showLoadingProgress, value); }
        private bool showLoadingProgress = false;
        public double LoadingProgress { get => loadingProgress; set => SetProperty(ref loadingProgress, value); }
        private double loadingProgress = 0;
        public bool ShowTopMessage { get => showTopMessage; set => SetProperty(ref showTopMessage, value); }
        private bool showTopMessage = false;
        public Thickness ThumbnailMargin { get => Device.FlowDirection == FlowDirection.LeftToRight ? new Thickness(26, 0, -26, 0) : new Thickness(-26, 0, 26, 0); }
        public ContactAction ContactAction { get => contactAction; set => SetProperty(ref contactAction, value); }
        private ContactAction contactAction = ContactAction.None;

        public bool DoNotReloadContactsFlag = false;
        #endregion



        #region CONTACTS PAGE
        public ObservableRangeCollection<BaseContactViewModel> ContactViews { get; set; } = new ObservableRangeCollection<BaseContactViewModel>();



        //public ContactManagerData ContactManagerData = null;
        //**********************************************************************************************
        //This is the most important function! It needs to be fast to ensure the app loads fast!
        //**********************************************************************************************
        public void LoadViewModel()
        {
            try
            {
                List<BaseContactViewModel> temp = new List<BaseContactViewModel>();
                //{
                //    new TogglersViewModel()
                //};
                temp.AddRange(AppCore.Model.Contacts.Select(c => new ContactViewModel(c)));
                this.ContactViews.Clear();
                this.ContactViews.AddRange(temp);
                this.ContactGroupConfigures.AddRange(AppCore.Model.ContactGroups.Select(g => new ContactGroupConfigureViewModel(g.Value, AppCore.Settings.ToggleGroups.Any(tG => tG == g.Key))));
            }
            catch (Exception ex)
            {
                AppCore.AppService.HandleError(ex);
            }
            return;
        }


        public Command DeleteGroupCommand { get; }
        public async Task OnDeleteGroupCommand(object obj)
        {
            bool answer = await AppCore.AppService.MessagePromptYesNo("Delete Group '" + obj + "'?", "This cannot be undone!", "Yes", "No");
            if (answer)
            {

            }


        }

        public Command ContactsFullRefreshCommand { get; }
        public async Task OnContactsFullRefreshCommand()
        {
            try
            {
                this.ShowLoading = true;
                this.ShowLoadingProgress = false;
                AppCore.CancellationTokenSource = new CancellationTokenSource();
                bool refresh = false;
                //Check For any Changes and prompt user
                await Task.Run(async () =>
                {
                    if (File.Exists(AppCore.ModelFile))
                    {
                        bool areEqual;
                        List<Contact> lastContacts;
                        try
                        {
                            lastContacts = AppCore.Model.Contacts;
                            var O1 = lastContacts.SerializeToProtoBufBytes();
                            var O2Object = this.ContactViews.Where(cvi => cvi.ContactViewType == ContactViewType.Contact).Select(cV => ((ContactViewModel)cV).ToModel()).ToList();
                            var O2 = O2Object.SerializeToProtoBufBytes();
                            areEqual = O1.SequenceEqual(O2);
                        }
                        catch { areEqual = false; }
                        if (areEqual) refresh = true;
                        else
                        {
                            refresh = await AppCore.AppService.MessagePromptYesNo("Refresh page?", "Recent changes will be lost!", "Yes", "No");
                        }
                    }
                }, AppCore.CancellationTokenSource.Token);

                //Perform the Refresh
                if (refresh)
                {
                    await AppCore.RefreshModel(AppCore.CancellationTokenSource.Token);
                    AppCore.VM.LoadViewModel();
                }
            }
            catch (Exception ex)
            {
                await AppCore.AppService.HandleError(ex);
                Debug.WriteLine(ex.ToString());
            }
            finally
            {
                this.ShowLoading = false;
                this.ShowLoadingProgress = false;
                this.LoadingProgress = 0;
            }
        }
        public bool ShowDetails { get => showDetails; set => SetProperty(ref showDetails, value); }
        private bool showDetails = true;
        public Command ShowDetailsTapCommand { get; }
        public Task OnShowDetailsTapCommand() { this.ShowDetails = !this.ShowDetails; return Task.CompletedTask;  }
       

        #endregion


        //This is the View Model for the Group Configuration Page. It is constructed from the latest Group Model
        public ObservableRangeCollection<ContactGroupConfigureViewModel> ContactGroupConfigures { get; set; } = new ObservableRangeCollection<ContactGroupConfigureViewModel>();


    }
}