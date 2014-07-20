using System;
using System.ComponentModel;

namespace PreOrder
{
	public class Store : BaseEntity
	{
		public string Name { get; set;}

		public string Image { get; set;}

		public string AddresseStreet { get; set;}

		public string AddresseNumber { get; set;}

		public string AddresseZip { get; set;}

		public string AddresseCity { get; set;}

		public string GPSCoordinate { get; set;}

		public string Telephone { get; set;}

		public String CombinedAddress { get
			{ return string.Format ("{0} {1}, {2} {3}", AddresseStreet, AddresseNumber, AddresseZip, AddresseCity); 
			}
		}

		private string favourite  = "notfav.png";

		public string Favourite {
			get {
				return favourite;
			}
			set {
				favourite = value;
			}
		}

		public Store ()
		{
		}

		public Store (string name, string image, string addresseStreet, string addresseNumber, string addresseZip, string addressCity, string gPSCoordinate, string telephone)
		{
			this.Name = name;
			this.Image = image;
			this.AddresseStreet = addresseStreet;
			this.AddresseNumber = addresseNumber;
			this.AddresseZip = addresseZip;
			this.AddresseCity = addressCity;
			this.GPSCoordinate = gPSCoordinate;
			this.Telephone = telephone;
		}
		
	}
}

