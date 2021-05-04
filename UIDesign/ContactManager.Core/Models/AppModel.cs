using ContactManager.Core;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ContactManager.Models
{
    [ProtoContract]
    public class AppModel
    {

        [ProtoMember(1)]
        public List<Contact> Contacts { get; set; } = new List<Contact>();

        [ProtoMember(2)]
        public Dictionary<string, ContactGroup> ContactGroups { get; set; } = new Dictionary<string, ContactGroup>(); //This is referred to in Contact.GroupMembership.Group

        //public void UpdateReferencesAfterDeserialize()
        //{
        //    foreach (Contact contact in Contacts)
        //    {
        //        foreach (var groupMembership in contact.GroupMemberships)
        //        {
        //            groupMembership.Contact = contact;
        //            var groupFound = this.ContactGroups.FirstOrDefault(g => g.Name == groupMembership.GroupName);
        //            if (groupFound != null) groupMembership.ContactGroup = groupFound;
        //            else
        //            {
        //                var k = 1; //CHECK WHY THIS IS HAPPENING
        //            }
        //        }
        //    }
        //}

        public void Save()
        {
            this.SerializeToProtoBufFile(AppCore.ModelFile);
        }
    }

}
