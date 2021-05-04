using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AzUtil.Core;
using ContactManager.Models;
using ContactManager.ViewModels;
using ProtoBuf;
using Xamarin.Forms;

namespace ContactManager
{
    public interface IDeviceService
    {
        public void SetTopStatusBar(Color color);
        public AppModel GetAppModel(Action<string,double> action, CancellationToken ctm, Action<Object> Debug); 
        public Task<Contact> TrySaveContact(Contact contact); 
        public Task<bool> TryDeleteGroup(string Id); 
        public byte[] GetImage_Droid(string uri);
    }


}
