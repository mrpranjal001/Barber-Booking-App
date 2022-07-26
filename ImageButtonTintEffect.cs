using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using static Barber.Controls.ImageButtonTintEffect;
using AWImageButton = Android.Widget.ImageButton;

[assembly: ResolutionGroupName("XfEffects")]
[assembly: ExportEffect(typeof(Barber.Droid.ImageButtonTintEffect), nameof(Barber.Controls.ImageButtonTintEffect))]
namespace Barber.Droid
{
    public class ImageButtonTintEffect : PlatformEffect
    {
        private static readonly int[][] _colorStates =
        {
            new[] { global::Android.Resource.Attribute.StateEnabled },
            new[] { -global::Android.Resource.Attribute.StateEnabled }, //disabled state
            new[] { global::Android.Resource.Attribute.StatePressed } //pressed state
        };
        protected override void OnAttached()
        {
            UpdateTintColor();
        }
        protected override void OnDetached()
        {
        }
        protected override void OnElementPropertyChanged(PropertyChangedEventArgs args)
        {
            if (args.PropertyName == ImageButtonTintEffectParameters.TintColorProperty.PropertyName)
                UpdateTintColor();

            if (args.PropertyName == Xamarin.Forms.ImageButton.SourceProperty.PropertyName)
                UpdateTintColor();
        }

        private void UpdateTintColor()
        {
            try
            {
                if (this.Control is AWImageButton imageButton)
                {
                    var androidColor = ImageButtonTintEffectParameters.GetTintColor(this.Element).ToAndroid();

                    var disabledColor = androidColor;
                    disabledColor.A = 0x1C; //140

                    var pressedColor = androidColor;
                    pressedColor.A = 0x1C; //140

                    imageButton.ImageTintList = new ColorStateList(_colorStates, new[] { pressedColor.ToArgb(), pressedColor.ToArgb(), pressedColor.ToArgb() });
                    imageButton.ImageTintMode = PorterDuff.Mode.SrcOver;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(
                    $"An error occurred when setting the {typeof(Barber.Controls.ImageButtonTintEffect)} effect: {ex.Message}\n{ex.StackTrace}");
            }
        }
    }
}