using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;

namespace UIDesign
{



    public enum CapsMode {  None, CapSentence, CapWords, CapAll }

    public class CustomCollectionView : CollectionView {}


    public class CustomFlexLayout : Xamarin.Forms.FlexLayout
    {
        public CustomFlexLayout()
        {
            Direction = Device.FlowDirection == FlowDirection.RightToLeft ? FlexDirection.RowReverse : FlexDirection.Row;
        }
    }

    public class CustomEditor : Editor 
    {
        public T GetValue<T>(BindableProperty property) { return (T)GetValue(property); }

        public static readonly BindableProperty ReturnTypeProperty = BindableProperty.Create(nameof(ReturnType), typeof(ReturnType), typeof(CustomEditor), ReturnType.Default);
        public ReturnType ReturnType { get => GetValue<ReturnType>(ReturnTypeProperty); set => SetValue(ReturnTypeProperty, value);}

        public static readonly BindableProperty AndroidInputTypeProperty = BindableProperty.Create(nameof(AndroidInputType), typeof(AndroidInputType), typeof(CustomEditor), AndroidInputType.ClassText);
        public AndroidInputType AndroidInputType { get => GetValue<AndroidInputType>(AndroidInputTypeProperty); set => SetValue(AndroidInputTypeProperty, value);}
        
        public static readonly BindableProperty SelectAllOnFocusProperty = BindableProperty.Create(nameof(SelectAllOnFocus), typeof(bool), typeof(CustomEditor), false);
        public bool SelectAllOnFocus { get => GetValue<bool>(SelectAllOnFocusProperty); set => SetValue(SelectAllOnFocusProperty, value);}
        
        public static readonly BindableProperty MultilineProperty = BindableProperty.Create(nameof(Multiline), typeof(bool), typeof(CustomEditor), true);
        public bool Multiline { get => GetValue<bool>(MultilineProperty); set => SetValue(MultilineProperty, value);}
    }
    
    public class CustomAutoWrapEditor : Editor 
    {
        public static readonly BindableProperty CapsModeProperty = BindableProperty.Create("CapsMode", typeof(CapsMode), typeof(CustomAutoWrapEditor), CapsMode.None);
        public CapsMode CapsMode
        {
            set { SetValue(CapsModeProperty, value); }
            get { return (CapsMode)GetValue(CapsModeProperty); }
        }
    }

    public class CustomTabbedPage : TabbedPage
    {
        public static readonly BindableProperty IsHiddenProperty = BindableProperty.Create("IsHidden", typeof(bool), typeof(CustomTabbedPage), false);
        public bool IsHidden
        {
            set { SetValue(IsHiddenProperty, value); }
            get { return (bool)GetValue(IsHiddenProperty); }
        }
    }

    public static class AttachedProperties
    {
        public static BindableProperty AnimatedProgressProperty =
           BindableProperty.CreateAttached("AnimatedProgress",
           typeof(double),
           typeof(ProgressBar),
           0.0d,
           BindingMode.OneWay,
           propertyChanged: (b, o, n) => ProgressBarProgressChanged((ProgressBar)b, (double)n));

        public static double GetAnimatedProgress(BindableObject target) => (double)target.GetValue(AnimatedProgressProperty);
        public static void SetAnimatedProgress(BindableObject target, double value) => target.SetValue(AnimatedProgressProperty, value);

        private static void ProgressBarProgressChanged(ProgressBar progressBar, double progress)
        {
            ViewExtensions.CancelAnimations(progressBar);
            progressBar.ProgressTo(progress, 250, Easing.Linear);
        }
    }
}
