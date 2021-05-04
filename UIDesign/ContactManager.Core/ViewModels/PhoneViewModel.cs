using ProtoBuf;
using System.ComponentModel;
using ContactManager.Models;
namespace ContactManager.ViewModels
{
    [ProtoContract]    
    public class PhoneViewModel : DataViewModel, INotifyPropertyChanged
    {
        public PhoneViewModel() : base() { }
        public PhoneViewModel(string id, string data, string type, string customType) : base(id, data, type, customType) {}
        public PhoneData ToModel()
        {
            return new PhoneData(this.Id, this.Type, this.Data, this.CustomType);
        }
    }
}
