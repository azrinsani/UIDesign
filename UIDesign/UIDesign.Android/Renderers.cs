using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Graphics.Drawables;
using UIDesign;
using UIDesign.Droid;
using Android.Content;
using System.ComponentModel;
using Android.Views;
using Android.Views.InputMethods;
using Xamarin.Forms.Platform.Android.AppCompat;
using Google.Android.Material.Tabs;
using System.Collections.Generic;
using System;
using System.Linq;

[assembly: ExportRenderer(typeof(CustomAutoWrapEditor), typeof(CustomAutoWrapEditorRenderer))]
[assembly: ExportRenderer(typeof(CustomEditor), typeof(CustomEditorRenderer))]
[assembly: ExportRenderer(typeof(CustomTabbedPage), typeof(CustomTabbedRenderer))]
namespace UIDesign.Droid
{
   
    class CustomAutoWrapEditorRenderer : EditorRenderer
    {
        public CustomAutoWrapEditorRenderer(Context context) : base(context) { }
        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                Control.SetPadding(6, 6, 6, 6);
                Control.SetSelectAllOnFocus(true);
                Control.SetBackground(null);
                Control.ImeOptions = ImeAction.Next;

                if (e.NewElement != null && e.NewElement is CustomAutoWrapEditor customAutoWrapEditor)
                {
                    switch (customAutoWrapEditor.CapsMode)
                    {
                        case CapsMode.CapWords:
                            Control.InputType = Android.Text.InputTypes.TextFlagMultiLine | Android.Text.InputTypes.TextFlagCapWords;
                            break;
                        case CapsMode.CapAll:
                            Control.InputType = Android.Text.InputTypes.TextFlagMultiLine | Android.Text.InputTypes.TextFlagCapCharacters;
                            break;
                        case CapsMode.CapSentence:
                            Control.InputType = Android.Text.InputTypes.TextFlagMultiLine | Android.Text.InputTypes.TextFlagCapSentences;
                            break;
                    }
                }
                //Control.InputType = Android.Text.InputTypes.TextFlagMultiLine;
                Control.SetHorizontallyScrolling(false); //This must be set last, if not the text editor won't wrap properly
                Control.SetMaxLines(int.MaxValue); //This must be set last, if not the text editor won't wrap properly
            }

            e.NewElement.Unfocused += (sender, evt) =>
            {
                if (Control != null)
                {
                    Control.SetPadding(6, 6, 6, 6);
                    Control.SetBackground(null);
                }
            };
            e.NewElement.Focused += (sender, evt) =>
            {
                if (Control != null)
                {
                    var gd = new GradientDrawable();
                    gd.SetStroke(5, Color.Accent.ToAndroid());
                    Control.SetPadding(6, 6, 6, 6);
                    //Control.SetBackgroundColor(global::Android.Graphics.Color.ParseColor("#F5F5F5"));
                    Control.SetBackground(gd);
                    //base.Selected = true;
                }
            };
        }
    }
    class CustomEditorRenderer : EditorRenderer
    {
        public CustomEditorRenderer(Context context) : base(context) {}
        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                if (e.NewElement != null && e.NewElement is CustomEditor customEditor)
                {
                    Control.SetPadding(6, 6, 6, 6);
                    Control.SetBackground(null);
                    Control.SetSelectAllOnFocus(customEditor.SelectAllOnFocus);
                    switch (customEditor.ReturnType)
                    {
                        case ReturnType.Default: break;
                        case ReturnType.Done:
                            Control.ImeOptions = ImeAction.Done;
                            break;
                        case ReturnType.Go:
                            Control.ImeOptions = ImeAction.Go;
                            break;
                        case ReturnType.Next:
                            Control.ImeOptions = ImeAction.Next;
                            break;
                        case ReturnType.Search:
                            Control.ImeOptions = ImeAction.Search;
                            break;
                        case ReturnType.Send:
                            Control.ImeOptions = ImeAction.Send;
                            break;
                        default:
                            break;
                    }
                    //Android.Text.InputTypes.ClassText | Android.Text.InputTypes.TextFlagMultiLine | Android.Text.InputTypes.TextFlagNoSuggestions
                    
                    Control.InputType = ((Android.Text.InputTypes)(int)customEditor.AndroidInputType); //. Android.Text.InputTypes.ClassText;                    
                    if (!customEditor.IsTextPredictionEnabled || !customEditor.IsSpellCheckEnabled) Control.InputType = Control.InputType | Android.Text.InputTypes.TextFlagNoSuggestions;
                    if (customEditor.Multiline) Control.InputType = Control.InputType | (Android.Text.InputTypes.TextFlagMultiLine);

                }
            }

            e.NewElement.Unfocused += (sender, evt) =>
            {
                if (Control != null)
                {
                    Control.SetPadding(6, 6, 6, 6);
                    Control.SetBackground(null);
                }
            };
            e.NewElement.Focused += (sender, evt) =>
            {
                if (Control != null)
                {
                    var gd = new GradientDrawable();
                    gd.SetStroke(5, Color.Accent.ToAndroid());
                    Control.SetPadding(6, 6, 6, 6);
                    //Control.SetBackgroundColor(global::Android.Graphics.Color.ParseColor("#F5F5F5"));
                    Control.SetBackground(gd);
                    //base.Selected = true;
                }
            };
        }
    }
    class CustomTabbedRenderer : TabbedPageRenderer
    {
        public CustomTabbedRenderer(Context context) : base(context) { }
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            TabLayout TabsLayout = null;
            for (int i = 0; i < this.ChildCount; ++i)
            {
                Android.Views.View view = (Android.Views.View)GetChildAt(i);
                if (view is TabLayout layout)
                {
                    TabsLayout = layout;
                    break;
                }
            }
            if ((Element as CustomTabbedPage).IsHidden)
            {
                TabsLayout.Visibility = ViewStates.Gone;
            }
            else
            {
                TabsLayout.Visibility = ViewStates.Visible;
            }
            TabsLayout.SetMinimumHeight(0);
            TabsLayout.SetPadding(0,0,0,0);
        

            
        }



    }
}
