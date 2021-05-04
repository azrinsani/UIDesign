using ProtoBuf;
using System.Collections.Generic;

namespace ContactManager.Models
{
    [ProtoContract]
    public class ContactGroup
    {
        public ContactGroup() { }
        public ContactGroup(string name, bool cannotBeDeleted, List<GroupGoogleAccounts> groupGoogleAccounts) { this.Name = name; this.CannotBeDeleted = cannotBeDeleted; this.GroupGoogleAccounts = groupGoogleAccounts; }
        [ProtoMember(1)]
        public string Name { get; set; }
        [ProtoMember(2)]
        public bool CannotBeDeleted { get; set; }
        [ProtoMember(3)]
        public List<GroupGoogleAccounts> GroupGoogleAccounts { get; set; } = new List<GroupGoogleAccounts>(); //Group with same name will be grouped and a record of each individual Google Accounts and Its Group Id (in the Android Database) is recorded here
    }


    //This stores all the google accounts associated with this group. A group can be in multiple accounts. We aggregate these groups with same name. The aggregation results in a List of Group Ids and Account Name
    //At least one value needs to be available here if it is to sync with a google account. 
    [ProtoContract]
    public class GroupGoogleAccounts
    {
        public GroupGoogleAccounts() { }
        public GroupGoogleAccounts(string groupId, string account) { this.GroupId = groupId; this.Account = account; }
        [ProtoMember(1)]
        public string GroupId { get; set; }
        [ProtoMember(2)]
        public string Account { get; set; }
    }
}
