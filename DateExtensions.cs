using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EisnelShared
{
	public static class DateExtensions
	{
		public static int getWeeks(this TimeSpan span)
		{
			return span.Days / 7;
		}

		public static double getTotalWeeks(this TimeSpan span)
		{
			return span.TotalDays / 7.0;
		}

		public static int DaysInMonth(this DateTime dateTime)
		{
			return DateTime.DaysInMonth(dateTime.Year, dateTime.Month);
		}

		public static Boolean isLastDayOfMonth(this DateTime dateTime)
		{
			return dateTime.Day == dateTime.DaysInMonth();
		}

		public static int GetAbsoluteMonth(this DateTime dateTime)
		{
			return dateTime.Month * dateTime.Year;
		}

		public static DateTime GetStartOfDay(this DateTime dateTime)
		{
			return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day);
		}

		public static DateTime GetStartOfWeek(this DateTime dateTime)
		{
			return GetStartOfDay(dateTime.AddDays(-(int)dateTime.DayOfWeek));
		}

		public static DateTime GetStartOfMonth(this DateTime dateTime)
		{
			return new DateTime(dateTime.Year, dateTime.Month, 1);
		}

		public static DateTime GetStartOfPreviousMonth(this DateTime dateTime)
		{
			return dateTime.GetStartOfMonth().AddMonths(-1);
		}

		public static DateTime GetStartOfNextMonth(this DateTime dateTime)
		{
			return dateTime.GetStartOfMonth().AddMonths(1);
		}

		public static DateTime GetEndOfMonth(this DateTime dateTime)
		{
			return new DateTime(dateTime.Year, dateTime.Month, dateTime.DaysInMonth(), 23, 59, 59, 999);
		}

		public static DateTime ChangeDayOfMonth(this DateTime dateTime, int dayOfMonth)
		{
			dayOfMonth = Math.Min(dayOfMonth, dateTime.DaysInMonth());
			return dateTime.AddDays(dayOfMonth - dateTime.Day);
		}
	}

	//public static class MiscExtensions
	//{
	//	public static bool AnyTrue(this BitArray ba)
	//	{
	//		return ba.GetTypeSafeEnumerator().Any(b => b);
	//	}

	//	public static IEnumerable<bool> GetTypeSafeEnumerator(this BitArray ba)
	//	{
	//		for (int i = 0; i < ba.Length; i++)
	//			yield return ba[i];
	//	}
	//}
}
