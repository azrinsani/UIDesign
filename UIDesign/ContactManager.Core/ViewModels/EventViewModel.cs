using ProtoBuf;
using System.ComponentModel;
using ContactManager.Models;
namespace ContactManager.ViewModels
{
    [ProtoContract]
    public class EventViewModel : DataViewModel, INotifyPropertyChanged
    {
        public EventViewModel() { }
        public EventViewModel(string id, string data, string type, string customType) : base(id, data, type, customType) { }
        public EventData ToModel()
        {
            return new EventData(this.Id, this.Type, this.Data, this.CustomType);
        }
    }
}
