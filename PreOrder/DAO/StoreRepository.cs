using System;
using System.Collections.Generic;

namespace PreOrder
{
	public static class StoreRepository
	{
		public static List<Store> Stores = new List<Store> (new Store[] {
			new Store ("Butik1", "Butik", "Borgergade", "2", "1300", "København K", "55,681949;12,583679", "12345678"),
			new Store ("Butik2", "Butik", "Sølvgade", "3", "1300", "København K", "55,685314;12,586309", "12345678"),
			new Store ("Butik3", "Butik", "Dronningens Tværgade", "58", "1300", "København K", "55,684859;12,582648", "12345678"),
			new Store ("Butik4", "Butik", "Store Kongensgade", "49", "1300", "København K", "55,683815;12,587251", "12345678"),
			new Store ("Butik5", "Butik", "Bredgade", "41", "1300", "København K", "55,683303;12,589343", "12345678")
		});

	}
}

