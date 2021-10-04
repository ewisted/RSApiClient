using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSApiClient.Hiscores.Models
{
	public enum HiscoreType
	{
		[HiscoreTypeModuleString("hiscore")]
		RS3Normal = 0,
		[HiscoreTypeModuleString("hiscore_ironman")]
		RS3Ironman = 1,
		[HiscoreTypeModuleString("hiscore_hardcore_ironman")]
		RS3HardcoreIronman = 2,
		[HiscoreTypeModuleString("hiscore_oldschool")]
		OSNormal = 3,
		[HiscoreTypeModuleString("hiscore_oldschool_ironman")]
		OSIronman = 4,
		[HiscoreTypeModuleString("hiscore_oldschool_hardcore_ironman")]
		OSHarcoreIronman = 5,
		[HiscoreTypeModuleString("hiscore_oldschool_ultimate")]
		OSUltimateIronman = 6,
		[HiscoreTypeModuleString("hiscore_oldschool_deadman")]
		OSDeamanMode = 7,
		[HiscoreTypeModuleString("hiscore_oldschool_seasonal")]
		OSSeasonal = 8,
		[HiscoreTypeModuleString("hiscore_oldschool_tournament")]
		OSTournament = 9
	}

	public class HiscoreTypeModuleStringAttribute : Attribute
	{
		public string ModuleName { get; set; }

		public HiscoreTypeModuleStringAttribute(string moduleName)
		{
			ModuleName = moduleName;
		}
	}
}
