using ContactManager.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;
using ContactManager.Core;
using AzUtil.Core;
using ContactManager.Core.Properties;

namespace ContactManager.ViewModels
{
    public enum ContactAction { Done, Saving, Deleting, None}

    public class ContactViewModel : BaseContactViewModel, INotifyPropertyChanged
    {
        public ContactViewModel() => CommonConstructor();
        private void CommonConstructor()
        {
            this.StarTappedCommand = new Command(OnStarTappedCommand);
            this.DeleteTappedCommand = new Command(async _ => await OnDeleteTappedCommand());
            this.UndoTappedCommand = new Command(OnUndoTappedCommand);
            this.NameDropDownTappedCommand = new Command(OnNameDropDownTappedCommand);
            this.AddActivityTappedCommand = new Command(OnAddActivityTappedCommand);
            this.AddOtherTapped = new Command(OnAddOtherTapped);
            this.MoreTappedCommand = new Command(OnMoreTappedCommand);
            this.MoreGroupsTappedCommand = new Command(OnMoreGroupsTappedCommand);
            this.AddNewGroupCommand = new Command(async _ => await OnAddNewGroupCommand());
            this.AppVM = AppCore.VM;
            this.ContactViewType = ContactViewType.Contact;
            this.GroupMemberships.CollectionChanged += (s, e) =>
            {
                OnPropertyChanged("GroupMembershipsView");
            };
        }

        public ContactViewModel(Contact contact)
        {
            LoadContact(contact);
        }
        public void LoadContact(Contact contact)
        {
            Id = contact.Id;
            Name = contact.Name;
            Prefix = contact.Prefix;
            FirstName = contact.FirstName;
            MiddleName = contact.MiddleName;
            LastName = contact.LastName;
            Suffix = contact.Suffix;
            PhotoThumbUri = contact.PhotoThumbUri;
            Nickname = contact.Nickname;
            Note = contact.Note;
            Starred = contact.Starred;
            Company = contact.Company;
            Job = contact.Job;
            this.DataFields.AddRange(contact.Phones.Select(e => new PhoneViewModel(e.Id, e.Data, e.Type, e.CustomType)));
            this.DataFields.AddRange(contact.Emails.Select(e => new EmailViewModel(e.Id, e.Data, e.Type, e.CustomType)));
            this.DataFields.AddRange(contact.Addresses.Select(e => new AddressViewModel(e.Id, e.Data, e.Type, e.CustomType,e.Street,e.POBox,e.Neighborhood,e.City,e.Region,e.Postcode,e.Country)));
            this.DataFields.AddRange(contact.Relations.Select(e => new RelationViewModel(e.Id, e.Data, e.Type, e.CustomType)));
            this.DataFields.AddRange(contact.Events.Select(e => new EventViewModel(e.Id, e.Data, e.Type, e.CustomType)));
            this.DataFields.AddRange(contact.Websites.Select(e => new WebsiteViewModel(e.Id, e.Data, e.Type, e.CustomType)));
            this.DataFields.AddRange(contact.IMs.Select(e => new IMViewModel(e.Id, e.Data, e.Type, e.CustomType)));
            this.ConstructGroupMemberships(contact);
            GoogleAccounts = contact.GoogleAccounts.Select(e=> new GoogleAccount(e.AccountName, e.AccountType, e.RawContactId)).ToList();
            StructuredNameDataId = contact.StructuredNameDataId;
            NicknameDataId = contact.NicknameDataId;
            NoteDataId = contact.NoteDataId;
            CompanyDataId = contact.CompanyDataId;
            RelatedRawContactIds = contact.RelatedRawContactIds.ToList();
            BeforeEditState = contact;
            this.UpdateThumbnail();
            CommonConstructor();

        }


        #region Methods

        public async Task Save()
        {
            Contact existingContactModel = null;
            if (this.Id != null) existingContactModel = AppCore.Model.Contacts.FirstOrDefault(c => c.Id == this.Id);
            if (existingContactModel == null)
            {
                var newContact = this.ToModel();
                Contact updatedContact = await AppCore.DeviceService.TrySaveContact(newContact);
                if (updatedContact != null) 
                {
                    AppCore.Model.Contacts.Add(updatedContact);
                    AppCore.Model.Save();
                }
                else this.SaveFailed = true;
            }
        }


