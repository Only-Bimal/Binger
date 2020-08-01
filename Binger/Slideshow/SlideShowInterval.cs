using System.ComponentModel;

namespace Binger
{
	
	[TypeConverter(typeof(LocalizedEnumConverter))]
	public enum SlideShowInterval
	{
		[Description("5 Seconds")]
		[DefaultValue(5000)]
		Seconds5,
		[Description("10 Seconds")]
		[DefaultValue(10000)]
		Seconds10,
		[Description("30 Seconds")]
		[DefaultValue(30000)]
		Seconds30,
		[Description("1 Minute")]
		[DefaultValue(60000)]
		Minute1,
		[Description("5 Minutes")]
		[DefaultValue(300000)]
		Minutes5,
		[Description("10 Minutes")]
		[DefaultValue(600000)]
		Minutes10,
		[Description("30 Minutes")]
		[DefaultValue(1800000)]
		Minutes30,
		[Description("1 Hour")]
		[DefaultValue(3600000)]
		Hour1,
		[Description("2 Hours")]
		[DefaultValue(7200000)]
		Hours2,
		[Description("6 Hours")]
		[DefaultValue(21600000)]
		Hours6,
		[Description("12 Hours")]
		[DefaultValue(43200000)]
		Hours12,
		[Description("1 Day")]
		[DefaultValue(86400000)]
		Day1

	}
}
