
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Xamarin.Forms;
using ContactManager.Models;
using ContactManager.Core;

namespace ContactManager.ViewModels
{
    public enum GroupMembershipChipType { Group, ShowHideGroupsButton, NewButton }
    public class GroupMembershipViewModel:BaseViewModel, INotifyPropertyChanged
    {
        public GroupMembershipViewModel()
        {
            this.ToggleGroupCommand = new Command(OnToggleGroupCommand);
            this.AppVM = AppCore.VM;
        }

        public GroupMembership ToModel()
        {
            return new GroupMembership(this.MembershipLinkId, this.ContactGroup.Name);
        }

        #region UI RELATED
        public AppViewModel AppVM { get => appVM; set => SetProperty(ref appVM, value); }
        private AppViewModel appVM;
        #endregion
        public string MembershipLinkId { get; set; } //The Row Id in the Data Table which this group relationship link is stored
        public ContactGroupViewModel ContactGroup { get => contactGroup; set => SetProperty(ref contactGroup, value); } //This does not provide any common reference unlike the model counterpart.
        private ContactGroupViewModel contactGroup;
        public bool Joined { get => joined; set => SetProperty(ref joined, value); } //If set it means that user will join this group when save button is set
        private bool joined = true;
        public GroupMembershipChipType GroupMembershipChipType { get => groupMembershipChipType; set => SetProperty(ref groupMembershipChipType, value); }
        private GroupMembershipChipType groupMembershipChipType = GroupMembershipChipType.Group;
        public ContactViewModel Contact { get; set; } = null;
        public Command ToggleGroupCommand { get; }
        private void OnToggleGroupCommand(object obj) 
        {
            if (this.GroupMembershipChipType == GroupMembershipChipType.Group)
            {
                this.Joined = !this.Joined;
                //if (this.IsToggleGroup) this.Joined = !this.Joined;
                //else
                //{
                //    this.Contact.GroupMembershipsView.Remove(this);
                //}
            }
        }

    }
}
