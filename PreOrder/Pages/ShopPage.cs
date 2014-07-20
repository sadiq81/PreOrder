using System;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace PreOrder
{
	public class ShopPage : ContentPage
	{

		Grid Grid;
		ObservableCollection<Product> Basket = new ObservableCollection<Product> ();

		public Store Store { get; set; }

		public ShopPage (Store store)
		{
			Store = store;

			Grid = new Grid {
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
			Image shopImage = new Image (){ Source = ImageSource.FromFile ("shop.png") };
			topLayout.Children.Add (shopImage, 
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


			StackLayout shopInfo = new StackLayout () {
				Children = {
					new Label () { 
						Text = Store.Name,
						Font = Font.SystemFontOfSize (NamedSize.Large),
						LineBreakMode = LineBreakMode.TailTruncation,
						HorizontalOptions = LayoutOptions.CenterAndExpand
					}, new Label () { 
						Text = Store.CombinedAddress,
						Font = Font.SystemFontOfSize (NamedSize.Medium),
						LineBreakMode = LineBreakMode.TailTruncation,
						HorizontalOptions = LayoutOptions.CenterAndExpand
					}
				}
			};

			topLayout.Children.Add (shopInfo, 
				Constraint.Constant (0),
				Constraint.RelativeToView (shopImage, (parent, sibling) => {
					return sibling.Y + sibling.Height + 5;
				}),
				Constraint.RelativeToParent ((parent => {
					return parent.Width;
				}))
			);


			Image time = new Image (){ Source = ImageSource.FromFile ("cancel.png") };
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

			Grid.Children.Add (topLayout, 0, 0);
			#endregion

			#region basketOverView
			Label basketSize = new Label () {
				HorizontalOptions = LayoutOptions.StartAndExpand,
				BackgroundColor = Color.Blue
			};
			basketSize.SetBinding (Label.TextProperty, "Basket.Count");

			Label basketSizeText = new Label () {
				HorizontalOptions = LayoutOptions.StartAndExpand,
				Text = "Vare(r)",
				BackgroundColor = Color.Blue
			};
			Label totalPrice = new Label () {
				HorizontalOptions = LayoutOptions.EndAndExpand,
				BackgroundColor = Color.Blue
			};

			StackLayout basketOverView = new StackLayout () {
				BackgroundColor = Color.Black,
				Spacing = 1,
				Padding = new Thickness (0, 1, 0, 0),
				Children = {basketSize,
					basketSizeText,
					totalPrice
				}
			};

			Grid.Children.Add (basketOverView, 0, 1);
			#endregion

			ScrollView scrollView = new ScrollView ();
			StackLayout products = new StackLayout () {
				Orientation = StackOrientation.Vertical
			};
			scrollView.Content = products;

			foreach (Product product in ProductRepository.GetProductsForStore (Store.Name)) {
				Grid grid = new Grid () {
					HorizontalOptions = LayoutOptions.FillAndExpand,
					RowSpacing = 0,
					ColumnSpacing = 0,
					BackgroundColor = Color.Black,
					Padding = new Thickness (0, 0, 0, 1),
					ColumnDefinitions = {
						new ColumnDefinition { Width = new GridLength (2, GridUnitType.Star) },
						new ColumnDefinition { Width = new GridLength (10, GridUnitType.Star) },
						new ColumnDefinition { Width = new GridLength (2, GridUnitType.Star) },
						new ColumnDefinition { Width = new GridLength (1, GridUnitType.Star) }
					}
				};

				#region itemCount
				StackLayout numberOfItems = new StackLayout () {
					BackgroundColor = Color.White
				};
				Label itemCount = new Label ();
				numberOfItems.Children.Add (itemCount);

				grid.Children.Add (numberOfItems, 0, 0);
				#endregion

				#region productInfo
				StackLayout productInfo = new StackLayout () {
					Orientation = StackOrientation.Vertical,
					Spacing = 0,
					BackgroundColor = Color.White,
					Children = {
						new Label () {
							//BackgroundColor = Color.Green,
							Text = product.Name,
							YAlign = TextAlignment.End,
							VerticalOptions = LayoutOptions.CenterAndExpand
						},
						new Label () {
							//BackgroundColor = Color.Blue,
							Text = product.Description,
							YAlign = TextAlignment.Start,
							Font = Font.SystemFontOfSize (NamedSize.Small),
							VerticalOptions = LayoutOptions.CenterAndExpand
						}
					}
				};
				grid.Children.Add (productInfo, 1, 0);
				#endregion

				#region priceInfo
				grid.Children.Add (new Label () {
					XAlign = TextAlignment.End,
					Text = product.Price.ToString ("####"),
					BackgroundColor = Color.White

				}, 2, 0);

				grid.Children.Add (new Label () {
					XAlign = TextAlignment.Start,
					Text = product.Price.ToString ("F").Substring (product.Price.ToString ("F").LastIndexOf ('.') + 1),
					Font = Font.SystemFontOfSize (NamedSize.Micro),
					BackgroundColor = Color.White
				}, 3, 0);
				#endregion

				products.Children.Add (grid);
			}


			Grid.Children.Add (scrollView, 0, 2);

			Content = Grid;
			Padding = new Thickness (0, Device.OnPlatform (20, 10, 10), 0, 0);

		}
	}
}

