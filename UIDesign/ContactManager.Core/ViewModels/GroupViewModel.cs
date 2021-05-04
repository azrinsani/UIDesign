
using ProtoBuf;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using ContactManager.Models;
namespace ContactManager.ViewModels
{
    public class ContactGroupConfigureViewModel : ContactGroupViewModel, INotifyPropertyChanged
    {
        public ContactGroupConfigureViewModel(ContactGroup contactGroup, bool isToggle) :base(contactGroup) { this.IsToggle = isToggle; }
        public bool IsToggle { get => isToggle; set => SetProperty(ref isToggle, value); }
        private bool isToggle = false;
        public bool ToDelete { get => toDelete; set => SetProperty(ref toDelete, value); }
        private bool toDelete = false;
    }
    public class ContactGroupViewModel : BaseViewModel, INotifyPropertyChanged
    {        
        public ContactGroupViewModel() {}
        public ContactGroupViewModel(ContactGroup contactGroup)
        {
            this.Name = contactGroup.Name;
            this.CannotBeDeleted = contactGroup.CannotBeDeleted;
        }
        public ContactGroup ToModel()
        {
            return new ContactGroup(this.Name, this.CannotBeDeleted, this.GroupGoogleAccounts);
        }
        public string Name { get => name; set => SetProperty(ref name, value, null, () => OnPropertyChanged("NameShort")); } //The name is the Key. A group can belong to multiple accounts
        private string name;
        public bool CannotBeDeleted { get => cannotBeDeleted; set => SetProperty(ref cannotBeDeleted, value); }
        private bool cannotBeDeleted = false;
        public string NameShort { get => (Name.Length > 20) ? name[0..20] + ".." : Name; }

        //Group with same name will be grouped and a record of each individual Google Accounts an Its Group Id (in the Android Database) is recorded here. 
        //Id there is not entry this means that it is a new group and needs to be created in the contact's account
        public List<GroupGoogleAccounts> GroupGoogleAccounts { get; set; } = new List<GroupGoogleAccounts>();
    }

    //This stores all the google accounts associated with this group. A group can be in multiple accounts. We aggregate these groups with same name. The aggregation results in a List of Group Ids and Account Name
    //At least one value needs to be available here if it is to sync with a google account. 
 
}