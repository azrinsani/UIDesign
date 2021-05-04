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
    public interface IAppService
    {
        public Task<bool> MessagePromptYesNo(string title, string content, string yes, string no);
        public Task<string> MessagePromptInput(string title, string content);
        public Task HandleError(Exception ex);

    }


  
}
