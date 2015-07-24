using Xamarin.Forms;
using Android.Graphics;
using Xamarin.Forms.Platform.Android;
using Android.Widget;
using ValidateCreditCardNumber;
using ValidateCreditCardNumber.Droid;

[assembly: ExportRenderer (typeof(LabelCustomFont), typeof(LabelRendererRegularFont))]

namespace ValidateCreditCardNumber.Droid
{
	public class LabelRendererRegularFont : LabelRenderer
	{
		private Typeface typeFaceRegular = Typeface.CreateFromAsset (Forms.Context.Assets, "fonts/OCRA.ttf");

		protected override void OnElementChanged (ElementChangedEventArgs<Label> e)
		{
			base.OnElementChanged (e);

			var labelToStyle = (TextView)Control;

			labelToStyle.SetTypeface (typeFaceRegular, TypefaceStyle.Normal);
		}
	}
}
