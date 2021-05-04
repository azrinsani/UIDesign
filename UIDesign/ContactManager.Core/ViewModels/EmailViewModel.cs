using ProtoBuf;
using System.ComponentModel;
using ContactManager.Models;
namespace ContactManager.ViewModels
{
    [ProtoContract]
    public class EmailViewModel : DataViewModel, INotifyPropertyChanged
    {
        public EmailViewModel() { }
        public EmailViewModel(string id, string data, string type, string customType) : base(id, data, type, customType) { }
        public EmailData ToModel()
        {
            return new EmailData(this.Id, this.Type, this.Data, this.CustomType);
        }
    }
}
