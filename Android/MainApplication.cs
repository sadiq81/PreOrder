
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Geolocation;

namespace PreOrder.Android
{
	/*
	<uses-feature android:glEsVersion="0x00020000" android:required="true" />
	<permission android:name="PreOrder.Android.permission.MAPS_RECEIVE" android:protectionLevel="signature" />
	 */

	[Application (Label = "PreOrder",Theme="@android:style/Theme.Holo.Light")]			
	[MetaData ("com.google.android.maps.v2.API_KEY", Value="AIzaSyAX6ip25l4xf6gacpTahikCTOvVmYLagv0")]
	[MetaData ("com.google.android.gms.version", Value="4323000")]
	//[UsesPermissionAttribute("PreOrder.Android.permission.MAPS_RECEIVE")]
	//[UsesPermissionAttribute("com.google.android.providers.gsf.permission.READ_GSERVICES")]
	public class MainApplication : Application
	{
		public MainApplication(IntPtr handle, JniHandleOwnership transfer) : base(handle,transfer)
		{
			ServiceContainer.Register<Geolocator> (() => new Geolocator (this));
		}
	}


}

