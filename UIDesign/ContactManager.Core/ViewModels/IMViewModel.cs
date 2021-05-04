using ProtoBuf;
using System.ComponentModel;
using ContactManager.Models;
namespace ContactManager.ViewModels
{
    [ProtoContract]
    public class IMViewModel : DataViewModel, INotifyPropertyChanged
    {
        public IMViewModel() { }
        public IMViewModel(string id, string data, string type, string customType) : base(id, data, type, customType) { }
        public IMData ToModel()
        {
            return new IMData(this.Id, this.Type, this.Data, this.CustomType);
        }
    }
}
