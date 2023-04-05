using System;

namespace _6466;

public partial class MainPage : ContentPage
{
	int count = 0;
	bool _animatingIn = false;

	public MainPage()
	{
		InitializeComponent();

		var trackRect = AbsoluteLayout.GetLayoutBounds(Track);
		var limit = (int)(trackRect.X + trackRect.Width - 30);
		var random = new Random();

		Thumb.Dispatcher.StartTimer(TimeSpan.FromMilliseconds(1000), () => {

			var rect = AbsoluteLayout.GetLayoutBounds(Thumb);

			rect.X = random.Next(30, limit);
			AbsoluteLayout.SetLayoutBounds(Thumb, rect);

			return true;
		});

		Dismiss.Clicked += Dismiss_Clicked;
	}

	private void Dismiss_Clicked(object sender, EventArgs e)
	{
		if (_animatingIn) 
		{
			return;
		}

		Sign.Dispatcher.StartTimer(TimeSpan.FromMilliseconds(10), () => {

			if (_animatingIn) 
			{
				return false;
			}

			var rect = AbsoluteLayout.GetLayoutBounds(Sign);
			rect.X -= 10;
			AbsoluteLayout.SetLayoutBounds(Sign, rect);

			return rect.X > -300;
		});
	}

	private void OnCounterClicked(object sender, EventArgs e)
	{
		count++;

		if (count == 1)
			CounterBtn.Text = $"Clicked {count} time";
		else
			CounterBtn.Text = $"Clicked {count} times";

		Sign.Dispatcher.StartTimer(TimeSpan.FromMilliseconds(10), () => {
			
			_animatingIn = true;
			var rect = AbsoluteLayout.GetLayoutBounds(Sign);
			rect.X += 10;
			AbsoluteLayout.SetLayoutBounds(Sign, rect);

			if (rect.X < 100)
			{
				return true;
			}
			else 
			{
				_animatingIn = false;
				return false;
			}
		});
	}
}



