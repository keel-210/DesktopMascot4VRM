using System;
using UnityEngine;

public static class DateTimeCompare
{
	public static bool SecondCompare(DateTime t1, DateTime t2)
	{
		return t1.Second == t2.Second;
	}
	public static bool MinuteCompare(DateTime t1, DateTime t2)
	{
		return t1.Minute == t2.Minute;
	}
	public static bool HourCompare(DateTime t1, DateTime t2)
	{
		return t1.Hour == t2.Hour;
	}
	public static bool DayCompare(DateTime t1, DateTime t2)
	{
		return t1.Day == t2.Day;
	}
	public static bool MonthCompare(DateTime t1, DateTime t2)
	{
		return t1.Month == t2.Month;
	}
	public static bool YearCompare(DateTime t1, DateTime t2)
	{
		return t1.Year == t2.Year;
	}
	public static bool MinuteSecondCompare(DateTime t1, DateTime t2)
	{
		return t1.Minute == t2.Minute && t1.Second == t2.Second;
	}
	public static bool HourMinuteCompare(DateTime t1, DateTime t2)
	{
		return t1.Hour == t2.Hour && t1.Minute == t2.Minute;
	}
	public static bool HourMinuteSecondCompare(DateTime t1, DateTime t2)
	{
		return t1.Hour == t2.Hour && t1.Minute == t2.Minute && t1.Second == t2.Second;
	}
	public static bool DayHourCompare(DateTime t1, DateTime t2)
	{
		return t1.Day == t2.Day && t1.Hour == t2.Hour;
	}
	public static bool DayHourMinuteCompare(DateTime t1, DateTime t2)
	{
		return t1.Day == t2.Day && t1.Hour == t2.Hour && t1.Minute == t2.Minute;
	}
	public static bool DayHourMinuteSecondCompare(DateTime t1, DateTime t2)
	{
		return t1.Day == t2.Day && t1.Hour == t2.Hour && t1.Minute == t2.Minute && t1.Second == t2.Second;
	}
	public static bool MonthDayCompare(DateTime t1, DateTime t2)
	{
		return t1.Month == t2.Month && t1.Day == t2.Day;
	}
	public static bool MonthDayHourCompare(DateTime t1, DateTime t2)
	{
		return t1.Month == t2.Month && t1.Day == t2.Day && t1.Hour == t2.Hour;
	}
	public static bool MonthDayHourMinuteCompare(DateTime t1, DateTime t2)
	{
		return t1.Month == t2.Month && t1.Day == t2.Day && t1.Hour == t2.Hour && t1.Minute == t2.Minute;
	}
	public static bool MonthDayHourMinuteSecondCompare(DateTime t1, DateTime t2)
	{
		return t1.Month == t2.Month && t1.Day == t2.Day && t1.Hour == t2.Hour && t1.Minute == t2.Minute && t1.Second == t2.Second;
	}
	public static bool YearDayCompare(DateTime t1, DateTime t2)
	{
		return t1.Year == t2.Year && t1.Day == t2.Day;
	}
	public static bool YearDayHourCompare(DateTime t1, DateTime t2)
	{
		return t1.Year == t2.Year && t1.Day == t2.Day && t1.Hour == t2.Hour;
	}
	public static bool YearDayHourMinuteCompare(DateTime t1, DateTime t2)
	{
		return t1.Year == t2.Year && t1.Day == t2.Day && t1.Hour == t2.Hour && t1.Minute == t2.Minute;
	}
	public static bool YearDayHourMinuteSecondCompare(DateTime t1, DateTime t2)
	{
		return t1.Year == t2.Year && t1.Day == t2.Day && t1.Hour == t2.Hour && t1.Minute == t2.Minute && t1.Second == t2.Second;
	}
	public static bool Compare(DateTime t1, DateTime t2, DateTimeCompareType type)
	{
		bool value = false;
		switch (type)
		{
			case DateTimeCompareType.Second:
				value = SecondCompare(t1, t2);
				break;
			case DateTimeCompareType.Minute:
				value = MinuteCompare(t1, t2);
				break;
			case DateTimeCompareType.Hour:
				value = HourCompare(t1, t2);
				break;
			case DateTimeCompareType.Day:
				value = DayCompare(t1, t2);
				break;
			case DateTimeCompareType.Month:
				value = MonthCompare(t1, t2);
				break;
			case DateTimeCompareType.Year:
				value = YearCompare(t1, t2);
				break;
			case DateTimeCompareType.MinuteSecond:
				value = MinuteSecondCompare(t1, t2);
				break;
			case DateTimeCompareType.HourMinute:
				value = HourMinuteCompare(t1, t2);
				break;
			case DateTimeCompareType.HourMinuteSecond:
				value = HourMinuteSecondCompare(t1, t2);
				break;
			case DateTimeCompareType.DayHour:
				value = DayHourCompare(t1, t2);
				break;
			case DateTimeCompareType.DayHourMinute:
				value = DayHourMinuteCompare(t1, t2);
				break;
			case DateTimeCompareType.DayHourMinuteSecond:
				value = DayHourMinuteSecondCompare(t1, t2);
				break;
			case DateTimeCompareType.MonthDay:
				value = MonthDayCompare(t1, t2);
				break;
			case DateTimeCompareType.MonthDayHour:
				value = MonthDayHourCompare(t1, t2);
				break;
			case DateTimeCompareType.MonthDayHourMinute:
				value = MonthDayHourMinuteCompare(t1, t2);
				break;
			case DateTimeCompareType.MonthDayHourMinuteSecond:
				value = MonthDayHourMinuteSecondCompare(t1, t2);
				break;
			case DateTimeCompareType.YearMonthDay:
				value = YearDayCompare(t1, t2);
				break;
			case DateTimeCompareType.YearMonthDayHour:
				value = YearDayHourCompare(t1, t2);
				break;
			case DateTimeCompareType.YearMonthDayHourMinute:
				value = YearDayHourMinuteCompare(t1, t2);
				break;
			case DateTimeCompareType.YearMonthDayHourMinuteSecond:
				value = YearDayHourMinuteSecondCompare(t1, t2);
				break;
		}
		return value;
	}
}
public enum DateTimeCompareType
{
	Second,
	Minute,
	Hour,
	Day,
	Month,
	Year,
	MinuteSecond,
	HourMinute,
	HourMinuteSecond,
	DayHour,
	DayHourMinute,
	DayHourMinuteSecond,
	MonthDay,
	MonthDayHour,
	MonthDayHourMinute,
	MonthDayHourMinuteSecond,
	YearMonth,
	YearMonthDay,
	YearMonthDayHour,
	YearMonthDayHourMinute,
	YearMonthDayHourMinuteSecond,
}