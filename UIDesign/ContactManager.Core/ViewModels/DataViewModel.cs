
using ProtoBuf;
using System.ComponentModel;
using ContactManager.Models;
namespace ContactManager.ViewModels
{
    [ProtoContract]
    [ProtoInclude(50, typeof(PhoneViewModel))]
    [ProtoInclude(51, typeof(RelationViewModel))]
    [ProtoInclude(52, typeof(IMViewModel))]
    [ProtoInclude(53, typeof(EventViewModel))]
    [ProtoInclude(54, typeof(EmailViewModel))]
    [ProtoInclude(55, typeof(AddressViewModel))]
    [ProtoInclude(56, typeof(WebsiteViewModel))]
    [ProtoInclude(57, typeof(CustomDataViewModal))]
    public class DataViewModel : BaseViewModel, INotifyPropertyChanged
    {
        public DataViewModel() { }
        public DataViewModel(string id, string data, string type, string customType)
        {
            this.Id = id;
            this.Data = data;
            this.Type = type;
            this.CustomType = customType;
        }        
        [ProtoMember(1)]
        public string Id { get => id; set => SetProperty(ref id, value); } //In Android this is used as the Row Id in the DataTable where this value is stored
        private string id;
        
        [ProtoMember(2)]
        public string Data { get => data; set => SetProperty(ref data, value); } //This holds the actual data such as '04321224' for phone number view model
        private string data;

        
        [ProtoMember(3)]
        public string Type { get => type; set => SetProperty(ref type, value); } //This holds the type data if the data has a particular type, for example phone data has type 'Home', 'Work', etc
        private string type;

        
        [ProtoMember(4)]
        public string CustomType { get => customType; set => SetProperty(ref customType, value); }
        private string customType;

    }
}
