using System;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Geolocation;

namespace PreOrder
{
	public class LandingPage : ContentPage
	{
		Grid grid;
		StackLayout List;
		StackLayout Map;

		public LandingPage ()
		{
			grid = new Grid {
				VerticalOptions = LayoutOptions.FillAndExpand,
				RowSpacing = 0,
				ColumnSpacing = 0,
				RowDefinitions = {
					new RowDefinition { Height = new GridLength (100) },
					new RowDefinition { Height = GridLength.Auto },
					new RowDefinition { Height = new GridLength (1, GridUnitType.Star) }

				},
				ColumnDefinitions = {
					new ColumnDefinition { Width = new GridLength (1, GridUnitType.Star) },
				}
			};

			#region topLayout
			RelativeLayout topLayout = new RelativeLayout ();
			Image search = new Image (){ Source = ImageSource.FromFile ("search.png") };
			topLayout.Children.Add (search, 
				Constraint.Constant (10),
				Constraint.Constant (0),
				Constraint.RelativeToParent ((parent) => {
					return parent.Width / 10;
				}),
				Constraint.RelativeToParent ((parent) => {
					return parent.Width / 10;
				}));

			Image shop = new Image (){ Source = ImageSource.FromFile ("shop.png") };
			topLayout.Children.Add (shop, 
				Constraint.RelativeToParent ((parent) => {
					return parent.Width / 2 - (parent.Width / 7) / 2;
				}),
				Constraint.Constant (0),
				Constraint.RelativeToParent ((parent) => {
					return parent.Width / 7;
				}),
				Constraint.RelativeToParent ((parent) => {
					return parent.Width / 7;
				}));

			Image time = new Image (){ Source = ImageSource.FromFile ("time.png") };
			topLayout.Children.Add (time, 
				Constraint.RelativeToParent ((parent) => {
					return (parent.Width - parent.Width / 10) - 10;
				}),
				Constraint.Constant (0),
				Constraint.RelativeToParent ((parent) => {
					return parent.Width / 10;
				}),
				Constraint.RelativeToParent ((parent) => {
					return parent.Width / 10;
				}));

			grid.Children.Add (topLayout, 0, 0);
			#endregion

			#region list/map
			StackLayout choose = new StackLayout ();
			choose.Orientation = StackOrientation.Horizontal;
			choose.Spacing = 1;
			choose.Padding = new Thickness (0, 1, 0, 1);
			choose.BackgroundColor = Color.Black;

			List = new StackLayout () {
				BackgroundColor = Color.Blue,
				HorizontalOptions = LayoutOptions.FillAndExpand
			};
			List.GestureRecognizers.Add (new TapGestureRecognizer () {
				Command = new Command (() => {
					ShowList ();
				})
			});

			List.Children.Add (new Label () {
				HorizontalOptions = LayoutOptions.Center,
				Text = "Liste",
				Font = Font.SystemFontOfSize (NamedSize.Large)
			});
			Map = new StackLayout () {
				BackgroundColor = Color.White,
				HorizontalOptions = LayoutOptions.FillAndExpand
			};

			Map.GestureRecognizers.Add (new TapGestureRecognizer () {
				Command = new Command (() => {
					ShowMap ();
				})
			});
			Map.Children.Add (new Label () {
				HorizontalOptions = LayoutOptions.Center,
				Text = "Kort",
				Font = Font.SystemFontOfSize (NamedSize.Large)

			});

			choose.Children.Add (List);
			choose.Children.Add (Map);
			grid.Children.Add (choose, 0, 1);
			#endregion

			Content = grid;
			Padding = new Thickness (0, Device.OnPlatform (20, 10, 10), 0, 0);

			ShowList ();
		}

		void ShowList ()
		{
			if (grid.Children.Count > 2) {
				grid.Children.RemoveAt (2);
			}

			ListView list = new ListView () {
				// Source of data items.
				ItemsSource = StoreRepository.Stores,
				ItemTemplate = new DataTemplate (typeof(ShopViewCell))
			};

			list.ItemSelected += async(sender, e) => {
				if (e.SelectedItem == null)
					return;

				Store store = (Store)e.SelectedItem;
				var shopPage = new ShopPage (store);
				await Navigation.PushModalAsync (shopPage);
				//deselect item when pushing
				list.SelectedItem = null;
			};

			grid.Children.Add (list, 0, 2);
			Map.BackgroundColor = Color.White;
			List.BackgroundColor = Color.Blue;
		}

		private async void ShowMap ()
		{
			if (grid.Children.Count > 2) {
				grid.Children.RemoveAt (2);
			}
			var locator = ServiceContainer.Resolve<Geolocator> ();
			var presentPosGeo = await locator.GetPositionAsync (10000);
			var presentPos = new Xamarin.Forms.Maps.Position (presentPosGeo.Latitude, presentPosGeo.Longitude);

			var map = new Map (MapSpan.FromCenterAndRadius (presentPos, Distance.FromMiles (0.3))) {
				IsShowingUser = true,
				HeightRequest = 100,
				WidthRequest = 960,
				VerticalOptions = LayoutOptions.FillAndExpand
			};

			foreach (Store shop in StoreRepository.Stores) {
				Xamarin.Forms.Maps.Position storePos = new Xamarin.Forms.Maps.Position (
					                                       double.Parse (shop.GPSCoordinate.Split (new Char[] { ';' }) [0]),
					                                       double.Parse (shop.GPSCoordinate.Split (new Char[] { ';' }) [1]));
				//double kms = CalcUtil.GetDistanceKM (presentPos, storePos);
				//if (radius < kms){
				var pin = new Pin {
					Type = PinType.Place,
					Position = storePos,
					Label = shop.Name,
					Address = shop.CombinedAddress
				};
				map.Pins.Add (pin);
				//}
			}

			grid.Children.Add (map, 0, 2);
			Map.BackgroundColor = Color.Blue;
			List.BackgroundColor = Color.White;
		}


	}
}

