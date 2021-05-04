using ProtoBuf;
using System.ComponentModel;
using ContactManager.Models;
namespace ContactManager.ViewModels
{
    [ProtoContract]
    public class RelationViewModel : DataViewModel, INotifyPropertyChanged
    {
        public RelationViewModel() { }
        public RelationViewModel(string id, string data, string type, string customType) : base(id, data, type, customType) { }
        public RelationData ToModel()
        {
            return new RelationData(this.Id, this.Type, this.Data, this.CustomType);
        }
    }
}
