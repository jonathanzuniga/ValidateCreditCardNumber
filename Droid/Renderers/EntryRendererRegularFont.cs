using Xamarin.Forms;
using Android.Graphics;
using Xamarin.Forms.Platform.Android;
using Android.Widget;
using ValidateCreditCardNumber;
using ValidateCreditCardNumber.Droid;

[assembly: ExportRenderer (typeof(EntryCustomFont), typeof(EntryRendererRegularFont))]

namespace ValidateCreditCardNumber.Droid
{
	public class EntryRendererRegularFont : EntryRenderer
	{
		private Typeface typeFaceRegular = Typeface.CreateFromAsset (Forms.Context.Assets, "fonts/OCRA.ttf");

		protected override void OnElementChanged (ElementChangedEventArgs<Entry> e)
		{
			base.OnElementChanged (e);

			var entryToStyle = (EditText)Control;

			entryToStyle.SetTypeface (typeFaceRegular, TypefaceStyle.Normal);
		}
	}
}
