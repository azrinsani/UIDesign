using ProtoBuf;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace ContactManager.ViewModels
{
    [ProtoContract]
    public class ColorsViewModel:BaseViewModel, INotifyPropertyChanged
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