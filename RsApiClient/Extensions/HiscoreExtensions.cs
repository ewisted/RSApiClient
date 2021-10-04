using RSApiClient.Hiscores.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RSApiClient.Extensions
{
	public static class HiscoreExtensions
	{
		public static string GetModuleStringValue(this HiscoreType value)
		{
			switch (value)
			{
				case HiscoreType.RS3Normal:
					return "hiscore";
				case HiscoreType.RS3Ironman:
					return "hiscore_ironman";
				case HiscoreType.RS3HardcoreIronman:
					return "hiscore_hardcore_ironman";
				case HiscoreType.OSNormal:
					return "hiscore_oldschool";
				case HiscoreType.OSIronman:
					return "hiscore_oldschool_ironman";
				case HiscoreType.OSHarcoreIronman:
					return "hiscore_oldschool_hardcore_ironman";
				case HiscoreType.OSUltimateIronman:
					return "hiscore_oldschool_ultimate";
				case HiscoreType.OSDeamanMode:
					return "hiscore_oldschool_deadman";
				case HiscoreType.OSSeasonal:
					return "hiscore_oldschool_seasonal";
				case HiscoreType.OSTournament:
					return "hiscore_oldschool_tournament";
				default:
					throw new ArgumentOutOfRangeException("HiscoreType", value, "HiscoreType value was not recognized.");
			}
		}

		public static string GetNameString(this Enum value)
		{
			string stringValue = Enum.GetName(value.GetType(), value) ?? throw new ArgumentException($"Unable to get name for enum type: {value.GetType()} value: {value}");
			stringValue = Regex.Replace(stringValue, "[a-z]([A-Z]|[0-9])", m => $"{m.Value[0]} {m.Value[1]}");
			stringValue = stringValue.Replace("_", " - ");
			return stringValue;
		}
	}
}
