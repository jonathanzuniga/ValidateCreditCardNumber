using System;
using Xamarin.Forms;
using System.Linq;
using System.Collections.Generic;

namespace ValidateCreditCardNumber
{
	public class ValidateNumberPage : ContentPage
	{
		public static LabelCustomFont cursorLabel;
		public static EntryCustomFont entryNumber;
		public static StackLayout stackLayout;

		public ValidateNumberPage ()
		{
			cursorLabel = new LabelCustomFont {
				Text = ">"
			};
			entryNumber = new EntryCustomFont {
				HorizontalOptions = LayoutOptions.FillAndExpand,
				Keyboard = Keyboard.Numeric
			};
			entryNumber.Completed += OnNumberEntryCompleted;

			stackLayout = new StackLayout {
				Padding = new Thickness(16),
				Children = {
					new LabelCustomFont {
						Text = "ENTER A CREDIT CARD NUMBER:"
					},
					new StackLayout {
						Children = {
							cursorLabel, entryNumber
						},
						Orientation = StackOrientation.Horizontal,
						Spacing = 0
					}
				}
			};

			Content = new ScrollView() {
				Content = stackLayout,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				Orientation = ScrollOrientation.Vertical
			}; 
		}

		void OnNumberEntryCompleted(object sender, EventArgs args)
		{
			var entry = (EntryCustomFont)sender;
			var resultText = "";
			var resultLabel = new LabelCustomFont {
				Text = resultText
			};

			if (Mod10Check (entry.Text)) {
				resultLabel.TextColor = Color.FromHex("#fff");
				resultText = ">__VALID NUMBER";
			} else
				resultText = ">INVALID NUMBER";

			entry.IsEnabled = false;
			resultLabel.Text = resultText;

			stackLayout.Children.Add (resultLabel);

			entryNumber = new EntryCustomFont {
				HorizontalOptions = LayoutOptions.FillAndExpand,
				Keyboard = Keyboard.Numeric
			};
			entryNumber.Focus ();
			entryNumber.Completed += OnNumberEntryCompleted;

			stackLayout.Children.Add (
				new StackLayout {
					Children = {
						cursorLabel, entryNumber
					},
					Orientation = StackOrientation.Horizontal,
					Spacing = 0
				}
			);
		}

		public static bool Mod10Check(string creditCardNumber)
		{
			// Check whether input string is null or empty.
			if (string.IsNullOrEmpty(creditCardNumber)) {
				return false;
			}

			char[] charArray = creditCardNumber.ToCharArray();

			// 1. Starting with the check digit double the value of every other digit 
			// 2. If doubling of a number results in a two digits number, add up.
			//    the digits to get a single digit number. This will results in eight single digit numbers.
			// 3. Get the sum of the digits.
			int sumOfDigits = charArray.Where((e) => e >= '0' && e <= '9')
				.Reverse()
				.Select((e, i) => ((int)e - 48) * (i % 2 == 0 ? 1 : 2))
				.Sum((e) => e / 10 + e % 10);


			// If the final sum is divisible by 10, then the credit card number.
			// is valid. If it is not divisible by 10, the number is invalid.            
			return sumOfDigits % 10 == 0;
		}
	}
}


