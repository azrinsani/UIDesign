using ContactManager.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using AzUtil.Core;
namespace ContactManager.Views
{

    public partial class Contact : ContentView
    {
        public Contact() { InitializeComponent();}

        private void Name_Tapped(object sender, EventArgs e) 
        { 
           // this.Name.Focus(); 
        }

      
    }
}
