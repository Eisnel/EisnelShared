using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EisnelShared
{
	public static class MathExtensions
	{
		public static double RootMeanSquare( this IEnumerable<double> values )
		{
			double total = 0.0;
			int count = 0;
			foreach( double d in values )
			{
				count++;
				total += d * d;
			}
			if (count > 0)
			{
				return Math.Sqrt(total / count);
			}
			return 0.0;
		}
	}
}
