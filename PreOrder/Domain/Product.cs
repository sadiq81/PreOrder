﻿using System;

namespace PreOrder
{
	public class Product : BaseEntity
	{
		public string Name { get; set; }

		public string Description { get; set; }

		public double Price { get; set; }

		public Product ()
		{
		}

		public Product (string name, string description, double price)
		{
			this.Name = name;
			this.Description = description;
			this.Price = price;
		}
		
	}
}

