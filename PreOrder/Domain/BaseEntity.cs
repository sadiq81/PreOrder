using System;

namespace PreOrder
{
	public abstract class BaseEntity
	{
		public long Id { get; set;}
		public DateTime Created { get; set;}
		public DateTime Updated { get; set;}

		public BaseEntity ()
		{
		}


	}
}

