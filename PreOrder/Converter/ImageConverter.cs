using System;
using Xamarin.Forms;

namespace PreOrder
{
	public class ImageConverter : IValueConverter { 

		public object Convert (object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture) 
		{ 
			return new FileImageSource { File = value.ToString () }; 
		}

		public object ConvertBack (object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new System.NotImplementedException ();
		}
	}
}

