using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EisnelShared
{
	public static class MiscExtensions
	{
		public static bool AnyTrue(this BitArray ba)
		{
			return ba.GetTypeSafeEnumerator().Any(b => b);
		}

		public static IEnumerable<bool> GetTypeSafeEnumerator(this BitArray ba)
		{
			for (int i = 0; i < ba.Length; i++)
				yield return ba[i];
		}
	}
}
