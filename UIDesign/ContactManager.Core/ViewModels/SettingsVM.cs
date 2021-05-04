using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
namespace ContactManager.ViewModels
{

    public class SettingsViewModel : BaseViewModel, INotifyPropertyChanged
    {
        public SettingsViewModel()
        {
        }
        //The Color Accent Pallete will have Saturation 55% and Lightness 50%. DEFAULT ACCENT IS #4079bf
        public void SetDefaultColors()
        {
            //COLOR ACCENT = 2d77d2  (213°, 65%, 50%)
            var colorAccent = Color.FromHex("#4079bf");

            this.Normal.TopStatusBar = Color.FromHex("#79aad2"); //a9c1d6
            //this.Normal.TopStatusBar = Color.FromHsla(colorAccent.Hue, 0.35, 0.50); //cfd8e2
            this.Normal.TabsBar = colorAccent;  //4079bf
            this.Normal.TabsBarShade = Color.FromHsla(colorAccent.Hue, colorAccent.Saturation, colorAccent.Luminosity);
            this.Normal.Background = Color.FromHsla(colorAccent.Hue - (5 / 360), 0.5, 0.85); //afbecf
            this.Normal.NameBG = Color.FromHsla(colorAccent.Hue, colorAccent.Saturation, 0.90); //d5e4f6
            this.Normal.NameDropDownBG = Color.FromHsla(colorAccent.Hue, 0.65, 0.70); //81aee4
            this.Normal.GroupBG = Color.FromHsla(colorAccent.Hue, 0.5, 0.5); //4079bf
            this.Normal.GroupInactiveBG = Color.FromHsla(colorAccent.Hue, 0, 0.95); //f2f2f2
            this.Normal.StarBG = Color.FromHex("#d2a02d");
            this.Normal.StarInactiveBG = Color.FromHsla(colorAccent.Hue, 0, 0.70); // #cccccc
            this.Normal.DeleteBG = Color.FromHex("#a65959");
            this.Normal.SeparatorLine = Color.FromHsla(colorAccent.Hue, 0, 0.85); // #d9d9d9
            this.Normal.Text = Color.FromHsla(colorAccent.Hue, 0, 0.14); //242424
            this.Normal.TextInactive = Color.FromHsla(colorAccent.Hue, 0, 0.65); // #a6a6a6
            this.Normal.NotesBG = Color.FromHsla(colorAccent.Hue, 0.3, 0.97);
            this.Normal.UndoSaveBorder = Color.FromHsla(colorAccent.Hue, 0, 0.85); //d9d9d9
            this.Normal.UndoSaveInactiveBG = Color.FromHsla(colorAccent.Hue, 0, 0.95); //f2f2f2
            this.Normal.UndoSaveInactiveText = Color.FromHsla(colorAccent.Hue, 0, 0.80); //cccccc
            this.Normal.SaveText = colorAccent;
            this.Normal.UndoText = colorAccent;
            this.Normal.UndoSaveBG = Color.FromHsla(0, 0, 0); //fff;
            this.Normal.NotesVerticalSeparatorLine = Color.FromHsla(colorAccent.Hue, colorAccent.Saturation, 0.90); //d5e4f6
            this.Normal.DetailsBG = Color.FromHsla(colorAccent.Hue, colorAccent.Saturation, 0.98); //f7fafd
            this.Normal.OutlineButtonBorder = Color.FromHsla(colorAccent.Hue, 0.5, 0.75); //9fbbdf
            this.Normal.AddContactButtonBG = Color.FromHsla(colorAccent.Hue, 0.65, 0.3); //1b487e
            this.Normal.DetailsToggleOn = Color.FromHsla(colorAccent.Hue, 0.95, 0.75); //83b9fc
        }