        public Contact ToModel()
        {
            var contact = new Contact()
            {
                Id = this.Id,
                Name = this.Name,
                Prefix = this.Prefix,
                FirstName = this.FirstName,
                MiddleName = this.MiddleName,
                LastName = this.LastName,
                Suffix = this.Suffix,
                Nickname = this.Nickname,
                Note = this.Note,
                Starred = this.Starred,
                Company = this.Company,
                Job = this.Job,
                StructuredNameDataId = this.StructuredNameDataId,
                NicknameDataId = this.NicknameDataId,
                RelatedRawContactIds = this.RelatedRawContactIds.OrderBy(d=>d).ToList(),
                NoteDataId = this.NoteDataId,
                CompanyDataId = this.CompanyDataId,
                CustomData = this.DataFields.Where(d => d is CustomDataViewModal).Select(d => (d as CustomDataViewModal).ToModel()).OrderBy(d => d.Type).ThenBy(d=>d.Id).ToList(),
                Phones = this.DataFields.Where(d => d is PhoneViewModel).Select(d => (d as PhoneViewModel).ToModel()).OrderBy(d => d.Type).ThenBy(d => d.Id).ToList(),
                Emails = this.DataFields.Where(d => d is EmailViewModel).Select(d => (d as EmailViewModel).ToModel()).OrderBy(d => d.Type).ThenBy(d => d.Id).ToList(),
                Addresses = this.DataFields.Where(d => d is AddressViewModel).Select(d => (d as AddressViewModel).ToModel()).OrderBy(d => d.Type).ThenBy(d => d.Id).ToList(),
                Relations = this.DataFields.Where(d => d is RelationViewModel).Select(d => (d as RelationViewModel).ToModel()).OrderBy(d => d.Type).ThenBy(d => d.Id).ToList(),
                Events = this.DataFields.Where(d => d is EventViewModel).Select(d => (d as EventViewModel).ToModel()).OrderBy(d => d.Type).ThenBy(d => d.Id).ToList(),
                Websites = this.DataFields.Where(d => d is WebsiteViewModel).Select(d => (d as WebsiteViewModel).ToModel()).OrderBy(d => d.Type).ThenBy(d => d.Id).ToList(),
                IMs = this.DataFields.Where(d => d is IMViewModel).Select(d => (d as IMViewModel).ToModel()).OrderBy(d => d.Type).ThenBy(d => d.Id).ToList(),
                GoogleAccounts = this.GoogleAccounts.OrderBy(g=>g.AccountName).ToList(),
                GroupMemberships = this.GroupMemberships.Where(d => d.Joined).Select(d => d.ToModel()).OrderBy(d => d.MembershipLinkId).ToList(),
                PhotoThumbUri = this.PhotoThumbUri
            };
            return contact;
        }




        private void RestoreBeforeEditState() { if (this.BeforeEditState != null) LoadContact(this.BeforeEditState);}
        protected bool SetPropertyEdited<T>(ref T backingStore, T value, [CallerMemberName] string propertyName = "", Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value)) return false;

