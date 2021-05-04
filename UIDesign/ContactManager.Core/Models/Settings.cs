using ContactManager.Core;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ContactManager.ViewModels
{

    public enum HighlightOnTap { None=0, Word=1, Full=2 }
    public enum DisplayType { Hide=0, Show=1, ShowIfExist=2 }
    public enum DateFormat { DMY=0, MDY=1, YMD=2 }

    [ProtoContract]
    public class Settings
    {
        public Settings() {}
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
            this.Normal.Background = Color.FromHsla(colorAccent.Hue, 0.25,0.75); //afbecf
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
        public List<string> ToggleGroups { get; set; } = new List<string>();
        [ProtoMember(2)]
        public string AccentHex { get; set; } = Color.Accent.ToHex();
        [ProtoMember(3)]
        public Colors Normal { get; set; } = new Colors();
        [ProtoMember(4)]
        public Colors Dark { get; set; } = new Colors();
        [ProtoMember(5)]
        public DateFormat DateFormat { get; set; } = DateFormat.DMY;
        [ProtoMember(6)]
        public bool Use24hTime { get; set; } = true;

        public void Save()
        {
            this.SerializeToProtoBufFile(AppCore.SettingsFile);
        }
    }


    [ProtoContract]
    public class Colors: BaseViewModel, INotifyPropertyChanged
    {


        [ProtoMember(1)]
        public string TopStatusBarHex { get; set; }
        public Color TopStatusBar { get => topStatusBar; set => SetColorProperty(ref topStatusBar, value, TopStatusBarHex); }
        private Color topStatusBar;

        [ProtoMember(2)]
        public string TabsBarHex { get; set; }
        public Color TabsBar { get => tabsBar; set => SetColorProperty(ref tabsBar, value, TabsBarHex); }
        private Color tabsBar;

        [ProtoMember(3)]
        public string TabsBarShadeHex { get; set; }
        public Color TabsBarShade { get => tabsBarShade; set => SetColorProperty(ref tabsBarShade, value, TabsBarShadeHex); }
        private Color tabsBarShade;

        [ProtoMember(4)]
        public string BackgroundHex { get; set; }
        public Color Background { get => background; set => SetColorProperty(ref background, value, BackgroundHex); }
        private Color background;

        [ProtoMember(5)]
        public string SeparatorLineHex { get; set; }
        public Color SeparatorLine { get => separatorLine; set => SetColorProperty(ref separatorLine, value, SeparatorLineHex); }
        private Color separatorLine;

        [ProtoMember(6)]
        public string UndoSaveBorderHex { get; set; }
        public Color UndoSaveBorder { get => notesBG; set => SetColorProperty(ref undoSaveBorder, value, UndoSaveBorderHex); }
        private Color undoSaveBorder;


        [ProtoMember(7)]
        public string UndoTextHex { get; set; }
        public Color UndoText { get => undoText; set => SetColorProperty(ref undoText, value, UndoTextHex); }
        private Color undoText;

        [ProtoMember(8)]
        public string SaveTextHex { get; set; }
        public Color SaveText { get => saveText; set => SetColorProperty(ref saveText, value, SaveTextHex); }
        private Color saveText;

        [ProtoMember(9)]
        public string UndoSaveBGHex { get; set; }
        public Color UndoSaveBG { get => undoSaveBG; set => SetColorProperty(ref undoSaveBG, value, UndoSaveBGHex); }
        private Color undoSaveBG;


        [ProtoMember(10)]
        public string UndoSaveInactiveTextHex { get; set; }
        public Color UndoSaveInactiveText { get => undoSaveInactiveText; set => SetColorProperty(ref undoSaveInactiveText, value, UndoSaveInactiveTextHex); }
        private Color undoSaveInactiveText;

        [ProtoMember(11)]
        public string UndoSaveInactiveBGHex { get; set; }
        public Color UndoSaveInactiveBG { get => undoSaveInactiveBG; set => SetColorProperty(ref undoSaveInactiveBG, value, UndoSaveInactiveBGHex); }
        private Color undoSaveInactiveBG;


        [ProtoMember(12)]
        public string TextHex { get; set; }
        public Color Text { get => text; set => SetColorProperty(ref text, value, TextHex); }
        private Color text;

        [ProtoMember(13)]
        public string TextInactiveHex { get; set; }
        public Color TextInactive { get => textInactive; set => SetColorProperty(ref textInactive, value, TextInactiveHex); }
        private Color textInactive;

        [ProtoMember(14)]
        public string NameBGHex { get; set; }
        public Color NameBG { get => nameBG; set => SetColorProperty(ref nameBG, value, NameBGHex); }
        private Color nameBG;


        [ProtoMember(15)]
        public string NameDropDownBGHex { get; set; }
        public Color NameDropDownBG { get => nameDropDownBG; set => SetColorProperty(ref nameDropDownBG, value, NameDropDownBGHex); }
        private Color nameDropDownBG;


        [ProtoMember(16)]
        public string GroupBGHex { get; set; }
        public Color GroupBG { get => groupBG; set => SetColorProperty(ref groupBG, value, GroupBGHex); }
        private Color groupBG;

        [ProtoMember(17)]
        public string GroupInactiveBGHex { get; set; }
        public Color GroupInactiveBG { get => groupInactiveBG; set => SetColorProperty(ref groupInactiveBG, value, GroupInactiveBGHex); }
        private Color groupInactiveBG;


        [ProtoMember(18)]
        public string StarBGHex { get; set; }
        public Color StarBG { get => starBG; set => SetColorProperty(ref starBG, value, StarBGHex); }
        private Color starBG;

        [ProtoMember(19)]
        public string StarInactiveBGHex { get; set; }
        public Color StarInactiveBG { get => starInactiveBG; set => SetColorProperty(ref starInactiveBG, value, StarInactiveBGHex); }
        private Color starInactiveBG;


        [ProtoMember(20)]
        public string DeleteBGHex { get; set; }
        public Color DeleteBG { get => deleteBG; set => SetColorProperty(ref deleteBG, value, DeleteBGHex); }
        private Color deleteBG;

        [ProtoMember(21)]
        public string NotesBGHex { get; set; }
        public Color NotesBG { get => notesBG; set => SetColorProperty(ref notesBG, value, NotesBGHex); }
        private Color notesBG;

        [ProtoMember(22)]
        public string NotesVerticalSeparatorLineHex { get; set; }
        public Color NotesVerticalSeparatorLine { get => notesVerticalSeparatorLine; set => SetColorProperty(ref notesVerticalSeparatorLine, value, NotesVerticalSeparatorLineHex); }
        private Color notesVerticalSeparatorLine;

        [ProtoMember(23)]
        public string DetailsBGHex { get; set; }
        public Color DetailsBG { get => detailsBG; set => SetColorProperty(ref detailsBG, value, DetailsBGHex); }
        private Color detailsBG;


        [ProtoMember(24)]
        public string OutlineButtonBorderHex { get; set; }
        public Color OutlineButtonBorder { get => outlineButtonBorder; set => SetColorProperty(ref outlineButtonBorder, value, OutlineButtonBorderHex); }
        private Color outlineButtonBorder;


        [ProtoMember(25)]
        public string AddContactButtonBGHex { get; set; }
        public Color AddContactButtonBG { get => addContactButtonBG; set => SetColorProperty(ref addContactButtonBG, value, AddContactButtonBGHex); }
        private Color addContactButtonBG;


        [ProtoMember(26)]
        public string DetailsToggleOnHex { get; set; }
        public Color DetailsToggleOn { get => detailsToggleOn; set => SetColorProperty(ref detailsToggleOn, value, DetailsToggleOnHex); }
        private Color detailsToggleOn;
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