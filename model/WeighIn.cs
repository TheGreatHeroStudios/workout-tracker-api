using System;
using System.Collections.Generic;

namespace model
{
	public partial class WeighIn
	{
		public int WeighInId { get; set; }
		public DateTime WeighInDate { get; set; }
		public decimal WeightLbs { get; set; }
	}
}