            if (backingStore != null) this.Edited = true;
            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            
            return true;
        }
        public void UpdateThumbnail()
        {
            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    throw new NotImplementedException();
                case Device.Android:
                    if (!this.PhotoThumbUri.IsNullOrEmpty())
                    {
                        var deviceService = DependencyService.Get<IDeviceService>();
                        thumbnailBytes = deviceService.GetImage_Droid(this.PhotoThumbUri);
                        OnPropertyChanged("Thumbnail");
                    }
                    break;
                default: throw new NotImplementedException();
            }
        }


       
        //Contructs the Initial GroupMembership after a refresh/
        public void ConstructGroupMemberships(Contact contact)
        {
            var toggleGroupsToAdd = new List<GroupMembershipViewModel>();

            //First add the Toggle Groups
            foreach (var tG in AppCore.Settings.ToggleGroups.OrderBy(tG=>tG)) 
            {
                ContactGroup contactGroup = AppCore.Model.ContactGroups.ValueOrDefault(tG);
                ContactGroupViewModel contactGroupViewModel;
                if (contactGroup == null) contactGroupViewModel = new ContactGroupViewModel() { Name = tG, CannotBeDeleted = false }; //If group is not found in Model, this means the group just exists in th toggle settings. Id must be null.
                else contactGroupViewModel = new ContactGroupViewModel(contactGroup);
                toggleGroupsToAdd.Add(new GroupMembershipViewModel()
                {
                    MembershipLinkId = null, //This means the contact has not joined this group yet
                    ContactGroup = contactGroupViewModel,
                    Joined = false,
                    GroupMembershipChipType = GroupMembershipChipType.Group,
                    Contact = this
                });
            }

            //Then add the other groups which the contact has joined
            var groupsToAdd = new List<GroupMembershipViewModel>();
            foreach (var groupMembership in contact.GroupMemberships)  
            {
                var gMVM = toggleGroupsToAdd.FirstOrDefault(g => g.ContactGroup.Name == groupMembership.GroupName);
                if (gMVM!= null) gMVM.Joined = true;
                else
                {
                    if (!AppCore.Model.ContactGroups.TryGetValue(groupMembership.GroupName, out ContactGroup contactGroup)) contactGroup = new ContactGroup(groupMembership.GroupName,false, new List<GroupGoogleAccounts>());
                    groupsToAdd.Add(new GroupMembershipViewModel()
                    {
                        MembershipLinkId = groupMembership.MembershipLinkId, 
                        ContactGroup = new ContactGroupViewModel(contactGroup),
                        Joined = true,
                        GroupMembershipChipType = GroupMembershipChipType.Group,
                        Contact = this
                    });
                }
            }

            //Finally add the remaining groups not joined by the contact
            foreach (ContactGroup contactGroup in AppCore.Model.ContactGroups.Where(g=>!toggleGroupsToAdd.Any(tG=>tG.ContactGroup.Name == g.Key) && !groupsToAdd.Any(g2 => g2.ContactGroup.Name == g.Key)).Select(e=>e.Value)) 
            {
                groupsToAdd.Add(new GroupMembershipViewModel()
                {
                    Contact = this,
                    ContactGroup = new ContactGroupViewModel(contactGroup),
                    GroupMembershipChipType = GroupMembershipChipType.Group,
                    Joined = false
                });
            }
            var groupsToAddSorted = groupsToAdd.OrderBy(g => g.ContactGroup.Name);
            toggleGroupsToAdd.AddRange(groupsToAddSorted);
            this.GroupMemberships.Clear();
            this.GroupMemberships.AddRange(toggleGroupsToAdd);            
        }

        ///Updates the Group Membership Views - This is used for Data Binding for the Group Chips in a Contact Profile <see cref="ContactViewModel.GroupMembershipsView"/>
        //public void UpdateGroupMembershipsView()
        //{
        //    //Create a Temporary Group to Work On
        //    var updatedGroupMembership = new List<GroupMembershipViewModel>();
        //    updatedGroupMembership.AddRange(this.GroupMembershipsView.Where(gM => gM.GroupMembershipChipType == GroupMembershipChipType.Group));  //Add existing groups

        //    //  Remove any Group Memberships where the the does not exist anymore in the Group Model
        //    var groupMembershipWithGroupsThatNoLongerExistInGroupsModel = updatedGroupMembership.Where(gM => !App.Model.ContactGroups.Any(g => g.Name == gM.ContactGroup.Name));
        //    foreach (var gM in groupMembershipWithGroupsThatNoLongerExistInGroupsModel) updatedGroupMembership.Remove(gM);

        //    //Update Toggle Groups
        //    updatedGroupMembership.ForEach(gM =>
        //    {
        //        if (gM.GroupMembershipChipType == GroupMembershipChipType.Group) gM.ContactGroup.IsToggleGroup = App.Settings.ToggleGroups.Any(tG => tG == gM.ContactGroup.Name);
        //    });

        //    //Add New Toggle Groups not added to group
        //    var newToggleGroupsNotAdded = App.Settings.ToggleGroups.Where(tG => !updatedGroupMembership.Any(gM => gM.ContactGroup.Name == tG));
        //    foreach (var newTG in newToggleGroupsNotAdded)
        //    {
        //        ContactGroup contactGroup = App.Model.ContactGroups.FirstOrDefault(g => g.Name == newTG);
        //        ContactGroupViewModel contactGroupViewModel;
        //        if (contactGroup == null) contactGroupViewModel = new ContactGroupViewModel() { Name = newTG, CannotBeDeleted = false, IsToggleGroup = true }; //If group is not found in ContactManagerData, this means the group just exists in th toggle settings. Id must be null.
        //        else contactGroupViewModel = new ContactGroupViewModel(contactGroup, true);
        //        updatedGroupMembership.Add(new GroupMembershipViewModel()
        //        {
        //            MembershipLinkId = null,
        //            ContactGroup = contactGroupViewModel,
        //            Joined = false,
        //            GroupMembershipChipType = GroupMembershipChipType.Group,
        //            Contact = this
        //        });
        //    }


        //    //Add New Groups which the contact has joined from the contact model
        //    foreach (var groupMembership in contact.GroupMemberships) 
        //    {
        //        var gMVM = groupsToAdd.FirstOrDefault(g => g.ContactGroup.Name == groupMembership.GroupName);
        //        if (gMVM != null) gMVM.Joined = true;
        //        else
        //        {
        //            groupsToAdd.Add(new GroupMembershipViewModel()
        //            {
        //                MembershipLinkId = groupMembership.MembershipLinkId,
        //                ContactGroup = new ContactGroupViewModel(groupMembership.ContactGroup, false),
        //                Joined = true,
        //                GroupMembershipChipType = GroupMembershipChipType.Group,
        //                Contact = this
        //            });
        //        }
        //    }

        //    if (this.ShowMoreGroups)
        //    {
        //        foreach (GroupViewModel groupInDB in App.VM.Groups)
        //        {
        //            if (!groupsToAdd.Any(g => g.GroupName == groupInDB.Name))
        //            {
        //                groupsToAdd.Add(new GroupMembershipViewModel()
        //                {
        //                    AppVM = App.VM,
        //                    Contact = this,
        //                    Group = groupInDB,
        //                    GroupName = groupInDB.Name,
        //                    GroupMembershipChipType = GroupMembershipChipType.Group,
        //                    Joined = false
        //                });
        //            }
        //        }
        //    }
        //    else
        //    {

        //        var toRemoves = groupsToAdd.Where(g => !g.Joined && !g.Group.IsToggleGroup).ToList();
        //        foreach (var g in toRemoves) groupsToAdd.Remove(g);

        //    }


        //    var groupsToAddSorted = groupsToAdd.OrderByDescending(g => g.Group.IsToggleGroup).ThenBy(g => g.Group.Name);
        //    this.GroupMembershipsView.Clear();
        //    this.GroupMembershipsView.AddRange(groupsToAddSorted);
        //    if (this.ShowMoreGroups) this.GroupMembershipsView.Add(new GroupMembershipViewModel() { GroupMembershipChipType = GroupMembershipChipType.NewButton, Contact = this });
        //    this.GroupMembershipsView.Add(new GroupMembershipViewModel() { GroupMembershipChipType = GroupMembershipChipType.ShowHideGroupsButton, Contact = this });
        //}



        public string GetName(string prefix, string fn, string mn, string ln, string suffix)
        {
            string res = "";
            if (!string.IsNullOrEmpty(prefix)) res += prefix;
            if (!string.IsNullOrEmpty(fn)) if (res == "") res += fn; else res = res + " " + fn;
            if (!string.IsNullOrEmpty(mn)) if (res == "") res += mn; else res = res + " " + mn;
            if (!string.IsNullOrEmpty(ln)) if (res == "") res += mn; else res = res + " " + ln;
            if (!string.IsNullOrEmpty(suffix)) if (res == "") res += suffix; else res = res + ", " + suffix;
            return res;
        }
        #endregion

        #region Name and Basic 
        public string Name { get => name; set => SetPropertyEdited(ref name, value); }  private string name;
        public string Prefix { get => prefix; set => SetPropertyEdited(ref prefix, value); } private string prefix;
        public string FirstName { get => firstName; set => SetPropertyEdited(ref firstName, value); } private string firstName;
        public string MiddleName { get => middleName; set => SetPropertyEdited(ref middleName, value); } private string middleName;
        public string LastName { get => lastName; set => SetPropertyEdited(ref lastName, value); } private string lastName;
        public string Suffix { get => suffix; set => SetPropertyEdited(ref suffix, value); } private string suffix;
        private void SetName(string fullName)
        {
            if (fullName == null) return;
            string[] portions = fullName.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (portions.Count() == 1)
            {
                this.Prefix = null;
                this.FirstName = fullName;
                this.MiddleName = null;
                this.LastName = null;
                this.Suffix = null;
            }
            if (portions.Count() == 2)
            {
                this.Prefix = null;
                this.FirstName = portions[0];
                this.MiddleName = null;
                this.LastName = portions[1];
                this.Suffix = null;
            }
            if (portions.Count() == 3)
            {
                this.Prefix = null;
                this.FirstName = portions[0];
                this.MiddleName = portions[1];
                this.LastName = portions[2];
                this.Suffix = null;
            }
            if (portions.Count() > 3)
            {
                this.Prefix = null;
                this.FirstName = portions[0];
                this.MiddleName = null;
                this.LastName = portions[1..].ToSentence();
                this.Suffix = null;
            }
        }
        public string PhotoThumbUri { get => photoThumbUri; set => SetProperty(ref photoThumbUri, value); } private string photoThumbUri;
        public string Nickname { get => nickname; set => SetPropertyEdited(ref nickname, value); } private string nickname;
        public string Note { get => note; set => SetPropertyEdited(ref note, value); } private string note;
        public bool Starred { get => starred; set => SetPropertyEdited(ref starred, value); } private bool starred;
        public string Company { get => company; set => SetPropertyEdited(ref company, value, null, () => { OnPropertyChanged("ShowCompany"); }); } private string company;
        public string Job { get => job; set => SetPropertyEdited(ref job, value, null, () => { OnPropertyChanged("ShowJob"); }); } private string job;
        #endregion
        public ObservableRangeCollection<DataViewModel> DataFields { get; set; } = new ObservableRangeCollection<DataViewModel>();
        public ObservableRangeCollection<GroupMembershipViewModel> GroupMemberships { get; set; } = new ObservableRangeCollection<GroupMembershipViewModel>();
      


        #region Commands
        public Command AddNewGroupCommand { get; private set; }
        private async Task OnAddNewGroupCommand()
        {
            string newGroupName = await AppCore.AppService.MessagePromptInput("Add New Group","Group Name?");
            if (newGroupName.IsNullOrWhiteSpace()) return;
            var groupAlreadyExist = this.GroupMemberships.FirstOrDefault(gM => gM.ContactGroup.Name == newGroupName);
            if (groupAlreadyExist != null)
            {
                groupAlreadyExist.Joined = true;
            }
            else
            {
                this.GroupMemberships.Add(new GroupMembershipViewModel()
                {
                    Contact = this,
                    ContactGroup = new ContactGroupViewModel() { Name = newGroupName,CannotBeDeleted=false },
                    GroupMembershipChipType = GroupMembershipChipType.Group,
                    Joined = true
                });
                UpdateGroupMembershipsView?.Invoke();
            }
        }

        public Action UpdateGroupMembershipsView;

        public Command MoreGroupsTappedCommand { get; private set; }
        private void OnMoreGroupsTappedCommand(object obj) 
        { 
            this.ShowMoreGroups = !this.ShowMoreGroups;
        }
        public Command MoreTappedCommand { get; private set; }
        private void OnMoreTappedCommand(object obj) { this.ShowMore = !this.ShowMore; }

        public Command StarTappedCommand { get; private set; }
        private void OnStarTappedCommand(object obj) { this.Starred = !this.Starred; }
        public Command DeleteTappedCommand { get; private set; }
        private async Task OnDeleteTappedCommand()
        {
            bool answer = await AppCore.AppService.MessagePromptYesNo(AppResources.DeleteContact, null, AppResources.Yes, AppResources.No);
            if (answer)
            {
                this.AppVM.ContactViews.Remove(this);
            }
        }
        public Command UndoTappedCommand { get; private set; }
        private void OnUndoTappedCommand(object obj)
        {
            this.Edited = false;
            this.RestoreBeforeEditState();
            this.BeforeEditState = this.ToModel();
            this.Edited = false;
        }
        public Command AddActivityTappedCommand { get; private set; }
        private void OnAddActivityTappedCommand(object obj)
        {
            if (this.Note.IsNullOrWhiteSpace()) this.Note = DateTime.Now.ToShortDateString() + " ";
            else this.Note = this.Note + "\r\n" + DateTime.Now.ToShortDateString() + " ";
        }
        public Command NameDropDownTappedCommand { get; private set; }
        private void OnNameDropDownTappedCommand(object obj) { this.ShowNameDropDown = !ShowNameDropDown; }
        public Command AddOtherTapped { get; private set; }
        private void OnAddOtherTapped(object obj) { this.ShowAddMoreWindow = true; }

        #endregion


        #region UI Related
        public bool SaveFailed { get => saveFailed; set => SetProperty(ref saveFailed, value); } private bool saveFailed = false;
        public bool ShowMoreGroups { get => showMoreGroups; set => SetProperty(ref showMoreGroups, value, null, () => OnPropertyChanged("GroupMembershipsView")); } private bool showMoreGroups = false;
        public bool ShowMore { get => showMore; set => SetProperty(ref showMore, value, null, () => { OnPropertyChanged("ShowCompany"); OnPropertyChanged("ShowJob"); }); } private bool showMore = false;
        public bool ShowAddMoreWindow { get => showAddMoreWindow; set => SetProperty(ref showAddMoreWindow, value); } private bool showAddMoreWindow = false;
        public bool ShowCompany { get => ShowMore || !this.Company.IsNullOrEmpty(); }
        public bool ShowJob { get => ShowMore || !this.Job.IsNullOrEmpty(); }
        public bool ShowNameDropDown { get => showNameDropDown; set => SetProperty(ref showNameDropDown, value); } private bool showNameDropDown = false;
        public ImageSource Thumbnail
        {
            get
            {
                if (thumbnailBytes == null) return null;
                ImageSource imageSource = ImageSource.FromStream(() => new MemoryStream(thumbnailBytes));
                return imageSource;
            }
        }
        private Byte[] thumbnailBytes;
        public AppViewModel AppVM { get => appVM; set => SetProperty(ref appVM, value); } private AppViewModel appVM;
        public bool Edited { get => edited; set => SetProperty(ref edited, value); } private bool edited;
        public bool NameUnderEdit { get => nameUnderEdit; set => SetProperty(ref nameUnderEdit, value); } private bool nameUnderEdit = false;
        public ContactAction ContactAction { get => contactAction; set => SetProperty(ref contactAction, value); } private ContactAction contactAction = ContactAction.None;


        private readonly ObservableRangeCollection<GroupMembershipViewModel> groupMembershipsView = new ObservableRangeCollection<GroupMembershipViewModel>();
        public ObservableRangeCollection<GroupMembershipViewModel> GroupMembershipsView
        {
            get
            {
                if (this.ShowMoreGroups)
                {
                    groupMembershipsView.Clear();
                    groupMembershipsView.AddRange(this.GroupMemberships);
                   // groupMembershipsView.Add(new GroupMembershipViewModel() { GroupMembershipChipType = GroupMembershipChipType.ShowHideGroupsButton, Contact = this });
                }
                else
                {
                    var res = this.GroupMemberships.Where(g => g.Joined).ToList();
                    var toggleGroupsNotAdded = AppCore.Settings.ToggleGroups.Where(tG => !res.Any(gM => gM.ContactGroup.Name == tG)).OrderByDescending(tG=>tG);
                    foreach (var tG in toggleGroupsNotAdded)
                    {
                        var tGtoAdd = this.GroupMemberships.FirstOrDefault(g => g.ContactGroup.Name == tG);
                        if (tGtoAdd != null) res.Insert(0, tGtoAdd);
                    }
                    res.Add(new GroupMembershipViewModel() { GroupMembershipChipType = GroupMembershipChipType.NewButton, Contact = this });
                    //res.Add(new GroupMembershipViewModel() { GroupMembershipChipType = GroupMembershipChipType.ShowHideGroupsButton, Contact = this });
                    groupMembershipsView.Clear();
                    groupMembershipsView.AddRange(res);
                }
                return groupMembershipsView;
            }
        }

        #endregion
        public Contact BeforeEditState { get; set; } = null;
    }
}
