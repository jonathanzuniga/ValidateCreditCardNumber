using System;
using System.Text;
using Xamarin.Forms;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;
using AdvancedTimer.Forms.Plugin.Abstractions;

namespace ValidateCreditCardNumber
{
	public class DecodeLabel : LabelCustomFont
	{
//		private readonly Timer _timerAnimate = new Timer();
		static IAdvancedTimer _timerAnimate = DependencyService.Get<IAdvancedTimer>();
		private TextDecodeEffect _decodeEffect;
		private bool _showing;
		private int _initGenCount;

		public int Interval
		{
//			get { return _timerAnimate.Interval; }
			get { return _timerAnimate.getInterval(); }
//			set { _timerAnimate.Interval = value; }
			set { _timerAnimate.setInterval(value); }
		}

		public DecodeLabel ()
		{
//			_timerAnimate.Interval = 100;
//			_timerAnimate.setInterval(100);
//			_timerAnimate.Tick += _timerAnimate_Tick;
//			_timerAnimate.initTimer(100, timerElapsed, true);
//			_timerAnimate.startTimer();
//			_timerAnimate.setInterval(100);
//			_timerAnimate.Tick += _timerAnimate_Tick; // How?
		}

		public void Animate(bool show, string text, int initGenCount)
		{
			_initGenCount = initGenCount;
			_decodeEffect = new TextDecodeEffect(text) { TextVisible = !show };
			Text = _decodeEffect.Peek (DecodeMode.None);
			_showing = show;
//			_timerAnimate.Start ();
			_timerAnimate.initTimer(100, timerElapsed, true);
			_timerAnimate.startTimer();
			_timerAnimate.setInterval(100);
		}

		public static void timerElapsed(object o, EventArgs e)
		{
			Xamarin.Forms.Device.BeginInvokeOnMainThread(() => {
//				_timerAnimate.setInterval(_timerAnimate.getInterval() + 1000);
				_timerAnimate.setInterval(100);
			});
		}

		private void _timerAnimate_Tick(object sender, EventArgs e)
		{
			if (_initGenCount != 0) {
				Text = _decodeEffect.GenerateNumberRange (Text.Length);
				_initGenCount--;
				return;
			}

			var decodeMode = _showing ? DecodeMode.Show : DecodeMode.Hide;
			var text = _decodeEffect.Peek (decodeMode);

			if (text == null) {
//				_timerAnimate.Stop ();
				_timerAnimate.stopTimer ();
			} else {
				Text = text;
			}
		}
	}

	public enum DecodeMode
	{
		None,
		Show,
		Numbers,
		Hide
	}

	class TextDecodeEffect
	{
		private int _visibleCount;
		private readonly Random _random = new Random ();

		public bool TextVisible
		{
			get { return _visibleCount == OriginalText.Length; }
			set { _visibleCount = value ? OriginalText.Length : 0; }
		}

		public string OriginalText { get; private set; }

		public TextDecodeEffect(string text)
		{
			OriginalText = text;
		}

		public string Peek(DecodeMode mode)
		{
			switch (mode) {
				case DecodeMode.Numbers:
					return GenerateNumberRange (OriginalText.Length);
				case DecodeMode.Hide:
					if (_visibleCount == 0)
						return null;

					_visibleCount--;
					break;
				case DecodeMode.Show:
					if (_visibleCount == OriginalText.Length)
						return null;

					_visibleCount++;
					break;
			}

			var text = GenerateNumberRange (OriginalText.Length - _visibleCount);

			text += OriginalText.Substring (OriginalText.Length - _visibleCount, _visibleCount);

			return text;
		}

		public string GenerateNumberRange(int count)
		{
			var SB = new StringBuilder ();

			for (int i = 0; i < count; i++)
				SB.Append(_random.Next(0, 10));

			return SB.ToString();
		}
	}
}

//internal delegate void TimerCallback(object state);
//
//internal sealed class Timer : CancellationTokenSource, IDisposable
//{
//	internal Timer(TimerCallback callback, object state, int dueTime, int period)
//	{
//		Contract.Assert(period == -1, "This stub implementation only supports dueTime.");
//		Task.Delay(dueTime, Token).ContinueWith((t, s) =>
//			{
//				var tuple = (Tuple<TimerCallback, object>)s;
//				tuple.Item1(tuple.Item2);
//			}, Tuple.Create(callback, state), CancellationToken.None,
//			TaskContinuationOptions.ExecuteSynchronously | TaskContinuationOptions.OnlyOnRanToCompletion,
//			TaskScheduler.Default);
//	}
//
//	public new void Dispose() { Cancel(); }
//}
