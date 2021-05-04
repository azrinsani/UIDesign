using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace ContactManager.Models
{
    [ProtoContract]
    [ProtoInclude(10, typeof(Contact))]
    public class NonBindableContactBase
    {
        public NonBindableContactBase() { }
        [ProtoMember(1)]
        public string Id { get; set; }

        #region Android Related

        [ProtoMember(2)]
        public List<string> RelatedRawContactIds { get; set; } = new List<string>(); //Stores all the Row Ids in the Raw Contact Table which holds data to construct this object.

        [ProtoMember(3)]
        public List<GoogleAccount> GoogleAccounts { get; set; } = new List<GoogleAccount>(); //This stores all the related raw contacts which should be updates if a contact is updated.
        [ProtoMember(4)]
        public string StructuredNameDataId { get; set; } //The row id in the Data Table where the note is taken from

        [ProtoMember(5)]
        public string NoteDataId { get; set; } //The row id in the Data Table where the note is taken from

        [ProtoMember(6)]
        public string NicknameDataId { get; set; } //The row id in the Data Table where the NickName is taken from

        //This is the 'DB level state' of the Group Membership Table. It remains constant until refresh. For Android as Android has a linking table. Any groups here mean user has ALREADY JOINED The group in Database
        [ProtoMember(7)]
        public string CompanyDataId { get; set; } //In Android this is used as the Row Id in the Data Table where the Organization and Position Data is stored

        #endregion
    }

    [ProtoContract]
    public class Contact: NonBindableContactBase
    {
        public Contact() { }      
        [ProtoMember(6)]
        public string Name { get; set; }
        [ProtoMember(7)]
        public string Prefix { get; set; }
        [ProtoMember(8)]
        public string FirstName { get; set; }
        [ProtoMember(9)]
        public string MiddleName { get; set; }
        [ProtoMember(10)]
        public string LastName { get; set; }
        [ProtoMember(11)]
        public string Suffix { get; set; }
        [ProtoMember(17)]
        public string PhotoThumbUri { get; set; }
        [ProtoMember(18)]
        public string Nickname { get; set; }
        [ProtoMember(19)]
        public string Note { get; set; }
        [ProtoMember(20)]
        public bool Starred { get; set; }
        [ProtoMember(21)]
        public string Company { get; set; }
        [ProtoMember(22)]
        public string Job { get; set; }
        [ProtoMember(23)]
        public List<CustomData> CustomData { get; set; } = new List<CustomData>();
        [ProtoMember(24)]
        public List<PhoneData> Phones { get; set; } = new List<PhoneData>();
        [ProtoMember(25)]
        public List<EmailData> Emails { get; set; } = new List<EmailData>();                
        [ProtoMember(26)]
        public List<AddressData> Addresses { get; set; } = new List<AddressData>();
        [ProtoMember(27)]
        public List<RelationData> Relations { get; set; } = new List<RelationData>();
        [ProtoMember(28)]
        public List<EventData> Events { get; set; } = new List<EventData>();
        [ProtoMember(29)]
        public List<WebsiteData> Websites { get; set; } = new List<WebsiteData>();
        [ProtoMember(30)]
        public List<IMData> IMs { get; set; } = new List<IMData>();

        [ProtoMember(31)]
        public List<GroupMembership> GroupMemberships { get; set; } = new List<GroupMembership>();

        //public void UpdateReferences()
        //{
        //    foreach (var groupMembership in this.GroupMemberships)
        //    {
        //        groupMembership.Contact = this;
        //        var groupFound = App.Model.ContactGroups.FirstOrDefault(g => g.Name == groupMembership.GroupName);
        //        if (groupFound != null) groupMembership.ContactGroup = groupFound;
        //        else
        //        {
        //            groupMembership.ContactGroup = null; //This should cause the Save to create a new group
        //            var k = 1; //CHECK WHY THIS IS HAPPENING
        //        }
        //    }
        //}
    }

    [ProtoContract]
    public class GoogleAccount
    {
        public GoogleAccount() { }
        public GoogleAccount(string ac, string at, string rawContactId) { this.AccountName = ac; this.AccountType = at; this.RawContactId = rawContactId; }
        [ProtoMember(1)]
        public string RawContactId { get; set; }
        [ProtoMember(2)]
        public string AccountName { get; set; }//The account Name which holds this Contact.
        [ProtoMember(3)]
        public string AccountType { get; set; }//The account type which holds this Contact e.g. (com.google, com.whatsapp)
    }

    [ProtoContract]
    public class GroupMembership
    {
        public GroupMembership() { }
        public GroupMembership(string membershipLinkId, string groupName) { this.MembershipLinkId = membershipLinkId; this.GroupName = groupName; }
        [ProtoMember(1)]
        public string MembershipLinkId { get; set; } //The Row Id in the Data Table which this group relationship link is stored
        [ProtoMember(2)]
        public string GroupName { get; set; } ///This is the group which is linked to the Contact Object. <see cref="ContactGroup"/> <see cref="AppModel.ContactGroups"/>
        //[ProtoIgnore] //This reference needs to be updated on deserialize since the same group object reference can be used here.
        //public ContactGroup ContactGroup { get; set; }
        //[ProtoIgnore] //This reference needs to be updated on deserialize
        //public Contact Contact { get; set; } = null;
    }


    [ProtoContract]
    [ProtoInclude(50, typeof(PhoneData))]
    [ProtoInclude(51, typeof(RelationData))]
    [ProtoInclude(52, typeof(IMData))]
    [ProtoInclude(53, typeof(EventData))]
    [ProtoInclude(54, typeof(EmailData))]
    [ProtoInclude(55, typeof(AddressData))]
    [ProtoInclude(56, typeof(WebsiteData))]
    [ProtoInclude(57, typeof(CustomData))]
    public class DataField
    {
        public DataField() { }
        public DataField(string id, string data, string type, string customType) { this.Id = id; this.Data = data; this.Type = type; this.CustomType = customType; }
        [ProtoMember(1)]
        public string Id { get; set; } //In Android this is used as the Row Id in the DataTable where this value is stored
        [ProtoMember(2)]
        public string Data { get; set; } //This holds the actual data such as '04321224' for phone number view model
        [ProtoMember(3)]
        public string Type { get; set; } //This holds the type data if the data has a particular type, for example phone data has type 'Home', 'Work', etc, OR FIELD in CUSTOM DATA FIELD
        [ProtoMember(4)]
        public string CustomType { get; set; } //If 'Custom' is chosen as type, this is the name of the custome field. This is not application for the Custom Model.
    }
    [ProtoContract]
    public class CustomData : DataField
    {
        public CustomData() { }
        public CustomData(string id, string type, string data) { this.Id = id; this.Type = type; this.Data = data; }       
    }
    [ProtoContract]
    public class PhoneData : DataField
    {
        public PhoneData() : base() { }
        public PhoneData(string id, string data, string type, string customType) : base(id, data, type, customType) { }
    }
    [ProtoContract]
    public class EmailData : DataField
    {
        public EmailData() { }
        public EmailData(string id, string data, string type, string customType) : base(id, data, type, customType) { }
    }
    [ProtoContract]
    public class IMData : DataField
    {
        public IMData() { }
        public IMData(string id, string data, string type, string customType) : base(id, data, type, customType) { }
    }
    [ProtoContract]
    public class AddressData : DataField
    {
        public AddressData() { }
        public AddressData(string id, string data, string type, string customType, string street, string pOBox, string neighborhood, string city, string region, string postcode, string country)
            : base(id, data, type, customType) { this.Street = street; this.POBox = pOBox; this.Neighborhood = neighborhood; this.City = city; this.Region = region; this.Postcode = postcode; this.Country = country; }
        [ProtoMember(6)]
        public string Street { get; set; }
        [ProtoMember(7)]
        public string POBox { get; set; }
        [ProtoMember(8)]
        public string Neighborhood { get; set; }
        [ProtoMember(9)]
        public string City { get; set; }
        [ProtoMember(10)]
        public string Region { get; set; }
        [ProtoMember(11)]
        public string Postcode { get; set; }
        [ProtoMember(12)]
        public string Country { get; set; }
    }
    [ProtoContract]
    public class WebsiteData : DataField
    {
        public WebsiteData() { }
        public WebsiteData(string id, string data, string type, string customType) : base(id, data, type, customType) { }
    }
    [ProtoContract]
    public class EventData : DataField
    {
        public EventData() { }
        public EventData(string id, string data, string type, string customType) : base(id, data, type, customType) { }
    }
    [ProtoContract]
    public class RelationData : DataField
    {
        public RelationData() { }
        public RelationData(string id, string data, string type, string customType) : base(id, data, type, customType) { }
    }
}
