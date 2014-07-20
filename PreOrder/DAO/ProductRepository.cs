using System;
using System.Collections.Generic;

namespace PreOrder
{
	public static class ProductRepository
	{
		public static IDictionary<string, List<Product>> Products = new Dictionary<string, List<Product>> () { { "Butik1", 
				new List<Product> () {
					new Product ("Produkt1", "Skidegodt produkt 1", 10),
					new Product ("Produkt2", "Skidegodt produkt 2", 20),
					new Product ("Produkt3", "Skidegodt produkt 3", 30),
					new Product ("Produkt4", "Skidegodt produkt 4", 15),
					new Product ("Produkt5", "Skidegodt produkt 5", 25),
					new Product ("Produkt6", "Skidegodt produkt 6", 35),
					new Product ("Produkt6", "Skidegodt produkt 6", 35),
					new Product ("Produkt6", "Skidegodt produkt 6", 35), 
					new Product ("Produkt6", "Skidegodt produkt 6", 35),
					new Product ("Produkt6", "Skidegodt produkt 6", 35),
					new Product ("Produkt6", "Skidegodt produkt 6", 35),
					new Product ("Produkt6", "Skidegodt produkt 6", 35),
					new Product ("Produkt6", "Skidegodt produkt 6", 35),
					new Product ("Produkt7", "Skidegodt produkt 7", 100)
				}
			}
		};

		public static List<Product> GetProductsForStore (string store)
		{
			List<Product> list = Products [store];
			return list;
		}
	}
}

