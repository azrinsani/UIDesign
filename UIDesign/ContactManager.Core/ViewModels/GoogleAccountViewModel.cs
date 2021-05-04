
using ProtoBuf;

namespace ContactManager.ViewModels
{
    [ProtoContract]
    public class GoogleAccountViewModel : BaseViewModel
    {
        public GoogleAccountViewModel() { }
        public GoogleAccountViewModel(string ac, string at, string rawContactId) { this.AccountName = ac;this.AccountType = at; this.RawContactId = rawContactId; }


        [ProtoIgnore]
        public string RawContactId { get; set; }

        [ProtoMember(1)]
        public string AccountName { get => accountName; set => SetProperty(ref accountName, value); } //The account Name which holds this Contact.
        private string accountName;

        
        [ProtoMember(2)]
        public string AccountType { get => accountType; set => SetProperty(ref accountType, value); } //The account type which holds this Contact e.g. (com.google, com.whatsapp)
        private string accountType;

    }




    [ProtoContract]
    public class AppAccountViewModel : BaseViewModel //THis stores an Application Detail (e.g. Whatsapp, Telegram, etc) which has been linked to this account
    {
        public AppAccountViewModel() { }
        public AppAccountViewModel(string ac, string at, string rawContactId) { this.AccountName = ac; this.AccountType = at; this.RawContactId = rawContactId; }

        [ProtoIgnore]
        public string RawContactId { get; set; }

        [ProtoMember(1)]
        public string AccountName { get => accountName; set => SetProperty(ref accountName, value); } //The account Name which holds this Contact.
        private string accountName;


        [ProtoMember(2)]
        public string AccountType { get => accountType; set => SetProperty(ref accountType, value); } //The account type which holds this Contact e.g. (com.google, com.whatsapp)
        private string accountType;

    }
}
