using ProtoBuf;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace ContactManager.ViewModels
{
    [ProtoContract]
    public class ColorsViewModel:BaseViewModel, INotifyPropertyChanged
    {
        [ProtoMember(1)]
        public Color TopStatusBar { get => topStatusBar; set => SetProperty(ref topStatusBar, value); }
        private Color topStatusBar;

        [ProtoMember(2)]
        public Color TabsBar { get => tabsBar; set => SetProperty(ref tabsBar, value); }
        private Color tabsBar;

        [ProtoMember(3)]
        public Color TabsBarShade { get => tabsBarShade; set => SetProperty(ref tabsBarShade, value); }
        private Color tabsBarShade;

        [ProtoMember(4)]
        public Color Background { get => background; set => SetProperty(ref background, value); }
        private Color background;

        [ProtoMember(5)]
        public Color SeparatorLine { get => separatorLine; set => SetProperty(ref separatorLine, value); }
        private Color separatorLine;

        [ProtoMember(6)]
        public Color UndoSaveBorder { get => notesBG; set => SetProperty(ref undoSaveBorder, value); }
        private Color undoSaveBorder;


        [ProtoMember(7)]
        public Color UndoText { get => undoText; set => SetProperty(ref undoText, value); }
        private Color undoText;

        [ProtoMember(8)]
        public Color SaveText { get => saveText; set => SetProperty(ref saveText, value); }
        private Color saveText;

        [ProtoMember(9)]
        public Color UndoSaveBG { get => undoSaveBG; set => SetProperty(ref undoSaveBG, value); }
        private Color undoSaveBG;


        [ProtoMember(10)]
        public Color UndoSaveInactiveText { get => undoSaveInactiveText; set => SetProperty(ref undoSaveInactiveText, value); }
        private Color undoSaveInactiveText;

        [ProtoMember(11)]
        public Color UndoSaveInactiveBG { get => undoSaveInactiveBG; set => SetProperty(ref undoSaveInactiveBG, value); }
        private Color undoSaveInactiveBG;


        [ProtoMember(12)]
        public Color Text { get => text; set => SetProperty(ref text, value); }
        private Color text;

        [ProtoMember(13)]
        public Color TextInactive { get => textInactive; set => SetProperty(ref textInactive, value); }
        private Color textInactive;

        [ProtoMember(14)]
        public Color NameBG { get => nameBG; set => SetProperty(ref nameBG, value); }
        private Color nameBG;


        [ProtoMember(15)]
        public string NameDropDownBGHex { get; set; }
        public Color NameDropDownBG { get => nameDropDownBG; set => SetProperty(ref nameDropDownBG, value); }
        private Color nameDropDownBG;
        

        [ProtoMember(16)]
        public Color GroupBG { get => groupBG; set => SetProperty(ref groupBG, value); }
        private Color groupBG;

        [ProtoMember(17)]
        public Color GroupInactiveBG { get => groupInactiveBG; set => SetProperty(ref groupInactiveBG, value); }
        private Color groupInactiveBG;


        [ProtoMember(18)]
        public Color StarBG { get => starBG; set => SetProperty(ref starBG, value); }
        private Color starBG;

        [ProtoMember(19)]
        public Color StarInactiveBG { get => starInactiveBG; set => SetProperty(ref starInactiveBG, value); }
        private Color starInactiveBG;


        [ProtoMember(20)]
        public Color DeleteBG { get => deleteBG; set => SetProperty(ref deleteBG, value); }
        private Color deleteBG;

        [ProtoMember(21)]
        public Color NotesBG { get => notesBG; set => SetProperty(ref notesBG, value); }
        private Color notesBG;

        [ProtoMember(22)]
        public Color NotesVerticalSeparatorLine { get => notesVerticalSeparatorLine; set => SetProperty(ref notesVerticalSeparatorLine, value); }
        private Color notesVerticalSeparatorLine;

        [ProtoMember(23)]
        public Color DetailsBG { get => detailsBG; set => SetProperty(ref detailsBG, value); }
        private Color detailsBG;


        [ProtoMember(24)]
        public Color OutlineButtonBorder { get => outlineButtonBorder; set => SetProperty(ref outlineButtonBorder, value); }
        private Color outlineButtonBorder;


        [ProtoMember(25)]
        public Color AddContactButtonBG { get => addContactButtonBG; set => SetProperty(ref addContactButtonBG, value); }
        private Color addContactButtonBG;


        [ProtoMember(26)]
        public Color DetailsToggleOn { get => detailsToggleOn; set => SetProperty(ref detailsToggleOn, value); }
        private Color detailsToggleOn;
    }

}