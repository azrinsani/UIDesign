using System.Threading.Tasks;
using Xamarin.Forms;
namespace ContactManager.Views
{
    public class FadeInAnimation : TriggerAction<VisualElement>
    {
        public bool Action { get; set; } = false;
        public uint Duration { get; set; } = 150;
        protected override async void Invoke(VisualElement visualElement)
        {
            if (visualElement == null) return;
            if (this.Action) await PerformAnimation(visualElement); else ViewExtensions.CancelAnimations(visualElement);
        }
        private async Task PerformAnimation(VisualElement visualElement) 
        {
            visualElement.IsVisible = true;
            await visualElement.FadeTo(1, this.Duration); 
        }
    }
    public class FadeOutAnimation : TriggerAction<VisualElement>
    {
        public bool Action { get; set; } = false;
        public uint Duration { get; set; } = 150;
        protected override async void Invoke(VisualElement visualElement)
        {
            if (visualElement == null) return;
            if (this.Action) await PerformAnimation(visualElement); else ViewExtensions.CancelAnimations(visualElement);            
        }
        private async Task PerformAnimation(VisualElement visualElement) 
        { 
            await visualElement.FadeTo(0, this.Duration);
            visualElement.IsVisible = false;
        }
    }

    //public class BlinkingAnimation : TriggerAction<VisualElement>
    //{
    //    public bool Action { get; set; } = false;
    //    protected override void Invoke(VisualElement visualElement)
    //    {
    //        if (visualElement == null) return;
    //        if (this.Action) 
    //        {
    //            var parentAnimation = new Animation();
    //            parentAnimation.Add(0, 0.5, new Animation(d => visualElement.Opacity = d, 1, 0, Easing.Linear)); //fadeOutAnimation
    //            parentAnimation.Add(0.5, 1, new Animation(d => visualElement.Opacity = d, 0, 1, Easing.Linear)); //fadeInAnimation
    //            parentAnimation.Commit(visualElement, "BlinkingVisualElement", 16, 480, repeat: () => true);
    //        }
    //        else ViewExtensions.CancelAnimations(visualElement);
    //    }
    //}
}
