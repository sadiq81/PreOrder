using System;
using Xamarin.Forms;
using System.Threading.Tasks;
using Xamarin.Geolocation;

namespace PreOrder
{
	public class ShopViewCell : ViewCell
	{
		Label Distance;

		protected override void OnAppearing ()
		{
			base.OnAppearing ();
			CalculateDistance (Distance);
		}

		public ShopViewCell ()
		{
			Image favourite = new Image ();
			favourite.SetBinding (Image.SourceProperty, "Favourite");
			favourite.GestureRecognizers.Add (new TapGestureRecognizer () {
				Command = new Command (() => {
					MakeFavourite (favourite);
				})
			});

			//---------------------------------------------------------------

			Label name = new Label ();
			name.SetBinding (Label.TextProperty, "Name");
			name.Font = Font.SystemFontOfSize (NamedSize.Medium);
			name.LineBreakMode = LineBreakMode.TailTruncation;

			Label address = new Label ();
			address.SetBinding (Label.TextProperty, "CombinedAddress");
			address.LineBreakMode = LineBreakMode.TailTruncation;
			address.Font = Font.SystemFontOfSize (NamedSize.Micro);

			//---------------------------------------------------------------

			Distance = new Label ();
			Distance.SetBinding<Store> (Label.TextProperty, m => m.GPSCoordinate);
			Distance.Font = Font.SystemFontOfSize (NamedSize.Small);
			Distance.IsVisible = false;

			Grid grid = new Grid () {
				VerticalOptions = LayoutOptions.FillAndExpand,
				RowSpacing = 0,
				ColumnSpacing = 0,
				ColumnDefinitions = {
					new ColumnDefinition { Width = new GridLength (2, GridUnitType.Star) },
					new ColumnDefinition { Width = new GridLength (10, GridUnitType.Star) },
					new ColumnDefinition { Width = new GridLength (1, GridUnitType.Star) },
					new ColumnDefinition { Width = new GridLength (3, GridUnitType.Star) },
					new ColumnDefinition { Width = new GridLength (1, GridUnitType.Star) }
				}
			};

			View = grid;

			grid.Children.Add (favourite, 0, 0);

			grid.Children.Add (new StackLayout {
				Orientation = StackOrientation.Vertical,
				VerticalOptions = LayoutOptions.Start,
				Spacing = 1,
				Children = {
					name,
					address
				}
			}, 1, 0);

			grid.Children.Add (new Image (){ Source = ImageSource.FromFile ("divider.png") }, 2, 0);
			grid.Children.Add (new StackLayout {
				Orientation = StackOrientation.Vertical,
				VerticalOptions = LayoutOptions.CenterAndExpand,
				HorizontalOptions = LayoutOptions.Center,
				Spacing = 1,
				Children = {
					Distance,
					new Label () {
						Text = "Km",
						Font = Font.SystemFontOfSize (NamedSize.Micro)
					}
				}
			}, 3, 0);

			grid.Children.Add (new Image (){ Source = ImageSource.FromFile ("more.png") }, 4, 0);


		}

		void MakeFavourite (Image favourite)
		{
			Store store = (Store)favourite.BindingContext;
			store.Favourite = store.Favourite.Equals ("fav.png") ? "notfav.png" : "fav.png";
			favourite.SetBinding (Image.SourceProperty, "Favourite");
		}

		private async void CalculateDistance (Label distance)
		{

			var locator = ServiceContainer.Resolve<Geolocator> ();
			Position presentPos = await locator.GetPositionAsync (10000);


			Position storePos = new Position () {
				Latitude = double.Parse (distance.Text.Split (new Char[] { ';' }) [0]),
				Longitude = double.Parse (distance.Text.Split (new Char[] { ';' }) [1])
			};
			double kms = CalcUtil.GetDistanceKM (presentPos, storePos);
			distance.Text = kms.ToString ("F1");
			Distance.IsVisible = true;
			

		}


	}
}