        public void AutoSetColors(Color colorAccent)
        {
            //COLOR ACCENT = 2d77d2  (213°, 65%, 50%)
            this.Normal.TopStatusBar = colorAccent; //Color.FromHsla(colorAccent.Hue, 0.5, 0.5); //4079bf
            this.Normal.TabsBar = colorAccent; // Color.FromHsla(colorAccent.Hue, 0.5, 0.5); //4079bf
            this.Normal.TabsBarShade = Color.FromHsla(colorAccent.Hue, colorAccent.Saturation, colorAccent.Luminosity);
            this.Normal.Background = Color.FromHsla(colorAccent.Hue, 0.25, 0.75); //afbecf
            this.Normal.NameBG = Color.FromHsla(colorAccent.Hue, colorAccent.Saturation, 0.90); //d5e4f6
            this.Normal.GroupBG = Color.FromHsla(colorAccent.Hue, 0.5, 0.5); //4079bf
            this.Normal.GroupInactiveBG = Color.FromHsla(colorAccent.Hue, 0, 0.95); //f2f2f2
            this.Normal.StarBG = colorAccent;
            this.Normal.StarInactiveBG = Color.FromHsla(colorAccent.Hue, 0, 0.80); // #cccccc
            this.Normal.DeleteBG = colorAccent;
            this.Normal.SeparatorLine = Color.FromHsla(colorAccent.Hue, 0, 0.85); // #d9d9d9
            this.Normal.Text = Color.FromHsla(colorAccent.Hue, 0, 0.14); //242424
            this.Normal.TextInactive = Color.FromHsla(colorAccent.Hue, 0, 0.65); // #a6a6a6
            this.Normal.NotesBG = Color.FromHsla(colorAccent.Hue, 0.3, 0.97);
            this.Normal.UndoSaveBorder = Color.FromHsla(colorAccent.Hue, 0, 0.85); //d9d9d9
            this.Normal.UndoSaveInactiveBG = Color.FromHsla(colorAccent.Hue, 0, 0.95); //f2f2f2
            this.Normal.UndoSaveInactiveText = Color.FromHsla(colorAccent.Hue, 0, 0.80); //cccccc
            this.Normal.UndoText = colorAccent;
            this.Normal.SaveText = colorAccent;
            this.Normal.UndoSaveBG = Color.FromHsla(0, 0, 0); //fff;
            this.Normal.NotesVerticalSeparatorLine = Color.FromHsla(colorAccent.Hue, colorAccent.Saturation, 0.90); //d5e4f6
            this.Normal.DetailsBG = Color.FromHsla(colorAccent.Hue, colorAccent.Saturation, 0.98); //f7fafd
            this.Normal.OutlineButtonBorder = Color.FromHsla(colorAccent.Hue, 0.5, 0.75); //9fbbdf
        }

        [ProtoMember(1)]
        public ObservableRangeCollection<string> ToggleGroups { get; set; } = new ObservableRangeCollection<string>();

        [ProtoMember(2)]
        public HighlightOnTap NameHighlightOnTap { get => nameHighlightOnTap; set => SetProperty(ref nameHighlightOnTap, value); }
        private HighlightOnTap nameHighlightOnTap = HighlightOnTap.Word;

        [ProtoMember(3)]
        public string AccentHex { get; set; } = Color.Accent.ToHex();

        [ProtoMember(4)]
        public ColorsViewModel Normal { get => normal; set => SetProperty(ref normal, value); }
        private ColorsViewModel normal = new ColorsViewModel();

        [ProtoMember(5)]
        public ColorsViewModel Dark { get => dark; set => SetProperty(ref dark, value); }
        private ColorsViewModel dark = new ColorsViewModel();

        [ProtoMember(6)]
        public string DateFormat { get => dateFormat; set => SetProperty(ref dateFormat, value); }
        private string dateFormat = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern;

        [ProtoMember(7)]
        public string DateTimeFormat { get => dateTimeFormat; set => SetProperty(ref dateTimeFormat, value); }
        private string dateTimeFormat = CultureInfo.CurrentCulture.DateTimeFormat.FullDateTimePattern;

        [ProtoMember(8)]
        public string DefaultGoogleAccount { get => defaultGoogleAccount; set => SetProperty(ref defaultGoogleAccount, value); } //This is the default account where the groups are obtained and saved to. Contacts are obtained and saved here too. If contacts come from multiple groups the contact will be saved in that Account. 
        private string defaultGoogleAccount = null;
        protected bool SetColorProperty(ref Color backingStore, Color value, string HexPropertyName, [CallerMemberName] string propertyName = "")
        {
            if (EqualityComparer<Color>.Default.Equals(backingStore, value)) return false;
            backingStore = value;
            OnPropertyChanged(HexPropertyName);
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}